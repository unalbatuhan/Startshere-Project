using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml;
using AppStudio.DataProviders.Rss;
using StartsHere.Sections;
using StartsHere.ViewModels;
using AppStudio.Uwp;

namespace StartsHere.Pages
{
    public sealed partial class YeniGelismelerListPage : Page
    {
	    public ListViewModel ViewModel { get; set; }
        public YeniGelismelerListPage()
        {
			ViewModel = ViewModelFactory.NewList(new YeniGelismelerSection());

            this.InitializeComponent();
			commandBar.DataContext = ViewModel;
			NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
			ShellPage.Current.ShellControl.SelectItem("1b7a7e04-2896-4204-b75c-28d285176e44");
			ShellPage.Current.ShellControl.SetCommandBar(commandBar);
			if (e.NavigationMode == NavigationMode.New)
            {			
				await this.ViewModel.LoadDataAsync();
                this.ScrollToTop();
			}			
            base.OnNavigatedTo(e);
        }

    }
}
