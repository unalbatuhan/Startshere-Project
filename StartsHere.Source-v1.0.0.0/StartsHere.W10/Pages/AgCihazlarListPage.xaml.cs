using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml;
using AppStudio.DataProviders.DynamicStorage;
using StartsHere.Sections;
using StartsHere.ViewModels;
using AppStudio.Uwp;

namespace StartsHere.Pages
{
    public sealed partial class AgCihazlarListPage : Page
    {
	    public ListViewModel ViewModel { get; set; }
        public AgCihazlarListPage()
        {
			ViewModel = ViewModelFactory.NewList(new AgCihazlarSection());

            this.InitializeComponent();
			commandBar.DataContext = ViewModel;
			NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
			ShellPage.Current.ShellControl.SelectItem("bb4d8c66-8a7f-4810-84bb-e5aa86465bdd");
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
