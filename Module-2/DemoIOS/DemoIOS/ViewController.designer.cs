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
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIBarButtonItem btnMap { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITableView tableViewProperties { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (btnMap != null) {
				btnMap.Dispose ();
				btnMap = null;
			}
			if (tableViewProperties != null) {
				tableViewProperties.Dispose ();
				tableViewProperties = null;
			}
		}
	}
}
