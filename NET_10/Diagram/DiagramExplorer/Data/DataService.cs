using Microsoft.Maui.Graphics.Platform;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

using Image = Microsoft.Maui.Graphics.IImage;

namespace DiagramExplorer.Data
{
#pragma warning disable 1591
    public class SalesDataItem
    {
        public string? type { get; set; }
        public double sales { get; set; }

        public SalesDataItem[]? items { get; set; }
    }

    // Simple hierarchical organization node
    public class OrgNode
    {
        Image? image = null;
        bool imageLoaded = false;

        public string Name { get; set; } = "";

        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";

        public string Department { get; set; } = "";

        public string JobTitle { get; set; } = "";
        public List<OrgNode> Childs { get; set; } = new List<OrgNode>();

        public Image? Image
        {
            get
            {
                if (!imageLoaded)
                {
                    var stream = DataService.GetResourceStream(Name.Replace(' ', '_') + ".png");
                    if (stream != null)
                        image = PlatformImage.FromStream(stream);
                    imageLoaded = true;
                }

                return image;
            }
        }
    }

    public class Country
    {
        public required string World { get; set; }
        public required string Continent { get; set; }
        public required string Region { get; set; }

        public required string Name { get; set; }
        public required string Code { get; set; }

        Image? flag;

        public Image? Flag
        {
            get
            {
                if (flag == null)
                {
                    var stream = DataService.GetResourceStream($"Images.flags.{Code}.png");
                    if (stream != null)
                        flag = PlatformImage.FromStream(stream);
                }
                return flag;
            }
        }
    }

    public class Animal
    {
        Image? image = null;
        ImageSource? imageSource = null;
        bool imageLoaded = false;
        bool imageSourceLoaded = false;

        public string Name { get; set; } = "";
        public string NameScientific { get; set; } = "";

        public string Phylum { get; set; } = "";

        public string Class { get; set; } = "";

        public string Order { get; set; } = "";

        public Image? Image
        {
            get
            {
                if (!imageLoaded && Class != "Class")
                {
                    var stream = DataService.GetResourceStream(Name.Replace(' ', '_') + ".png");
                    if (stream != null)
                        image = PlatformImage.FromStream(stream);
                    imageLoaded = true;
                }

                return image;
            }
        }

        public ImageSource? ImageSource
        {
            get
            {
                if (!imageSourceLoaded && Class != "Class")
                {
                    var stream = DataService.GetResourceStream(Name.Replace(' ', '_') + ".png");
                    if (stream != null)
                        imageSource = ImageSource.FromStream(() => stream);
                    imageSourceLoaded = true;
                }

                return imageSource;
            }
        }


        public override string ToString() => Name;
    }

    public class DataService
    {
        static DataService? _ds;
        static Random rnd = new Random();

        public static DataService GetService()
        {
            if (_ds == null)
                _ds = new DataService();
            return _ds;
        }

        static List<OrgNode>? orgData = null;

        public static List<OrgNode> GetOrgChartData()
        {
            if(orgData!=null)
                return orgData;

            var company = new OrgNode
            {
                Name = "Acme\nCorporation",
                Department = "CEO",
                Childs = new List<OrgNode>
                {
                    new OrgNode
                    {
                        FirstName = "Sarah",
                        LastName = "Mitchell",
                        Department = "CEO",
                        Name = "Sarah Mitchell",
                        JobTitle = "CEO",
                        Childs = new List<OrgNode>
                        {
                            new OrgNode
                            {
                                FirstName = "David",
                                LastName = "Chen",
                                Name = "David Chen",
                                Department = "Engineering",
                                JobTitle = "VP Engineering",
                                Childs = new List<OrgNode>
                                {
                                    new OrgNode
                                    {
                                        FirstName = "Amanda",
                                        LastName = "Rodriguez",
                                        Department = "Engineering",
                                        Name = "Amanda Rodriguez",
                                        JobTitle = "Engineering Manager",
                                    },
                                    new OrgNode
                                    {
                                        FirstName = "Robert",
                                        LastName = "Taylor",
                                        Department = "Engineering",
                                        Name = "Robert Taylor",
                                        JobTitle = "QA Manager",
                                    }
                                }
                            },
                            new OrgNode
                            {
                                FirstName = "Jennifer",
                                LastName = "Williams",
                                Name = "Jennifer Williams",
                                JobTitle = "VP Product",
                                Department = "Product",
                                Childs = new List<OrgNode>
                                {
                                    new OrgNode
                                    {
                                        FirstName = "Andrew",
                                        LastName = "Miller",
                                        Department = "Product",
                                        Name = "Andrew Miller",
                                        JobTitle = "Senior Product Manager",
                                    },
                                    new OrgNode
                                    {
                                        FirstName = "Sophia",
                                        LastName = "Lewis",
                                        Department = "Product",
                                        Name = "Sophia Lewis",
                                        JobTitle = "Design Lead",
                                    }
                                }
                            },
                            new OrgNode
                            {
                                FirstName = "Thomas",
                                LastName = "Anderson",
                                Name = "Thomas Anderson",
                                JobTitle = "VP Sales & Marketing",
                                Department = "Sales",
                                Childs = new List<OrgNode>
                                {
                                    new OrgNode
                                    {
                                        FirstName = "Karen",
                                        LastName = "White",
                                        Name = "Karen White",
                                        Department = "Sales",
                                        JobTitle = "Sales Director",
                                    },
                                    new OrgNode
                                    {
                                        FirstName = "Linda",
                                        LastName = "Campbell",
                                        Department = "Sales",
                                        Name = "Linda Campbell",
                                        JobTitle = "Marketing Director",
                                    }
                                }
                            },
                        }
                    }
                }
            };

            return orgData = new List<OrgNode> { company };
        }


