using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using AppStudio.DataProviders;
using AppStudio.DataProviders.Core;
using AppStudio.DataProviders.Flickr;
using AppStudio.Uwp.Actions;
using AppStudio.Uwp.Commands;
using AppStudio.Uwp;
using System.Linq;

using StartsHere.Navigation;
using StartsHere.ViewModels;

namespace StartsHere.Sections
{
    public class GaleriSection : Section<FlickrSchema>
    {
		private FlickrDataProvider _dataProvider;	

		public GaleriSection()
		{
			_dataProvider = new FlickrDataProvider();
		}

		public override async Task<IEnumerable<FlickrSchema>> GetDataAsync(SchemaBase connectedItem = null)
        {
            var config = new FlickrDataConfig
            {
                QueryType = FlickrQueryType.Tags,
                Query = @"cisco networking",
				OrderBy = "FeedUrl",
				OrderDirection = SortDirection.Ascending
            };
            return await _dataProvider.LoadDataAsync(config, MaxRecords);
        }

        public override async Task<IEnumerable<FlickrSchema>> GetNextPageAsync()
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

        public override ListPageConfig<FlickrSchema> ListPage
        {
            get 
            {
                return new ListPageConfig<FlickrSchema>
                {
                    Title = "Galeri",

                    Page = typeof(Pages.GaleriListPage),

                    LayoutBindings = (viewModel, item) =>
                    {
                        viewModel.Title = item.Title.ToSafeString();
                        viewModel.SubTitle = item.Summary.ToSafeString();
                        viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.ImageUrl.ToSafeString());
                    },
                    DetailNavigation = (item) =>
                    {
						return NavInfo.FromPage<Pages.GaleriDetailPage>(true);
                    }
                };
            }
        }

        public override DetailPageConfig<FlickrSchema> DetailPage
        {
            get
            {
                var bindings = new List<Action<ItemViewModel, FlickrSchema>>();
                bindings.Add((viewModel, item) =>
                {
                    viewModel.PageTitle = item.Title.ToSafeString();
                    viewModel.Title = item.Title.ToSafeString();
                    viewModel.Description = item.Summary.ToSafeString();
                    viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.ImageUrl.ToSafeString());
                    viewModel.Content = null;
					viewModel.Source = item.FeedUrl;
                });

                var actions = new List<ActionConfig<FlickrSchema>>
                {
                    ActionConfig<FlickrSchema>.Link("Detay", (item) => item.FeedUrl.ToSafeString()),
                };

                return new DetailPageConfig<FlickrSchema>
                {
                    Title = "Galeri",
                    LayoutBindings = bindings,
                    Actions = actions
                };
            }
        }
    }
}
