using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CartKaro.Models
{
  public class PropertyChangedNotifier : INotifyPropertyChanged
  {
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    // Overloaded OnPropertyChanged method to accept property name as a parameter
    protected void OnPropertyChanged<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
    {
      if (!EqualityComparer<T>.Default.Equals(field, value))
      {
        field = value;
        OnPropertyChanged(propertyName);
      }
    }
  }
}