using System.ComponentModel.DataAnnotations;

namespace SistemPeminjamanAPI.DTOs
{
    // Formulir untuk Mahasiswa saat mau pinjam ruangan
    public class CreateBookingDTO
    {
        [Required]
        public int RoomId { get; set; }

        [Required]
        public string BorrowerName { get; set; } = string.Empty;

        [Required]
        public DateTime BookingDate { get; set; }
    }

    // Formulir untuk Admin saat mau Approve / Reject
    public class UpdateBookingStatusDTO
    {
        [Required]
        public string Status { get; set; } = string.Empty; // Nanti isinya: "Approved" atau "Rejected"
    }
}
