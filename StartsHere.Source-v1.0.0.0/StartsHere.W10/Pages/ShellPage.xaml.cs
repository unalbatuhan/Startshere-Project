using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Media.Imaging;

using AppStudio.Uwp;
using AppStudio.Uwp.Controls;
using AppStudio.Uwp.Navigation;

using StartsHere.Navigation;

namespace StartsHere.Pages
{
    public sealed partial class ShellPage : Page
    {
        public static ShellPage Current { get; private set; }

        public ShellControl ShellControl
        {
            get { return shell; }
        }

        public Frame AppFrame
        {
            get { return frame; }
        }

        public ShellPage()
        {
            InitializeComponent();

            this.DataContext = this;
            ShellPage.Current = this;

            this.SizeChanged += OnSizeChanged;
            if (SystemNavigationManager.GetForCurrentView() != null)
            {
                SystemNavigationManager.GetForCurrentView().BackRequested += ((sender, e) =>
                {
                    if (SupportFullScreen && ShellControl.IsFullScreen)
                    {
                        e.Handled = true;
                        ShellControl.ExitFullScreen();
                    }
                    else if (NavigationService.CanGoBack())
                    {
                        NavigationService.GoBack();
                        e.Handled = true;
                    }
                });
				
                NavigationService.Navigated += ((sender, e) =>
                {
                    if (NavigationService.CanGoBack())
                    {
                        SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
                    }
                    else
                    {
                        SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
                    }
                });
            }
        }

		public bool SupportFullScreen { get; set; }

		#region NavigationItems
        public ObservableCollection<NavigationItem> NavigationItems
        {
            get { return (ObservableCollection<NavigationItem>)GetValue(NavigationItemsProperty); }
            set { SetValue(NavigationItemsProperty, value); }
        }

        public static readonly DependencyProperty NavigationItemsProperty = DependencyProperty.Register("NavigationItems", typeof(ObservableCollection<NavigationItem>), typeof(ShellPage), new PropertyMetadata(new ObservableCollection<NavigationItem>()));
        #endregion

		protected override void OnNavigatedTo(NavigationEventArgs e)
        {
#if DEBUG
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size { Width = 320, Height = 500 });
#endif
            NavigationService.Initialize(typeof(ShellPage), AppFrame);
			NavigationService.NavigateToPage<HomePage>(e);

            InitializeNavigationItems();

            Bootstrap.Init();
        }		        
		
