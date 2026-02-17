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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookings()
        {
            return await _context.Bookings.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Booking>> CreateBooking(CreateBookingDTO dto)
        {
            var roomExists = await _context.Rooms.AnyAsync(r => r.Id == dto.RoomId);
            if (!roomExists) return NotFound("Ruangan tidak ditemukan!");

            if (dto.EndTime <= dto.BookingDate) return BadRequest("Waktu selesai harus lebih dari waktu mulai!");

            // Cek Bentrok
            var isConflict = await _context.Bookings.AnyAsync(b => 
                b.RoomId == dto.RoomId && 
                b.Status != "Rejected" && 
                b.BookingDate < dto.EndTime && 
                b.EndTime > dto.BookingDate);

            if (isConflict) return BadRequest("Gagal! Ruangan sudah dibooking pada jam tersebut.");

            var waktuSekarang = DateTime.Now.ToString("dd MMM yyyy HH:mm");

            var booking = new Booking
            {
                RoomId = dto.RoomId,
                BorrowerName = dto.BorrowerName,
                BookingDate = dto.BookingDate,
                EndTime = dto.EndTime,
                Status = "Pending",
                StatusHistory = $"[{waktuSekarang}] 📝 Pengajuan dibuat (Status: Pending)"
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return Ok(booking);
        }

        // UPDATE KESELURUHAN DATA (EDIT)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooking(int id, UpdateBookingDTO dto)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null) return NotFound("Data tidak ditemukan!");

            if (dto.EndTime <= dto.BookingDate) return BadRequest("Waktu selesai harus lebih dari waktu mulai!");

            var waktuSekarang = DateTime.Now.ToString("dd MMM yyyy HH:mm");
            
            booking.RoomId = dto.RoomId;
            booking.BorrowerName = dto.BorrowerName;
            booking.BookingDate = dto.BookingDate;
            booking.EndTime = dto.EndTime;
            booking.StatusHistory += $"\n[{waktuSekarang}] ✏️ Admin mengedit data peminjaman.";

            await _context.SaveChangesAsync();
            return Ok(booking);
        }

        // UPDATE STATUS SAJA (ACC/TOLAK)
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, UpdateBookingStatusDTO dto)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null) return NotFound("Data tidak ditemukan!");

            var waktuSekarang = DateTime.Now.ToString("dd MMM yyyy HH:mm");
            
            booking.Status = dto.Status;
            booking.StatusHistory += $"\n[{waktuSekarang}] 🔄 Status diubah menjadi: {dto.Status}";
            
            await _context.SaveChangesAsync();
            return Ok(new { message = $"Status diubah jadi {dto.Status}" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null) return NotFound();

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}