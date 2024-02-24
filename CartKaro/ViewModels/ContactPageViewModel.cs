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

    private string m_searchContactText;
    public string SearchContactText
    {
      get{ return m_searchContactText; }
      set
      {
        m_searchContactText = value;
        if (ContactDetails == null)
        {
          ContactDetails = new ObservableCollection<ContactPageModel>();
        }
        ContactDetails = ContactRepository.SearchContact(m_searchContactText);
        OnPropertyChanged(nameof(SearchContactText));
      }
    }

    private ContactPageModel m_selectedContactItem;
    public ContactPageModel SelectedContactItem
    {
      get
      {
        if (m_selectedContactItem == null)
        {
          m_selectedContactItem = new ContactPageModel();
        }
        return m_selectedContactItem;
      }
      set
      {
        m_selectedContactItem = value;
        if (m_selectedContactItem != null)
        {
          //Shell.Current.GoToAsync(nameof(EditContactPage));
          Shell.Current.GoToAsync($"{nameof(EditContactPage)}?Id={m_selectedContactItem.ContactId}");
        }
        OnPropertyChanged(nameof(SelectedContactItem));
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
      ContactRepository.DeleteContact(SelectedContactItem.ContactId);
    }
  }
}

