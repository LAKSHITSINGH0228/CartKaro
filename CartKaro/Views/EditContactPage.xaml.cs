using CartKaro.ViewModels;

namespace CartKaro.Views;

public partial class EditContactPage : ContentPage
{
  public EditContactPage()
  {
    InitializeComponent();

    BindingContext = new EditContactPageViewModel();
  }
}
