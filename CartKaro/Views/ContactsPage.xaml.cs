﻿using CartKaro.ViewModels;

namespace CartKaro.Views;

public partial class ContactsPage : ContentPage
{
  public ContactsPage()
  {
    InitializeComponent();
  }

  protected override void OnAppearing()
  {
    base.OnAppearing();

    BindingContext = new ContactPageViewModel();
  }

  private void listContacts_ItemTapped(System.Object sender, Microsoft.Maui.Controls.ItemTappedEventArgs e)
  {
    listContacts.SelectedItem = null;
  }
}
