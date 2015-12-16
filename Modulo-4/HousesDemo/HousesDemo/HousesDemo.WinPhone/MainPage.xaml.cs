using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using GalaSoft.MvvmLight.Ioc;
using Xamarin;

namespace HousesDemo.WinPhone
{
	public partial class MainPage : global::Xamarin.Forms.Platform.WinPhone.FormsApplicationPage
	{
		public MainPage ()
		{
			InitializeComponent ();
			SupportedOrientations = SupportedPageOrientation.PortraitOrLandscape;
			SimpleIoc.Default.Register<IGpsService, LocationService>();
			FormsMaps.Init ("08e1cc01-0c65-4d43-b86f-ae11209970d8", "_x83E4hb52Fk4DGr-HWz_Q");
			global::Xamarin.Forms.Forms.Init ();
			LoadApplication (new HousesDemo.App ());
		}
	}
}
