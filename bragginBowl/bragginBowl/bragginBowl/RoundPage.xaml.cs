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
    public partial class RoundPage : ContentPage {
        public RoundPage() {
            InitializeComponent();
            GetRound();

        }

        public void GetRound() {
            roundList.ItemsSource = new List<Round>() {
                new Round() {
                    RoundNum = 1, IsComplete = "True", GamerTagsTeam1 = "Player1 Player2", GamerTagsTeam2 = "Player3 Player4", ScoreTeam1 = 500, ScoreTeam2 = 600,
                },
                new Round() {
                    RoundNum = 1, IsComplete = "True", GamerTagsTeam1 = "Player5 Player6", GamerTagsTeam2 = "Player7 Player8", ScoreTeam1 = 100, ScoreTeam2 = 300,
                },
            };
        }
    }
}