using Foundation;
using System;
using UIKit;
using CoreGraphics;
using CoreAnimation;
using System.Net;
using System.IO;
using System.Text;
using Stripe;
using Newtonsoft.Json;
using DictaFoule.Mobile.iOS.API;
using DictaFoule.Mobile.iOS.Models;
using System.Threading.Tasks;

namespace DictaFoule.Mobile.iOS
{
    public partial class PayementController : UIViewController
    {
        public Sound Item;
        private double _price;
        private string _token;

        public PayementController(IntPtr handle) : base(handle)
        {
        }

        public void SetItem(Sound item)
        {
            Item = item;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            CGColor[] colorsOrange = { new UIColor(red: 0.97f, green: 0.56f, blue: 0.37f, alpha: 1.0f).CGColor,
                new UIColor(red: 0.95f, green: 0.74f, blue: 0.43f, alpha: 1.0f).CGColor};
            CAGradientLayer gradientLayerOrange = new CAGradientLayer
            {
                Colors = colorsOrange,
                Transform = CATransform3D.MakeRotation((System.nfloat)(-Math.PI / 2), 0, 0, 1),
                Frame = new CGRect(0, 0, View.Bounds.Height, View.Bounds.Width + 50)
            };
            this.View.Layer.InsertSublayer(gradientLayerOrange, 0);

            TimeTranscriptionTxt.Text = Item.Time.Substring(3, 2) + " minutes et " + Item.Time.Substring(6) + " secondes de transcription.";
            TimeSpan time = TimeSpan.Parse(Item.Time);

            WaitPayement.Hidden = true;
            WaitPayement.HidesWhenStopped = true;

            var g = new UITapGestureRecognizer(() => View.EndEditing(true));
            g.CancelsTouchesInView = false; //for iOS5

            View.AddGestureRecognizer(g);

            if (time.TotalMinutes < 1)
            {
                PriceLbl.Text = "1€";
                _price = 1;
            }
            else
            {
                PriceLbl.Text = Math.Ceiling(time.TotalMinutes).ToString() + "€";
                _price = Math.Ceiling(time.TotalMinutes);
            }

            NameTxt.Tag = 1;
            NameTxt.ShouldReturn += NameTxt_ShouldReturn;
            EmailTxt.ShouldReturn += NameTxt_ShouldReturn;
            NbCbTxt.ShouldReturn += NameTxt_ShouldReturn;

        }

        private bool NameTxt_ShouldReturn(UITextField textField)
        {
            View.EndEditing(true);
            return true;
        }


        public override void ViewWillAppear(bool animated)
        {
            EffectViewBlur.Hidden = true;
        }


        async partial void PayBtn_TouchUpInside(UIButton sender)
        {

           EffectViewBlur.Hidden = false;
           WaitPayement.StartAnimating();

               
            if (await ApiCall.GetUserToDataBase() == false)
                await ApiCall.CreateUserToDataBase();
            await SendAudio();
            if (Item.IdProject == 0)
                return;
            if (!await CreateToken())
                return;
            var result = await Payement();
            if (!result)
                return;
            else
            {
                NavigationController.PopToRootViewController(true);
                Item.State = Business.SoundState.Upload;
            }
            this.WaitPayement.StopAnimating();
            EffectViewBlur.Hidden = true;
        }

        partial void CancelBtn_TouchUpInside(UIButton sender)
        {
            Item.IdProject = 0;
            NavigationController.PopToRootViewController(true);
        }

        async Task SendAudio()
        {
            var nsUid = UIDevice.CurrentDevice.IdentifierForVendor;
            var guidElements = nsUid.AsString();

            if (Item.IdProject > 0)
                return;

            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var audio = Path.Combine(path, Item.Name);
            var toEncodeAsBytes = System.IO.File.ReadAllBytes(audio);
            var returnValue = Convert.ToBase64String(toEncodeAsBytes);

            var soundFileModel = new SoundFileModel
            {
                File64 = returnValue,
                Name = Item.Name,
                Guid = guidElements
            };

            var result = await ApiCall.SendAudioToCreateProject(soundFileModel);
            if (result.Contains("Erreur") )
            {
                var erreurAlertController = UIAlertController.Create("Erreur", result, UIAlertControllerStyle.Alert);
                erreurAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
                PresentViewController(erreurAlertController, true, null);
                this.WaitPayement.StopAnimating();
                EffectViewBlur.Hidden = true;
                MgsError.Text = "Une erreur est survenue lors de l'envoie.";
            }
            else
                Item.IdProject = Int32.Parse(result);
        }

        async Task<bool> CreateToken()
        {
            if (String.IsNullOrWhiteSpace(EmailTxt.Text) || !EmailTxt.Text.Contains("@"))
            {
                this.WaitPayement.StopAnimating();
                EffectViewBlur.Hidden = true;
                MgsError.Text = "Email invalide";
                return false;
            }
            if (String.IsNullOrWhiteSpace(NameTxt.Text))
            {
                this.WaitPayement.StopAnimating();
                EffectViewBlur.Hidden = true;
                MgsError.Text = "Le champs Nom est vide";
                return false;
            }
            Int32.TryParse(cardExpYear.Text, out int cardexpyear);
            Int32.TryParse(cardExpMonth.Text, out int cardexpmonth);
            var stripeTokenModel = new StripeTokenModel() 
            { 
                Name = NameTxt.Text, 
                Number = NbCbTxt.Text, 
                ExpirationYear = cardexpyear, 
                ExpirationMonth = cardexpmonth,
                Cvc = CVCTxt.Text
            };
            _token = await ApiCall.StripeCreateToken(stripeTokenModel);
            if (String.IsNullOrEmpty(_token))
            {
                this.WaitPayement.StopAnimating();
                EffectViewBlur.Hidden = true;
                MgsError.Text = "Un problème est survenu lors du paiement.";
                return false;
            }
                
            return true;

        }

        async Task<bool> Payement()
        {
            var payementModel = new PayementModel()
            {
                Token = _token,
                Amount = (decimal)_price,
                IdProject = Item.IdProject,
                Name = Item.Name,
                Email = EmailTxt.Text,
            };
            if (!await ApiCall.Payement(payementModel))
            {
                this.WaitPayement.StopAnimating();
                EffectViewBlur.Hidden = true;
                MgsError.Text = "Un problème est survenu lors du paiement.";
                return false;
            }
            else
                return true;
        }
    }
}