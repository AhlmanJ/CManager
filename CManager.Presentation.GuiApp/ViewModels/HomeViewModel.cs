using CommunityToolkit.Mvvm.ComponentModel;

namespace CManager.Presentation.GuiApp.ViewModels;

public partial class HomeViewModel : ObservableObject
{
    [ObservableProperty]
    private string _title = "HomePage";
}
