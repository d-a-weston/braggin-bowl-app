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
	public partial class TournamentDetails : ContentPage
	{

        int tournID;

		public TournamentDetails ()
		{
			InitializeComponent ();
            GetTournament(1);

        }

        public TournamentDetails (int tournamentID)
        {
            InitializeComponent();
            tournID = tournamentID;
            GetTournament(tournID);
        }

        public async void GetTournament(int tournamentID)
        {
            HttpClient client = new HttpClient();

            try
            {
                activity_indicator.IsRunning = true;

                string uri = "http://bearfoot.design:8080/api/tournament/" + tournamentID;
                string tournInfo = await client.GetStringAsync(uri);

                var trn = JsonConvert.DeserializeObject<List<Tournament>>(tournInfo);
                ObservableCollection<Tournament> tournaments = new ObservableCollection<Tournament>(trn);

                tournamentDate.Text = tournaments[0].tournament_date.ToShortDateString();
                tournamentGame.Text = tournaments[0].game;
                tournamentModifiers.Text = tournaments[0].modifiers;

                uri += "/attending";
                string attendInfo = await client.GetStringAsync(uri);
                var plyr = JsonConvert.DeserializeObject<List<Player>>(attendInfo);
                ObservableCollection<Player> players = new ObservableCollection<Player>(plyr);

                playerInfo.ItemsSource = players;
                playerInfo.ItemSelected += OnItemSelected;

                foreach (Player player in players)
                {
                    if (player.gamertag == Application.Current.Properties["Gamertag"] as string)
                    {
                        SignUpButton.IsVisible = false;
                    }
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

        public async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (Player)e.SelectedItem;
            await this.Navigation.PushAsync(new ProfilePage(item.gamertag));
        }

        public async void ViewLeaderboard() {
            await this.Navigation.PushAsync(new LeaderboardPage());
        }
        public async void ViewRound() {
            await this.Navigation.PushAsync(new RoundPage());
        }

        public async void SignUp()
        {
            HttpClient client = new HttpClient();
            string uri = "http://bearfoot.design:8080/api/tournament/" + tournID + "/attending/" + Application.Current.Properties["PlayerID"] as string;
            await client.PostAsync(uri, null);

            uri = "http://bearfoot.design:8080/api/tournament/" + tournID + "/attending";
            string attendInfo = await client.GetStringAsync(uri);
            var plyr = JsonConvert.DeserializeObject<List<Player>>(attendInfo);
            ObservableCollection<Player> players = new ObservableCollection<Player>(plyr);

            playerInfo.ItemsSource = players;
            playerInfo.ItemSelected += OnItemSelected;
            SignUpButton.IsVisible = false;
        }
    }

}