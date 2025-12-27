using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CManager.Presentation.GuiApp.ViewModels;

public partial class DisplayInfoOneCustomerViewModel : ObservableObject
{
    [ObservableProperty]
    public string _title = "Display One Customer, All Info";
}
