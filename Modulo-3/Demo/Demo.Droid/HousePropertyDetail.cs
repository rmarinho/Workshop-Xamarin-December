using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Webkit;
using Demo.Shared;

namespace Demo.Droid
{
	[Activity(Icon = "@drawable/icon")]
    public class HousePropertyDetail : Activity
    {
        protected override void OnCreate(Android.OS.Bundle savedInstanceState)
        {
            // set the view to the details page
            SetContentView(Resource.Layout.HousePropertyDetail);

            // setup the view
            this.FindViewById<TextView>(Resource.Id.lblId).Text = SelectedItem.Id;
            this.FindViewById<TextView>(Resource.Id.lblLat).Text = SelectedItem.Latitude.ToString();
            this.FindViewById<TextView>(Resource.Id.lblLon).Text = SelectedItem.Longitude.ToString();
            this.FindViewById<WebView>(Resource.Id.webView).LoadData(SelectedItem.Description, "text/html", "utf-8");

            // set the title
            this.Title = SelectedItem.Name;

            base.OnCreate(savedInstanceState);
        }

        /// <summary>
        /// The item selected
        /// </summary>
        public static HouseProperty SelectedItem { get; set; }
    }
}