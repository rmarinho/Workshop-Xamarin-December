using CoreLocation;
using Foundation;
using HousesDemo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIKit;

namespace HousesDemo.iOS
{
    public partial class LocationService : IGpsService
    {

        CLLocationManager manager = new CLLocationManager();

        public Task<Location> GetLocation()
        {
			// iOS 8 has additional permissions requirements
			if (UIDevice.CurrentDevice.CheckSystemVersion (8, 0)) {
				manager.RequestAlwaysAuthorization ();
			}

			
            manager.StartUpdatingLocation();
            
            return Task.Run(async () =>
            {

				var ret = new Location () {
					Latitude = manager.Location.Coordinate.Latitude,
					Longitude = manager.Location.Coordinate.Longitude
                        };
                
                var locationFound = true;

                // create the locator
                manager.LocationsUpdated += (object sender, CLLocationsUpdatedEventArgs e) =>
                {
                    if (e.Locations.Length > 0)
                    {
                        ret = new Location()
                        {
                            Latitude = e.Locations[0].Coordinate.Latitude,
                            Longitude = e.Locations[0].Coordinate.Longitude
                        };
                    }

                    locationFound = true;
                };
                manager.Failed += (object sender, NSErrorEventArgs e) =>
                {
                    locationFound = true;
                };

                // wait till done
                while (!locationFound)
                    await Task.Delay(200);

                // stop udpating the location
                manager.StopUpdatingLocation();

                // return the location
                return ret;
            });
        }
    }
}