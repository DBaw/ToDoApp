using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using System;
using ToDoApp.Utilities.Repository;
using ToDoApp.ViewModels;
using ToDoApp.Views;

namespace ToDoApp
{
    public partial class App : Application
    {
        public IServiceProvider? ServiceProvider { get; private set; }

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            // Set up DI container
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                // Line below is needed to remove Avalonia data validation.
                // Without this line you will get duplicate validations from both Avalonia and CT
                BindingPlugins.DataValidators.RemoveAt(0);
                var vm = ServiceProvider.GetRequiredService<MainWindowViewModel>();
                desktop.MainWindow = new MainWindow
                {
                    DataContext = vm
                };
            }

            base.OnFrameworkInitializationCompleted();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            string jsonFilePath = System.IO.Path.Combine(AppContext.BaseDirectory, "users.json");
            
            // Register Repositories and Messenger
            services.AddSingleton<IUserRepository>(provider => new JsonUserRepository(jsonFilePath)); // or SqlUserRepository
            services.AddSingleton<IMessenger, WeakReferenceMessenger>();
            
            // Register Views
            services.AddSingleton<MainWindow>();

            // Register ViewModels
            services.AddSingleton<HomePageViewModel>();
            services.AddSingleton(sp => new MainWindowViewModel(
                sp.GetRequiredService<IUserRepository>()
                ));
            
        }
    }
}