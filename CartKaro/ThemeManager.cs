using Themes = CartKaro.Resources.Themes;

namespace CartKaro
{
  public static class ThemeManager
  {
    private const string ThemeKey = "theme";
    private const string PrevThemeKey = "previous-theme";
    private static readonly IDictionary<string, ResourceDictionary> _themeMap = new Dictionary<string, ResourceDictionary>
    {
      [nameof(Themes.Default)] = new Themes.Default(),
      [nameof(Themes.Dark)] = new Themes.Dark(),
      [nameof(Themes.Light)] = new Themes.Light(),
    };

    public static string SelectedTheme { get; set; } = nameof(Themes.Default);

    static ThemeManager()
    {
       Application.Current.RequestedThemeChanged += Current_RequestedThemeChanged;
    }

    public static void Initilize()
    {
      var selectedTheme = Preferences.Default.Get<string>(ThemeKey, null);
      if (selectedTheme is null && Application.Current.PlatformAppTheme == AppTheme.Dark)
      {
        selectedTheme = nameof(Themes.Dark);
      }
      SetTheme(selectedTheme ?? nameof(Themes.Default));
    }

    private static void Current_RequestedThemeChanged(object sender, AppThemeChangedEventArgs e)
    {
      if (e.RequestedTheme == AppTheme.Dark)
      {
        if (SelectedTheme != nameof(Themes.Dark))
        {
          Preferences.Default.Set<string>(PrevThemeKey, SelectedTheme);
        }
        SetTheme(nameof(Themes.Dark));
      }
      else
      {
        var prevTheme = Preferences.Default.Get<string>(PrevThemeKey, nameof(Themes.Default));
        SetTheme(prevTheme);
      }  
    }

    public static string[] ThemesNames = _themeMap.Keys.ToArray();

    public static void SetTheme(string themeName)
    {
      if (SelectedTheme == themeName) return;

      var themeToBeApplied = _themeMap[themeName];

      Application.Current.Resources.MergedDictionaries.Clear();
      Application.Current.Resources.MergedDictionaries.Add(themeToBeApplied);

      SelectedTheme = themeName;

      Preferences.Default.Set<string>(ThemeKey, themeName);
    }
  }
}

