using System;
using Xamarin.Forms;

namespace DynamicNavigation
{
    public class ColoredWizardPage : ContentPage
    {
        protected async void GoNextButton_Clicked(object sender, EventArgs e)
        {
            await NavigationService.Instance.NavigateNextAsync(GetType().Name);
        }

        protected async void GoPrevButton_Clicked(object sender, EventArgs e)
        {
            await NavigationService.Instance.NavigatePreviousAsync(GetType().Name);
        }

        protected async void GoHomeButton_Clicked(object sender, EventArgs e)
        {
            await NavigationService.Instance.NavigateHomeAsync();
        }
    }
}
