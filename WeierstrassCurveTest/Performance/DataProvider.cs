using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using WeierstrassCurveTest.Performance.Interfaces;

namespace WeierstrassCurveTest.Performance
{
    internal class DataProvider
    {
        DatastItem[] records;
        int pointer;
        public bool complete
        {
            get
            {
                return pointer + 1 == records.Length;
            }
        }

        public bool heating
        {
            get
            {
                return pointer <= records.Length * 0.05;
            }
        }

        public DataProvider(string filename)
        {// C:\Users\Danylo Hulko\source\repos\WeierstrassCurveTest\WeierstrassCurveTest\Performance\Datasets\Dataset_weierstrass_2pow30.csv
            string filePath = Path.Combine(Environment.CurrentDirectory, @"Performance\Datasets\", filename);
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                records = csv.GetRecords<DatastItem>().ToArray();
                pointer = -1;
            }
        }

        public DatastItem GetNext()
        {
            if (complete)
            {
                throw new InvalidOperationException("Dataset was already processed");
            }
            return records[++pointer];
        }

        public void Restart()
        {
            pointer = -1;
        }
    }
}
