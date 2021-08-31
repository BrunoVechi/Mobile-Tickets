using Rg.Plugins.Popup.Services;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace People
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QRViewPage 
    {
        ZXingBarcodeImageView barcode;

        public QRViewPage()
        {                       
                InitializeComponent();

                barcode = new ZXingBarcodeImageView
                {
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    AutomationId = "zxingBarcodeImageView",
                };
                barcode.BarcodeFormat = ZXing.BarcodeFormat.QR_CODE;
                barcode.BarcodeOptions.Width = 200;
                barcode.BarcodeOptions.Height = 200;
                barcode.BarcodeOptions.Margin = 0;
                barcode.BarcodeValue = App.PersonRepo.Codigo_;


                Codigo.Text = App.PersonRepo.Codigo_;
                NOME.Text = App.PersonRepo.Nome_;
                CPF.Text = App.PersonRepo.Cpf_;

                BOXView.Children.Add(barcode);                       
                                    
        }

        public async void ClosePage(object sender, EventArgs args) { await PopupNavigation.Instance.PopAllAsync(); }
        public async void GerarQR(object sender, EventArgs args) 
        {
            await PopupNavigation.Instance.PopAllAsync();

            await PopupNavigation.Instance.PushAsync(new QRViewPage());

        }
    }
}