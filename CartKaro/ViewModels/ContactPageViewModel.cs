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
    public ObservableCollection<ContactPageModel> ContactDetails { get; set; }
    public ICommand AddContactCommand { get; }

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
    }

    private void AddContactAction()
    {
      Shell.Current.GoToAsync(nameof(AddContactPage));
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}

