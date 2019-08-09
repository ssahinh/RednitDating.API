using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using RednitDating.Api.Helpers;
using RednitDating.Api.Models;

namespace RednitDating.Api.Data
{
    public interface IDatingRepository
    {
        void Add<T>(T entity) where T: class;
        void Delete<T>(T entity) where T: class;
        Task<bool> SaveAll();
        Task<PagedList<User>> GetUsers(UserParams userParams);
        Task<User> GetUser(int Id);
        Task<Photo> GetPhoto(int id);
        Task<Photo> GetProfile(int userId);
        Task<Like> GetLike(int userId, int recipientId);
    
    }
}