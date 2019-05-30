// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace DictaFoule.Mobile.iOS
{
    [Register ("PayementController")]
    partial class PayementController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton CancelBtn { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField CVTxt { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField EmailTxt { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField ExpiratationDateTxt { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField FirstNameTxt { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField NbCbTxt { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton PayBtn { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel PriceTxt { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel TimeTranscriptionTxt { get; set; }

        [Action ("CancelBtn_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void CancelBtn_TouchUpInside (UIKit.UIButton sender);

        [Action ("PayBtn_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void PayBtn_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (CancelBtn != null) {
                CancelBtn.Dispose ();
                CancelBtn = null;
            }

            if (CVTxt != null) {
                CVTxt.Dispose ();
                CVTxt = null;
            }

            if (EmailTxt != null) {
                EmailTxt.Dispose ();
                EmailTxt = null;
            }

            if (ExpiratationDateTxt != null) {
                ExpiratationDateTxt.Dispose ();
                ExpiratationDateTxt = null;
            }

            if (FirstNameTxt != null) {
                FirstNameTxt.Dispose ();
                FirstNameTxt = null;
            }

            if (NbCbTxt != null) {
                NbCbTxt.Dispose ();
                NbCbTxt = null;
            }

            if (PayBtn != null) {
                PayBtn.Dispose ();
                PayBtn = null;
            }

            if (PriceTxt != null) {
                PriceTxt.Dispose ();
                PriceTxt = null;
            }

            if (TimeTranscriptionTxt != null) {
                TimeTranscriptionTxt.Dispose ();
                TimeTranscriptionTxt = null;
            }
        }
    }
}