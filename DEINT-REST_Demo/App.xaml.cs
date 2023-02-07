using DEINT_REST_Demo.MVVM.Views;

namespace DEINT_REST_Demo;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new RESTView();
	}
}
