using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using StartsHere.ViewModels;

namespace StartsHere.Pages
{
    public sealed partial class SearchPage : Page
    {
        public SearchPage()
        {
            ViewModel = new SearchViewModel();
            this.InitializeComponent();
        }
        public SearchViewModel ViewModel { get; private set; }
		protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            await ViewModel.SearchDataAsync(e.Parameter.ToString());
        }
    }    
}
