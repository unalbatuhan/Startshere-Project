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
    public class UrunVeServisSection : Section<UrunVeServis1Schema>
    {
		private DynamicStorageDataProvider<UrunVeServis1Schema> _dataProvider;		

		public UrunVeServisSection()
		{
			_dataProvider = new DynamicStorageDataProvider<UrunVeServis1Schema>();
		}

		public override async Task<IEnumerable<UrunVeServis1Schema>> GetDataAsync(SchemaBase connectedItem = null)
        {
            var config = new DynamicStorageDataConfig
            {
                Url = new Uri("http://ds.winappstudio.com/api/data/collection?dataRowListId=0954c60b-8d8e-4706-b5dc-3ba956d49b9f&appId=9c94979e-f93e-42aa-a362-eafba6987413"),
                AppId = "9c94979e-f93e-42aa-a362-eafba6987413",
                StoreId = ApplicationData.Current.LocalSettings.Values[LocalSettingNames.StoreId] as string,
                DeviceType = ApplicationData.Current.LocalSettings.Values[LocalSettingNames.DeviceType] as string,
            };
            return await _dataProvider.LoadDataAsync(config, MaxRecords);
        }

        public override async Task<IEnumerable<UrunVeServis1Schema>> GetNextPageAsync()
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

        public override ListPageConfig<UrunVeServis1Schema> ListPage
        {
            get 
            {
                return new ListPageConfig<UrunVeServis1Schema>
                {
                    Title = "Ürün ve Servis",

                    Page = typeof(Pages.UrunVeServisListPage),

                    LayoutBindings = (viewModel, item) =>
                    {
                        viewModel.Title = item.Title.ToSafeString();
                        viewModel.SubTitle = item.Subtitle.ToSafeString();
                        viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.ImageUrl.ToSafeString());
                    },
                    DetailNavigation = (item) =>
                    {
						return NavInfo.FromPage<Pages.UrunVeServisDetailPage>(true);
                    }
                };
            }
        }

        public override DetailPageConfig<UrunVeServis1Schema> DetailPage
        {
            get
            {
                var bindings = new List<Action<ItemViewModel, UrunVeServis1Schema>>();
                bindings.Add((viewModel, item) =>
                {
                    viewModel.PageTitle = "Detail";
                    viewModel.Title = item.Title.ToSafeString();
                    viewModel.Description = item.Description.ToSafeString();
                    viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.ImageUrl.ToSafeString());
                    viewModel.Content = null;
                });

                var actions = new List<ActionConfig<UrunVeServis1Schema>>
                {
                };

                return new DetailPageConfig<UrunVeServis1Schema>
                {
                    Title = "Ürün ve Servis",
                    LayoutBindings = bindings,
                    Actions = actions
                };
            }
        }
    }
}
