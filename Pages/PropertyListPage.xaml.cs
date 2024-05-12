
using RealEstateApp.Models;
using RealEstateApp.Services;

namespace RealEstateApp.Pages;

public partial class PropertyListPage : ContentPage
{
	public PropertyListPage(int categoryId, string categoryName )
	{
        try
        {
            InitializeComponent();
            Title = categoryName;
            GetPropertiesListAsync(categoryId);
        }
        catch (Exception)
        {

            throw;
        }
	
	}

    private async void GetPropertiesListAsync(int categoryId)
    {
       var properties = await ApiService.GetPropertyByCategory(categoryId);
        CvProperties.ItemsSource = properties;
    }


    private async void CvProperties_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var currentSelection = e.CurrentSelection.FirstOrDefault() as PropertyByCategory;
        if (currentSelection != null)
        {
            await Navigation.PushModalAsync(new PropertyDetailPage(currentSelection.Id));
            ((CollectionView)sender).SelectedItem = null;
        }
    }
}