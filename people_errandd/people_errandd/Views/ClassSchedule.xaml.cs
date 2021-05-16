﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace people_errandd.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClassSchedule : TabbedPage
    {
        public ClassSchedule()
        {
            InitializeComponent();
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#88BBD6");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.Black;
        }
        private void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            Navigation.PopAsync();
        }

    }
}