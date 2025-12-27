using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace CManager.Presentation.GuiApp.ViewModels;

public partial class HomeViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;

    public HomeViewModel(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    [ObservableProperty]
    private string _title = "Home Page";

    [RelayCommand]
    private void GoToCreateCustomer()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<CreateCustomerViewModel>();
    }

    [RelayCommand]
    private void GoToDisplayCustomers()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<DisplayAllCustomersViewModel>();
    }

   
}
