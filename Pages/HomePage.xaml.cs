using RealEstateApp.Models;
using RealEstateApp.Services;

namespace RealEstateApp.Pages;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
        LblUserName.Text = "Hi" + Preferences.Get("username", string.Empty);
		GetCategories();
        GetTrendingPropertiesAsync();
	}

    private async Task GetTrendingPropertiesAsync()
    {
       var properties = await ApiService.GetTrendingProperties();
        CvTopPicks.ItemsSource = properties;
    }

    private async void GetCategories()
	{
		var categories = await ApiService.GetCategories();
        CvCategories.ItemsSource = categories;

    }

    private async void CvCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
       var currentSelection = e.CurrentSelection.FirstOrDefault() as Categories;
        if (currentSelection != null) {
            await Navigation.PushAsync(new PropertyListPage(currentSelection.Id, currentSelection.Name));
            ((CollectionView)sender).SelectedItem = null;
        }

    }

    private async void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var currentSelection = e.CurrentSelection.FirstOrDefault() as Categories;
        if (currentSelection != null)
        {
            await Navigation.PushAsync(new PropertyListPage(currentSelection.Id, currentSelection.Name));
            ((CollectionView)sender).SelectedItem = null;
        }
    }

    private async void CvTopPicks_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var currentSelection = e.CurrentSelection.FirstOrDefault() as TrendingProperty;
        if (currentSelection != null)
        {
            await Navigation.PushModalAsync(new PropertyDetailPage(currentSelection.Id));
            ((CollectionView)sender).SelectedItem = null;
        }
    }

    private void Tap_Search_Tapped(object sender, TappedEventArgs e)
    {
        Navigation.PushModalAsync(new SearchPage());
    }
}