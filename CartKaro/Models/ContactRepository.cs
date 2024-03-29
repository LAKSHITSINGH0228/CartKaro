﻿using System;
using System.Collections.ObjectModel;

namespace CartKaro.Models
{
  public static class ContactRepository
  {
    public static ObservableCollection<ContactPageModel> _contacts = new ObservableCollection<ContactPageModel>()
    {
      new ContactPageModel{ ContactId = 0, Name = "Kanak", Email = "kanak@gmail.com", Phone = "9862124433", Address = "Hathrus"},
      new ContactPageModel{ ContactId = 1, Name = "Lakshit Singh", Email = "lakshit@gmail.com", Phone = "7483920111", Address = "Delhi"},
      new ContactPageModel{ ContactId = 2, Name = "Sagar Kumar", Email = "sagar@gmail.com", Phone = "7930732390", Address = "Tatiri"},
      new ContactPageModel{ ContactId = 3, Name = "Kuldeep Gupta", Email = "kuldeep@gmail.com", Phone = "9870066356", Address = "Meerut"}
    };

    public static ObservableCollection<ContactPageModel> GetContacts() => _contacts;

    public static ContactPageModel GetContactById(int contactId)
    {
      var contact = _contacts.FirstOrDefault(x => x.ContactId == contactId);
      try
      {
        if (contact != null)
        {
          return new ContactPageModel
          {
            ContactId = contact.ContactId,
            Address = contact.Address,
            Name = contact.Name,
            Email = contact.Email,
            Phone = contact.Phone
          };
        }
      }
      catch (Exception ex)
      {
        Application.Current.MainPage.DisplayAlert("Error: GetContactById", ex.Message, "OK");
      }
      return null;
    }

    public static void UpdateContact(int contactId, ContactPageModel contact)
    {
      try
      {
        if (contactId != contact.ContactId)
        {
          return;
        }
        var contactToUpdate = _contacts.FirstOrDefault(x => x.ContactId == contactId);
        if (contactToUpdate != null)
        {
          contactToUpdate.Name = contact.Name;
          contactToUpdate.Email = contact.Email;
          contactToUpdate.Phone = contact.Phone;
          contactToUpdate.Address = contact.Address;
        }
      }
      catch (Exception ex)
      {
        Application.Current.MainPage.DisplayAlert("Error: UpdateContact", ex.Message, "OK");
      }
    }

    public static void AddContact(ContactPageModel contact)
    {
      try
      {
        var maxId = _contacts.Max(x => x.ContactId);
        contact.ContactId = maxId + 1;
        _contacts.Add(contact);
      }
      catch (Exception ex)
      {
        Application.Current.MainPage.DisplayAlert("Error: AddContact", ex.Message, "OK");
      }
    }

    public static void DeleteContact(ContactPageModel contact)
    {
      try
      {
        if (contact != null)
        {
          _contacts.Remove(contact);
        }
      }
      catch (Exception ex)
      {
        Application.Current.MainPage.DisplayAlert("Error: DeleteContact", ex.Message, "OK");
      }
    }

    public static ObservableCollection<ContactPageModel> SearchContact(string filterText)
    {
      if (string.IsNullOrWhiteSpace(filterText))
      {
        return new ObservableCollection<ContactPageModel>(_contacts);
      }
      try
      {
        var filteredContacts = _contacts.Where(x =>
            (!string.IsNullOrWhiteSpace(x.Name) && x.Name.StartsWith(filterText, StringComparison.OrdinalIgnoreCase)) ||
            (!string.IsNullOrWhiteSpace(x.Email) && x.Email.StartsWith(filterText, StringComparison.OrdinalIgnoreCase)) ||
            (!string.IsNullOrWhiteSpace(x.Phone) && x.Phone.StartsWith(filterText, StringComparison.OrdinalIgnoreCase)) ||
            (!string.IsNullOrWhiteSpace(x.Address) && x.Address.StartsWith(filterText, StringComparison.OrdinalIgnoreCase))).ToList();

        return new ObservableCollection<ContactPageModel>(filteredContacts);
      }
      catch (Exception ex)
      {
        Application.Current.MainPage.DisplayAlert("Error: SearchContact", ex.Message, "OK");
        return new ObservableCollection<ContactPageModel>(_contacts);
      }
    }
  }
}

