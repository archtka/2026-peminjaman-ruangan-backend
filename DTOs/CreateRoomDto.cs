using System.ComponentModel.DataAnnotations;

namespace SistemPeminjamanAPI.DTOs
{
    public class CreateRoomDto
    {
        [Required(ErrorMessage = "Nama ruangan wajib diisi!")]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Required]
        [Range(1, 1000, ErrorMessage = "Kapasitas harus antara 1 sampai 1000")]
        public int Capacity { get; set; }
    }
}