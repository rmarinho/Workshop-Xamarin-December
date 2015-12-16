using CoreLocation;
using MapKit;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoIOS
{
	 /// <summary>
    /// Custom point annotation so we can store the HouseProperty value
    /// </summary>
    public class HousePropertyAnnotation : MKPointAnnotation
    {
        public HousePropertyAnnotation(HouseProperty property)
        {
            this.Property = property;
            Title = property.Name;
            Coordinate = new CLLocationCoordinate2D(property.Latitude, property.Longitude);
        }

        /// <summary>
        /// Gets the value of house property
        /// </summary>
        public HouseProperty Property { get; private set; }
    }
}
