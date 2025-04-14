using System.Diagnostics;
using System.Globalization;
using System.Numerics;
using WeierstrassCurveTest.DLP;
using WeierstrassCurveTest.EllipticCurves;
using WeierstrassCurveTest.Performance.Interfaces;
using WeierstrassCurveTest.Types;
using WeierstrassCurveTest.UI;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WeierstrassCurveTest.Performance
{
    public enum CurveName
    {
        Weierstrass,
        Edwards
    }
    public enum MethodName
    {
        BSGS,
        GrympyGiants,
        Kangaroo,
        LasVegas,
        PollardRho,
    }

    internal class PerformanceEvaluator
    {
        private UserInterface form;

        private CurveName curveName;
        private MethodName methodName;
        private bool usePohligHellman;
        private bool withNegMap;
        private bool withExtNegMap;
        private string datasetFilename;

        public PerformanceEvaluator (UserInterface form)
        {
            this.form = form;
        }

        public string ResultsFilePath
        {
            get
            {
                string negMapPostfix = (withNegMap ? ".neg" : "") + (withExtNegMap ? "-ext" : "");
                return Path.Combine(Environment.CurrentDirectory, $"v3_{curveName}_{methodName}{negMapPostfix}.{datasetFilename}");
            }
        }

        public string Evaluate(
            DatastItem data,
            CurveName curveName,
            MethodName methodName,
            bool usePohligHellman,
            bool withNegMap,
            bool withExtNegMap
        )
        {
            this.curveName = curveName;
            this.methodName = methodName;
            this.withNegMap = withNegMap;
            this.withExtNegMap = withExtNegMap;
            this.usePohligHellman = usePohligHellman;
            //Console.WriteLine(data);

            var (method, curve, P, Q) = ProcessData(data);
            //Console.WriteLine("Data is proccessed");

            BigInteger ek = data.dlpSolution;
            BigInteger k = method.Solve(P, Q);
            
            bool isCorrect = k == ek || curve.Mult(P, k).Equals(Q);
            string resultCharacterization = isCorrect ? "correct" : "incorrect";

            //Console.WriteLine("The calculations are done");
            return $"Input: P {P} and Q {Q}.\n\r" +
                $"Expected: k = {ek}\n\r" +
                $"Output: k = {k} - this is {resultCharacterization} result!\n\r";
        }

        public string Evaluate(
            string filePath,
            CurveName curveName,
            MethodName methodName,
            bool usePohligHellman,
            bool withNegMap,
            bool withExtNegMap
        )
        {
            this.curveName = curveName;
            this.methodName = methodName;
            this.datasetFilename = Path.GetFileName(filePath);
            this.withNegMap = withNegMap;
            this.withExtNegMap = withExtNegMap;
            this.usePohligHellman = usePohligHellman;

            Console.WriteLine($"withExtNegMap = {withExtNegMap}, this.withExtNegMap = {this.withExtNegMap}");


            DataProvider dataProvider = new DataProvider(filePath);

            form.SetHeatingTotalItems(dataProvider.heatingTotal);
            form.SetProccessingTotalItems(dataProvider.proccessingTotal);

            // Object to lock for thread-safety when fetching data and writing to file
            object lockObject = new object();

            var parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount - 5 };
            Parallel.For(0, Environment.ProcessorCount - 5, parallelOptions, workerId => 
            {
                while (!dataProvider.complete)
                {
                    Console.WriteLine("Evaluating dataitem");
                    DatastItem data = null;
                    bool isHeating = false;
                    // Ensure that only one thread fetches a new data item at a time
                    lock (lockObject)
                    {
                        if (!dataProvider.complete)
                        {
                            (data, isHeating) = dataProvider.GetNext();
                        }
                    }

                    if (data == null) break; // Stop if no more data items are available

                    var (method, curve, P, Q) = ProcessData(data);

                    // Collect garbage to get a baseline memory usage
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();

                    //Console.WriteLine($"Solving DLP for points: P {P}, Q {Q}, on curve with params a = {data.curveParam1}; b = {data.curveParam2}");

                    BigInteger result = -1;
                    long memoryBefore = GC.GetAllocatedBytesForCurrentThread();
                    var stopwatch = Stopwatch.StartNew();

                    try
                    {
                        result = method.Solve(P, Q);
                    }
                    catch (Exception ex) { }

                    stopwatch.Stop();
                    long memoryAfter = GC.GetAllocatedBytesForCurrentThread();
                    long memoryUsed = memoryAfter - memoryBefore;
                    double timeUsed = stopwatch.ElapsedMilliseconds;
                    bool isSolutionFound = isSolutionFound = result == data.dlpSolution || curve.Mult(P, result).Equals(Q);

                    if (!isHeating)
                    {
                        lock (lockObject)
                        {
                            StoreResults(
                                timeUsed, 
                                memoryUsed, 
                                isSolutionFound,
                                ((method as PohligHellman).supportMethod as PollardRho).iterationsCount,
                                ((method as PohligHellman).supportMethod as PollardRho).foundWithNegationMap,
                                ((method as PohligHellman).supportMethod as PollardRho).foundWithExtendedNegationMap
                            );
                        }
                        form.IncrementProccessingProgress();
                        Console.WriteLine("Evaluation finished by worker " + workerId);
                    }
                    else
                    {
                        form.IncrementHeatingProgress();
                        Console.WriteLine("Program is heating up by worker " + workerId);
                    }

                }

            });

            Console.WriteLine("All workers completed.");
            return ResultsFilePath;
        }

        public string CalculateAndLogStatistics(string filePath)
        {
            // Initialize accumulators
            double sum1 = 0;
            double sum2 = 0;
            double sum3 = 0;
            int correctSolutionsCount = 0;
            int negationMapUsageCount = 0;
            int extendedNegationMapUsageCount = 0;
            int totalCount = 0;

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        // Split the line into fields
                        string[] fields = line.Split(',');

                        if (fields.Length != 6)
                        {
                            Console.WriteLine($"Invalid number of fields in line: {line}");
                            continue;
                        }

                        // Parse the numeric values
                        if (double.TryParse(fields[0], NumberStyles.Any, CultureInfo.InvariantCulture, out double num1) &&
                            double.TryParse(fields[1], NumberStyles.Any, CultureInfo.InvariantCulture, out double num2) &&
                            bool.TryParse(fields[2], out bool isSolutionFound) &&
                            double.TryParse(fields[3], NumberStyles.Any, CultureInfo.InvariantCulture, out double num3) &&
                            bool.TryParse(fields[4], out bool foundWithNegationMap) &&
                            bool.TryParse(fields[5], out bool foundWithExtendedNegationMap))
                        {
                            sum1 += num1;
                            sum2 += num2;
                            sum3 += num3;
                            if (isSolutionFound)
                            {
                                correctSolutionsCount++;
                            }
                            if (foundWithNegationMap)
                            {
                                negationMapUsageCount++;
                            }
                            if (foundWithExtendedNegationMap)
                            {
                                extendedNegationMapUsageCount++;
                            }
                            totalCount++;
                        }
                        else
                        {
                            throw new Exception($"Failed to parse line: {line}");
                        }
                    }
                }

                if (totalCount > 0)
                {
                    // Calculate averages and accuracy
                    double averageTime = sum1 / totalCount;
                    double averageSpace = sum2 / totalCount;
                    double accuracy = (correctSolutionsCount / (double)totalCount) * 100;
                    double averageIterations = sum3 / totalCount;
                    double negationMapUsage = (negationMapUsageCount / (double)totalCount) * 100;
                    double extendedNegationMapUsage = (extendedNegationMapUsageCount / (double)totalCount) * 100;

                    // Log results to the console
                    string result = $"Evaluation is finished!\n\r" +
                        $"File with results: {filePath}\n\r" +
                        $"Total number of items: {totalCount}\n\r" +
                        $"Average speed (ms): {averageTime:F2}\n\r" +
                        $"Average space usage (bytes): {averageSpace:F2}\n\r" +
                        $"Accuracy of solving DLP: {accuracy:F2}%\n\r" +
                        $"Average itterations (scalar): {averageIterations:F2}\n\r" +
                        $"Negation maps usage: {negationMapUsage:F2}%\n\r" +
                        $"Extended negation maps usage: {extendedNegationMapUsage:F2}%\n\r";
                    //Console.WriteLine($"--------------------------------------------------");
                    //Console.WriteLine($"File name: {filePath}");
                    //Console.WriteLine($"Average speed (ms): {averageTime:F2}");
                    //Console.WriteLine($"Average space usage (bytes): {averageSpace:F2}");
                    //Console.WriteLine($"Accuracy of solving DLP: {accuracy:F2}%");
                    //Console.WriteLine($"Average itterations (scalar): {averageIterations:F2}");
                    //Console.WriteLine($"Negation maps usage: {negationMapUsage:F2}%");
                    //Console.WriteLine($"Extended negation maps usage: {extendedNegationMapUsage:F2}%");
                    //Console.WriteLine($"--------------------------------------------------");
                    return result;
                }
                else
                {
                    throw new Exception("No valid data found in the file.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error reading the file: {ex.Message}");
            }
        }

        private Tuple<DLPMethod, EllipticCurve, Types.Point, Types.Point> ProcessData(DatastItem data)
        {
            EllipticCurve curve = CreateCurve(data);
            DLPMethod method = CreateMethod(curve);

            if (usePohligHellman)
            {
                method = new PohligHellman(curve, method);
            }

            if (withNegMap)
            {
                method.EnableNegationMaps(withExtNegMap);
                if (withExtNegMap)
                {
                    var listOfPointsWithYZero = new List<Types.Point>();

                    if (data.x0 != -1)
                    {
                        listOfPointsWithYZero.Add(new Types.Point(data.x0, 0));
                    }

                    if (data.x1 != -1)
                    {
                        listOfPointsWithYZero.Add(new Types.Point(data.x1, 0));
                    }

                    if (data.x2 != -1)
                    {
                        listOfPointsWithYZero.Add(new Types.Point(data.x2, 0));
                    }

                    curve.setPointsWithYZero(listOfPointsWithYZero);
                }
            }

            Types.Point P = new Types.Point(data.point1_x, data.point1_y);
            Types.Point Q = new Types.Point(data.point2_x, data.point2_y);

            return new Tuple<DLPMethod, EllipticCurve, Types.Point, Types.Point>(method, curve, P, Q);
        }

        private EllipticCurve CreateCurve(DatastItem data)
        {
            switch (curveName)
            {
                case CurveName.Weierstrass:
                    return new WeierstrassCurve(data.curveParam1, data.curveParam2, data.modulo, data.point1_order);
                case CurveName.Edwards:
                    return new EdwardsCurve(data.curveParam1, data.modulo, data.point1_order);
            }

            return null;
        }

        private DLPMethod CreateMethod(EllipticCurve curve)
        {
            switch (methodName)
            {
                case MethodName.BSGS:
                    return new BSGS(curve);
                case MethodName.GrympyGiants:
                    return new GrumpyGiants(curve);
                case MethodName.Kangaroo:
                    return new Kangaroo(curve);
                case MethodName.LasVegas:
                    return new LasVegasc(curve);
                case MethodName.PollardRho:
                    return new PollardRho(curve);
            }

            return null;
        }

        private void StoreResults(double timeUsed, long memoryUsed, bool isSolutionFound, int iterationsCount, bool foundWithNegMap, bool foundWithExtNegMap)
        {
            string[] record = { timeUsed.ToString(), memoryUsed.ToString(), isSolutionFound.ToString(), iterationsCount.ToString(), foundWithNegMap.ToString(), foundWithExtNegMap.ToString() };

            using (StreamWriter writer = new StreamWriter(ResultsFilePath, append: true))
            {
                string newLine = string.Join(",", record);
                writer.WriteLine(newLine);
            }
        }
    }
}