        static int rand() => rnd.Next(10, 100);

        public static SalesDataItem[] CreateHierarchicalData()
        {
            var data = new SalesDataItem[] {
                    new SalesDataItem {
                        type = "Electronics",
                        items = new SalesDataItem[] {
                            new SalesDataItem{
                                type = "Camera",
                                items = new SalesDataItem[]
                                {
                                        new SalesDataItem{ type = "Digital", sales = rand() },
                                        new SalesDataItem{ type = "Film", sales = rand() },
                                }
                            },
                            new SalesDataItem{
                                type = "Headphones",
                                items = new SalesDataItem[]
                                {
                                        new SalesDataItem{ type = "Earbud", sales = rand() },
                                        new SalesDataItem{ type = "Over-ear", sales = rand() },
                                        new SalesDataItem{ type = "On-ear", sales = rand() },
                                }
                            }
                        }
                    },
                    new SalesDataItem{
                        type = "Computers\n& Tablets",
                        items = new SalesDataItem[]
                        {
                            new SalesDataItem
                            {
                                type = "Desktops",
                                items = new SalesDataItem[]
                                {
                                    new SalesDataItem{ type = "All-in-ones", sales = rand() },
                                    new SalesDataItem{ type = "Minis", sales = rand() },
                                }
                            },
                            new SalesDataItem
                            {
                                type = "Laptops",
                                items = new SalesDataItem[]
                                {
                                    new SalesDataItem{ type = "2 in 1", sales = rand() },
                                    new SalesDataItem{ type = "Traditional", sales = rand() }
                                }
                            },
                        }
                    }
                };
            return data;
        }

        readonly List<Country> countryData = [];

        public List<Country> GetCountryData()
        {
            if (countryData.Count == 0)
            {
                var stream = GetResourceStream("countries.csv");

                var text = "";
                if (stream != null)
                {
                    stream.Position = 0;
                    using var sr = new StreamReader(stream);
                    text = sr.ReadToEnd();
                }

                var ss = text.Split(['\n','\r'], StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < ss.Length; i++)
                {
                    var values = ss[i].Split(',');
                    countryData.Add(new Country { World = "World", Continent = values[0], Region = values[1], Name = values[2], Code = values[3] });
                }
            }

            return countryData;
        }

        public static Stream? GetResourceStream(string name)
        {
            var asm = Assembly.GetExecutingAssembly();
            return asm.GetManifestResourceStream($"{asm.GetName().Name}.Resources.{name}");
        }

        static List<Animal> GetAnimals(string name)
        {
            var list = new List<Animal>();

            using var stream = GetResourceStream(name);

            var text = "";
            if (stream != null)
            {
                using (var sr = new StreamReader(stream))
                    text = sr.ReadToEnd();
            }

            var ss = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < ss.Length; i++)
            {
                var values = ss[i].Split(',');
                list.Add(new Animal { Name = values[0], NameScientific = values[1], Phylum = values[2], Class = values[3], Order = values[4] });
            }

            return list;
        }

        static List<Animal>? animals = null;
        static List<Animal>? mammals = null;

        public static List<Animal> Animals
        {
            get
            {
                if (animals != null)
                    return animals;

                return animals = GetAnimals("animals10.csv");
            }
        }

        public static List<Animal> Mammals
        {
            get
            {
                if (mammals != null)
                    return mammals;

                return mammals = GetAnimals("mammals.csv");
            }
        }
    }
}
