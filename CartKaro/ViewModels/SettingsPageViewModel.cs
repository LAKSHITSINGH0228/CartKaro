using System;
using System.Windows.Input;
using CartKaro.Models;
using CartKaro.Views;

namespace CartKaro.ViewModels
{
  public class SettingsPageViewModel : PropertyChangedNotifier
  {
    public ICommand ThemeChangeCommand { get; }
    private string _previousPageTitle;
    public string PreviousPageTitle
    {
      get => _previousPageTitle;
      set
      {
        _previousPageTitle = value;
        OnPropertyChanged();
      }
    }

    public SettingsPageViewModel()
    {
      PreviousPageTitle = "Contacts";
      ThemeChangeCommand = new Command(ThemeChangeAction);
    }

    private void ThemeChangeAction()
    {
      Shell.Current.Navigation.PushAsync(new ThemeAppearancePage());
    }
  }
}

