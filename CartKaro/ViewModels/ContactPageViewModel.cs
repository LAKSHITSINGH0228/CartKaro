using System.Collections.ObjectModel;
using System.Windows.Input;
using CartKaro.Models;
using CartKaro.Views;

namespace CartKaro.ViewModels
{
  public class ContactPageViewModel : PropertyChangedNotifier
  {
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
    public ICommand AddContactCommand { get; }
    public ICommand DeleteContactCommand { get; private set; }

    private string m_searchContact;
    public string SearchContact
    {
      get{ return m_searchContact; }
      set
      {
        m_searchContact = value;
        if (ContactDetails == null)
        {
          ContactDetails = new ObservableCollection<ContactPageModel>();
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

    ObservableCollection<ContactPageModel> contacts = ContactRepository.GetContacts();

    public ContactPageViewModel()
    {
      ContactDetails = contacts;
      AddContactCommand = new Command(AddContactAction);
      DeleteContactCommand = new Command(DeleteContactAction);
    }

    private void AddContactAction()
    {
      Shell.Current.GoToAsync(nameof(AddContactPage));
    }

    private void DeleteContactAction()
    {
      ContactRepository.DeleteContact(SelectedContact.ContactId);
    }
  }
}

