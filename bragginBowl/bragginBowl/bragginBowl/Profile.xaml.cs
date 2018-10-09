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
	public partial class Profile : ContentPage
	{

		public Profile ()
		{
			InitializeComponent ();
            GetPlayers();
		}

        public async void GetPlayers()
        {
            HttpClient client = new HttpClient();

            try
            {
                activity_indicator.IsRunning = true;

                string uri = "http://bearfoot.design:8080/api/player/";
                string responseBody = await client.GetStringAsync(uri);

                var plyr = JsonConvert.DeserializeObject<List<Player>>(responseBody);
                ObservableCollection<Player> players = new ObservableCollection<Player>(plyr);

                playerInfo.ItemsSource = players;

                int i = players.Count;
                if (i > 0)
                {
                    activity_indicator.IsRunning = false;
                }

                i = (players.Count * 90);
                activity_indicator.HeightRequest = i;
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