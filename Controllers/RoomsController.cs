using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemPeminjamanAPI.Data;
using SistemPeminjamanAPI.Models;
using SistemPeminjamanAPI.DTOs; // 👈 Kita panggil folder DTOs

namespace SistemPeminjamanAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RoomsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Rooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Room>>> GetRooms()
        {
            return await _context.Rooms.ToListAsync();
        }

        // GET: api/Rooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> GetRoom(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null) return NotFound();
            return room;
        }

        // POST: api/Rooms
        // 👇 PERUBAHAN DISINI: Pakai CreateRoomDto, bukan Room
        [HttpPost]
        public async Task<ActionResult<Room>> PostRoom(CreateRoomDto roomDto)
        {
            // 1. Pindahkan data dari DTO (Formulir) ke Entity (Barang Gudang)
            var newRoom = new Room
            {
                Name = roomDto.Name,
                Description = roomDto.Description,
                Capacity = roomDto.Capacity,
                IsAvailable = true // Default selalu tersedia
            };

            // 2. Simpan ke Database
            _context.Rooms.Add(newRoom);
            await _context.SaveChangesAsync();

            // 3. Kembalikan hasil
            return CreatedAtAction("GetRoom", new { id = newRoom.Id }, newRoom);
        }
    }
}