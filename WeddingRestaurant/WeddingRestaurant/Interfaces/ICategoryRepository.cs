using WeddingRestaurant.Models;

namespace WeddingRestaurant.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<bool> GetCategoryByName(string name);
    }
}
