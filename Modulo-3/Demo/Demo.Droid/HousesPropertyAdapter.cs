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
using Demo.Shared;

namespace Demo.Droid
{
    public class HousePropertyAdapter : BaseAdapter<HouseProperty>
    {
        private List<HouseProperty> _items;
        private Activity _context;

        public HousePropertyAdapter(Activity context, List<HouseProperty> items)
        {
            this._items = items;
            this._context = context;
        }

        public override HouseProperty this[int position]
        {
            get { return this._items[position]; }
        }

        public override int Count
        {
            get { return this._items.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            // get the item for the position pointed to
            var item = this._items[position];

            // use the simple list adapter
            var view = convertView;
            if (view == null)
                view = this._context.LayoutInflater.Inflate(Resource.Layout.HousePropertyListViewLayout, null);

            // set the text
            view.FindViewById<TextView>(Resource.Id.text1).Text = item.Name;
            view.FindViewById<TextView>(Resource.Id.text2).Text = string.Format("{0}, {1}", item.Latitude, item.Longitude);
            view.FindViewById<TextView>(Resource.Id.text3).Text = item.Id;

            // return the view
            return view;
        }
    }
}