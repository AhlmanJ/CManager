/*

The program crashed because I had not registered my service and repository in "App.xaml.cs".
I did not understand why until I got help with troubleshooting chatGPT. So in this code I have got help with troubleshooting chatGPT. 
And also got help with how to include StreetAddress, ZipCode and City in my RelayCommand.

NOTE!! 
The WPF application differs from the console application when saving data!
The WPF application saves files to a "DATA folder" in: CManager.Presentation.GuiApp/bin/Debug/net10.0-windows/Data.
The Console application saves files to a "DATA folder" in: CManager.Presentation.ConsoleApp/bin/Debug/net10.0/Data.

*/

using CManager.Business.Services;
using CManager.Domain.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Text.RegularExpressions;
using System.Windows;

namespace CManager.Presentation.GuiApp.ViewModels;

public partial class CreateCustomerViewModel : ObservableObject
{
    private readonly ICustomerService _customerService;
    private readonly IServiceProvider _serviceProvider;

    public CreateCustomerViewModel(IServiceProvider serviceProvider, ICustomerService customerService)
    {
        _serviceProvider = serviceProvider;
        _customerService = customerService;
    }

    [ObservableProperty]
    private CustomerModel customerModel = new()
    {
        Address = new CustomerAddressModel()  // ----> I got help from chatGPT with this.
    };

    // I got help from chatGPT on how to include the "Customer Address model" in the input fields to be able to pass them in my CreateCustomer method.
    [RelayCommand]
    private void CreateCustomer()
    {
        
        Regex emailRegex = new Regex (@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
        
        if (string.IsNullOrEmpty(CustomerModel.FirstName))
        {
            MessageBox.Show("Please enter a First name.");
            return;
        }

        if (string.IsNullOrEmpty(CustomerModel.LastName))
        {
            MessageBox.Show("Please enter a Last name.");
            return;
        }

        if (!emailRegex.IsMatch(CustomerModel.Email))
        {
            MessageBox.Show("Not a valid Email address! Use: name@example.com");
            return;
        }

        if (string.IsNullOrEmpty(CustomerModel.PhoneNr))
        {
            MessageBox.Show("Please enter a valid Phone number.");
            return;
        }

        if (string.IsNullOrEmpty(CustomerModel.Address.StreetAddress))
        {
            MessageBox.Show("Please enter a Street address");
            return;
        }

        if (string.IsNullOrEmpty(CustomerModel.Address.ZipCode))
        {
            MessageBox.Show("Please enter a Zipcode.");
            return;
        }

        if (string.IsNullOrEmpty(CustomerModel.Address.City))
        {
            MessageBox.Show("Please enter a City");
            return;
        }

        var result = _customerService.CreateCustomer(

            CustomerModel.FirstName.ToLower().Trim(),
            CustomerModel.LastName.ToLower().Trim(),
            CustomerModel.Email.ToLower().Trim(),
            CustomerModel.PhoneNr.Trim(),
            CustomerModel.Address.StreetAddress.ToLower().Trim(),
            CustomerModel.Address.ZipCode.Trim(),
            CustomerModel.Address.City.ToLower().Trim()

        );

        if (result)
        {
            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<HomeViewModel>();
        }
    }

    [RelayCommand]
    private void Return()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<HomeViewModel>();
    }
}
