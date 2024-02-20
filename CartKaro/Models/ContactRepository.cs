using System;
using System.Collections.ObjectModel;

namespace CartKaro.Models
{
  public static class ContactRepository
  {
    public static ObservableCollection<ContactPageModel> _contacts = new ObservableCollection<ContactPageModel>()
    {
      new ContactPageModel{ ContactId = 0, Name = "Kanak", Email = "kanak@gmail.com"},
      new ContactPageModel{ ContactId = 1, Name = "Lakshit Singh", Email = "lakshit@gmail.com"},
      new ContactPageModel{ ContactId = 2, Name = "Sagar Kumar", Email = "sagar@gmail.com"},
      new ContactPageModel{ ContactId = 3, Name = "Kuldeep Gupta", Email = "kuldeep@gmail.com"}
    };

    public static ObservableCollection<ContactPageModel> GetContacts() => _contacts;

    public static ContactPageModel GetContactById(int contactId)
    {
      return _contacts.FirstOrDefault(x => x.ContactId == contactId);
    }
  }
}

