﻿using CartKaro.Views;

namespace CartKaro;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(ContactsPage), typeof(ContactsPage));
		Routing.RegisterRoute(nameof(EditContactPage), typeof(EditContactPage));
		Routing.RegisterRoute(nameof(AddContactPage), typeof(AddContactPage));
		Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
		Routing.RegisterRoute(nameof(ThemeAppearancePage), typeof(ThemeAppearancePage));
	}
}

