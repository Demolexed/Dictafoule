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
    [Register ("DetailViewController")]
    partial class DetailViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIActivityIndicatorView ActivityIndicartorTranscrib { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton AskTranscrib { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ExportBtn { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel NameRecordTxt { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton OptionBtn { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton PlayBtn { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISlider SliderTime { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel TimerTxt { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView TranscriptionTxt { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel WaitTranscribTxT { get; set; }

        [Action ("AskTranscrib_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void AskTranscrib_TouchUpInside (UIKit.UIButton sender);

        [Action ("ExportBtn_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void ExportBtn_TouchUpInside (UIKit.UIButton sender);

        [Action ("PlayBtn_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void PlayBtn_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (ActivityIndicartorTranscrib != null) {
                ActivityIndicartorTranscrib.Dispose ();
                ActivityIndicartorTranscrib = null;
            }

            if (AskTranscrib != null) {
                AskTranscrib.Dispose ();
                AskTranscrib = null;
            }

            if (ExportBtn != null) {
                ExportBtn.Dispose ();
                ExportBtn = null;
            }

            if (NameRecordTxt != null) {
                NameRecordTxt.Dispose ();
                NameRecordTxt = null;
            }

            if (OptionBtn != null) {
                OptionBtn.Dispose ();
                OptionBtn = null;
            }

            if (PlayBtn != null) {
                PlayBtn.Dispose ();
                PlayBtn = null;
            }

            if (SliderTime != null) {
                SliderTime.Dispose ();
                SliderTime = null;
            }

            if (TimerTxt != null) {
                TimerTxt.Dispose ();
                TimerTxt = null;
            }

            if (TranscriptionTxt != null) {
                TranscriptionTxt.Dispose ();
                TranscriptionTxt = null;
            }

            if (WaitTranscribTxT != null) {
                WaitTranscribTxT.Dispose ();
                WaitTranscribTxT = null;
            }
        }
    }
}