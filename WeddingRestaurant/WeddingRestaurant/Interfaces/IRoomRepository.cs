using WeddingRestaurant.Models;

namespace WeddingRestaurant.Interfaces
{
    public interface IRoomRepository: IRepository<Room>
    {
        Task<bool> GetRoomByName(string name); 

    }
}
