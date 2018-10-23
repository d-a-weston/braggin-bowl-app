using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bragginBowl.models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net.Http;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bragginBowl
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LeaderboardPage : ContentPage
	{
		public LeaderboardPage ()
		{
			InitializeComponent ();
            GetLeaderboard();

        }
        public async void GetLeaderboard() {
            leaderboardList.ItemsSource = new List<Leaderboard>() {
                new Leaderboard() {
                    Position = 1, Gamertag="player1", Score = 1000,
                },
                new Leaderboard() {
                    Position = 2, Gamertag="player2", Score = 900,
                },
                new Leaderboard() {
                    Position = 3, Gamertag="player3", Score = 800,
                },
                new Leaderboard() {
                    Position = 4, Gamertag="player4", Score = 700,
                },
                new Leaderboard() {
                    Position = 5, Gamertag="player5", Score = 600,
                },
                new Leaderboard() {
                    Position = 6, Gamertag="player6", Score = 500,
                },
                new Leaderboard() {
                    Position = 7, Gamertag="player7", Score = 400,
                },
                new Leaderboard() {
                    Position = 8, Gamertag="player8", Score = 300,
                },
            };
        }

    }
}