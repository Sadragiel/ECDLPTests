using System.Diagnostics;
using System.Globalization;
using System.Numerics;
using WeierstrassCurveTest.DLP;
using WeierstrassCurveTest.EllipticCurves;
using WeierstrassCurveTest.Performance.Interfaces;
using WeierstrassCurveTest.Types;

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
        private CurveName curveName;
        private MethodName methodName;
        private string datasetFilename;

        public void Evaluate(string datasetFilename, CurveName curveName, MethodName methodName)
        {
            this.curveName = curveName;
            this.methodName = methodName;
            this.datasetFilename = datasetFilename;

            DataProvider dataProvider = new DataProvider(datasetFilename);

            // Object to lock for thread-safety when fetching data and writing to file
            object lockObject = new object();

            var parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount - 5 };
            Parallel.For(0, Environment.ProcessorCount - 5, parallelOptions, workerId => 
            {
                while (!dataProvider.complete)
                {
                    Console.WriteLine("Evaluating dataitem");
                    DatastItem data = null;
                    // Ensure that only one thread fetches a new data item at a time
                    lock (lockObject)
                    {
                        if (!dataProvider.complete)
                        {
                            data = dataProvider.GetNext();
                        }
                    }

                    if (data == null) break; // Stop if no more data items are available

                    EllipticCurve curve = CreateCurve(data);
                    DLPMethod method = CreateMethod(curve);

                    Point P = new Point(data.point1_x, data.point1_y);
                    Point Q = new Point(data.point2_x, data.point2_y);

                    // Collect garbage to get a baseline memory usage
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();

                    long memoryBefore = GC.GetAllocatedBytesForCurrentThread();
                    var stopwatch = Stopwatch.StartNew();
                    bool isSolutionFound = false;

                    try
                    {
                        BigInteger result = method.Solve(P, Q);
                        isSolutionFound = result == data.dlpSolution;
                    }
                    catch (Exception ex) { }

                    stopwatch.Stop();
                    long memoryAfter = GC.GetAllocatedBytesForCurrentThread();
                    long memoryUsed = memoryAfter - memoryBefore;
                    double timeUsed = stopwatch.ElapsedMilliseconds;

                    if (!dataProvider.heating)
                    {
                        lock (lockObject)
                        {
                            StoreResults(timeUsed, memoryUsed, isSolutionFound);
                        }
                        Console.WriteLine("Evaluation finished by worker " + workerId);
                    }
                    else
                    {
                        Console.WriteLine("Program is heating up by worker " + workerId);
                    }

                }

            });

            Console.WriteLine("All workers completed.");
        }

        public void CalculateAndLogStatistics(string filePath)
        {
            // Initialize accumulators
            double sum1 = 0;
            double sum2 = 0;
            int trueCount = 0;
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

                        if (fields.Length != 3)
                        {
                            Console.WriteLine($"Invalid number of fields in line: {line}");
                            continue;
                        }

                        // Parse the numeric values
                        if (double.TryParse(fields[0], NumberStyles.Any, CultureInfo.InvariantCulture, out double num1) &&
                            double.TryParse(fields[1], NumberStyles.Any, CultureInfo.InvariantCulture, out double num2) &&
                            bool.TryParse(fields[2], out bool boolValue))
                        {
                            sum1 += num1;
                            sum2 += num2;
                            if (boolValue)
                            {
                                trueCount++;
                            }
                            totalCount++;
                        }
                        else
                        {
                            Console.WriteLine($"Failed to parse line: {line}");
                        }
                    }
                }

                if (totalCount > 0)
                {
                    // Calculate averages and accuracy
                    double average1 = sum1 / totalCount;
                    double average2 = sum2 / totalCount;
                    double accuracy = (trueCount / (double)totalCount) * 100;

                    // Log results to the console
                    Console.WriteLine($"File name: {filePath}");
                    Console.WriteLine($"Average speed (ms): {average1:F2}");
                    Console.WriteLine($"Average space usage (bytes): {average2:F2}");
                    Console.WriteLine($"Accuracy of solving DLP: {accuracy:F2}%");
                }
                else
                {
                    Console.WriteLine("No valid data found in the file.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading the file: {ex.Message}");
            }
        }

        private EllipticCurve CreateCurve(DatastItem data)
        {
            switch (curveName)
            {
                case CurveName.Weierstrass:
                    return new WeierstrassCurve(data.curveParam1, data.curveParam2, data.modulo, data.order);
                case CurveName.Edwards:
                    return new EdwardsCurve(data.curveParam1, data.modulo, data.order);
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

        private void StoreResults(double timeUsed, long memoryUsed, bool isSolutionFound)
        {
            string filePath = Path.Combine(Environment.CurrentDirectory, $"{curveName}_{methodName}.{datasetFilename}");
            string[] record = { timeUsed.ToString(), memoryUsed.ToString(), isSolutionFound.ToString() };

            using (StreamWriter writer = new StreamWriter(filePath, append: true))
            {
                string newLine = string.Join(",", record);
                writer.WriteLine(newLine);
            }
        }
    }
}
