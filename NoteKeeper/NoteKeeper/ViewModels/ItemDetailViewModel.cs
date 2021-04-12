using NoteKeeper.Models;
using NoteKeeper.Views;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NoteKeeper.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private string itemId;
        private string text;
        private string description;

        public Command CancelItemCommand { get; }

        public Command SaveItemCommand { get; }

        public string Id { get; set; }

        public ItemDetailViewModel()
        {
            CancelItemCommand = new Command(CancelItem);

            SaveItemCommand = new Command(UpdateItem);

        }

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }

        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await DataStore.GetItemAsync(itemId);
                Id = item.Id;
                Text = item.Text;
                Description = item.Description;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        private async void CancelItem(object obj)
        {
            //await Shell.Current.GoToAsync($"//{nameof(ItemsPage)}");
            await Shell.Current.GoToAsync($"//{nameof(ItemsPage)}");
        }

        private async void UpdateItem(object obj)
        {
            try
            {
                var item = await DataStore.GetItemAsync(this.ItemId);
                item.Text = text;
                item.Description = description;
                await DataStore.UpdateItemAsync(item);
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Update Item");
            }
        }

    }
}
