using CartKaro.ViewModels;

namespace CartKaro.Views;

public partial class SettingsPage : ContentPage
{
  public SettingsPage()
  {
    InitializeComponent();

    BindingContext = new SettingsPageViewModel();
  }
}
