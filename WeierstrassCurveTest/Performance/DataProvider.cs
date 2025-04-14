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
        public int proccessingTotal
        {
            get
            {
                return records.Length - heatingTotal;
            }
        }

        public int heatingTotal
        {
            get
            {
                return (int)(records.Length * 0.05);
            }
        }

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
                return pointer < heatingTotal;
            }
        }


        public DataProvider(string filePath)
        {// C:\Users\Danylo Hulko\source\repos\WeierstrassCurveTest\WeierstrassCurveTest\Performance\Datasets\Dataset_weierstrass_2pow30.csv
            //string filePath = Path.Combine(Environment.CurrentDirectory, @"Performance\Datasets\", filename);
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                records = csv.GetRecords<DatastItem>().ToArray();
                pointer = -1;
            }
        }

        public (DatastItem item, bool isHeating) GetNext()
        {
            if (complete)
            {
                throw new InvalidOperationException("Dataset was already processed");
            }
            return (records[++pointer], heating);
        }

        public void Restart()
        {
            pointer = -1;
        }
    }
}
