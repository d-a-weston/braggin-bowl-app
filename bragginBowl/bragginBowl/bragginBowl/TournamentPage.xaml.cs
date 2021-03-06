﻿using bragginBowl.models;
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
	public partial class TournamentPage : ContentPage
	{
		public TournamentPage ()
		{
			InitializeComponent();
            GetTournaments();
		}

        public async void GetTournaments()
        {
            HttpClient client = new HttpClient();

            try
            {
                activity_indicator.IsRunning = true;

                string uri = "http://bearfoot.design:8080/api/tournament/";
                string responseBody = await client.GetStringAsync(uri);

                var trn = JsonConvert.DeserializeObject<List<Tournament>>(responseBody);
                ObservableCollection<Tournament> tournaments = new ObservableCollection<Tournament>(trn);

                tournamentInfo.ItemsSource = tournaments;
                tournamentInfo.ItemSelected += OnItemSelected;

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
            var item = (Tournament)e.SelectedItem;
            await this.Navigation.PushAsync(new TournamentDetails(item.tournamentID));
        }
    }
}