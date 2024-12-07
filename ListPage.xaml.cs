namespace Linca_David_Lab7;
using Linca_David_Lab7.Models;

public partial class ListPage : ContentPage
{
    public ListPage()
    {
        InitializeComponent();
    }

    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var slist = (ShopList)BindingContext;
        slist.Date = DateTime.UtcNow;
        await App.Database.SaveShopListAsync(slist);
        await Navigation.PopAsync();
    }

    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var slist = (ShopList)BindingContext;
        await App.Database.DeleteShopListAsync(slist);
        await Navigation.PopAsync();
    }

    async void OnChooseButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ProductPage((ShopList)
       this.BindingContext)
        {
            BindingContext = new Product()
        });
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var shopl = (ShopList)BindingContext;

        listView.ItemsSource = await App.Database.GetListProductsAsync(shopl.ID);
    }

    private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem != null)
        {
            deleteItemButton.IsVisible = true;
        }
        else
        {
            deleteItemButton.IsVisible = false;
        }
    }

    async void OnDeleteItemButtonClicked(object sender, EventArgs e)
    {
        var selectedProduct = listView.SelectedItem as ListProduct;
        if (selectedProduct != null)
        {
            await App.Database.DeleteListProductAsync(selectedProduct);
            listView.ItemsSource = await App.Database.GetListProductsAsync(((ShopList)BindingContext).ID);
            deleteItemButton.IsVisible = false;
        }
    }
}