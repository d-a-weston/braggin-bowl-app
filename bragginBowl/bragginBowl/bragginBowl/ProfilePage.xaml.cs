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
	public partial class ProfilePage : ContentPage
	{
		public ProfilePage ()
		{
			InitializeComponent ();
            GetPlayer();
		}

        public async void GetPlayer()
        {
            HttpClient client = new HttpClient();

            try
            {
                activity_indicator.IsRunning = true;

                string playerUri = "http://bearfoot.design:8080/api/player/1";
                string playerResponseBody = await client.GetStringAsync(playerUri);

                var plyr = JsonConvert.DeserializeObject<List<Player>>(playerResponseBody);
                Player Player = plyr[0];

                if (Player.photo != null)
                {
                    playerPhoto.Source = Player.photo.ToString();
                }
                
                playerGamertag.Text = Player.gamertag;
                playerName.Text = Player.name;
                playerTagline.Text = Player.tagline;

                string attendingUri = "http://bearfoot.design:8080/api/player/1/attending";
                string attendingResponseBody = await client.GetStringAsync(attendingUri);

                var atnd = JsonConvert.DeserializeObject<List<Plays_In>>(attendingResponseBody);
                ObservableCollection<Plays_In> attending = new ObservableCollection<Plays_In>(atnd);

                attendingInfo.ItemsSource = attending;

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