﻿using CartKaro.Models;

namespace CartKaro
{
  public static class ThemeManager
  {
    private const string ThemeKey = "theme";
    private const string PrevThemeKey = "previous-theme";
    private static readonly IDictionary<string, ResourceDictionary> _themeMap = new Dictionary<string, ResourceDictionary>
    {
      ["Light"] = new Resources.Themes.Light(),
      ["Dark"] = new Resources.Themes.Dark(),
      ["System default"] = new Resources.Themes.Default(),
    };

    private static string _selectedTheme = "Light";

    static ThemeManager()
    {
      Application.Current.RequestedThemeChanged += CurrentRequestedThemeChanged;
    }

    public static string SelectedTheme
    {
      get => _selectedTheme;
      set
      {
        if (_selectedTheme != value)
        {
          if (_themeMap.ContainsKey(value))
          {
            ApplyTheme(value);
            _selectedTheme = value;
          }
          else
          {
            throw new KeyNotFoundException($"Theme '{value}' not found in the theme map.");
          }
        }
      }
    }

    public static string[] ThemeNames => _themeMap.Keys.ToArray();

    public static void Initialize()
    {
      var selectedTheme = Preferences.Get(ThemeKey, null);
      if (selectedTheme == null && Application.Current.RequestedTheme == AppTheme.Dark)
      {
        selectedTheme = "Dark";
      }
      SetTheme(selectedTheme ?? "Light");
    }

    private static void CurrentRequestedThemeChanged(object sender, AppThemeChangedEventArgs e)
    {
      if (e.RequestedTheme == AppTheme.Dark)
      {
        if (SelectedTheme != "Dark")
        {
          Preferences.Default.Set(PrevThemeKey, SelectedTheme);
        }
        SetTheme("Dark");
      }
      else
      {
        var prevTheme = Preferences.Default.Get(PrevThemeKey, "Light");
        SetTheme(prevTheme);
      }
    }

    public static void SetTheme(string themeName)
    {
      SelectedTheme = themeName;
      Preferences.Default.Set(ThemeKey, themeName);
    }

    private static void ApplyTheme(string themeName)
    {
      var themeToBeApplied = _themeMap[themeName];
      Application.Current.Resources.MergedDictionaries.Clear();
      Application.Current.Resources.MergedDictionaries.Add(themeToBeApplied);
    }
  }
}