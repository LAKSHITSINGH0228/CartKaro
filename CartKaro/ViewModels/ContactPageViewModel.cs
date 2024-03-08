using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using CartKaro.Models;
using CartKaro.Views;

namespace CartKaro.ViewModels
{
  public class ContactPageViewModel : PropertyChangedNotifier
  {
    public ICommand AddContactCommand { get; }
    public ICommand DeleteContactCommand { get; private set; }
    public ICommand ThemeChangeCommand { get; private set; }

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
      get{ return m_searchContact; }
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
      ContactDetails = contacts;
      AddContactCommand = new Command(AddContactAction);
      DeleteContactCommand = new Command<ContactPageModel>(DeleteContactAction);
      ThemeChangeCommand = new Command(ThemeChangeAction);
    }

    private void AddContactAction()
    {
      Shell.Current.GoToAsync(nameof(AddContactPage));
    }

    private void DeleteContactAction(ContactPageModel contact)
    {
      ContactRepository.DeleteContact(contact);
    }

    private async void ThemeChangeAction()
    {
      var setTheme = await Application.Current.MainPage.DisplayActionSheet("Choose Theme", "Cancel", null, ThemeManager.ThemeNames);
      if (!string.IsNullOrWhiteSpace(setTheme) && setTheme != "Cancel")
      {
        ThemeManager.SetTheme(setTheme);
      }
    }
  }
}

