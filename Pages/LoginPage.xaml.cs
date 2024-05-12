using RealEstateApp.Services;

namespace RealEstateApp.Pages;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

    private async void BtnLogin_Clicked(object sender, EventArgs e)
    {
        ApiService apiService = new ApiService();
        var response = await apiService.LoginAsync(EntEmail.Text, EntPassword.Text);
        if(response)
        {
            Application.Current.MainPage = new HomePage();  

        }
        else 
        { 
            await DisplayAlert("","Oops something went wrong","Cancel");
        }
    }

    private async void TapJoinNow_Tapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new RegisterPage());    
    }
}