using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DemoIOS
{
    public class HousePropertyService
    {
        private static HousePropertyService _Default;
        public static HousePropertyService Default
        {
            get { return _Default = _Default ?? new HousePropertyService(); }
        }
        
        private HousePropertyService() { }

        public Task<List<HouseProperty>> LoadAsync()
        {
            // Start a task to load the data
            return Task.Factory.StartNew(() =>
            {
                var filename = "./data/data.kml";
                var ns = XNamespace.Get("http://www.opengis.net/kml/2.2");
                var xdoc = XDocument.Load(filename);
                var ps = xdoc.Element(ns + "kml").
                    Element(ns + "Document").
                    Element(ns + "Folder").
                    Elements(ns + "Placemark");

                var ret = new List<HouseProperty>();
                var count = 0;
                foreach (var item in ps)
                {
                    var t = HouseProperty.Parse(item, ns);
                    var e = ret.Find(i => { return t.Name.Equals(i.Name); });
                    if (e == null)
                        ret.Add(t);
                    else
                        count++;
                }

                return ret;
            });
        }
    }
}