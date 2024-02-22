using System;
namespace CartKaro.Models
{
  public class ContactPageModel
  {
    public int ContactId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public ContactPageModel()
    {
      
    }
  }
}

