using bragginBowl.models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
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

                if (Password1.Text == Password2.Text)
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

                    await this.Navigation.PushAsync(new ProfilePage(player.gamertag));
                }
                else
                {
                    await DisplayAlert("Alert", "Passwords don't match", "OK");
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