using CManager.Domain.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;


namespace CManager.Presentation.GuiApp.ViewModels;

public partial class DisplayInfoOneCustomerViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;

    [ObservableProperty]
    private CustomerModel customer = new();

    public DisplayInfoOneCustomerViewModel(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        
    }

    [RelayCommand]
    private void EditCustomer()
    {
        var editCustomerViewModel = _serviceProvider.GetRequiredService<EditCustomerViewModel>();
        editCustomerViewModel.Customer = Customer;

        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = editCustomerViewModel;
    }

    [RelayCommand]
    private void CancelButton()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<DisplayAllCustomersViewModel>();
    }
}
