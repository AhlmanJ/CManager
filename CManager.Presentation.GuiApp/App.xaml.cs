using CManager.Presentation.GuiApp.ViewModels;
using CManager.Presentation.GuiApp.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace CManager.Presentation.GuiApp;


public partial class App : Application
{
    public IHost _host;

    public App()
    {
        _host = Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                /*
                Instantiates the "MainViewModel" and "MainWindow" with a singleton because it will always be there as long as the program runs and therefore only needs
                to be instantiated once when the program starts. 
                The main window is the application itself.
                */

                services.AddSingleton<MainViewModel>();
                services.AddSingleton<MainWindow>();

                services.AddTransient<HomeViewModel>();
                services.AddTransient<HomeView>();

                services.AddTransient<CreateCustomerViewModel>();
                services.AddTransient<CreateCustomerView>();

                services.AddTransient<DisplayAllCustomersViewModel>();
                services.AddTransient<DisplayAllCustomersView>();

                services.AddTransient<DisplayInfoOneCustomerViewModel>();
                services.AddTransient<DisplayInfoOneCustomerView>();
            })
            .Build();
    }

    // Installed the "Host" and "Dependency Injection" extensions to be able to change how the application starts.
    protected override void OnStartup(StartupEventArgs e)
    {
        var mainWindow = _host.Services.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }
}
