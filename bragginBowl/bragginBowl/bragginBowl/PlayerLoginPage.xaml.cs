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
	public partial class PlayerLoginPage : ContentPage
	{
		public PlayerLoginPage ()
		{
			InitializeComponent ();
		}

        public async void Login()
        {
            HttpClient client = new HttpClient();

            try
            {
                if (GamertagEntry.Text == "")
                {
                    await DisplayAlert("Alert", "Gamertag Empty", "OK");
                }
                else if (PasswordEntry.Text == "")
                {
                    await DisplayAlert("Alert", "Password Empty", "OK");
                }
                else
                {
                    client.DefaultRequestHeaders.Add("gamertag", GamertagEntry.Text);
                    client.DefaultRequestHeaders.Add("password", PasswordEntry.Text);

                    string uri = "http://bearfoot.design:8080/api/player/login";
                    string responseBody = await client.GetStringAsync(uri);

                    if (responseBody.Equals("\"error\""))
                    {
                        PasswordEntry.Text = "";
                        await DisplayAlert("Alert", "Username/Password Incorrect\nCase Sensitive!", "OK");
                    }
                    else
                    {
                        Player player = JsonConvert.DeserializeObject<Player>(responseBody);
                        Application.Current.Properties["Gamertag"] = player.gamertag;
                        Application.Current.Properties["PlayerID"] = player.playerID;
                        await this.Navigation.PushAsync(new ProfilePage());
                        Navigation.RemovePage(this);
                    }
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught");
                Console.WriteLine("Message :{0} ", e.Message);
            }

            client.Dispose();
        }

        public async void CreateAccount()
        {
            await this.Navigation.PushAsync(new PlayerCreationPage());
        }
    }
}