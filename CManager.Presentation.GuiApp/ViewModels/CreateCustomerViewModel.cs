using CommunityToolkit.Mvvm.ComponentModel;

namespace CManager.Presentation.GuiApp.ViewModels;

public partial class CreateCustomerViewModel : ObservableObject
{
    [ObservableProperty]
    public string _title = "Create Customer";
}
