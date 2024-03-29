﻿using System;
using System.Collections.ObjectModel;
using CartKaro.Models;

namespace CartKaro.ViewModels
{
  public class ThemeAppearancePageViewModel : PropertyChangedNotifier
  {
    private bool _darkCbChecked;
    private bool _lightCbChecked;

    public bool DarkCbChecked
    {
      get => _darkCbChecked;
      set
      {
        _darkCbChecked = value;
        if (value)
        {
          LightCbChecked = false;
          ThemeManager.SetTheme("Dark");
        }
        OnPropertyChanged();
      }
    }

    public bool LightCbChecked
    {
      get => _lightCbChecked;
      set
      {
        _lightCbChecked = value;
        if (value)
        {
          DarkCbChecked = false;
          ThemeManager.SetTheme("Light");
        }
        OnPropertyChanged();
      }
    }

    public ThemeAppearancePageViewModel()
    {
      switch (ThemeManager.SelectedTheme)
      {
        case "Dark":
          DarkCbChecked = true;
          break;
        case "Light":
          LightCbChecked = true;
          break;
        default:
          LightCbChecked = true;
          break;
      }
    }
  }
}

