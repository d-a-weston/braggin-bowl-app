using bragginBowl.models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bragginBowl
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PlayerCreationPage : ContentPage
	{
		public PlayerCreationPage ()
		{
			InitializeComponent ();
		}

        public async void OnButtonClicked(object sender, EventArgs args)
        {
            HttpClient client = new HttpClient();

            try
            {
                activity_indicator.IsRunning = true;

                Player player = new Player();
                Regex r = new Regex("^[a-zA-Z0-9]*$");

                if (Password1.Text != Password2.Text)
                {
                    await DisplayAlert("Alert", "Passwords don't match", "OK");
                }
                else if (GamertagEntry.Text.Length < 6 || GamertagEntry.Text.Length > 20)
                {
                    await DisplayAlert("Alert", "Gamertag 6-20 Characters ", "OK");
                }
                else if (!r.IsMatch(GamertagEntry.Text))
                {
                    await DisplayAlert("Alert", "Alphanumeric Gamertags Only", "OK");
                }
                else
                {
                    player.gamertag = GamertagEntry.Text;
                    player.name = NameEntry.Text;
                    player.tagline = TaglineEntry.Text;
                    player.photo = null;
                    player.password = Password1.Text;
                    player.salt = "12345";

                    var data = JsonConvert.SerializeObject(player);
                    var content = new StringContent(data, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync("http://bearfoot.design:8080/api/player", content);

                    Application.Current.Properties["Gamertag"] = player.gamertag;
                    await this.Navigation.PushModalAsync(new MainPage());
                    Navigation.RemovePage(this);
                }

                activity_indicator.IsRunning = false;
                activity_indicator.IsVisible = false;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught");
                Console.WriteLine("Message :{0} ", e.Message);
            }

            client.Dispose();
        }
	}
}