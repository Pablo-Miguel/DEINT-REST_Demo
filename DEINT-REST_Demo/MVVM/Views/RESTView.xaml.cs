using DEINT_REST_Demo.MVVM.ViewModels;

namespace DEINT_REST_Demo.MVVM.Views;

public partial class RESTView : ContentPage
{
	public RESTView()
	{
		InitializeComponent();
		BindingContext = new RESTViewModel();
	}
}