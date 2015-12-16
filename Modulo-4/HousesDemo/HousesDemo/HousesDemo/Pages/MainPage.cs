using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace HousesDemo
{
    public class MainPage : NavigationPage
    {


        /// <summary>
        /// Member variable for the listview
        /// </summary>
        private ListView list;


        /// <summary>
        /// Reference to map control
        /// </summary>
		private Map map;

        /// <summary>
        /// Reference to list toolbar button
        /// </summary>
        private ToolbarItem btnList;

        /// <summary>
        /// Reference to map toolbar button
        /// </summary>
        private ToolbarItem btnMap;

        public MainPage()
        {

            // setup the navigation for dependency injection
            SimpleIoc.Default.Register<INavigation>(() => { return this.Navigation; });

            // wire up property changed events
            ViewModel.PropertyChanged += Main_PropertyChanged;

            // create a content page to embed within the navigation page
            var page = new ContentPage
            {
                BindingContext = this.ViewModel,
                Title = "House Properties"
            };

            // setup toolbar items
            btnList = new ToolbarItem("List", "list.png", () =>
            {
                this.list.IsVisible = true;
                this.map.IsVisible = false;
                page.ToolbarItems.Remove(btnList);
                page.ToolbarItems.Add(btnMap);
            });

            btnMap = new ToolbarItem("Map", "map.png", () =>
            {
                this.list.IsVisible = false;
                this.map.IsVisible = true;
                page.ToolbarItems.Add(btnList);
                page.ToolbarItems.Remove(btnMap);
				this.map.MoveToRegion (new MapSpan (new Position(ViewModel.HouseProperties.First ().Latitude, ViewModel.HouseProperties.First ().Longitude), 1, 1));
            });
            page.ToolbarItems.Add(btnList);


            // Create the map
            map = new Map()
            {
                IsShowingUser = true,
                HeightRequest = 100,
                WidthRequest = 960,
                VerticalOptions = LayoutOptions.FillAndExpand,
                MapType = MapType.Street
            };

                   // create the list
            list = new ListView()
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                IsVisible = false
            };
            this.list.ItemSelected += (s, e) => this.ViewModel.HeritagePropertyItemSelectedCommand.Execute(e.SelectedItem);

            // set the item template for the list
            list.ItemTemplate = new DataTemplate(typeof(TextCell));

            // set the item source data binding
            list.SetBinding(ListView.ItemsSourceProperty, MainViewModel.HousePropertiesPropertyName);

            // set the item template databinding
            // it is binding to a HeritageProperty object
            list.ItemTemplate.SetBinding(TextCell.TextProperty, "Name");
            list.ItemTemplate.SetBinding(TextCell.DetailProperty, "Id");

            // create a stack panel to hold the views
            var stack = new StackLayout
            {
                Spacing = 0,
                Children = { list, map }
            };

            // set the stack layout as the content to the page
            page.Content = stack;

            // push the map page to the top of the navigation stack
            this.PushAsync(page);

            // load the data
            ViewModel.CurrentLocationCommand.Execute(null);
            ViewModel.LoadHousePropertiesCommand.Execute(null);
	    }

        private void LoadPropertiesOnMap()
        {
			foreach (var item in ViewModel.HouseProperties) {
				var pin = new Pin {
					Position = new Position (item.Latitude, item.Longitude), Label = item.Name
				};
				map.Pins.Add (pin);

				pin.Clicked += Pin_Clicked;
			}
		     this.map.MoveToRegion (new MapSpan (new Position(ViewModel.HouseProperties.First ().Latitude, ViewModel.HouseProperties.First ().Longitude), 1, 1));
            
        }

		private void Pin_Clicked (object sender, EventArgs e)
		{
			var pin = (sender as Pin);
			var i = (from t in ViewModel.HouseProperties
					 where t.Longitude == pin.Position.Longitude && t.Latitude == pin.Position.Latitude
					 select t).FirstOrDefault ();
			if (i != null)
				this.ViewModel.HeritagePropertyItemSelectedCommand.Execute (i);
		}
	

		private void Main_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(MainViewModel.HousePropertiesPropertyName))
                LoadPropertiesOnMap();

        }

        /// <summary>
        /// View model to associate with the page
        /// </summary>
        public MainViewModel ViewModel { get { return App.Locator.GetViewModel<MainViewModel>(); } }

    }
}
