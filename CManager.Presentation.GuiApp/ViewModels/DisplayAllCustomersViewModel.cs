using CommunityToolkit.Mvvm.ComponentModel;

namespace CManager.Presentation.GuiApp.ViewModels;

public partial class DisplayAllCustomersViewModel : ObservableObject
{
    [ObservableProperty]
    public string _tilte = "Display All Customers";
}
