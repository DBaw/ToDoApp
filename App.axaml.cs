using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using System;
using ToDoApp.DB;
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

        private static void ConfigureServices(IServiceCollection services)
        {
            string jsonUsersPath = System.IO.Path.Combine(AppContext.BaseDirectory, "users.json");
            string jsonNotesPath = System.IO.Path.Combine(AppContext.BaseDirectory, "notes.json");

            AppDbContext dbContext = new();
            dbContext.Database.EnsureCreated();

            // Register Repositories and Messenger
            services.AddSingleton<IUserRepository>(provider => new DbUserRepository(dbContext));
            services.AddSingleton<INotesRepository>(provider => new DbNotesRepository(dbContext));
            services.AddSingleton<IMessenger, WeakReferenceMessenger>();
            
            // Register Views
            services.AddSingleton<MainWindow>();

            // Register ViewModels
            services.AddSingleton<HomePageViewModel>();
            services.AddSingleton(sp => new MainWindowViewModel(
                sp.GetRequiredService<IUserRepository>(),
                sp.GetRequiredService<INotesRepository>()
                ));
            
        }
    }
}