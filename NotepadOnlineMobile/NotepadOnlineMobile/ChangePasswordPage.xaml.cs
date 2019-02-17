﻿using System;

using Xamarin.Forms;

namespace NotepadOnlineMobile
{
    public partial class ChangePasswordPage : ContentPage
    {
        private string password;
        private string newPassword;
        private string confirmNewPassword;

        public string Password
        {
            get
            {
                return password ?? "";
            }
            set
            {
                password = value.Trim();
                OnPropertyChanged("password");
            }
        }

        public string NewPassword
        {
            get
            {
                return newPassword ?? "";
            }
            set
            {
                newPassword = value?.Trim();
                OnPropertyChanged("NewPassword");
            }
        }

        public string ConfirmNewPassword
        {
            get
            {
                return confirmNewPassword ?? "";
            }
            set
            {
                confirmNewPassword = value?.Trim();
                OnPropertyChanged("ConfirmNewPassword");
            }
        }

        public ChangePasswordPage()
		{
			InitializeComponent();
            BindingContext = this;
        }

        private async void Change_Clicked(object sender, EventArgs e)
        {
            if (NewPassword != ConfirmNewPassword)
            {
                await DisplayAlert("Error", "You should confirm new password by typing it to the needed box", "OK");
                return;
            }

            IsBusy = true;
            var result = await DataBase.Manager.ChangePasswordAsync(NewPassword);
            IsBusy = false;

            if (result != DataBase.ReturnCode.Success)
            {
                await DisplayAlert("Error", $"An error occurred during changing password: {result}", "OK");
                return;
            }

            Settings.Storage.Password = DataBase.Manager.Password;

            await DisplayAlert("Success", "Password changed successfuly", "OK");
        }
    }
}