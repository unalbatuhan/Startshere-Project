using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

using AppStudio.Uwp;
using AppStudio.Uwp.Controls;

using StartsHere.ViewModels;

namespace StartsHere.Pages
{
    public sealed partial class FavoritesPage : Page
    {
        public FavoritesPage()
        {
            this.ViewModel = new FavoritesViewModel();
            this.InitializeComponent();
        }
        public FavoritesViewModel ViewModel { get; private set; }
		protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
			base.OnNavigatedTo(e);
            await ViewModel.LoadDataAsync();
            ShellPage.Current.ShellControl.SelectItem("Favorites");			
        }
    }    
}
