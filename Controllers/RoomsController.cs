using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemPeminjamanAPI.Data;
using SistemPeminjamanAPI.Models;
using SistemPeminjamanAPI.DTOs;

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

        // 1. GET: Ambil semua data ruangan
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Room>>> GetRooms()
        {
            return await _context.Rooms.ToListAsync();
        }

        // 2. GET: Ambil satu ruangan berdasarkan ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> GetRoom(int id)
        {
            var room = await _context.Rooms.FindAsync(id);

            if (room == null)
            {
                return NotFound("Ruangan tidak ditemukan!");
            }

            return room;
        }

        // 3. POST: Tambah ruangan baru (Pakai DTO)
        [HttpPost]
        public async Task<ActionResult<Room>> PostRoom(CreateRoomDto roomDto)
        {
            var newRoom = new Room
            {
                Name = roomDto.Name,
                Description = roomDto.Description,
                Capacity = roomDto.Capacity,
                IsAvailable = true
            };

            _context.Rooms.Add(newRoom);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRoom", new { id = newRoom.Id }, newRoom);
        }

        // 4. PUT: Update data ruangan (Edit)
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoom(int id, CreateRoomDto roomDto)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound("Ruangan tidak ditemukan!");
            }

            room.Name = roomDto.Name;
            room.Description = roomDto.Description;
            room.Capacity = roomDto.Capacity;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // 5. DELETE: Hapus ruangan
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound("Ruangan tidak ditemukan!");
            }

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    } 
} 