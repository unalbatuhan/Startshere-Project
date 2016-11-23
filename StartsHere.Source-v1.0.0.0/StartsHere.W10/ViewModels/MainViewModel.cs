using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Windows.Input;
using AppStudio.Uwp;
using AppStudio.Uwp.Actions;
using AppStudio.Uwp.Navigation;
using AppStudio.Uwp.Commands;
using AppStudio.DataProviders;

using AppStudio.DataProviders.Rss;
using AppStudio.DataProviders.Menu;
using AppStudio.DataProviders.LocalStorage;
using AppStudio.DataProviders.DynamicStorage;
using StartsHere.Sections;


namespace StartsHere.ViewModels
{
    public class MainViewModel : PageViewModelBase
    {
        public ListViewModel AgCihazlar { get; private set; }
        public ListViewModel UrunVeServis { get; private set; }
        public ListViewModel AgYazlm { get; private set; }
        public ListViewModel TrendOlanlar { get; private set; }
        public ListViewModel Aglar { get; private set; }
        public ListViewModel YeniGelismeler { get; private set; }
		public AdvertisingViewModel SectionAd { get; set; }

        public MainViewModel(int visibleItems) : base()
        {
            Title = "Starts Here";
			this.SectionAd = new AdvertisingViewModel();
            AgCihazlar = ViewModelFactory.NewList(new AgCihazlarSection(), visibleItems);
            UrunVeServis = ViewModelFactory.NewList(new UrunVeServisSection(), visibleItems);
            AgYazlm = ViewModelFactory.NewList(new AgYazlmSection(), visibleItems);
            TrendOlanlar = ViewModelFactory.NewList(new TrendOlanlarSection(), visibleItems);
            Aglar = ViewModelFactory.NewList(new AglarSection(), visibleItems);
            YeniGelismeler = ViewModelFactory.NewList(new YeniGelismelerSection(), visibleItems);

            if (GetViewModels().Any(vm => !vm.HasLocalData))
            {
                Actions.Add(new ActionInfo
                {
                    Command = RefreshCommand,
                    Style = ActionKnownStyles.Refresh,
                    Name = "RefreshButton",
                    ActionType = ActionType.Primary
                });
            }
        }

		#region Commands
		public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(async () =>
                {
                    var refreshDataTasks = GetViewModels()
                        .Where(vm => !vm.HasLocalData).Select(vm => vm.LoadDataAsync(true));

                    await Task.WhenAll(refreshDataTasks);
					LastUpdated = GetViewModels().OrderBy(vm => vm.LastUpdated, OrderType.Descending).FirstOrDefault()?.LastUpdated;
                    OnPropertyChanged("LastUpdated");
                });
            }
        }
		#endregion

        public async Task LoadDataAsync()
        {
            var loadDataTasks = GetViewModels().Select(vm => vm.LoadDataAsync());

            await Task.WhenAll(loadDataTasks);
			LastUpdated = GetViewModels().OrderBy(vm => vm.LastUpdated, OrderType.Descending).FirstOrDefault()?.LastUpdated;
            OnPropertyChanged("LastUpdated");
        }

        private IEnumerable<ListViewModel> GetViewModels()
        {
            yield return AgCihazlar;
            yield return UrunVeServis;
            yield return AgYazlm;
            yield return TrendOlanlar;
            yield return Aglar;
            yield return YeniGelismeler;
        }
    }
}
