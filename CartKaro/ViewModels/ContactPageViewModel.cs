using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using CartKaro.Models;
using CartKaro.Views;
using Microsoft.Maui.Handlers;

namespace CartKaro.ViewModels
{
  public class ContactPageViewModel : PropertyChangedNotifier
  {
    public ICommand AddContactCommand { get; }
    public ICommand DeleteContactCommand { get; private set; }
    public ICommand ThemeChangeCommand { get; private set; }
    public ICommand SettingsPageCommand { get; private set; }

    readonly ObservableCollection<ContactPageModel> contacts = ContactRepository.GetContacts();

    private ObservableCollection<ContactPageModel> m_contactDetails;
    public ObservableCollection<ContactPageModel> ContactDetails
    {
      get { return m_contactDetails; }
      set
      {
        m_contactDetails = value;
        OnPropertyChanged(nameof(ContactDetails));
      }
    }

    private string m_searchContact;
    public string SearchContact
    {
      get { return m_searchContact; }
      set
      {
        m_searchContact = value;
        if (ContactDetails == null)
        {
          ContactDetails = new ObservableCollection<ContactPageModel>(contacts);
        }
        ContactDetails = ContactRepository.SearchContact(m_searchContact);
        OnPropertyChanged(nameof(SearchContact));
      }
    }

    private ContactPageModel m_selectedContact;
    public ContactPageModel SelectedContact
    {
      get
      {
        if (m_selectedContact == null)
        {
          m_selectedContact = new ContactPageModel();
        }
        return m_selectedContact;
      }
      set
      {
        m_selectedContact = value;
        if (m_selectedContact != null)
        {
          //Shell.Current.GoToAsync(nameof(EditContactPage));
          Shell.Current.GoToAsync($"{nameof(EditContactPage)}?Id={m_selectedContact.ContactId}");
        }
        OnPropertyChanged(nameof(SelectedContact));
      }
    }

    public ContactPageViewModel()
    {
      ModifySearchBar();
      ContactDetails = contacts;
      AddContactCommand = new Command(AddContactAction);
      DeleteContactCommand = new Command<ContactPageModel>(DeleteContactAction);
      ThemeChangeCommand = new Command(ThemeChangeAction);
      SettingsPageCommand = new Command(async () => await SettingsPageAction());
    }

    private void AddContactAction()
    {
      Shell.Current.Navigation.PushAsync(new AddContactPage());
    }

    private void DeleteContactAction(ContactPageModel contact)
    {
      ContactRepository.DeleteContact(contact);
    }

    private async Task SettingsPageAction()
    {
      await Shell.Current.Navigation.PushAsync(new SettingsPage());
    }

    private async void ThemeChangeAction()
    {
      var setTheme = await Application.Current.MainPage.DisplayActionSheet("Choose Theme", "Cancel", null, ThemeManager.ThemeNames);
      if (!string.IsNullOrWhiteSpace(setTheme) && setTheme != "Cancel")
      {
        ThemeManager.SetTheme(setTheme);
      }
    }

    private void ModifySearchBar()
    {
      SearchBarHandler.Mapper.AppendToMapping("CustomSearchIconColor", (handler, view) =>
      {
#if ANDROID
        var context = handler.PlatformView.Context;
        var searchIconId = context!.Resources!.GetIdentifier("search_mag_icon", "id", context.PackageName);
        if (searchIconId != 0)
        {
          var searchIcon = handler.PlatformView.FindViewById<Android.Widget.ImageView>(searchIconId);
          searchIcon!.SetColorFilter(Android.Graphics.Color.Rgb(172, 157, 185), Android.Graphics.PorterDuff.Mode.Dst);
        }
#endif
      });
    }
  }
}

