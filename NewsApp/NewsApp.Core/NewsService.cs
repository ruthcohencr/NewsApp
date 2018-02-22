using NewsApp.Core.Models;
using NewsApp.Core.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.Core
{
    //i would like to take the instance of httpService and call it.
    //i would like to save or add result
    public class NewsService : INotifyPropertyChanged
    {
        private ObservableCollection<StoryHeader> _newsItems;
        public ObservableCollection<StoryHeader> NewsItems
        {
            get { return _newsItems; }
            set
            {
                _newsItems = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        //-------------------------how is it that i dont make new and it's not throwing excaption
        public StoryHeader[] StoriesHeaders { get; set; }

        private HttpService _httpService;
        public NewsService(HttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task LoadStories(string sectionName = null)
        {
            var parameters = new Dictionary<string, string>
            {
                {Constants.API_KEY_PARAM, Constants.API_KEY },
                {Constants.SHOW_FIELDS_PARAM, "thumbnail" },
                //{Constants.API_KEY_PARAM, Constants.PAGE_SIZE_PARAM },
                //{Constants.API_KEY_PARAM, Constants.SHOW_FIELDS_PARAM },
            };

            var result = await _httpService.GetAsync<SearchResult>(Constants.BASE_API_URL + sectionName, parameters);
            StoriesHeaders = result.SearchResponse.StoryHeaders;
            //Prepare the observableCollection to work with in the view

            NewsItems = ConvertHeadersToObserverCollection();

        }


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<StoryHeader> ConvertHeadersToObserverCollection()
        {
            //ObservableCollection<StoryHeader> newHeaderCollection = new ObservableCollection<StoryHeader>();
             
            //foreach (var header in StoriesHeaders)
            //{
            //    newHeaderCollection.Add(header);
            //}

            //return newHeaderCollection;
            return new ObservableCollection<StoryHeader>(StoriesHeaders);
        }
    }
}
