using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemPeminjamanAPI.Data;
using SistemPeminjamanAPI.Models;

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

        // 1. LIHAT SEMUA RUANGAN
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Room>>> GetRooms()
        {
            return await _context.Rooms.ToListAsync();
        }

        // 2. LIHAT DETAIL 1 RUANGAN
        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> GetRoom(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null) return NotFound("Ruangan tidak ditemukan!");
            return room;
        }

        // 3. TAMBAH RUANGAN BARU (Ini yang tadi bikin Error 404!)
        [HttpPost]
        public async Task<ActionResult<Room>> CreateRoom(Room room)
        {
            // Kita paksa statusnya selalu tersedia saat baru dibuat
            // (Mengatasi error Schema Swagger yang minta IsAvailable: true)
            
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRoom), new { id = room.Id }, room);
        }

        // UPDATE RUANGAN (EDIT)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoom(int id, Room updatedRoom)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null) return NotFound("Ruangan tidak ditemukan!");

            room.Name = updatedRoom.Name;
            room.Capacity = updatedRoom.Capacity;
            room.Description = updatedRoom.Description;

            await _context.SaveChangesAsync();
            return Ok(room);
        }

        // 4. HAPUS RUANGAN
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null) return NotFound("Ruangan tidak ditemukan!");

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Ruangan berhasil dihapus" });
        }
    }
}