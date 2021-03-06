﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace bragginBowl
{
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();
            masterPage.listView.ItemSelected += OnItemSelected;

            if (Application.Current.Properties.ContainsKey("Gamertag"))
            {
                Detail = new NavigationPage(new ProfilePage());
            }
            else
            {
                Detail = new NavigationPage(new PlayerLoginPage());
            }
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterPageItem;
            if (item != null)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
                masterPage.listView.SelectedItem = null;
                IsPresented = false;
            }

        }
    }
}
