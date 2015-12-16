using CoreLocation;
using Foundation;
using MapKit;
using System;
using System.Collections.Generic;
using UIKit;

namespace DemoIOS
{
	public partial class ViewController : UIViewController
	{
		public ViewController (IntPtr handle) : base (handle)
		{
		}

		public List<HouseProperty> Properties
		{
			get; set;
		}

		private bool _listView = true;
        private MKMapView _mapView;

		public override async void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			System.Console.WriteLine ("ViewDidLoad");
			// Perform any additional setup after loading the view, typically from a nib.

			this.Properties = await HousePropertyService.Default.LoadAsync ();
			var source = new HousePropertiesTableViewSource (this.Properties);
			this.tableViewProperties.Source = source;
			this.tableViewProperties.ReloadData ();

			 // wire up the button click
            btnMap.Clicked += delegate
            {
                if (_listView)
                {
                    // we are showing the list so let's show the mapp
                    _listView = false;

                    // if first time with map then add
                    if (_mapView == null)
                    {
                        // create the map
                        this._mapView = new MKMapView(UIScreen.MainScreen.Bounds);

                        // create the custom delegate
                        this._mapView.Delegate = new HousePropertyMapViewDelegate((item) =>
                        {
                            // select the row
                            this.tableViewProperties.SelectRow(
                                NSIndexPath.FromRowSection(this.Properties.IndexOf(item), 0),
                                false,
                                UITableViewScrollPosition.Top);

                            // perform the segue
                            this.PerformSegue("segueDetails", this);
                        });

                        // add the subview
                        this.tableViewProperties.AddSubview(_mapView);
                    }

                    // switch the image
                    btnMap.Image = UIImage.FromBundle("list.png");

                    // transition the two views
                    UIView.Transition(this.tableViewProperties, this._mapView, 1, UIViewAnimationOptions.TransitionFlipFromLeft, null);
                    this.View = _mapView;
                }
                else
                {
                    // we are showing the map so let's show the list
                    _listView = true;

                    // switch the image
                    btnMap.Image = UIImage.FromBundle("pin.png");

                    // transition the two views
                    UIView.Transition(this._mapView, this.tableViewProperties, 1, UIViewAnimationOptions.TransitionFlipFromLeft, null);
                    this.View = this.tableViewProperties;
                }

                // Load the map data
                LoadMapData();
            };
		}

		 /// <summary>
        /// Load the map data from the properties retreived 
        /// </summary>
        private void LoadMapData()
        {
            // value to determin where to zoom in
            var topLeft = new CLLocationCoordinate2D(-90, 180);
            var bottomRight = new CLLocationCoordinate2D(90, -180);

            // loop through all the properties and add them to the list
            foreach (var item in Properties)
            {
                // create the pin and add the annoation
                var pin = new HousePropertyAnnotation(item);
                _mapView.AddAnnotation(pin);

                // determin the topleft and right
                topLeft.Longitude = Math.Min(topLeft.Longitude, pin.Coordinate.Longitude);
                topLeft.Latitude = Math.Max(topLeft.Latitude, pin.Coordinate.Latitude);
                bottomRight.Longitude = Math.Max(bottomRight.Longitude, pin.Coordinate.Longitude);
                bottomRight.Latitude = Math.Min(bottomRight.Latitude, pin.Coordinate.Latitude);
            }

            // zoom in on the annotations
            var region = new MKCoordinateRegion();
            region.Center = new CLLocationCoordinate2D(
                topLeft.Latitude - (topLeft.Latitude - bottomRight.Latitude) * 0.5,
                topLeft.Longitude + (bottomRight.Longitude - topLeft.Longitude) * 0.5);
            region.Span.LatitudeDelta = Math.Abs(topLeft.Latitude - bottomRight.Latitude) * 1.1;
            region.Span.LongitudeDelta = Math.Abs(bottomRight.Longitude - topLeft.Longitude) * 1.1;

            // set the region
            region = _mapView.RegionThatFits(region);
            _mapView.SetRegion(region, true);
        }

		/// <summary>
		/// Prepare the details view controller with the item selected
		/// </summary>
		/// 

		//snippet 4
        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            // make sure we are checking for the right seque
            if (segue.Identifier.Equals("segueDetails"))
            {
                // grab the destination controller
                var vc = segue.DestinationViewController as HousePropertyDetailViewController;
                if (vc != null)
                {
                    // grab the tableviewsource
                    var source = this.tableViewProperties.Source as HousePropertiesTableViewSource;

                    // set the property inside the view controller
                    vc.DetailProperty = source.GetItem(this.tableViewProperties.IndexPathForSelectedRow.Row);
                }
            }
            base.PrepareForSegue(segue, sender);
        }

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			System.Console.WriteLine ("ViewWillAppear");
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			System.Console.WriteLine ("ViewDidAppear");
		}

		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);
			System.Console.WriteLine ("ViewDidDisappear");
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
			System.Console.WriteLine ("ViewWillDisappear");
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}