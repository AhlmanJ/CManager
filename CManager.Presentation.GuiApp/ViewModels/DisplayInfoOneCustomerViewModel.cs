using CManager.Business.Services;
using CManager.Domain.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CManager.Presentation.GuiApp.ViewModels;

public partial class DisplayInfoOneCustomerViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;

    [ObservableProperty]
    private CustomerModel _customer = new();

    public DisplayInfoOneCustomerViewModel(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        
    }

    [RelayCommand]
    private void CancelButton()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<DisplayAllCustomersViewModel>();
    }


}
