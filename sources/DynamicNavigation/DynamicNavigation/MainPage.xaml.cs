using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DynamicNavigation
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private static readonly Random random = new Random((int)DateTime.UtcNow.Ticks);

        private async void RandomizeButton_Clicked(object sender, EventArgs e)
        {
            var navigationService = NavigationService.Instance;
            navigationService.Clear();

            var destinations = new List<string> { "GreenPage", "RedPage", "BluePage", "OrangePage" };

            int count = destinations.Count;

            var indexes = new List<int>(count);
            int maxValue = count;
            int minValue = 0;

            while (minValue < maxValue && indexes.Count < count)
            {
                int randomIndex = random.Next(minValue, maxValue);
                if (indexes.Contains(randomIndex))
                    continue;                

                if (randomIndex == minValue)
                    minValue++;
                if (randomIndex == maxValue - 1)
                    maxValue--;
                
                indexes.Add(randomIndex);
            }

            List<string> toto = new List<string>();
            string source = "MainPage";
            for (int i = 0; i < count; i++)
            {
                var destination = destinations[indexes[i]];
                navigationService.RegisterLink(source, destination);
                toto.Add(destination);
                source = destination;
            }

            listView.ItemsSource = toto;
        }

        private async void GoNextButton_Clicked(object sender, EventArgs e)
        {
            await NavigationService.Instance.NavigateNextAsync(GetType().Name);
        }
    }
}
