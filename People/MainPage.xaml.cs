using Android.App;
using Rg.Plugins.Popup.Services;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;


namespace People
{

    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public async void OnNewButtonClicked(object sender, EventArgs args)
        {
            statusMessage.Text = "";
            await App.PersonRepo.AddNewPersonAsync(newPerson.Text, Cpf.Text, validade.Text);
            statusMessage.Text = App.PersonRepo.StatusMessage;
            if (!App.PersonRepo.exe)
            {
                await PopupNavigation.Instance.PushAsync(new QRViewPage());
            }
            var people = await App.PersonRepo.GetAllPeopleAsync();
            peopleList.ItemsSource = people;
        }

        public async void OnDelButtonClicked(object sender, EventArgs args)
        {
            statusMessage.Text = "";
            await App.PersonRepo.DeletePersonAsync(newPerson.Text);
            statusMessage.Text = App.PersonRepo.StatusMessage;
            var people = await App.PersonRepo.GetAllPeopleAsync();
            peopleList.ItemsSource = people;
        }

        public async void OnUpButtonClicked(object sender, EventArgs e)
        {
            statusMessage.Text = "";
            await App.PersonRepo.UpdatePersonAsync(newPerson.Text, Cpf.Text, validade.Text);
            statusMessage.Text = App.PersonRepo.StatusMessage;
        }
          

    }

}
