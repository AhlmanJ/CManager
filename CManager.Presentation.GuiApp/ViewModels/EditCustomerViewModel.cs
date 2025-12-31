using CManager.Business.Services;
using CManager.Domain.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace CManager.Presentation.GuiApp.ViewModels;

public partial class EditCustomerViewModel(IServiceProvider serviceProvider, ICustomerService customerService) : ObservableObject
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly ICustomerService _customerService = customerService;

    [ObservableProperty]
    private CustomerModel customer = new()
    {
        Address = new CustomerAddressModel()
    };

    [RelayCommand]
    private void SaveEdit()
    {
        var result = _customerService.UpdateCustomer(Customer);
        if(result)
        {
            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<DisplayAllCustomersViewModel>();
        }
    }

    [RelayCommand]
    private void Cancel()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<DisplayAllCustomersViewModel>();
    }

}
