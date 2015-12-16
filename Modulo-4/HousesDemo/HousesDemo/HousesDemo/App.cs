using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace HousesDemo
{
	public class App : Application
	{
		public App ()
		{
			// The root page of your application
			MainPage = new MainPage ();
		}

		private static ViewModelLocator _locator;
        public static ViewModelLocator Locator
        {
            get
            {
                return _locator ?? (_locator = new ViewModelLocator());
            }
        }

		
		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}

	public class ViewModelLocator
	{
		static ViewModelLocator ()
		{
			ServiceLocator.SetLocatorProvider (() => SimpleIoc.Default);

			// add the models
			SimpleIoc.Default.Register<MainViewModel> ();
			SimpleIoc.Default.Register<DetailsViewModel> ();

			// add the service
			SimpleIoc.Default.Register<HousePropertyService> ();
		}


		public T GetViewModel<T> ()
		{
			return ServiceLocator.Current.GetInstance<T> ();
		}
	}
}
