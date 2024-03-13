using CartKaro.Views;

namespace CartKaro;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(EditContactPage), typeof(EditContactPage));
	}
}

