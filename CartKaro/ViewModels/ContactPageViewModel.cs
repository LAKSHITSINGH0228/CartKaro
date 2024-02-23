using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CartKaro.Models;
using CartKaro.Views;

namespace CartKaro.ViewModels
{
  public class ContactPageViewModel : INotifyPropertyChanged
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
    public ICommand DeleteContactCommand { get; }

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

    public event PropertyChangedEventHandler PropertyChanged;

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

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}

