using API;
using Dapper;
using MainModule.Common;
using MainModule.Common.Utils;
using MainModule.DataAccess;
using MainModule.DataModel;
using MainModule.Services;
using MainModule.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Prism.Events;
using System;
using System.Data.SQLite;
using System.IO;
using System.Text.Json.Nodes;
using System.Windows;
using UI.Common.Helpers;
using UI.Services;
using UI.Views;
using UI.Windows;
using Velopack;

namespace UI;

public partial class App : Application 
{
    public static IHost? AppHost { get; private set; }

    public App()
    {
        VelopackApp.Build().Run();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        ConfigureServices();
        await AppHost!.StartAsync();

        // Create the application base directory if it doesn't exist
        CreateAppDirectoryFolder();
        // Create the application settings directory if it doesn't exist
        CreateAppDirectoryFolder(Constants.ApplicationSettingsFolderName);
        // Create the application data directory if it doesn't exist
        CreateAppDirectoryFolder(Constants.ApplicationDataFolderName);

        try
        {
            ConfigureConnectionStrings();
            CreateDatabase(AppHost.Services.GetRequiredService<IConfigurationService>().GetConfiguration()["connection_string"]!);

            AppHost.Services.GetRequiredService<IUIConfigurationService>().CreateDefaultSettings(new());
            var startPoint = AppHost.Services.GetRequiredService<MainWindow>();
            startPoint.Show();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
            throw;
        }
        
        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await AppHost!.StopAsync();

        base.OnExit(e);
    }

