using System;
using Foundation;
using UIKit;
using System.Collections.Generic;

namespace DictaFoule.Mobile.iOS
{
    internal class TableSource : UITableViewSource
    {
        //private List<string> tableItems;
        protected NSString cellIdentifier =  new NSString ("TableCell");
        private ViewcController ViewcController;

        List<TableItem> tableItems;
        public TableSource(List<TableItem> tableItems, ViewcController viewcController)
        {
            this.tableItems = tableItems;
            this.ViewcController = viewcController;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(cellIdentifier) as CellsCustoms;
            if (cell == null)
                cell = new CellsCustoms(cellIdentifier);
            cell.UpdateCell(tableItems[indexPath.Row].Name, tableItems[indexPath.Row].Date, tableItems[indexPath.Row].Time, UIImage.FromBundle("DetailsIcon"));
            //cell.TextLabel.Text = tableItems[indexPath.Row];
            //cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;

            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return tableItems.Count;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            var index = indexPath.Row;
            var detailController = ViewcController.Storyboard.InstantiateViewController("DetailViewController") as DetailViewController;
            ViewcController.NavigationController.PushViewController(detailController, true);
            detailController.SetItem(tableItems[index]);
            tableView.DeselectRow(indexPath, true);
        }
    }
}