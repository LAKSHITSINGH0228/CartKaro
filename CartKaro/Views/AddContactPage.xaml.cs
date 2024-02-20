using CartKaro.ViewModels;

namespace CartKaro.Views;


public partial class AddContactPage : ContentPage
{
  public AddContactPage()
  {
    InitializeComponent();

    BindingContext = new AddContactPageViewModel();
  }
}
