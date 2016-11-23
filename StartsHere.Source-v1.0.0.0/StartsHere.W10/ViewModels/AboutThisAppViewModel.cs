using System;
using System.Windows.Input;

using Windows.ApplicationModel;
using Windows.UI.Xaml.Media.Imaging;

using AppStudio.Uwp;
using AppStudio.Uwp.Commands;

namespace StartsHere.ViewModels
{
    public class AboutThisAppViewModel : PageViewModelBase
    {
        public AboutThisAppViewModel()
        {
            this.AppName = "Starts Here";
            this.Title = "NavigationPaneAbout".StringResource();
            this.Publisher = "Batuhan UNAL";
            this.AppVersion = string.Format("{0}.{1}.{2}.{3}", Package.Current.Id.Version.Major, Package.Current.Id.Version.Minor, Package.Current.Id.Version.Build, Package.Current.Id.Version.Revision);
            this.AboutText = "Kullanıcıların oluşturmak istediği ya da kullandıkları topolojiden itibaren network'un sonlandırılmasına kadar olan süreçte dallanmadan tek bir platform üzerinden ilerleyebilmelerini sağlamak.";
            this.AppLogo = new BitmapImage(new Uri("ms-appx:///Assets/ApplicationLogo.png"));
            this.Privacy = "http://unalbatuhan.azurewebsites.net";
            this.WasLibs = "http://unalbatuhan.azurewebsites.net";
            this.WindowsAppStudioWeb = "https://unalbatuhan.azurewebsites.net/";
            this.NewtonsoftWeb = "http://www.newtonsoft.com/json";
        }

        public string AppName { get; set; }
        public string Publisher { get; set; }
        public string AppVersion { get; set; }
        public string AboutText { get; set; }
        public string Privacy { get; set; }
        public string WasLibs { get; set; }
        public string WindowsAppStudioWeb { get; set; }
        public string NewtonsoftWeb { get; set; }
        public BitmapImage AppLogo { get; set; }

        private bool _isMoreInfoVisible;
        public bool IsMoreInfoVisible
        {
            get { return _isMoreInfoVisible; }
            set { SetProperty(ref _isMoreInfoVisible, value); }
        }

        private ICommand _viewMoreInfoCommand;
        public ICommand ViewMoreInfoCommand
        {
            get
            {
                if (_viewMoreInfoCommand == null)
                {
                    _viewMoreInfoCommand = new RelayCommand(() => { IsMoreInfoVisible = !IsMoreInfoVisible; });
                }
                return _viewMoreInfoCommand;
            }
        }
    }
}