    private void ConfigureConnectionStrings()
    {
        var settingsFile = File.ReadAllText(Constants.ProductionSettingsJsonFilePath);

        var settings = JObject.Parse(settingsFile);

        settings["connection_string"] = $"Data Source={Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), 
                                                                    Constants.ApplicationBaseFolderName,  
                                                                    Constants.ApplicationDataFolderName,
                                                                    "database.db")};Version=3; foreign keys = true";

        JsonFileUtils.SerializeJsonFile(settings, Constants.ProductionSettingsJsonFilePath);
    }

    private void CreateDatabase(string connectionString)
    {
        string createScript = File.ReadAllText(Constants.CreateDatabaseScriptPath);

        using var connection = new SQLiteConnection(connectionString);
        connection.Execute(createScript);
    }

    private void CreateAppDirectoryFolder(string folderPath = "")
    {
        var baseDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                                                                   Constants.ApplicationBaseFolderName);

        if (!Directory.Exists(Path.Combine(baseDirectory, folderPath)))
        {
            Directory.CreateDirectory(Path.Combine(baseDirectory, folderPath));
        }
    }

    private void ConfigureServices()
    {
        AppHost = Host.CreateDefaultBuilder()
            .ConfigureServices((hostContext, services) =>
            {
                //Hard Dependencies
                services.AddSingleton<IEventAggregator, EventAggregator>();
                services.AddSingleton<INavigationService, NavigationService>();
                services.AddTransient<INavigationHelper, MainNavigationHelper>();
                services.AddSingleton<IConfigurationService, ConfigurationService>();
                services.AddSingleton<IUIConfigurationService, UIConfigurationService>();
                services.AddSingleton<Func<Type, IViewModel>>
                    (provider => viewModelType => (IViewModel)provider.GetRequiredService(viewModelType));
                //Windows
                services.AddSingleton(provider => new MainWindow(provider.GetRequiredService<IUIConfigurationService>(),
                                                                 provider.GetRequiredService<IEventAggregator>())
                    {
                    DataContext = provider.GetRequiredService<INavigationHelper>()
                });
                services.AddTransient(provider => new SelectLanguageWindow(provider.GetRequiredService<IUIConfigurationService>()));
                services.AddTransient(provider => new AddTradeWindow(provider.GetRequiredService<IEventAggregator>())
                {
                    DataContext = provider.GetRequiredService<HomeViewModel>()
                });
                services.AddTransient(provider => new AddAccountWindow(provider.GetRequiredService<IEventAggregator>())
                {
                    DataContext = provider.GetRequiredService<AccountViewModel>()
                });
                services.AddTransient(provider => new EditTradeWindow
                {
                    DataContext = provider.GetRequiredService<HomeViewModel>()
                });
                services.AddTransient(provider => new EditStrategyWindow
                {
                    DataContext = provider.GetRequiredService<StrategyViewModel>()
                });
                services.AddTransient(provider => new TradeImageWindow
                {
                    DataContext = provider.GetRequiredService<TradeImageViewModel>()
                });
                services.AddTransient(provider => new TradeMistakesWindow
                {
                    DataContext = provider.GetRequiredService<HomeViewModel>()
                });
                services.AddTransient(provider => new TradeNotesWindow
                {
                    DataContext = provider.GetRequiredService<HomeViewModel>()
                });
                services.AddTransient(provider => new TradeCostsWindow
                {
                    DataContext = provider.GetRequiredService<HomeViewModel>()
                });
                services.AddTransient(provider => new PortfolioWindow
                {
                    DataContext = provider.GetRequiredService<SymbolViewModel>()
                });
                services.AddTransient(provider => new AddSymbolWindow(provider.GetRequiredService<IEventAggregator>())
                {
                    DataContext = provider.GetRequiredService<SymbolViewModel>()
                });
                services.AddTransient(provider => new AddStrategyWindow(provider.GetRequiredService<IEventAggregator>())
                {
                    DataContext = provider.GetRequiredService<StrategyViewModel>()
                });
                services.AddTransient(provider => new AddAnalysisNoteWindow
                {
                    DataContext = provider.GetRequiredService<AnalysisNoteViewModel>()
                });
                services.AddTransient(provider => new AnalysisNotesWindow
                {
                    DataContext = provider.GetRequiredService<AnalysisNoteViewModel>()
                });
                //Views
                services.AddTransient(provider => new StrategyView
                {
                    DataContext = provider.GetRequiredService<StrategyViewModel>()
                });
                services.AddTransient(provider => new AccountView
                {
                    DataContext = provider.GetRequiredService<AccountViewModel>()
                });
                services.AddTransient(provider => new HomeView
                {
                    DataContext = provider.GetRequiredService<HomeViewModel>()
                });
                //Data Access
                services.AddTransient<AccountAccess>();
                services.AddTransient<TradeAccess>();
                services.AddTransient<PerformanceAccess>();
                services.AddTransient<TradeImageAccess>();
                services.AddTransient<AnalysisNoteAccess>();
                services.AddTransient<SymbolAccess>();
                services.AddTransient<StrategyAccess>();
                //ViewModels
                services.AddSingleton(provider => new StrategyViewModel(provider.GetRequiredService<IEventAggregator>(),
                                                                        provider.GetRequiredService<StrategyAccess>(),
                                                                        provider.GetRequiredService<INavigationHelper>()));

                services.AddSingleton(provider => new SymbolViewModel(provider.GetRequiredService<SymbolAccess>(),
                                                                      provider.GetRequiredService<INavigationHelper>(),
                                                                      provider.GetRequiredService<IEventAggregator>()));

                services.AddSingleton(provider => new PerformanceViewModel(provider.GetRequiredService<PerformanceAccess>()));

                services.AddSingleton(provider => new AnalysisNoteViewModel(provider.GetRequiredService<IEventAggregator>(),
                                                                            provider.GetRequiredService<INavigationHelper>(),
                                                                            provider.GetRequiredService<StrategyViewModel>(),
                                                                            provider.GetRequiredService<AnalysisNoteAccess>()));

                services.AddSingleton(provider => new HomeViewModel(provider.GetRequiredService<AccountViewModel>(),
                                                                    provider.GetRequiredService<SymbolViewModel>(),
                                                                    provider.GetRequiredService<StrategyViewModel>(),
                                                                    provider.GetRequiredService<PerformanceViewModel>(),
                                                                    provider.GetRequiredService<TradeAccess>(),
                                                                    provider.GetRequiredService<IEventAggregator>(),
                                                                    provider.GetRequiredService<INavigationHelper>()));

                services.AddSingleton(provider => new TradeImageViewModel(provider.GetRequiredService<HomeViewModel>(),
                                                                          provider.GetRequiredService<TradeImageAccess>(),
                                                                          provider.GetRequiredService<INavigationHelper>()));

                services.AddSingleton(provider => new AccountViewModel(provider.GetRequiredService<AccountAccess>(),
                                                                       provider.GetRequiredService<PerformanceViewModel>(),
                                                                       provider.GetRequiredService<INavigationHelper>(),
                                                                       provider.GetRequiredService<IEventAggregator>()));
            }).Build();
    }
}
