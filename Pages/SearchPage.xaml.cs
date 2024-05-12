using RealEstateApp.Models;
using RealEstateApp.Services;

namespace RealEstateApp.Pages;

public partial class SearchPage : ContentPage
{
	public SearchPage()
	{
		InitializeComponent();
	}

    private void ImgBackBtn_Clicked(object sender, EventArgs e)
    {
        Navigation.PopModalAsync();
    }

    private async void sbProperty_TextChanged(object sender, TextChangedEventArgs e)
    {
        if(e.NewTextValue == null) return;
      var propertyResult = await ApiService.FindProperties(e.NewTextValue);
        CvSearch.ItemsSource = propertyResult;  
    }

    private async void CvSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var currentSelection = e.CurrentSelection.FirstOrDefault() as SearchProperty;
        if (currentSelection != null)
        {
            await Navigation.PushModalAsync(new PropertyDetailPage(currentSelection.Id));
            ((CollectionView)sender).SelectedItem = null;
        }
    }
}