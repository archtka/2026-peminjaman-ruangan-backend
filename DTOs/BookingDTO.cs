using System.ComponentModel.DataAnnotations;

namespace SistemPeminjamanAPI.DTOs
{
    public class CreateBookingDTO
    {
        [Required] public int RoomId { get; set; }
        [Required] public string BorrowerName { get; set; } = string.Empty;
        [Required] public DateTime BookingDate { get; set; }
        [Required] public DateTime EndTime { get; set; } 
    }

    public class UpdateBookingStatusDTO
    {
        [Required] public string Status { get; set; } = string.Empty; 
    }

    public class UpdateBookingDTO
    {
        [Required] public int RoomId { get; set; }
        [Required] public string BorrowerName { get; set; } = string.Empty;
        [Required] public DateTime BookingDate { get; set; }
        [Required] public DateTime EndTime { get; set; } 
    }
}