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
    [Register ("CellViewsSelected")]
    partial class CellViewsSelected
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton DeleteButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ExportButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton PlayButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIProgressView ProgressAudioBar { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel TitleAudio { get; set; }

        [Action ("DeleteButton_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void DeleteButton_TouchUpInside (UIKit.UIButton sender);

        [Action ("ExportButton_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void ExportButton_TouchUpInside (UIKit.UIButton sender);

        [Action ("PlayButton_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void PlayButton_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (DeleteButton != null) {
                DeleteButton.Dispose ();
                DeleteButton = null;
            }

            if (ExportButton != null) {
                ExportButton.Dispose ();
                ExportButton = null;
            }

            if (PlayButton != null) {
                PlayButton.Dispose ();
                PlayButton = null;
            }

            if (ProgressAudioBar != null) {
                ProgressAudioBar.Dispose ();
                ProgressAudioBar = null;
            }

            if (TitleAudio != null) {
                TitleAudio.Dispose ();
                TitleAudio = null;
            }
        }
    }
}