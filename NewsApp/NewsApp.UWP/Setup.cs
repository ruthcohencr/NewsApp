using Windows.UI.Xaml.Controls;
using MvvmCross.Core.ViewModels;
using MvvmCross.Uwp.Platform;
using NewsApp.Core;
using MvvmCross.Platform;

namespace NewsApp.UWP
{
    public class Setup : MvxWindowsSetup
    {
        public Setup(Frame rootFrame) : base(rootFrame)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            Mvx.LazyConstructAndRegisterSingleton<ILocalStorage, LocalStorage>();
            return new Core.App();
        }
    }
}
