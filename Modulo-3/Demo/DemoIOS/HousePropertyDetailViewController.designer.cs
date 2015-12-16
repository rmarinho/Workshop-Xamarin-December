// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace DemoIOS
{
	[Register ("HousePropertyDetailViewController")]
	partial class HousePropertyDetailViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblID { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblLat { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblLon { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIWebView webview { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (lblID != null) {
				lblID.Dispose ();
				lblID = null;
			}
			if (lblLat != null) {
				lblLat.Dispose ();
				lblLat = null;
			}
			if (lblLon != null) {
				lblLon.Dispose ();
				lblLon = null;
			}
			if (webview != null) {
				webview.Dispose ();
				webview = null;
			}
		}
	}
}
