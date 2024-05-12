
using RealEstateApp.Models;
using RealEstateApp.Services;

namespace RealEstateApp.Pages;

public partial class PropertyDetailPage : ContentPage
{
    private string phoneNumber;
    private bool IsBookmarkEnabled;
    private int propertyId;
    private int bookmarkId;
    public PropertyDetailPage(int propertyId)
	{
		InitializeComponent();
		GetPropertyDetails(propertyId);
        this.propertyId = propertyId;
	}

    private async void GetPropertyDetails(int propertyId)
    {
       var property = await ApiService.GetPropertyByDetails(propertyId);
		lblPrice.Text = "$" + property.Price;
		lblDescription.Text = property.Address;
		lblDescription.Text = property.Detail;
		ImgProperty.Source = property.FullImageUrl;
        phoneNumber = property.Phone;

        if(property.Bookmark == null)
        {
            ImgbookmarkBtn.Source = "bookmark_empty_icon";
            IsBookmarkEnabled = false;
        }
        else
        {
            ImgbookmarkBtn.Source = property.Bookmark.Status ? "bookmark_fill_icon" : "bookmark_empty_icon";
            bookmarkId = property.Bookmark.Id;
            IsBookmarkEnabled = true;
        }
    }

    private void TapMessage_Tapped(object sender, TappedEventArgs e)
    {
        new SmsMessage("Hi! I am interest in your property", phoneNumber);
        Sms.ComposeAsync();
    }
    private void TapCall_Tapped(object sender, TappedEventArgs e)
    {
        PhoneDialer.Open(phoneNumber);
    }

    private void ImgbackBtn_Clicked(object sender, EventArgs e)
    {
        Navigation.PopModalAsync();
    }

    private async void ImgbookmarkBtn_Clicked(object sender, EventArgs e)
    {
        if(IsBookmarkEnabled == false)
        {
            var addBookmark = new AddBookMark()
            {
                User_Id = Preferences.Get("userid", 0),
                PropertyId = propertyId
            };

            var response = await ApiService.AddBookMark(addBookmark);
            if(response)
            {
                ImgbookmarkBtn.Source = "bookmark_fill_icon";
                IsBookmarkEnabled = true;
            }
            else
            {
                var responses = await ApiService.DeleteBookMark(bookmarkId);
                if (responses)
                {
                    ImgbackBtn.Source = "bookmark_empty_icon";
                    IsBookmarkEnabled = false;
                }
                
            }

        }
    } 
}