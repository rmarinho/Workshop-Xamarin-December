using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Demo.Shared
{
	public class HousePropertyService
	{
		private static HousePropertyService _Default;
		public static HousePropertyService Default
		{
			get
			{
				return _Default = _Default ?? new HousePropertyService ();
			}
		}

		private HousePropertyService ()
		{
		}

#if __IOS__
		public Task<List<HouseProperty>> LoadAsync ()
#else
        public Task<List<HouseProperty>> LoadAsync(System.IO.Stream stream)
#endif
		{
			// Start a task to load the data
			return Task.Factory.StartNew (() => {
				var ns = XNamespace.Get ("http://www.opengis.net/kml/2.2");
#if __IOS__
				var filename = "./data/data.kml";
				var xdoc = XDocument.Load (filename);
#else
                var xdoc = XDocument.Load(stream);
#endif
				var ps = xdoc.Element (ns + "kml").
					Element (ns + "Document").
					Element (ns + "Folder").
					Elements (ns + "Placemark");

				var ret = new List<HouseProperty> ();
				var count = 0;
				foreach (var item in ps) {
					var t = HouseProperty.Parse (item, ns);
					var e = ret.Find (i => {
						return t.Name.Equals (i.Name);
					});
					if (e == null)
						ret.Add (t);
					else
						count++;
				}

				return ret;
			});
		}
	}
}