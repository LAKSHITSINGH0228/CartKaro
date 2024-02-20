using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CartKaro.Models;
using CartKaro.Views;

namespace CartKaro.ViewModels
{
  public class ContactPageViewModel : INotifyPropertyChanged
  {
    public ObservableCollection<ContactPageModel> ContactDetails { get; set; }

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
          Shell.Current.GoToAsync($"{nameof(EditContactPage)}?Id={SelectedContact.ContactId}");
        }
        OnPropertyChanged(nameof(SelectedContact));
      }
    }

    ObservableCollection<ContactPageModel> contacts = ContactRepository.GetContacts();

    public event PropertyChangedEventHandler PropertyChanged;

    public ContactPageViewModel()
    {
      ContactDetails = contacts;
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}

