using NoteKeeper.Models;
using NoteKeeper.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NoteKeeper.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private Note _selectedItem;

        public ObservableCollection<Note> Notes { get; }
        public Command LoadNotesCommand { get; }
        public Command AddNotesCommand { get; }
        public Command<Note> NotesTapped { get; }

        public ItemsViewModel()
        {
            Title = "Browse";
            Notes = new ObservableCollection<Note>();
            LoadNotesCommand = new Command(async () => await ExecuteLoadItemsCommand());

            NotesTapped = new Command<Note>(OnItemSelected);

            AddNotesCommand = new Command(OnAddItem);

            //MessagingCenter.Subscribe<ItemDetailPage, Note>(this, "SaveNote",
            //    async (sender, note) =>
            //    {
            //        Notes.Add(note);
            //        await ObjectNoteStore.AddObjectAsync(note);

            //    });
        }

        async Task ExecuteLoadItemsCommand()
        {

            IsBusy = true;

            try
            {
                Notes.Clear();
                var notes = await ObjectNoteStore.GetObjectsAsync(true);
                foreach (var note in notes)
                {
                    Notes.Add(note);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public Note SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={""}");
        }

        async void OnItemSelected(Note note)
        {
            if (note == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={note.Id}");
        }
    }
}