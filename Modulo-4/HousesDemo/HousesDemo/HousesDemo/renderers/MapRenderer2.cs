using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace HousesDemo
{
    public class MapRenderer2 : Map
    {
        public const string MESSAGE_ON_INFO_WINDOW_CLICKED = "OnInfoWindowClicked";
        public const string MESSAGE_ADD_AND_ZOOM_ON_PINS = "AddAndZoomOnPins";
        public const string MESSAGE_ZOOM_ON_PINS = "ZoomOnPins";
        public MapRenderer2()
            : base()
        {
            MessagingCenter.Subscribe<Location>(this, MESSAGE_ON_INFO_WINDOW_CLICKED, (loc) =>
            {
                if (OnInfoWindowClicked != null)
                    OnInfoWindowClicked(loc);
            });
        }
        public MapRenderer2(MapSpan region)
            : base(region)
        {
            MessagingCenter.Subscribe<Location>(this, MESSAGE_ON_INFO_WINDOW_CLICKED, (loc) =>
            {
                if (OnInfoWindowClicked != null)
                    OnInfoWindowClicked(loc);
            });
        }

        public Action<Location> OnInfoWindowClicked { get; set; }

        public void AddAndZoomOnPins(IEnumerable<HouseProperty> items)
        {
            MessagingCenter.Send<IEnumerable<HouseProperty>>(items, MESSAGE_ADD_AND_ZOOM_ON_PINS);
        }

        public void ZoomOnPins(IEnumerable<HouseProperty> items)
        {
            MessagingCenter.Send<IEnumerable<HouseProperty>>(items, MESSAGE_ZOOM_ON_PINS);
        }
    }
}
