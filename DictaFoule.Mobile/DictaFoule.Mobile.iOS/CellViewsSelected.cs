using Foundation;
using System;
using UIKit;

namespace DictaFoule.Mobile.iOS
{
    public partial class CellViewsSelected : UITableViewCell
    {
        public static readonly NSString Key = new NSString("CellViewsSelected");

        public static readonly UINib Nib;

        public static readonly int ExpandedHeight = 210;

        public static readonly int NormalHeight = 45;

        


        static CellViewsSelected()
        {
            Nib = UINib.FromName("CellViewsSelected", NSBundle.MainBundle);
        }

        private Action reloadParentRow { get; set; }


        public CellViewsSelected (IntPtr handle) : base (handle)
        {
        }

        partial void PlayButton_TouchUpInside(UIButton sender)
        {
            throw new NotImplementedException();
        }

        partial void ExportButton_TouchUpInside(UIButton sender)
        {
            throw new NotImplementedException();
        }

        partial void DeleteButton_TouchUpInside(UIButton sender)
        {
            throw new NotImplementedException();
        }

        public void SetTitleAudio(string title)
        { TitleAudio.Text = title;}
    }
}