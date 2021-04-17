using NoteKeeper.Models;
using NoteKeeper.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        private string heading;
        private IList<string> courselist;
        private int courseindex;
        private Note note;
        private Course course;
        public IEnumerable<Course> CourseList { get; set; }

        public Course CourseSelected {
            get
            {
                return course;
            }
            set {
                course = note.Course;
                SetProperty(ref course, value);
                Note.Course = course;
            }
        }

        public Note Note
        {
            get => note;
            set => SetProperty(ref note, value);
        }

        public Command CancelItemCommand { get; }

        public Command SaveItemCommand { get; }

        public string Id { get; set; }

        public ItemDetailViewModel()
        {
            CancelItemCommand = new Command(CancelItem);

            SaveItemCommand = new Command(UpdateItem, ValidateSave);
            this.PropertyChanged +=
                (_, __) => SaveItemCommand.ChangeCanExecute();
            InitializeCourseList();

            Note = new Note() { Text = "This is a test text", Heading = "This is a heading test", Course = CourseList.ElementAt(1), Id = Guid.NewGuid().ToString() };
            CourseSelected = Note.Course;
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

        public string Heading
        {
            get => heading;
            set => SetProperty(ref heading, value);
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(Note.Heading)
                && !String.IsNullOrWhiteSpace(Note.Text);
        }

        public int CourseIndex
        {
            get => courseindex;
            set => SetProperty(ref courseindex, value);
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

        async void InitializeCourseList()
        {
            CourseList = await ObjectCourseStore.GetObjectsAsync();
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
            //await Shell.Current.GoToAsync($"//{nameof(..)}");
            await Shell.Current.GoToAsync($"//{nameof(ItemsPage)}");
        }

        private async void UpdateItem()
        {
            try
            {
                var item = await DataStore.GetItemAsync(this.ItemId);
                item.Text = text;
                //item.Description = description;
                await DataStore.UpdateItemAsync(item);
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Update Item");
            }
        }

    }
}
