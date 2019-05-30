using Foundation;
using System;
using UIKit;
using CoreGraphics;
using CoreAnimation;
using System.Net;
using System.IO;
using System.Text;

namespace DictaFoule.Mobile.iOS
{
    public partial class PayementController : UIViewController
    {
        public TableItem Item;
        public PayementController (IntPtr handle) : base (handle)
        {
        }

        public void SetItem(TableItem item)
        {
            Item = item;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            CGColor[] colorsOrange = new CGColor[] { new UIColor(red: 0.97f, green: 0.56f, blue: 0.37f, alpha: 1.0f).CGColor,
                new UIColor(red: 0.95f, green: 0.74f, blue: 0.43f, alpha: 1.0f).CGColor};
            CAGradientLayer gradientLayerOrange = new CAGradientLayer
            {
                Colors = colorsOrange,
                Transform = CATransform3D.MakeRotation((System.nfloat)(-Math.PI / 2), 0, 0, 1),
                Frame = new CGRect(0, 0, View.Bounds.Height, View.Bounds.Width + 50)
            };
            this.View.Layer.InsertSublayer(gradientLayerOrange, 0);

            TimeTranscriptionTxt.Text = Item.Time.Substring(3, 2)  + " minutes et " + Item.Time.Substring(6) + " secondes de transcription.";
        }

        partial void PayBtn_TouchUpInside(UIButton sender)
        {
            //var nsUid = UIDevice.CurrentDevice.IdentifierForVendor;
            //var guidElements = nsUid.AsString();
            //Console.WriteLine("ToString() : {0}\nAsString() : {1}", nsUid, guidElements);
            NavigationController.PopToRootViewController(true);
            Item.Transcribed = 10;
            //var file = "UklGRmQAAABXQVZFZm10IBAAAAABAAEAwoA+AAAAfQAAAgAQAGRhdGFAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA==";
            //var postData = "File64=" + file + "&"+ "Name=test.mp3";


            //var data = Encoding.UTF8.GetBytes(postData);
            //var request = (HttpWebRequest)WebRequest.Create("http://dictafouleapi20180412121507.azurewebsites.net/v1/Project/Create");
            //request.Method = "POST";
            //request.ContentType = "application/x-www-form-urlencoded";
            //request.ContentLength = data.Length;
            //request.UserAgent = "curl/7.37.0";
            //using (var stream = request.GetRequestStream())
            //{
            //    stream.Write(data, 0, data.Length);
            //}
            //WebResponse response = request.GetResponse();
            //Console.WriteLine(((HttpWebResponse)response).StatusDescription);



        }

        partial void CancelBtn_TouchUpInside(UIButton sender)
        {
            NavigationController.PopToRootViewController(true);
        }
    }
}