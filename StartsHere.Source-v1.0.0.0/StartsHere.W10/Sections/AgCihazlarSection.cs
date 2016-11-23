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
    public class AgCihazlarSection : Section<AgCihazlar1Schema>
    {
		private DynamicStorageDataProvider<AgCihazlar1Schema> _dataProvider;		

		public AgCihazlarSection()
		{
			_dataProvider = new DynamicStorageDataProvider<AgCihazlar1Schema>();
		}

		public override async Task<IEnumerable<AgCihazlar1Schema>> GetDataAsync(SchemaBase connectedItem = null)
        {
            var config = new DynamicStorageDataConfig
            {
                Url = new Uri("http://ds.winappstudio.com/api/data/collection?dataRowListId=69d6b4d5-a418-4686-9789-e46f3af874d3&appId=9c94979e-f93e-42aa-a362-eafba6987413"),
                AppId = "9c94979e-f93e-42aa-a362-eafba6987413",
                StoreId = ApplicationData.Current.LocalSettings.Values[LocalSettingNames.StoreId] as string,
                DeviceType = ApplicationData.Current.LocalSettings.Values[LocalSettingNames.DeviceType] as string,
            };
            return await _dataProvider.LoadDataAsync(config, MaxRecords);
        }

        public override async Task<IEnumerable<AgCihazlar1Schema>> GetNextPageAsync()
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

        public override ListPageConfig<AgCihazlar1Schema> ListPage
        {
            get 
            {
                return new ListPageConfig<AgCihazlar1Schema>
                {
                    Title = "Ağ Cihazlar",

                    Page = typeof(Pages.AgCihazlarListPage),

                    LayoutBindings = (viewModel, item) =>
                    {
                        viewModel.Title = item.Title.ToSafeString();
                        viewModel.SubTitle = item.Subtitle.ToSafeString();
                        viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.Thumbnail.ToSafeString());
                    },
                    DetailNavigation = (item) =>
                    {
						return NavInfo.FromPage<Pages.AgCihazlarDetailPage>(true);
                    }
                };
            }
        }

        public override DetailPageConfig<AgCihazlar1Schema> DetailPage
        {
            get
            {
                var bindings = new List<Action<ItemViewModel, AgCihazlar1Schema>>();
                bindings.Add((viewModel, item) =>
                {
                    viewModel.PageTitle = "Detail";
                    viewModel.Title = item.Title.ToSafeString();
                    viewModel.Description = item.Description.ToSafeString();
                    viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.ImageUrl.ToSafeString());
                    viewModel.Content = null;
                });

                var actions = new List<ActionConfig<AgCihazlar1Schema>>
                {
                };

                return new DetailPageConfig<AgCihazlar1Schema>
                {
                    Title = "Ağ Cihazlar",
                    LayoutBindings = bindings,
                    Actions = actions
                };
            }
        }
    }
}
