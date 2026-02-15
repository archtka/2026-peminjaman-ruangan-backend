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

        // 1. LIHAT SEMUA PEMINJAMAN (Untuk Admin)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookings()
        {
            return await _context.Bookings.ToListAsync();
        }

        // 2. MAHASISWA PINJAM RUANGAN (Status otomatis "Pending")
        [HttpPost]
        public async Task<ActionResult<Booking>> CreateBooking(CreateBookingDTO dto)
        {
            // Cek apakah ruangan yang mau dipinjam itu beneran ada?
            var roomExists = await _context.Rooms.AnyAsync(r => r.Id == dto.RoomId);
            if (!roomExists) return NotFound("Ruangan tidak ditemukan!");

            var booking = new Booking
            {
                RoomId = dto.RoomId,
                BorrowerName = dto.BorrowerName,
                BookingDate = dto.BookingDate,
                Status = "Pending" // Default selalu nunggu persetujuan
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBookings), new { id = booking.Id }, booking);
        }

        // 3. ADMIN SETUJUI / TOLAK PEMINJAMAN (Approve/Reject)
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, UpdateBookingStatusDTO dto)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null) return NotFound("Data peminjaman tidak ditemukan!");

            // Validasi biar admin cuma bisa masukin tulisan yang bener
            if (dto.Status != "Approved" && dto.Status != "Rejected" && dto.Status != "Pending")
            {
                return BadRequest("Status hanya boleh: Pending, Approved, atau Rejected");
            }

            booking.Status = dto.Status;
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Status peminjaman berhasil diubah menjadi {dto.Status}", booking });
        }
    }
}