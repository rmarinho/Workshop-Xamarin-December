using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundation;
using UIKit;

namespace DemoIOS
{
    /// <summary>
    /// Custom tableviewSource used to display data in a UITableView
    /// </summary>
    public class HousePropertiesTableViewSource : UITableViewSource
    {
        /// <summary>
        /// The in memory list of properties
        /// </summary>
        private List<HouseProperty> tableItems;

        /// <summary>
        /// The cell identifer set in the storyboard
        /// </summary>
        private string _cellIdentifier = "housePropertyCell";

        public HousePropertiesTableViewSource(List<HouseProperty> items)
        {
            tableItems = items;
        }

        /// <summary>
        /// Called by the framework when it needs to know how many rows in a section
        /// </summary>
        /// <param name="tableview"></param>
        /// <param name="section"></param>
        /// <returns></returns>
	    public override nint RowsInSection(UITableView tableview, nint section)
        {
            // We only have one section so we just return total table items
            // if we were to have sections we would need to return the total items in a section
            // using the section paramater
            return (nint)tableItems.Count;
        }

		/// <summary>
		/// Called by the framework when a cell is requested
		/// </summary>
		/// <param name="tableView"></param>
		/// <param name="indexPath"></param>
		/// <returns></returns>
		/// 
		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
		    // in a Storyboard, Dequeue will ALWAYS return a cell, 
            UITableViewCell cell = tableView.DequeueReusableCell(_cellIdentifier);

            // now set the properties as normal
            var item = this.GetItem(indexPath.Row);

            // get and set the name
            var name = item.Name;
            if (string.IsNullOrEmpty(name))
                name = "Unknown";
            cell.TextLabel.Text = name;

            // set the lat/long in the detail
			cell.DetailTextLabel.Text = string.Format("{0:0.00000},{1:0.00000}", item.Latitude, item.Longitude);

            // return the cell
            return cell;
        }

        /// <summary>
        /// Gets a item by index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public HouseProperty GetItem(int index)
        {
            return tableItems[index];
        }
    }
}