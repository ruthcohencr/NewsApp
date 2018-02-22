using MvvmCross.Core.ViewModels;
using NewsApp.Core.Models;
using NewsApp.Core.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace NewsApp.Core.ViewModels
{

    public class NewsAppViewModel : MvxViewModel
    {
        private ILocalStorage _localStorage;
        event EventHandler SectionChanged;
        private NewsService _newsService;
        private Sections _sections;

        public NewsAppViewModel(NewsService newsService, ILocalStorage localStorage, Sections sections)
        {
            _newsService = newsService;
            //ref to class who has the list of names in dictionary
            _sections = sections;
            _localStorage = localStorage;
            SelectedSection = _sections.GetSectionNames()[0];
            SectionChanged += SectionUpdate;
        }

        private ObservableCollection<StoryHeader> _newsItems;
        public ObservableCollection<StoryHeader> NewsItems
        {
            get { return _newsItems; }
            set
            {
                _newsItems = value;
                RaisePropertyChanged();
            }
        }

        public async Task Init()
        {
            _savedSection = await _localStorage.Load();
            SelectedSection = LocalStorageSection();
            //SelectedSection = "Main";
            Debug.WriteLine("Waiting for LoadStories()...");
            await _newsService.LoadStories(_sections.GetSectionByKey(SelectedSection));
            Debug.WriteLine("LoadStories() loaded!");
            NewsItems = _newsService.NewsItems;
        }

        public string LocalStorageSection()
        {
            if (_savedSection != null) { return _savedSection; }
            else return _sections.GetSectionNames()[0];
        }


        Task onFormLoadTask = null; // track the task, can implement cancellation
        private void SectionUpdate(object sender, EventArgs e)
        {
            this.onFormLoadTask = OnSectionChanged(sender, e);
        }
        private async Task OnSectionChanged(object sender, EventArgs e)
        {
            await _newsService.LoadStories(_sections.GetSectionByKey(SelectedSection));
            NewsItems = _newsService.NewsItems;
            IsPaneOpen = false;
            _savedSection = SelectedSection;
            await _localStorage.Save(_savedSection);
        }

        private MvxCommand _hamburgerButtonClicked;

        private string _savedSection;

        private string _selectedSection;
        public string SelectedSection
        {
            get { return _selectedSection; }
            set
            {
                SetProperty(ref _selectedSection, value);
                //calling to event.
                SectionChanged?.Invoke(this, EventArgs.Empty);
            }
        }


        private MvxCommand<StoryHeader> _openNewsItem;
        public MvxCommand<StoryHeader> OpenNewsItem
        {
            get
            {
                return _openNewsItem ?? (_openNewsItem = new MvxCommand<StoryHeader>((storyHeader) =>
                {
                    ShowViewModel<NewsItemViewModel>(new { storyUrl = storyHeader.WebUrl });
                }));
            }
        }
        private StoryHeader _selectedStory;
        public StoryHeader SelectedStory
        {
            get { return _selectedStory; }
            set
            {
                SetProperty(ref _selectedStory, value);
            }
        }


        public List<String> SectionList
        {
            get { return _sections.GetSectionNames(); }
        }

        private bool _isPaneOpen = false;
        public bool IsPaneOpen
        {
            get { return _isPaneOpen; }
            set
            {
                _isPaneOpen = value;
                RaisePropertyChanged();
            }
        }

        public MvxCommand HamburgerButtonClicked
        {
            get
            {
                return _hamburgerButtonClicked ?? (_hamburgerButtonClicked = new MvxCommand(
                    () =>
                    {
                        IsPaneOpen = !IsPaneOpen;
                    }));
            }
        }
    }
}
