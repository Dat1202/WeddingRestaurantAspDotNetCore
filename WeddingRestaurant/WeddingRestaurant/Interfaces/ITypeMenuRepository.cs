using WeddingRestaurant.Models;
using WeddingRestaurant.ViewModels;

namespace WeddingRestaurant.Interfaces
{
    public interface ITypeMenuRepository : IRepository<TypeMenu>
    {
        Task<bool> GetTypeMenuByName(string name);
    }
}
