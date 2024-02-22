using CartKaro.ViewModels;

namespace CartKaro.Views;


public partial class AddContactPage : ContentPage
{
  public AddContactPage()
  {
    InitializeComponent();

    BindingContext = new AddContactPageViewModel();
  }

    void ContactControlPage_OnSave(System.Object sender, System.EventArgs e)
    {
    }

    void ContactControlPage_OnError(System.Object sender, System.String e)
    {
    }
}
