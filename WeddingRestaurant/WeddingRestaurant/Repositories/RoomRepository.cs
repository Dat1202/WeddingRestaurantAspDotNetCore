using WeddingRestaurant.Interfaces;
using WeddingRestaurant.Models;

namespace WeddingRestaurant.Repositories
{
    public class RoomRepository : Repository<Room>, IRoomRepository
    {
        private readonly ModelContext _context;

        public RoomRepository(ModelContext context) : base(context)
        {
            _context = context;
        }
    }
}
