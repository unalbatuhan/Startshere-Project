using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using AppStudio.DataProviders;
using AppStudio.DataProviders.Core;
using AppStudio.DataProviders.DynamicStorage;
using AppStudio.Uwp;
using AppStudio.Uwp.Actions;
using AppStudio.Uwp.Commands;
using Windows.ApplicationModel.Appointments;
using System.Linq;
using Windows.Storage;

using StartsHere.Navigation;
using StartsHere.ViewModels;

namespace StartsHere.Sections
{
    public class TrendOlanlarSection : Section<TrendOlanlar1Schema>
    {
		private DynamicStorageDataProvider<TrendOlanlar1Schema> _dataProvider;		

		public TrendOlanlarSection()
		{
			_dataProvider = new DynamicStorageDataProvider<TrendOlanlar1Schema>();
		}

		public override async Task<IEnumerable<TrendOlanlar1Schema>> GetDataAsync(SchemaBase connectedItem = null)
        {
            var config = new DynamicStorageDataConfig
            {
                Url = new Uri("http://ds.winappstudio.com/api/data/collection?dataRowListId=feeffb39-b7ca-4592-ad8b-64130df683e4&appId=9c94979e-f93e-42aa-a362-eafba6987413"),
                AppId = "9c94979e-f93e-42aa-a362-eafba6987413",
                StoreId = ApplicationData.Current.LocalSettings.Values[LocalSettingNames.StoreId] as string,
                DeviceType = ApplicationData.Current.LocalSettings.Values[LocalSettingNames.DeviceType] as string,
            };
            return await _dataProvider.LoadDataAsync(config, MaxRecords);
        }

        public override async Task<IEnumerable<TrendOlanlar1Schema>> GetNextPageAsync()
        {
            return await _dataProvider.LoadMoreDataAsync();
        }

        public override bool HasMorePages
        {
            get
            {
                return _dataProvider.HasMoreItems;
            }
        }

        public override ListPageConfig<TrendOlanlar1Schema> ListPage
        {
            get 
            {
                return new ListPageConfig<TrendOlanlar1Schema>
                {
                    Title = "Trend Olanlar",

                    Page = typeof(Pages.TrendOlanlarListPage),

                    LayoutBindings = (viewModel, item) =>
                    {
                        viewModel.Title = item.Title.ToSafeString();
                        viewModel.SubTitle = item.Subtitle.ToSafeString();
                        viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.ImageUrl.ToSafeString());
                    },
                    DetailNavigation = (item) =>
                    {
						return NavInfo.FromPage<Pages.TrendOlanlarDetailPage>(true);
                    }
                };
            }
        }

        public override DetailPageConfig<TrendOlanlar1Schema> DetailPage
        {
            get
            {
                var bindings = new List<Action<ItemViewModel, TrendOlanlar1Schema>>();
                bindings.Add((viewModel, item) =>
                {
                    viewModel.PageTitle = "Detail";
                    viewModel.Title = item.Title.ToSafeString();
                    viewModel.Description = item.Description.ToSafeString();
                    viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.ImageUrl.ToSafeString());
                    viewModel.Content = null;
                });

                var actions = new List<ActionConfig<TrendOlanlar1Schema>>
                {
                };

                return new DetailPageConfig<TrendOlanlar1Schema>
                {
                    Title = "Trend Olanlar",
                    LayoutBindings = bindings,
                    Actions = actions
                };
            }
        }
    }
}
