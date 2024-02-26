namespace CartKaro
{
  public static class ThemeManager
  {
    private const string ThemeKey = "theme";
    private const string PrevThemeKey = "previous-theme";
    private static readonly IDictionary<string, ResourceDictionary> _themeMap = new Dictionary<string, ResourceDictionary>
    {
      ["Default"] = new Resources.Themes.Default(),
      ["Dark"] = new Resources.Themes.Dark(),
      ["Light"] = new Resources.Themes.Light(),
    };

    private static string _selectedTheme = "Default";

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

    public static string Initialize()
    {
      var selectedTheme = Preferences.Default.Get<string>(ThemeKey, null);
      if (selectedTheme == null && Application.Current.RequestedTheme == AppTheme.Dark)
      {
        selectedTheme = "Dark";
      }
      SetTheme(selectedTheme ?? "Default");

      return selectedTheme;
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
        var prevTheme = Preferences.Default.Get(PrevThemeKey, "Default");
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