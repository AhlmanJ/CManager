
// NOTE!!! ChatGPT helped me with the code for the RelayCommand "DeleteCustomer"!!!


using CManager.Business.Services;
using CManager.Domain.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;

namespace CManager.Presentation.GuiApp.ViewModels;

public partial class DisplayAllCustomersViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ICustomerService _customerService;

    [ObservableProperty]
    private ObservableCollection<CustomerModel> _customers = [];

    public DisplayAllCustomersViewModel(IServiceProvider serviceProvider, ICustomerService customerService)
    {
        _serviceProvider = serviceProvider;
        _customerService = customerService;

        // Converts my list, which is of type Enumerable, to an ObservableCollection. This must be done in order to display the list in the WPF application.
        _customers = new ObservableCollection<CustomerModel>(_customerService.GetAllCustomers(out bool hasError));
    }

    [RelayCommand]
    private void Return()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<HomeViewModel>();
    }


    // -------------------- NOTE! I got help from chatGPT with this code/method! ----------------------------------

    [RelayCommand]

    // DeleteCustomer retrieves the "CustomerModel" object from the list.
    private void DeleteCustomer(CustomerModel customer) 
    {

        // Passes the "Email" parameter from the CustomerModel to the method in my "Service" and removes the object from the list.
        _customerService.DeleteCustomer(customer.Email);

        // Removes the "Customer" object from the ObservableCollection.
        Customers.Remove(customer);

    }

    [RelayCommand]
    private void DisplayInfoOneCustomer()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<DisplayInfoOneCustomerViewModel>();
    }
}

