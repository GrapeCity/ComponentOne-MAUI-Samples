using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Flights
{
    record Flight
    {
        static Random _rnd = new Random();

        static string[] _destinations = "Doha|Tokyo|Singapore|Seoul|Tokyo|Munich|Zurich|London|San Francisco|Dallas-Fort Worth|Denver|Guangzhou|Atlanta|Baiyun|Shuangliu-Wuhou|Chicago|Delhi".Split('|');
        static string[] _remarks = "|Check-in|Delayed|On Time".Split('|');

        public Flight()
        {
            Time = new TimeOnly(_rnd.Next(0, 23), _rnd.Next(0, 59));
            Destination = GetRandomString(_destinations);
            FlightId = $"{GetRandomLetter()}{GetRandomLetter()}{_rnd.Next(1000, 9999)}";
            Gate = _rnd.Next(0, 50) + 1;
            Remark = GetRandomString(_remarks);
        }

        private static char GetRandomLetter()
        {
            return (char)((int)'A' + _rnd.Next(0, (int)'Z' - (int)'A' + 1));
        }

        public TimeOnly Time { get; set; }

        public string Destination { get; set; }

        [Display(Name = "Flight")]
        public string FlightId { get; set; }

        public int Gate { get; set; }

        public string Remark { get; set; }

        internal static IEnumerable<Flight> CreateRandomList(int count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return new Flight();
            }
            yield break;
        }

        static string GetRandomString(string[] arr)
        {
            return arr[_rnd.Next(arr.Length)];
        }
    }
}