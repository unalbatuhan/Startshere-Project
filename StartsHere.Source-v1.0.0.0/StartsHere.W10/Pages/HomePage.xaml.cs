using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml;
using AppStudio.Uwp;
using AppStudio.Uwp.Commands;
using AppStudio.Uwp.Navigation;

using StartsHere.ViewModels;
using Microsoft.WindowsAzure.MobileServices;
using System;
using Windows.UI.Popups;


namespace StartsHere.Pages
{
    public sealed partial class HomePage : Page
    {
        public HomePage()
        {
            ViewModel = new MainViewModel(12);
            InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Required;
            commandBar.DataContext = ViewModel;
            searchBox.SearchCommand = SearchCommand;
            this.SizeChanged += OnSizeChanged;
        }
        public MainViewModel ViewModel { get; set; }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            await Authenticate();
            await this.ViewModel.LoadDataAsync();
            ShellPage.Current.ShellControl.SetCommandBar(commandBar);
            ShellPage.Current.ShellControl.SelectItem("Home");
        }
        public async System.Threading.Tasks.Task Authenticate()
        {
            while (App.MobileService.CurrentUser == null)
            {
                string message;
                try
                {
                    await App.MobileService.LoginAsync(MobileServiceAuthenticationProvider.MicrosoftAccount);
                    message = string.Format("Kullanıcı Kimlik Dogrulaması - {0}", App.MobileService.CurrentUser.UserId);
                }
                catch (InvalidOperationException)
                {
                    message = "Giris Basarisiz oldu";
                }
                var diaolog = new MessageDialog(message, "Oturum Durumu");
                await diaolog.ShowAsync();
            }
        }

    }

    private void OnSizeChanged(object sender, SizeChangedEventArgs e)
    {
        searchBox.SearchWidth = e.NewSize.Width > 640 ? 230 : 190;
    }

    public ICommand SearchCommand
    {
        get
        {
            return new RelayCommand<string>(text =>
            {
                searchBox.Reset();
                ShellPage.Current.ShellControl.CloseLeftPane();
                NavigationService.NavigateToPage("SearchPage", text, true);
            },
            SearchViewModel.CanSearch);
        }
    }

}

