using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HousesDemo
{
    public class HouseProperty
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string StyleUrl { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public string Location { get { return string.Format("{0:0.00000},{1:0.00000}", this.Latitude, this.Longitude); } }

        public static HouseProperty Parse(XElement element, XNamespace ns = null)
        {
            return new HouseProperty()
            {
                Id = element.Attribute("id").Value,
                Name = element.Elements(ns + "name").FirstOrDefault().Value,
                StyleUrl = element.Elements(ns + "styleUrl").FirstOrDefault().Value,
                Description = element.Elements(ns + "description").FirstOrDefault().Value,
                Longitude = double.Parse(element.Elements(ns + "Point").Elements(ns + "coordinates").FirstOrDefault().Value.Split(',')[0], CultureInfo.InvariantCulture),
                Latitude = double.Parse(element.Elements(ns + "Point").Elements(ns + "coordinates").FirstOrDefault().Value.Split(',')[1], CultureInfo.InvariantCulture),
            };
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
