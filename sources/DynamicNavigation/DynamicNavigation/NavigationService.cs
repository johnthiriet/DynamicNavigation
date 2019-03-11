using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DynamicNavigation
{
    public class NavigationService
    {
        private static readonly NavigationService _instance = new NavigationService();

        protected NavigationService()
        {

        }

        public static NavigationService Instance => _instance;

        private Dictionary<string, List<string>> _map = new Dictionary<string, List<string>>();

        public void Clear()
        {
            _map = new Dictionary<string, List<string>>();
        }

        public void RegisterLink(string source, string destination)
        {
            if (_map.TryGetValue(source, out List<string> destinations))
            {
                destinations.Add(destination);
            }
            else
            {
                _map.Add(source, new List<string>() { destination });
            }
        }

        public void RemoveLink(string source, string destination)
        {
            if (_map.TryGetValue(source, out List<string> destinations))
            {
                destinations.Remove(destination);
            }
        }

        public async Task NavigateHomeAsync()
        {
            var navigationPage = (NavigationPage)App.Current.MainPage;
            await navigationPage.Navigation.PopToRootAsync();
        }

        public async Task NavigatePreviousAsync(string source)
        {
            var navigationPage = (NavigationPage)App.Current.MainPage;
            await navigationPage.Navigation.PopAsync();
        }

        public async Task NavigateNextAsync(string source)
        {
            if (_map.TryGetValue(source, out List<string> destinations))
            {
                if (destinations.Count == 1)
                {
                    var destination = destinations[0];
                    var navigationPage = (NavigationPage)App.Current.MainPage;
                    var destinationPageType = Type.GetType($"{GetType().Namespace}.{destination}");
                    var destinationPage = (Page)Activator.CreateInstance(destinationPageType);
                    await navigationPage.Navigation.PushAsync(destinationPage);
                }
            }
        }

        public async Task NavigateAsync(string source, string destination)
        {
            if (_map.TryGetValue(source, out List<string> destinations))
            {
                if (destinations.Contains(destination))
                {
                    var navigationPage = (NavigationPage)App.Current.MainPage;
                    var destinationPageType = Type.GetType($"{GetType().Namespace}.{destination}");
                    var destinationPage = (Page)Activator.CreateInstance(destinationPageType);
                    await navigationPage.Navigation.PushAsync(destinationPage);                    
                }
            }
        }
    }
}
