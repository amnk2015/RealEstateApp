using RealEstateApp.Services;

namespace RealEstateApp.Pages;
[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class RegisterPage : ContentPage
{
 
    public RegisterPage()
	{
		InitializeComponent();
	}

 

    private void Clear()
    {
        EntFullName.Text = "";
        EntEmail.Text = ""; 
        EntPassword.Text = ""; 
        EntPhone.Text = "";
    }

  

    private async void Button_Clicked(object sender, EventArgs e)
    {
        ApiService apiService = new ApiService();
        var response = await apiService.RegisterUserAsync(EntFullName.Text, EntEmail.Text, EntPassword.Text, EntPhone.Text);
        await Navigation.PushAsync(new LoginPage());

        if (response)
        {
            await DisplayAlert("", "Your account has been created", "Alright");
            Clear();
        }
        else
        {
            await DisplayAlert("", "Oops something went wrong", "Cancel");
        }
    }

    private async void TapLogin_Tapped(object sender, TappedEventArgs e)
    {  
        await Navigation.PushAsync(new LoginPage());
    }
}