		#region Navigation
        private void InitializeNavigationItems()
        {
            NavigationItems.Add(AppNavigation.NodeFromAction(
				"Home",
                "Ana Sayfa",
                (ni) => NavigationService.NavigateToRoot(),
                AppNavigation.IconFromSymbol(Symbol.Home)));
            NavigationItems.Add(AppNavigation.NodeFromAction(
				"Favorites",
                "Favorilerim",                
                AppNavigation.ActionFromPage("FavoritesPage"),
				AppNavigation.IconFromGlyph("\ue113")));
            NavigationItems.Add(AppNavigation.NodeFromAction(
				"bb4d8c66-8a7f-4810-84bb-e5aa86465bdd",
                "Ağ Cihazlar",                
                AppNavigation.ActionFromPage("AgCihazlarListPage"),
				AppNavigation.IconFromGlyph("\ue178")));

            NavigationItems.Add(AppNavigation.NodeFromAction(
				"8a1bfc60-cdbb-416a-a7d9-3c9837139a74",
                "Ürün ve Servis",                
                AppNavigation.ActionFromPage("UrunVeServisListPage"),
				AppNavigation.IconFromGlyph("\ue13c")));

            NavigationItems.Add(AppNavigation.NodeFromAction(
				"007a0e05-279a-4604-81e7-0b8f9d0acc7f",
                "Ağ Yazılım",                
                AppNavigation.ActionFromPage("AgYazlmListPage"),
				AppNavigation.IconFromGlyph("\ue1d3")));

            NavigationItems.Add(AppNavigation.NodeFromAction(
				"e99729bd-3ab3-47a4-8b6f-7755d15e0f4d",
                "Trend Olanlar",                
                AppNavigation.ActionFromPage("TrendOlanlarListPage"),
				null, new Image() { Source = new BitmapImage(new Uri("ms-appx:///Assets/DataImages/laptops-1.png")) }));

            NavigationItems.Add(AppNavigation.NodeFromAction(
				"5f04feca-ec6c-4a13-bd4f-9b5a7daee5a1",
                "Ağlar",                
                AppNavigation.ActionFromPage("AglarListPage"),
				AppNavigation.IconFromGlyph("\ue167")));

            NavigationItems.Add(AppNavigation.NodeFromAction(
				"1b7a7e04-2896-4204-b75c-28d285176e44",
                "Yeni Gelişmeler",                
                AppNavigation.ActionFromPage("YeniGelismelerListPage"),
				AppNavigation.IconFromGlyph("\ue12a")));


            var menuKaynakcaItems = new List<NavigationItem>();

            menuKaynakcaItems.Add(AppNavigation.NodeFromAction(
				"bfee629c-561f-430f-b9c0-5fe87cc34f66",
                "Microsoft Subnetting",
				AppNavigation.ActionFromPage("MicrosoftSubnettingListPage"),
                AppNavigation.IconFromGlyph("\ue12a")));

            menuKaynakcaItems.Add(AppNavigation.NodeFromAction(
				"bfee629c-561f-430f-b9c0-5fe87cc34f66",
                "Cisco Subnetting",
				AppNavigation.ActionFromPage("CiscoSubnettingListPage"),
                AppNavigation.IconFromGlyph("\ue12a")));

            menuKaynakcaItems.Add(AppNavigation.NodeFromAction(
				"bfee629c-561f-430f-b9c0-5fe87cc34f66",
                "Ağ Teknolojileri",
				AppNavigation.ActionFromPage("AgTeknolojileriListPage"),
                AppNavigation.IconFromGlyph("\ue12a")));

            menuKaynakcaItems.Add(AppNavigation.NodeFromAction(
				"bfee629c-561f-430f-b9c0-5fe87cc34f66",
                "Bulut Bilişim",
				AppNavigation.ActionFromPage("BulutBilisimListPage"),
                AppNavigation.IconFromGlyph("\ue12a")));

            menuKaynakcaItems.Add(AppNavigation.NodeFromAction(
				"bfee629c-561f-430f-b9c0-5fe87cc34f66",
                "Infrastructure",
				AppNavigation.ActionFromPage("InfrastructureListPage"),
                AppNavigation.IconFromGlyph("\ue12a")));

            menuKaynakcaItems.Add(AppNavigation.NodeFromAction(
				"bfee629c-561f-430f-b9c0-5fe87cc34f66",
                "İşletim Sistemleri",
				AppNavigation.ActionFromPage("IsletimSistemleriListPage"),
                AppNavigation.IconFromGlyph("\ue12a")));

            NavigationItems.Add(AppNavigation.NodeFromSubitems(
				"bfee629c-561f-430f-b9c0-5fe87cc34f66",
                "Kaynakça",                
                menuKaynakcaItems,
				AppNavigation.IconFromGlyph("\ue126")));            


            var menuDigerItems = new List<NavigationItem>();

            menuDigerItems.Add(AppNavigation.NodeFromAction(
				"4fa75842-7088-488f-92e2-bce979ca377d",
                "Galeri",
				AppNavigation.ActionFromPage("GaleriListPage"),
                AppNavigation.IconFromGlyph("\ue114")));

            menuDigerItems.Add(AppNavigation.NodeFromAction(
				"4fa75842-7088-488f-92e2-bce979ca377d",
                "Videolar",
				AppNavigation.ActionFromPage("VideolarListPage"),
                AppNavigation.IconFromGlyph("\ue173")));

            NavigationItems.Add(AppNavigation.NodeFromSubitems(
				"4fa75842-7088-488f-92e2-bce979ca377d",
                "Diğer",                
                menuDigerItems,
				AppNavigation.IconFromGlyph("\ue109")));            


            var menuYardmItems = new List<NavigationItem>();

            menuYardmItems.Add(AppNavigation.NodeFromAction(
				"9e209af9-d237-46b9-afda-fcf9c12b9786",
                "Geliştirici",
				AppNavigation.ActionFromPage("GelistiriciListPage"),
                AppNavigation.IconFromGlyph("\ue185")));

            NavigationItems.Add(AppNavigation.NodeFromSubitems(
				"9e209af9-d237-46b9-afda-fcf9c12b9786",
                "Yardım",                
                menuYardmItems,
				AppNavigation.IconFromGlyph("\ue11b")));            

            NavigationItems.Add(NavigationItem.Separator);

            NavigationItems.Add(AppNavigation.NodeFromControl(
				"About",
                "NavigationPaneAbout".StringResource(),
                new AboutPage(),
                AppNavigation.IconFromImage(new Uri("ms-appx:///Assets/about.png"))));
        }        
        #endregion        

		private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateDisplayMode(e.NewSize.Width);
        }

        private void UpdateDisplayMode(double? width = null)
        {
            if (width == null)
            {
                width = Window.Current.Bounds.Width;
            }
            this.ShellControl.DisplayMode = width > 640 ? SplitViewDisplayMode.CompactOverlay : SplitViewDisplayMode.Overlay;
            this.ShellControl.CommandBarVerticalAlignment = width > 640 ? VerticalAlignment.Top : VerticalAlignment.Bottom;
        }

        private async void OnKeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.F11)
            {
                if (SupportFullScreen)
                {
                    await ShellControl.TryEnterFullScreenAsync();
                }
            }
            else if (e.Key == Windows.System.VirtualKey.Escape)
            {
                if (SupportFullScreen && ShellControl.IsFullScreen)
                {
                    ShellControl.ExitFullScreen();
                }
                else
                {
                    NavigationService.GoBack();
                }
            }
        }
    }
}
