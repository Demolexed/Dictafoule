using Foundation;
using System;
using UIKit;
using CoreGraphics;
using CoreAnimation;
using AVFoundation;
using System.Timers;

namespace DictaFoule.Mobile.iOS
{
    public partial class DetailViewController : UIViewController
    {

        public int index;
        public TableItem Item;

        public TimeSpan time = new TimeSpan();
        Timer playTimer = new Timer();

        public TimeSpan spentTime = new TimeSpan();
        Timer timer = new Timer();


        GameAudioManager player;

        public DetailViewController()
        {
        }

        public DetailViewController(IntPtr handle) : base(handle)
        {
        }

        public DetailViewController(int index)
        {
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
                Frame = new CGRect(0, 0, View.Bounds.Height, View.Bounds.Width - 200)
            };
            this.View.Layer.InsertSublayer(gradientLayerOrange, 0);

            PlayBtn.SetBackgroundImage(UIImage.FromBundle("PlayIcon"), UIControlState.Normal);

            time = TimeSpan.Zero;
            playTimer.Interval = 1000;
            playTimer.Elapsed += UpdateSliderTime;

            spentTime = TimeSpan.Zero;
            timer.Interval = 1000;
            timer.Elapsed += Timer_Elapsed;

            ActivityIndicartorTranscrib.Hidden = true;
            WaitTranscribTxT.Hidden = true;
            TranscriptionTxt.Hidden = true;
            AskTranscrib.Hidden = true;
            OptionBtn.Hidden = true;
            ExportBtn.Hidden = true;

        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            NameRecordTxt.Text = Item.Name;
            if (Item.Transcribed == 0)
                AskTranscrib.Hidden = false;
            if (Item.Transcribed == 10)
            {
                AskTranscrib.Hidden = true;
                ActivityIndicartorTranscrib.Hidden = false;
                WaitTranscribTxT.Hidden = false;
            }
            if (Item.Transcribed == 20)
            {
                ActivityIndicartorTranscrib.Hidden = true;
                WaitTranscribTxT.Hidden = true;
                TranscriptionTxt.Hidden = false;

            }
            if (Item.Transcribed == 30)
            {
                TranscriptionTxt.Hidden = false;
                OptionBtn.Hidden = false;
                ExportBtn.Hidden = false;
            }   
        }

        public void SetItem(TableItem item)
        {
            Item = item;
        }

        partial void ExportBtn_TouchUpInside(UIButton sender)
        {
            this.View.BackgroundColor = UIColor.Gray;
            //CustomPopUpView cpuv = new CustomPopUpView(new CoreGraphics.CGSize(300, 175), "EMAIL*", "SendButton.png", "CancelButton.png");
            //cpuv.PopUp();
        }

        partial void PlayBtn_TouchUpInside(UIButton sender)
        {
            /* ActivityIndicartorTranscrib.Hidden = true;
            WaitTranscribTxT.Hidden = true;
            TranscriptionTxt.Hidden = false;
            ExportBtn.Hidden = false;
            OptionBtn.Hidden = false;
            Item.Transcribed = 30; */

            try
            {
                player = new GameAudioManager();
                playTimer.Enabled = true;
                timer.Enabled = true;
                playTimer.Start();
                timer.Start();

                player.PlaySound(Item.Pathfile);

                SliderTime.MinValue = 0;
                SliderTime.MaxValue = (float)(int)player.Duration();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        partial void AskTranscrib_TouchUpInside(UIButton sender)
        {
            var payementController = this.Storyboard.InstantiateViewController("PayementController") as PayementController;
            this.NavigationController.PushViewController(payementController, true);
            payementController.SetItem(Item);
        }

        private void UpdateSliderTime(object sender, ElapsedEventArgs e)
        {
            if (player.IsPlaying())
                InvokeOnMainThread(delegate {
                    SliderTime.Value += 1;
                });
            else
            {
                playTimer.Enabled = false;
                timer.Enabled = false;
                playTimer.Stop();
                timer.Stop();
                InvokeOnMainThread(delegate
                {
                    SliderTime.Value = SliderTime.MinValue;
                    time = TimeSpan.Zero;
                    TimerTxt.Text = time.ToString("t");
                });
            }
        }

void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            time = time.Add(TimeSpan.FromSeconds(1));
            InvokeOnMainThread(() =>
            {
                TimerTxt.Text = time.ToString("t");

            });
        }

    }
}