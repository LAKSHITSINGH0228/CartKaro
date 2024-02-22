using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CartKaro.Models;
using CartKaro.Views;

namespace CartKaro.ViewModels
{
  [QueryProperty(nameof(ContactId), "Id")]
  public class ContactControlPageViewModel
  {
    private ContactPageModel contact;
    private string m_errorInEmailValidator;

    public ICommand CancelContactCommand { get; }
    public ICommand SaveContactCommand { get; }

    public bool TextValidator { get; set; }
    public bool EmailValidator { get; set; }
    public bool EmailTextValidator { get; set; }

    public string ErrorInEmailValidator
    {
      get { return m_errorInEmailValidator; }
      set
      {
        m_errorInEmailValidator = value;
        OnPropertyChanged(nameof(ErrorInEmailValidator));
      }
    }

    private string m_entryName;
    private string m_entryEmail;
    private string m_entryPhone;
    private string m_entryAddress;

    public string EntryName
    {
      get { return m_entryName; }
      set
      {
        m_entryName = value;
        OnPropertyChanged(nameof(EntryName));
      }
    }
    public string EntryEmail
    {
      get { return m_entryEmail; }
      set
      {
        m_entryEmail = value;
        OnPropertyChanged(nameof(EntryEmail));
      }
    }
    public string EntryPhone
    {
      get { return m_entryPhone; }
      set
      {
        m_entryPhone = value;
        OnPropertyChanged(nameof(EntryPhone));
      }
    }
    public string EntryAddress
    {
      get { return m_entryAddress; }
      set
      {
        m_entryAddress = value;
        OnPropertyChanged(nameof(EntryAddress));
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;


    public string ContactId
    {
      set
      {
        contact = ContactRepository.GetContactById(int.Parse(value));
        if (contact != null)
        {
          EntryName = contact.Name;
          EntryEmail = contact.Email;
          EntryPhone = contact.Phone;
          EntryAddress = contact.Address;
        }
      }
    }

    public ContactControlPageViewModel()
    {
      CancelContactCommand = new Command(CancelContactAction);
      SaveContactCommand = new Command(SaveContactAction);
    }

    private void CancelContactAction()
    {
      //Shell.Current.GoToAsync("..");
      Shell.Current.GoToAsync($"//{nameof(ContactsPage)}");
    }

    private void SaveContactAction()
    {
      if (TextValidator)
      {
        Application.Current.MainPage.DisplayAlert("Error", "Name is required.", "OK");
        return;
      }

      if (EmailValidator && EmailTextValidator)
      {
        Application.Current.MainPage.DisplayAlert("Error", "Email is Required & Email Format is wrong.", "OK");
        return;
      }
      else if (EmailValidator)
      {
        Application.Current.MainPage.DisplayAlert("Error", "Email format is wrong.", "OK");
        return;
      }
      else if (EmailTextValidator)
      {
        Application.Current.MainPage.DisplayAlert("Error", "Email is Required.", "OK");
        return;
      }

      contact.Name = EntryName;
      contact.Email = EntryEmail;
      contact.Phone = EntryPhone;
      contact.Address = EntryAddress;

      ContactRepository.UpdateContact(contact.ContactId, contact);
      Shell.Current.GoToAsync("..");
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}

