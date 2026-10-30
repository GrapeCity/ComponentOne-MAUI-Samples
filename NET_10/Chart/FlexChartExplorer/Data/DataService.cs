using System;
using System.Collections.Generic;
using System.Globalization;
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

    public class TemperatureDiff
    {
        public DateTime Date { get; set; }
        public double Temp1 { get; set; }
        public double Temp2 { get; set; }
        public double Diff => Temp2 - Temp1;
    }


    public class PingSample
    {
        public DateTime Timestamp { get; set; }
        public double? LatencyMs { get; set; } // null = timeout / packet loss
    }

    public class Profile
    {
        public double BaseLatencyMs { get; set; }
        public double JitterMs { get; set; }
        public double SpikeChance { get; set; }
        public double PacketLossChance { get; set; }
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

        private static List<TemperatureDiff> temperatureDiffs;

        public static List<TemperatureDiff> GetTemperatureDifferenceData()
        {
            if (temperatureDiffs == null)
            {
                var list = new List<TemperatureDiff>();
                var asm = Assembly.GetExecutingAssembly();

                using (var stream = asm.GetManifestResourceStream(asm.GetName().Name + ".Resources.tempNY-SF.csv"))
                {
                    using (var sr = new StreamReader(stream))
                    {
                        for (var line = sr.ReadLine(); line != null; line = sr.ReadLine())
                        {
                            if (line.StartsWith("date"))
                                continue;

                            var fields = line.Split(',');
                            if (fields.Length == 3)
                            {
                                list.Add(new TemperatureDiff()
                                {
                                    Date = DateTime.ParseExact(fields[0], "dd/MM/yyyy", CultureInfo.InvariantCulture),
                                    Temp1 = double.Parse(fields[1], CultureInfo.InvariantCulture),
                                    Temp2 = double.Parse(fields[2], CultureInfo.InvariantCulture),
                                });
                            }
                        }
                    }
                }

                temperatureDiffs = list;
            }

            return temperatureDiffs;
        }

        public static List<PingSample> GeneratePingData(Profile profile, DateTime start, TimeSpan duration, TimeSpan interval)
        {
            var random = new Random(123);
            var result = new List<PingSample>();

            for (var time = start; time <= start + duration; time += interval)
            {
                double? latency = GenerateLatency(random, profile, time, start);
                result.Add(new PingSample { Timestamp = time, LatencyMs = latency });
            }

            return result;
        }

        private static double? GenerateLatency(Random random, Profile profile, DateTime timestamp, DateTime start)
        {
            if (random.NextDouble() < profile.PacketLossChance)
                return null;

            var minutes = (timestamp - start).TotalMinutes;
            var latency = profile.BaseLatencyMs + RandomNormal(random, 0, profile.JitterMs);

            latency += Math.Sin(minutes / 30.0) * profile.JitterMs * 0.5;

            if (random.NextDouble() < profile.SpikeChance)
                latency += random.Next(80, 400);

            return Math.Max(1, Math.Round(latency, 1));
        }

        private static double RandomNormal(Random random, double mean, double stdDev)
        {
            var u1 = 1.0 - random.NextDouble();
            var u2 = 1.0 - random.NextDouble();

            var randStdNormal =
                Math.Sqrt(-2.0 * Math.Log(u1)) *
                Math.Sin(2.0 * Math.PI * u2);

            return mean + stdDev * randStdNormal;
        }

    }
}
