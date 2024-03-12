using CartKaro.ViewModels;

namespace CartKaro.Views;

public partial class ThemeAppearancePage : ContentPage
{
  public ThemeAppearancePage()
  {
    InitializeComponent();

    BindingContext = new ThemeAppearancePageViewModel();
  }

}
