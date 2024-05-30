using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace FlexChartExplorer.Data
{
#pragma warning disable 1591

    [DataContract]
    public class GdpData
    {
        [DataMember(Name = "country")]
        public string Country { get; set; }

        [DataMember(Name = "continent")]
        public string Continent { get; set; }

        [DataMember(Name = "pop")]
        public int Population { get; set; }

        [DataMember(Name = "gdp")]
        public double Gdp { get; set; }


        public double GdpBillions => Gdp * 0.001;
        public double PopulationMillions => Population * 0.0000001;
        public double GdpPerCapita => Gdp * 1e6 / Population ;
    }

    [DataContract]
    public class CarData
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Origin { get; set; }


        [DataMember(Name = "Miles_per_Gallon")]
        public double MilesPerGallon { get; set; }

        [DataMember]
        public int Cylinders { get; set; }

        [DataMember]
        public double Displacement { get; set; }

        [DataMember(Name="Horsepower")]
        public double HorsePower { get; set; }
    }

    public class Quote
    {
        public DateTime Date { get; set; }

        public double Open { get; set; }
        public double Close { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Volume { get; set; }
    }

    public class CountrySalesData
    {
        public string Name { get; set; }
        public double Sales { get; set; }
    }

    public class DataService
    {
        static DataService _ds;
        static List<CarData> _carData;
        static Random rnd = new Random();

        public static DataService GetService()
        {
            if (_ds == null)
                _ds = new DataService();
            return _ds;
        }

        List<T> GetData<T>(string name)
        {
            string path = string.Format("FlexChartExplorer.Resources.{0}.json", name);
            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
            var ser = new DataContractJsonSerializer(typeof(T[]));
            var data = (T[])ser.ReadObject(stream);

            return data.ToList();
        }

        public List<GdpData> GetGdpData()
        {
            return GetData<GdpData>("gdp");
        }

        public List<CarData> GetCarData()
        {
            if (_carData == null)
                _carData = GetData<CarData>("cars");
            return _carData;
        }

        public List<Quote> CreateFinancialData(DateTime start, int days)
        {
            List<Quote> list = new List<Quote>();
            var dt = start;
            for (var i = 0; i < days; i++)
            {
                var q = new Quote();
                q.Date = dt.AddDays(i);
                if (i > 0)
                    q.Open = list[i - 1].Close;
                else
                    q.Open = 1000;
                q.High = q.Open + rnd.Next(50);
                q.Low = q.Open - rnd.Next(50);
                q.Close = rnd.Next((int)q.Low, (int)q.High);
                q.Volume = rnd.Next(0, 100);
                list.Add(q);
            }
            return list;
        }

        static string[] countries = { "US", "Japan", "China", "India", "Germany", "UK" };

        public List<CountrySalesData> GetCountrySales(int rangeMin = 100, int rangeMax = 1000)
        {
            var data = new List<CountrySalesData>();
            for (int i = 0; i < countries.Length; i++)
            {
                var country = new CountrySalesData
                {
                    Name = countries[i],
                    Sales = rnd.Next(rangeMin, rangeMax),
                };
                data.Add(country);
            }
            return data;
        }
    }
}
