
using RealEstateApp.Models;
using RealEstateApp.Services;

namespace RealEstateApp.Pages;

public partial class BookmarkPage : ContentPage
{
	public BookmarkPage()
	{
		InitializeComponent();
		GetpropertiesList();

	}

    private async void GetpropertiesList( )
    {
	var properties = await ApiService.GetBookMarkList();
		CvProperties.ItemsSource = properties;
    }
    private async void CvProperties_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var currentSelection = e.CurrentSelection.FirstOrDefault() as BookMarkList;
        if (currentSelection != null)
        {
            await Navigation.PushModalAsync(new PropertyDetailPage(currentSelection.PropertyId));
            ((CollectionView)sender).SelectedItem = null;
        }
    }
}