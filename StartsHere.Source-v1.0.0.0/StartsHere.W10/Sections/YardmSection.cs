using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using AppStudio.DataProviders;
using AppStudio.DataProviders.Core;
using AppStudio.DataProviders.LocalStorage;
using AppStudio.DataProviders.Menu;
using AppStudio.Uwp;
using System.Linq;

using StartsHere.Navigation;
using StartsHere.ViewModels;

namespace StartsHere.Sections
{
    public class YardmSection : Section<MenuSchema>
    {
		private LocalStorageDataProvider<MenuSchema> _dataProvider;

		public YardmSection()
		{
			_dataProvider = new LocalStorageDataProvider<MenuSchema>();
		}

		public override async Task<IEnumerable<MenuSchema>> GetDataAsync(SchemaBase connectedItem = null)
        {
            var config = new LocalStorageDataConfig
            {
                FilePath = "/Assets/Data/Yardm.json"
            };
            return await _dataProvider.LoadDataAsync(config, MaxRecords);
        }

        public override async Task<IEnumerable<MenuSchema>> GetNextPageAsync()
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

        public override bool NeedsNetwork
        {
            get
            {
                return false;
            }
        }

        public override ListPageConfig<MenuSchema> ListPage
        {
            get 
            {
                return new ListPageConfig<MenuSchema>
                {
                    Title = "Yardım",

                    Page = typeof(Pages.YardmListPage),

                    LayoutBindings = (viewModel, item) =>
                    {
                        viewModel.Title = item.Title;						
                        viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.Icon);
                    },
                    DetailNavigation = (item) =>
                    {
                        return NavInfo.FromMenu(item);
                    }
                };
            }
        }

        public override DetailPageConfig<MenuSchema> DetailPage
        {
            get { return null; }
        }
    }
}
