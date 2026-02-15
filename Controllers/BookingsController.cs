using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemPeminjamanAPI.Data;
using SistemPeminjamanAPI.DTOs;
using SistemPeminjamanAPI.Models;

namespace SistemPeminjamanAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BookingsController(AppDbContext context)
        {
            _context = context;
        }

        // 1. LIHAT SEMUA PEMINJAMAN + FITUR PENCARIAN & FILTER
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookings([FromQuery] string? search, [FromQuery] string? status)
        {
            // Ambil semua data dulu
            var query = _context.Bookings.AsQueryable();

            // Fitur Pencarian berdasarkan Nama Peminjam
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(b => b.BorrowerName.ToLower().Contains(search.ToLower()));
            }

            // Fitur Filter berdasarkan Status (Pending/Approved/Rejected)
            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(b => b.Status.ToLower() == status.ToLower());
            }

            return await query.ToListAsync();
        }

        // 2. LIHAT DETAIL 1 PEMINJAMAN (Berdasarkan ID)
        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null) return NotFound("Data peminjaman tidak ditemukan!");
            return booking;
        }

        // 3. MAHASISWA PINJAM RUANGAN (Create)
        [HttpPost]
        public async Task<ActionResult<Booking>> CreateBooking(CreateBookingDTO dto)
        {
            var roomExists = await _context.Rooms.AnyAsync(r => r.Id == dto.RoomId);
            if (!roomExists) return NotFound("Ruangan tidak ditemukan!");

            var booking = new Booking
            {
                RoomId = dto.RoomId,
                BorrowerName = dto.BorrowerName,
                BookingDate = dto.BookingDate,
                Status = "Pending"
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBooking), new { id = booking.Id }, booking);
        }

        // 4. EDIT DATA PEMINJAMAN (Ganti tanggal / ruangan)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooking(int id, UpdateBookingDTO dto)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null) return NotFound("Data peminjaman tidak ditemukan!");

            var roomExists = await _context.Rooms.AnyAsync(r => r.Id == dto.RoomId);
            if (!roomExists) return NotFound("Ruangan baru tidak ditemukan!");

            booking.RoomId = dto.RoomId;
            booking.BorrowerName = dto.BorrowerName;
            booking.BookingDate = dto.BookingDate;

            await _context.SaveChangesAsync();
            return Ok(new { message = "Data peminjaman berhasil diubah", booking });
        }

        // 5. ADMIN SETUJUI / TOLAK PEMINJAMAN (Ubah Status)
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, UpdateBookingStatusDTO dto)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null) return NotFound("Data peminjaman tidak ditemukan!");

            if (dto.Status != "Approved" && dto.Status != "Rejected" && dto.Status != "Pending")
            {
                return BadRequest("Status hanya boleh: Pending, Approved, atau Rejected");
            }

            booking.Status = dto.Status;
            await _context.SaveChangesAsync();
            return Ok(new { message = $"Status berhasil diubah menjadi {dto.Status}", booking });
        }

        // 6. HAPUS DATA PEMINJAMAN (Batal Pinjam / Dihapus Admin)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null) return NotFound("Data peminjaman tidak ditemukan!");

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Data peminjaman berhasil dihapus" });
        }
    }
}