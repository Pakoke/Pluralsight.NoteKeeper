using NoteKeeper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NoteKeeper.Services
{
    public class MockDataNoteStore : IObjectStore<Note>
    {
        readonly IList<Note> notes;


        public MockDataNoteStore()
        {
            IObjectStore<Course> ObjectCourseStore = DependencyService.Get<IObjectStore<Course>>();
            IEnumerable<Course> courselist = ObjectCourseStore.GetObjectsAsync().Result;
            notes = new List<Note>()
            {
                new Note { Id = Guid.NewGuid().ToString(), Text = "First item", Heading="This is an note heading description.",Course = courselist.ElementAt(0) },
                new Note { Id = Guid.NewGuid().ToString(), Text = "Second item", Heading="This is an note heading description.",Course = courselist.ElementAt(1) },
                new Note { Id = Guid.NewGuid().ToString(), Text = "Third item", Heading="This is an note heading description.",Course = courselist.ElementAt(2) },
                new Note { Id = Guid.NewGuid().ToString(), Text = "Fourth item", Heading="This is an note heading description.",Course = courselist.ElementAt(3) },
                new Note { Id = Guid.NewGuid().ToString(), Text = "Fifth item", Heading="This is an note heading description.",Course = courselist.ElementAt(4) },
                new Note { Id = Guid.NewGuid().ToString(), Text = "Sixth item", Heading="This is an note heading description.",Course = courselist.ElementAt(1) },
            };
        }

        public async Task<bool> AddObjectAsync(Note note)
        {
            notes.Add(note);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateObjectAsync(Note note)
        {
            var oldItem = notes.Where((Note arg) => arg.Id == note.Id).FirstOrDefault();
            notes.Remove(oldItem);
            notes.Add(note);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteObjectAsync(string id)
        {
            var oldItem = notes.Where((Note arg) => arg.Id == id).FirstOrDefault();
            notes.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Note> GetObjectAsync(string id)
        {
            return await Task.FromResult(notes.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Note>> GetObjectsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(notes);
        }
    }
}
