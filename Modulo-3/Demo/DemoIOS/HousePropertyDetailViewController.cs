using Demo.Shared;
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace DemoIOS
{
	partial class HousePropertyDetailViewController : UIViewController
	{
		public HousePropertyDetailViewController (IntPtr handle) : base (handle)
		{
		}

		public HouseProperty DetailProperty
		{
			get; set;
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			this.Title = DetailProperty.Name;
            this.lblID.Text = DetailProperty.Id;
            this.lblLat.Text = DetailProperty.Latitude.ToString();
            this.lblLon.Text = DetailProperty.Longitude.ToString();
            this.webview.LoadHtmlString(DetailProperty.Description, null);
		}
	}
}
