using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml;
using AppStudio.DataProviders.DynamicStorage;
using StartsHere.Sections;
using StartsHere.ViewModels;
using AppStudio.Uwp;

namespace StartsHere.Pages
{
    public sealed partial class AgYazlmListPage : Page
    {
	    public ListViewModel ViewModel { get; set; }
        public AgYazlmListPage()
        {
			ViewModel = ViewModelFactory.NewList(new AgYazlmSection());

            this.InitializeComponent();
			commandBar.DataContext = ViewModel;
			NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
			ShellPage.Current.ShellControl.SelectItem("007a0e05-279a-4604-81e7-0b8f9d0acc7f");
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
