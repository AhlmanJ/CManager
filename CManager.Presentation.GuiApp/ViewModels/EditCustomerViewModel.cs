using CManager.Business.Services;
using CManager.Domain.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Text.RegularExpressions;
using System.Windows;

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

        Regex emailRegex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");

        if (string.IsNullOrEmpty(Customer.FirstName))
        {
            MessageBox.Show("Please enter a First name.");
            return;
        }

        if (string.IsNullOrEmpty(Customer.LastName))
        {
            MessageBox.Show("Please enter a Last name.");
            return;
        }

        if (!emailRegex.IsMatch(Customer.Email))
        {
            MessageBox.Show("Not a valid Email address! Use: name@example.com");
            return;
        }

        if (string.IsNullOrEmpty(Customer.PhoneNr))
        {
            MessageBox.Show("Please enter a valid Phonenumber.");
            return;
        }

        if (string.IsNullOrEmpty(Customer.Address.StreetAddress))
        {
            MessageBox.Show("Please enter a Street address");
            return;
        }

        if (string.IsNullOrEmpty(Customer.Address.ZipCode))
        {
            MessageBox.Show("Please enter a Zipcode.");
            return;
        }

        if (string.IsNullOrEmpty(Customer.Address.City))
        {
            MessageBox.Show("Please enter a City");
            return;
        }

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
