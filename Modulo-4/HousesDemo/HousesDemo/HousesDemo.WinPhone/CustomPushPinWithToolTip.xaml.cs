﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Maps.Controls;

namespace HousesDemo.WinPhone
{
	public partial class CustomPushPinWithToolTip : UserControl
	{
	   private string _description;
 
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public HouseProperty Item { get; set; }

        public MapOverlay ParentOverlay { get; set; }

        public CustomPushPinWithToolTip(HouseProperty item, MapOverlay overlay)
        {
            this.ParentOverlay = overlay;
            this.Item = item;
            this.Description = item.Name;
            InitializeComponent();
            Loaded += UCCustomToolTip_Loaded;
        }
 
        void UCCustomToolTip_Loaded(object sender, RoutedEventArgs e)
        {
            Lbltext.Text = Description;
        }
        public void FillDescription()
        {
            Lbltext.Text = Description;
 
        }

        private void imgmarker_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            HideCallout();

            if (MarkerTapped != null)
                MarkerTapped(this);
        }

        public void HideCallout()
        {
            if (imgpath.Opacity == 0)
            {
                imgpath.Opacity = 1;
                imgpath.Visibility = System.Windows.Visibility.Visible;
                imgborder.Visibility = System.Windows.Visibility.Visible;
                imgborder.Opacity = 1;
            }
            else
            {
                imgpath.Opacity = 0;
                imgborder.Opacity = 0;
                imgpath.Visibility = System.Windows.Visibility.Collapsed;
                imgborder.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void imgborder_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (ToolTipTapped != null)
                ToolTipTapped(this.Item);
        }

        public Action<HouseProperty> ToolTipTapped { get; set; }

        public Action<CustomPushPinWithToolTip> MarkerTapped { get; set; }
	}
}
