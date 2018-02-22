using MvvmCross.Platform;
using MvvmCross.Platform.IoC;
using NewsApp.Core.Models;

namespace NewsApp.Core
{
    public class App : MvvmCross.Core.ViewModels.MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsTypes()
                .RegisterAsLazySingleton();

            Mvx.LazyConstructAndRegisterSingleton<Sections, Sections>();
            RegisterAppStart<ViewModels.NewsAppViewModel>();


        }
    }
}
