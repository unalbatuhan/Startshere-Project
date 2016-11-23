using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using AppStudio.DataProviders;
using AppStudio.DataProviders.Core;
using AppStudio.DataProviders.LocalStorage;
using AppStudio.Uwp;
using Windows.ApplicationModel.Appointments;
using System.Linq;

using StartsHere.Navigation;
using StartsHere.ViewModels;

namespace StartsHere.Sections
{
    public class AglarSection : Section<Aglar1Schema>
    {
		private LocalStorageDataProvider<Aglar1Schema> _dataProvider;

		public AglarSection()
		{
			_dataProvider = new LocalStorageDataProvider<Aglar1Schema>();
		}

		public override async Task<IEnumerable<Aglar1Schema>> GetDataAsync(SchemaBase connectedItem = null)
        {
            var config = new LocalStorageDataConfig
            {
                FilePath = "/Assets/Data/Aglar.json",
            };
            return await _dataProvider.LoadDataAsync(config, MaxRecords);
        }

        public override async Task<IEnumerable<Aglar1Schema>> GetNextPageAsync()
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

        public override ListPageConfig<Aglar1Schema> ListPage
        {
            get 
            {
                return new ListPageConfig<Aglar1Schema>
                {
                    Title = "Ağlar",

                    Page = typeof(Pages.AglarListPage),

                    LayoutBindings = (viewModel, item) =>
                    {
                        viewModel.Title = item.Title.ToSafeString();
                        viewModel.SubTitle = item.Description.ToSafeString();
                        viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.ImageUrl.ToSafeString());
                    },
                    DetailNavigation = (item) =>
                    {
						return NavInfo.FromPage<Pages.AglarDetailPage>(true);
                    }
                };
            }
        }

        public override DetailPageConfig<Aglar1Schema> DetailPage
        {
            get
            {
                var bindings = new List<Action<ItemViewModel, Aglar1Schema>>();
                bindings.Add((viewModel, item) =>
                {
                    viewModel.PageTitle = "Detail";
                    viewModel.Title = item.Title.ToSafeString();
                    viewModel.Description = item.Description.ToSafeString();
                    viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.ImageUrl.ToSafeString());
                    viewModel.Content = null;
                });

                var actions = new List<ActionConfig<Aglar1Schema>>
                {
                };

                return new DetailPageConfig<Aglar1Schema>
                {
                    Title = "Ağlar",
                    LayoutBindings = bindings,
                    Actions = actions
                };
            }
        }
    }
}
