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
    public class AgYazlmSection : Section<AgYazlm1Schema>
    {
		private DynamicStorageDataProvider<AgYazlm1Schema> _dataProvider;		

		public AgYazlmSection()
		{
			_dataProvider = new DynamicStorageDataProvider<AgYazlm1Schema>();
		}

		public override async Task<IEnumerable<AgYazlm1Schema>> GetDataAsync(SchemaBase connectedItem = null)
        {
            var config = new DynamicStorageDataConfig
            {
                Url = new Uri("http://ds.winappstudio.com/api/data/collection?dataRowListId=5260530a-0186-4813-95df-d7a5be96711c&appId=9c94979e-f93e-42aa-a362-eafba6987413"),
                AppId = "9c94979e-f93e-42aa-a362-eafba6987413",
                StoreId = ApplicationData.Current.LocalSettings.Values[LocalSettingNames.StoreId] as string,
                DeviceType = ApplicationData.Current.LocalSettings.Values[LocalSettingNames.DeviceType] as string,
            };
            return await _dataProvider.LoadDataAsync(config, MaxRecords);
        }

        public override async Task<IEnumerable<AgYazlm1Schema>> GetNextPageAsync()
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

        public override ListPageConfig<AgYazlm1Schema> ListPage
        {
            get 
            {
                return new ListPageConfig<AgYazlm1Schema>
                {
                    Title = "Ağ Yazılım",

                    Page = typeof(Pages.AgYazlmListPage),

                    LayoutBindings = (viewModel, item) =>
                    {
                        viewModel.Title = item.Title.ToSafeString();
                        viewModel.SubTitle = item.Subtitle.ToSafeString();
                        viewModel.Description = item.Description.ToSafeString();
                        viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.Thumbnail.ToSafeString());
                    },
                    DetailNavigation = (item) =>
                    {
						return NavInfo.FromPage<Pages.AgYazlmDetailPage>(true);
                    }
                };
            }
        }

        public override DetailPageConfig<AgYazlm1Schema> DetailPage
        {
            get
            {
                var bindings = new List<Action<ItemViewModel, AgYazlm1Schema>>();
                bindings.Add((viewModel, item) =>
                {
                    viewModel.PageTitle = "Detail";
                    viewModel.Title = item.Subtitle.ToSafeString();
                    viewModel.Description = item.Description.ToSafeString();
                    viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.ImageUrl.ToSafeString());
                    viewModel.Content = null;
                });

                var actions = new List<ActionConfig<AgYazlm1Schema>>
                {
                };

                return new DetailPageConfig<AgYazlm1Schema>
                {
                    Title = "Ağ Yazılım",
                    LayoutBindings = bindings,
                    Actions = actions
                };
            }
        }
    }
}
