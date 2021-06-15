﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using people_errandd.ViewModels;
using people_errandd.Models;
using Xamarin.Essentials;

namespace people_errandd.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private bool allowTap = true;
        private readonly Login Login = new Login();
        private string deviceId;
        public LoginPage()
        {
            InitializeComponent();
            //隱藏navigationpage導航欄。
            NavigationPage.SetHasNavigationBar(this, false);
            Animation ani = new Animation();

            if (string.IsNullOrEmpty(Preferences.Get("uuid", string.Empty)))
            {
                Preferences.Set("uuid", Guid.NewGuid().ToString());
            }
            deviceId = Preferences.Get("uuid", "");
            
        }

        private async void LogInButton(object sender, EventArgs e)
        {
            try
            {
                if (allowTap)
                {
                    allowTap = false;
                    if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                    {
                        await DisplayAlert("Error", "No Intenet", "OK");
                        return;
                    }
                    if (await Login.ConfirmCompanyHash(company.Text.Trim()))
                    {
                        if (!await Login.ConfirmUUID(deviceId))
                        {
                            await Login.SetUUID();
                        }
                        if (string.IsNullOrEmpty(Preferences.Get("Login", string.Empty)))
                        {
                            Preferences.Set("HashAccount", await Login.GetHashAccount(deviceId));
                        }
                        Navigation.InsertPageBefore(new MainPage(), this);
                        await Navigation.PopAsync();
                    }
                    else
                    {
                        await DisplayAlert("錯誤", "輸入錯誤", "請重新輸入");
                    }
                }
            }
            finally
            {
                allowTap = true;
            }

        }
        private async void QuestionButton(object sender, EventArgs e)
        {
            try
            {
                if (allowTap)
                {
                    allowTap = false;
                    await DisplayAlert("", "有任何問題，請與我們聯繫", "確定");
                }
            }
            finally
            {
                allowTap = true;
            }
        }
        protected async override void OnAppearing()
        {

            base.OnAppearing();
            transition.Opacity = 0;
            await transition.FadeTo(1, 2500);
            await image.ScaleTo(1.5, 1000, Easing.CubicIn);
            await image.ScaleTo(1, 1000, Easing.CubicOut);

        }

    }
}