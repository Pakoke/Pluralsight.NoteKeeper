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
        private Note note;
        private Course course;
        public IEnumerable<Course> CourseList { get; set; }

        public Course CourseSelected {
            get
            {
                return course;
            }
            set {
                //course = value;
                this.Note.Course = value;
                SetProperty(ref course, value);
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
            Title = "Note Edition";
            CancelItemCommand = new Command(CancelItem);

            SaveItemCommand = new Command(AddAndUpdateItem);
            this.PropertyChanged +=
                (_, __) => SaveItemCommand.ChangeCanExecute();

            InitializeCourseList();
            LoadItemId("");
            //this.course = new Course() {Name="",Id="" };
            //this.itemId = "";
            //Note = new Note() { Text = "This is a test text", Heading = "This is a heading test", Course = CourseList.ElementAt(1), Id = Guid.NewGuid().ToString() };
            //CourseSelected = Note.Course;
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(this.Note.Heading)
                && !String.IsNullOrWhiteSpace(this.Note.Text);
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
                if (!String.IsNullOrWhiteSpace(itemId))
                {

                    var note = await ObjectNoteStore.GetObjectAsync(itemId);
                    if(note != null)
                        this.Note = new Note() { Id = note.Id, Text = note.Text, Heading = note.Heading, Course = note.Course };
                    else
                        this.Note = new Note() { Text = "", Heading = "", Course = new Course() { Name = "" }, Id = Guid.NewGuid().ToString() };
                }
                else
                {
                    this.Note = new Note() { Text = "", Heading = "", Course = new Course() { Name = ""}, Id = Guid.NewGuid().ToString() };
                    
                }
                this.CourseSelected = this.Note.Course;
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

        private async void AddAndUpdateItem()
        {
            try
            {
                if (String.IsNullOrWhiteSpace(this.ItemId)) 
                {
                    var note = new Note()
                    {
                        Course = this.CourseSelected,
                        Heading = this.Note.Heading,
                        Text = this.Note.Text,
                        Id = Guid.NewGuid().ToString()
                    };

                    var result = await ObjectNoteStore.AddObjectAsync(note);
                    if (!result)
                        throw new Exception("The item was not saved");
                    this.ItemId = note.Id;
                }
                else
                {
                    var note = await ObjectNoteStore.GetObjectAsync(this.ItemId);
                    note.Text = this.note.Text;
                    note.Heading = this.note.Heading;
                    note.Course = this.course;
                    //item.Description = description;
                    await ObjectNoteStore.UpdateObjectAsync(note);
                }
                //MessagingCenter.Send<Note>(this.Note, "SaveNote");

                await Shell.Current.GoToAsync($"//{nameof(ItemsPage)}");
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Update Item");
            }
        }

    }
}
