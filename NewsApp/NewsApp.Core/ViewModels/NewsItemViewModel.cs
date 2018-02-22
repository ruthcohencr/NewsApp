using MvvmCross.Core.ViewModels;
using NewsApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.Core.ViewModels
{
    public class NewsItemViewModel : MvxViewModel
    {
        public string StoryUrl { get; set; }

        public void Init(string storyUrl)
        {
            StoryUrl = storyUrl;
        }

        public NewsItemViewModel()
        { }

        private MvxCommand _goBackCommand;
        public MvxCommand GoBackCommand
        {
            get
            {
                return _goBackCommand ?? (_goBackCommand = new MvxCommand(
                    () => ShowViewModel<NewsAppViewModel>()));
            }
        }
    }
}
