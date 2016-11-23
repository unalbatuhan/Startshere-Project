using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml;
using AppStudio.DataProviders.Rss;
using StartsHere.Sections;
using StartsHere.ViewModels;
using AppStudio.Uwp;

namespace StartsHere.Pages
{
    public sealed partial class MicrosoftSubnettingListPage : Page
    {
	    public ListViewModel ViewModel { get; set; }
        public MicrosoftSubnettingListPage()
        {
			ViewModel = ViewModelFactory.NewList(new MicrosoftSubnettingSection());

            this.InitializeComponent();
			commandBar.DataContext = ViewModel;
			NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
			ShellPage.Current.ShellControl.SelectItem("bfee629c-561f-430f-b9c0-5fe87cc34f66");
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
