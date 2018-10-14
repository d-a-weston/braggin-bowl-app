using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using bragginBowl.models;
using Newtonsoft.Json;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bragginBowl
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TournamentCreationPage : ContentPage
	{
		public TournamentCreationPage ()
		{
			InitializeComponent ();
        }
        public async void OnButtonClicked(object sender, EventArgs args) {
            HttpClient client = new HttpClient();

            try {
                activity_indicator.IsRunning = true;

                Tournament tournament = new Tournament();
                int n;
                if (!int.TryParse(TeamSize.Text, out n) && (TeamSize.Text != null && TeamSize.Text != "")) {
                    await DisplayAlert("Alert", "Team Size must be numeric", "OK");
                }else if(TournamentTypePicker.SelectedItem == null) {
                    await DisplayAlert("Alert", "Tournament Type is required", "OK");
                }else if(Game.Text == null || Game.Text =="") {
                    await DisplayAlert("Alert", "Game is required", "OK");
                }else if (Date.Date == null ) {
                    await DisplayAlert("Alert", "Date is required", "OK");
                }else if(TeamSize.Text == null || TeamSize.Text == "") {
                    TeamSize.Text = "1";
                }else {
                    if(TournamentTypePicker.SelectedIndex == 0) {
                        tournament.tournament_type = "SWISS";
                    } else {
                        tournament.tournament_type = "DOUBLE_ELIM";
                    }
                    if (LegalTournamentPicker.SelectedIndex == 0) {
                        tournament.legal_tournament = "YES";
                    } else {
                        tournament.legal_tournament = "NO";
                    }
                    tournament.game = Game.Text;
                    tournament.modifiers = Modifiers.Text;
                    tournament.tournament_date = Date.Date;
                    tournament.team_size = Int32.Parse(TeamSize.Text);
                    tournament.team_num = 0;


                }

                var data = JsonConvert.SerializeObject(tournament);
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("http://bearfoot.design:8080/api/tournament", content);

                activity_indicator.IsRunning = false;
                activity_indicator.IsVisible = false;
            } catch (HttpRequestException e) {
                Console.WriteLine("\nException Caught");
                Console.WriteLine("Message :{0} ", e.Message);
            }

            client.Dispose();
        }
    }
}