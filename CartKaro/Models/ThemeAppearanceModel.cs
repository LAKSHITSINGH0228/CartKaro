using System;
using System.Collections.ObjectModel;

namespace CartKaro.Models
{
  public class ThemeAppearanceModel
  {
    public string SystemDefault { get; set; }
    public string Dark { get; set; }
    public string Light { get; set; }

    public ThemeAppearanceModel()
    {
    }
  }
}

