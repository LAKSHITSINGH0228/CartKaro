using System;
using System.Windows.Input;
using CartKaro.Views;

namespace CartKaro.ViewModels
{
  public class SettingsPageViewModel
  {
    public ICommand ThemeChangeCommand { get; }

    public SettingsPageViewModel()
    {
      ThemeChangeCommand = new Command(ThemeChangeAction);
    }

    private void ThemeChangeAction()
    {
      Shell.Current.GoToAsync(nameof(ThemeAppearancePage));
    }
  }
}

