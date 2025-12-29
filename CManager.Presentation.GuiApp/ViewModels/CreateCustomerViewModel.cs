
/*

The program crashed because I had not registered my service and repository in "App.xaml.cs".
I did not understand why until I got help with troubleshooting chatGPT. So in this code I have got help with troubleshooting chatGPT. 
And also got help with how to include StreetAddress, ZipCode and City in my RelayCommand.

I got help from chatGPT on how to include the "Customer Address model" in the input fields to be able to pass them in my CreateCustomer method.

NOTE!! 
The WPF application differs from the console application when saving data!
The WPF application saves files to a "DATA folder" in: CManager.Presentation.GuiApp/bin/Debug/net10.0-windows/Data.
The Console application saves files to a "DATA folder" in: CManager.Presentation.ConsoleApp/bin/Debug/net10.0/Data.

*/

using CManager.Business.Services;
using CManager.Business.Validators;
using CManager.Domain.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;

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


    [RelayCommand]
    private void CreateCustomer()
    {
     
        var result = _customerService.CreateCustomer(

            CustomerModel.FirstName,
            CustomerModel.LastName,
            CustomerModel.Email,
            CustomerModel.PhoneNr,
            CustomerModel.Address.StreetAddress,
            CustomerModel.Address.ZipCode,
            CustomerModel.Address.City

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
