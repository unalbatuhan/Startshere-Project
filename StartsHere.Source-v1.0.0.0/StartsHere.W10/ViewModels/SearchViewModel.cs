using System;
using System.Collections.Generic;
using AppStudio.Uwp;
using AppStudio.Uwp.Commands;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using StartsHere.Sections;
namespace StartsHere.ViewModels
{
    public class SearchViewModel : PageViewModelBase
    {
        public SearchViewModel() : base()
        {
			Title = "Starts Here";
            AgCihazlar = ViewModelFactory.NewList(new AgCihazlarSection());
            UrunVeServis = ViewModelFactory.NewList(new UrunVeServisSection());
            AgYazlm = ViewModelFactory.NewList(new AgYazlmSection());
            TrendOlanlar = ViewModelFactory.NewList(new TrendOlanlarSection());
            Aglar = ViewModelFactory.NewList(new AglarSection());
            YeniGelismeler = ViewModelFactory.NewList(new YeniGelismelerSection());
            MicrosoftSubnetting = ViewModelFactory.NewList(new MicrosoftSubnettingSection());
            CiscoSubnetting = ViewModelFactory.NewList(new CiscoSubnettingSection());
            AgTeknolojileri = ViewModelFactory.NewList(new AgTeknolojileriSection());
            BulutBilisim = ViewModelFactory.NewList(new BulutBilisimSection());
            Infrastructure = ViewModelFactory.NewList(new InfrastructureSection());
            IsletimSistemleri = ViewModelFactory.NewList(new IsletimSistemleriSection());
            Galeri = ViewModelFactory.NewList(new GaleriSection());
            Videolar = ViewModelFactory.NewList(new VideolarSection());
            Gelistirici = ViewModelFactory.NewList(new GelistiriciSection());
                        
        }

        private string _searchText;
        private bool _hasItems = true;

        public string SearchText
        {
            get { return _searchText; }
            set { SetProperty(ref _searchText, value); }
        }

        public bool HasItems
        {
            get { return _hasItems; }
            set { SetProperty(ref _hasItems, value); }
        }

		public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand<string>(
                async (text) =>
                {
                    await SearchDataAsync(text);
                }, SearchViewModel.CanSearch);
            }
        }      
        public ListViewModel AgCihazlar { get; private set; }
        public ListViewModel UrunVeServis { get; private set; }
        public ListViewModel AgYazlm { get; private set; }
        public ListViewModel TrendOlanlar { get; private set; }
        public ListViewModel Aglar { get; private set; }
        public ListViewModel YeniGelismeler { get; private set; }
        public ListViewModel MicrosoftSubnetting { get; private set; }
        public ListViewModel CiscoSubnetting { get; private set; }
        public ListViewModel AgTeknolojileri { get; private set; }
        public ListViewModel BulutBilisim { get; private set; }
        public ListViewModel Infrastructure { get; private set; }
        public ListViewModel IsletimSistemleri { get; private set; }
        public ListViewModel Galeri { get; private set; }
        public ListViewModel Videolar { get; private set; }
        public ListViewModel Gelistirici { get; private set; }
        public async Task SearchDataAsync(string text)
        {
            this.HasItems = true;
            SearchText = text;
            var loadDataTasks = GetViewModels()
                                    .Select(vm => vm.SearchDataAsync(text));

            await Task.WhenAll(loadDataTasks);
			this.HasItems = GetViewModels().Any(vm => vm.HasItems);
        }

        private IEnumerable<ListViewModel> GetViewModels()
        {
            yield return AgCihazlar;
            yield return UrunVeServis;
            yield return AgYazlm;
            yield return TrendOlanlar;
            yield return Aglar;
            yield return YeniGelismeler;
            yield return MicrosoftSubnetting;
            yield return CiscoSubnetting;
            yield return AgTeknolojileri;
            yield return BulutBilisim;
            yield return Infrastructure;
            yield return IsletimSistemleri;
            yield return Galeri;
            yield return Videolar;
            yield return Gelistirici;
        }
		private void CleanItems()
        {
            foreach (var vm in GetViewModels())
            {
                vm.CleanItems();
            }
        }
		public static bool CanSearch(string text) { return !string.IsNullOrWhiteSpace(text) && text.Length >= 3; }
    }
}
