using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml;
using AppStudio.DataProviders.LocalStorage;
using AppStudio.DataProviders.Menu;
using StartsHere.Sections;
using StartsHere.ViewModels;
using AppStudio.Uwp;

namespace StartsHere.Pages
{
    public sealed partial class YardmListPage : Page
    {
	    public ListViewModel ViewModel { get; set; }
        public YardmListPage()
        {
			ViewModel = ViewModelFactory.NewList(new YardmSection());

            this.InitializeComponent();
			commandBar.DataContext = ViewModel;
			NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
			ShellPage.Current.ShellControl.SelectItem("9e209af9-d237-46b9-afda-fcf9c12b9786");
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
