using CommunityToolkit.Mvvm.ComponentModel;

namespace CManager.Presentation.GuiApp.ViewModels;

public partial class HomeViewModel : ObservableObject
{
    [ObservableProperty]
    public string _tilte = "Home Page";
}
