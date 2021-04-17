using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NoteKeeper.Services
{
    public interface IObjectStore<T>
    {
        Task<bool> AddObjectAsync(T obj);
        Task<bool> UpdateObjectAsync(T obj);
        Task<bool> DeleteObjectAsync(string id);
        Task<T> GetObjectAsync(string id);
        Task<IEnumerable<T>> GetObjectsAsync(bool forceRefresh = false);
    }
}
