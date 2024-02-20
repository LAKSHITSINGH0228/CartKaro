using CartKaro.Models;
using CartKaro.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace CartKaro.ViewModels
{
  [QueryProperty(nameof(ContactId),"Id")]
  public class EditContactPageViewModel : INotifyPropertyChanged
  {
    private ContactPageModel contact;

    public ICommand CancelContactCommand { get; }

    private string m_contactName;

    public event PropertyChangedEventHandler PropertyChanged;

    public string ContactName
    {
      get
      {
        return m_contactName;
      }
      set
      {
        m_contactName = value;
        OnPropertyChanged(nameof(ContactName));
      }
    }

    public string ContactId
    {
      set
      {
        contact = ContactRepository.GetContactById(int.Parse(value));
        ContactName = contact.Name;
      }
    }

    public EditContactPageViewModel()
    {
      CancelContactCommand = new Command(CancelContactAction);
    }

    private void CancelContactAction()
    {
      //Shell.Current.GoToAsync("..");
      Shell.Current.GoToAsync($"//{nameof(ContactsPage)}");
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}

