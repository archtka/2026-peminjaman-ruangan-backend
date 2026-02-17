using System.ComponentModel.DataAnnotations;

namespace SistemPeminjamanAPI.Models
{
    public class Booking
    {
        public int Id { get; set; }
        [Required] public int RoomId { get; set; }
        [Required] public string BorrowerName { get; set; } = string.Empty;
        [Required] public DateTime BookingDate { get; set; }
        [Required] public DateTime EndTime { get; set; } 
        public string Status { get; set; } = "Pending"; 
        
        // FITUR BARU: Buku Harian Riwayat
        public string StatusHistory { get; set; } = string.Empty;
    }
}