using System.ComponentModel.DataAnnotations;

namespace SistemPeminjamanAPI.Models
{
    public class Booking
    {
        public int Id { get; set; }

        [Required]
        public int RoomId { get; set; } // Ini nyambung ke ID Ruangan yang dipinjam

        [Required]
        public string BorrowerName { get; set; } = string.Empty; // Nama Mahasiswa/Dosen

        [Required]
        public DateTime BookingDate { get; set; } // Tanggal mau pakai ruangan

        // Status default selalu "Pending" saat pertama kali pinjam
        public string Status { get; set; } = "Pending"; 
    }
}