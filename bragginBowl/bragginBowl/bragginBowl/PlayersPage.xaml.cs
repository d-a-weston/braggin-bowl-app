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
	public partial class PlayersPage : ContentPage
	{

		public PlayersPage ()
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