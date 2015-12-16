using Demo.Shared;
using Foundation;
using MapKit;
using System;
using System.Collections.Generic;
using System.Text;
using UIKit;

namespace DemoIOS
{
	class HousePropertyMapViewDelegate
		: MKMapViewDelegate
    {
        string pId = "PinAnnotation";

        public HousePropertyMapViewDelegate(Action<HouseProperty> calloutTapCallBack)
        {
            this.CalloutTappedCallback = calloutTapCallBack;
        }
        
        /// <summary>
        /// Call back function when accessory is tapped
        /// </summary>
        private Action<HouseProperty> CalloutTappedCallback { get; set; }

		public override MKAnnotationView GetViewForAnnotation (MKMapView mapView, IMKAnnotation annotation)
		{
			  // if it's the user location just return 
            if (annotation is MKUserLocation)
                return null;

            MKAnnotationView anView;

            // show pin annotation
            anView = (MKPinAnnotationView)mapView.DequeueReusableAnnotation(pId);

            // if we didn't deque reuse
            if (anView == null)
                anView = new MKPinAnnotationView(annotation, pId);

            // set the accessory and pin
            ((MKPinAnnotationView)anView).PinColor = MKPinAnnotationColor.Red;
            anView.RightCalloutAccessoryView = UIButton.FromType(UIButtonType.DetailDisclosure);
            anView.CanShowCallout = true;

            // return the view
            return anView;
		}

        public override void CalloutAccessoryControlTapped(MKMapView mapView, MKAnnotationView view, UIControl control)
        {
            // make sure its the right annotation
            var ann = view.Annotation as HousePropertyAnnotation;

            // call the callback
            if (ann != null && this.CalloutTappedCallback != null)
                CalloutTappedCallback(ann.Property);
        }
	}
}
