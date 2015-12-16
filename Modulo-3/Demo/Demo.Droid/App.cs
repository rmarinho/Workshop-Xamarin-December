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

namespace Demo.Droid
{
	 [Application]
    public class App : Application
    {
        public App(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        public override void OnCreate()
        {
            System.Diagnostics.Debug.Write("------------> App.OnCreate()");
            base.OnCreate();
        }
    }
}