using Microsoft.EntityFrameworkCore;
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

        public async Task<bool> GetRoomByName(string name)
        {
            return await _context.Rooms.AnyAsync(e => e.Name.Equals(name));
        }
    }
}
