using System.Net;

namespace APIInterfaces
{
    public interface IStarWarsAPIService<T>
    {
        public Task<(HttpStatusCode, List<T>?)> GetAll(string apiName);
        public Task<(HttpStatusCode, T?)> GetById(string apiName, int id);
        public Task<List<T>> GetByIds(string apiName, int[] ids);


    }
}
