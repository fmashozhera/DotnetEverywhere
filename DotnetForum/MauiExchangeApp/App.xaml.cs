namespace MauiExchangeApp;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		var window = new Window(new MainPage()) { Title = "Antigravity FX - Currency Converter" };

#if WINDOWS || MACCATALYST
		window.Width = 1000;
		window.Height = 800;
#endif

		return window;
	}
}
