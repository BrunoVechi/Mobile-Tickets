using Xamarin.Forms;
using ZXing;
using ZXing.Net.Mobile.Forms;


namespace People
{
    
    public partial class ScanQR : ContentPage
    {		
		public ScanQR()
        {    
            InitializeComponent();
        }

		public void Handle_OnScanResult(Result result)
		{ 
			Device.BeginInvokeOnMainThread(async () =>
			{
				Message.Text = "";				
				Message.Text = await App.PersonRepo.ReadQRcode(result.Text);
				
			});
		}	
		
	}
}