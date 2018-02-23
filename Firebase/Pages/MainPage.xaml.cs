using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Firebase.Pages
{
    public partial class MainPage : ContentPage
    {
        public ViewModel.MainPageViewModel ViewModel => (BindingContext as ViewModel.MainPageViewModel);
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if(ViewModel.OnAppearingCommand.CanExecute(null))
            {
                ViewModel.OnAppearingCommand.Execute(null);
            }
        }
    }
}
