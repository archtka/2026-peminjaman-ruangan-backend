# Changelog

Semua perubahan penting pada proyek ini akan didokumentasikan dalam file ini.

## [v1.0.0] - 2026-02-11

### 🚀 Fitur Baru (Features)
- Menambahkan **Modul Ruangan (Rooms)** lengkap dengan operasi CRUD.
- **POST**: Menambahkan ruangan baru dengan validasi input (Nama wajib, Kapasitas 1-1000).
- **GET**: Menampilkan daftar seluruh ruangan dan detail per ruangan.
- **PUT**: Mengupdate informasi ruangan (Nama, Deskripsi, Kapasitas).
- **DELETE**: Menghapus data ruangan dari database.
- Menambahkan **Data Seeding** otomatis (Lab Komputer & Aula).
- Implementasi **DTO (Data Transfer Object)** untuk keamanan input data.

### 🛠️ Teknis (Chures)
- Inisialisasi proyek menggunakan ASP.NET Core Web API (.NET 8).
- Setup database SQLite dengan Entity Framework Core.
- Dokumentasi API menggunakan Swagger UI.