using Linca_David_Lab7.Models;

namespace Linca_David_Lab7;

public partial class ListEntryPage : ContentPage
{
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        ListView.ItemsSource = await App.Database.GetShopListsAsync();
    }
    async void OnShopListAddedClicked(object sender, EventArgs e) { await Navigation.PushAsync(new ListPage { BindingContext = new ShopList() }); }
    async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem != null) { await Navigation.PushAsync(new ListPage { BindingContext = e.SelectedItem as ShopList }); }
    }
}