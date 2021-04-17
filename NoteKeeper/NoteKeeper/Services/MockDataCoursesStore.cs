using NoteKeeper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteKeeper.Services
{
    public class MockDataCoursesStore : IObjectStore<Course>
    {
        readonly IList<Course> courses;

        public MockDataCoursesStore()
        {
            courses = new List<Course>()
            {
                new Course { Id = Guid.NewGuid().ToString(), Name = "Course 1" },
                new Course { Id = Guid.NewGuid().ToString(), Name = "Course 2" },
                new Course { Id = Guid.NewGuid().ToString(), Name = "Course 3" },
                new Course { Id = Guid.NewGuid().ToString(), Name = "Course 4" },
                new Course { Id = Guid.NewGuid().ToString(), Name = "Course 5" },
                new Course { Id = Guid.NewGuid().ToString(), Name = "Course 6" },
                new Course { Id = Guid.NewGuid().ToString(), Name = "Course 7" },
            };
        }

        public async Task<bool> AddObjectAsync(Course course)
        {
            courses.Add(course);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateObjectAsync(Course course)
        {
            var oldItem = courses.Where((Course arg) => arg.Id == course.Id).FirstOrDefault();
            courses.Remove(oldItem);
            courses.Add(course);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteObjectAsync(string id)
        {
            var oldItem = courses.Where((Course arg) => arg.Id == id).FirstOrDefault();
            courses.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Course> GetObjectAsync(string id)
        {
            return await Task.FromResult(courses.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Course>> GetObjectsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(courses);
        }
    }
}
