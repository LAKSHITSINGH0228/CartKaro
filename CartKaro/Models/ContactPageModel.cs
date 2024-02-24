using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CartKaro.Models
{
  public class ContactPageModel : PropertyChangedNotifier
  {
    private string _name;
    private string _email;

    public int ContactId { get; set; }
    public string Name
    {
      get { return _name; }
      set { _name = value; OnPropertyChanged(nameof(Name)); }
    }
    public string Email
    {
      get { return _email; }
      set { _email = value; OnPropertyChanged(nameof(Email)); }
    }
    public string Phone { get; set; }
    public string Address { get; set; }
  }
}

