using CartKaro.ViewModels;

namespace CartKaro.Views;

public partial class ContactsPage : ContentPage
{
  public ContactsPage()
  {
    InitializeComponent();

    BindingContext = new ContactPageViewModel();
  }

  void listContacts_ItemTapped(System.Object sender, Microsoft.Maui.Controls.ItemTappedEventArgs e)
  {
    listContacts.SelectedItem = null;
  }
}
