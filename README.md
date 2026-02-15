# 🏨 Sistem Peminjaman Ruangan (Backend)

Backend API untuk sistem peminjaman ruangan di kampus, dibuat menggunakan **ASP.NET Core Web API**.
Proyek ini bertujuan untuk mempermudah pengelolaan data ruangan, jadwal peminjaman, dan validasi ketersediaan ruangan.

## 🚀 Fitur Utama
* **CRUD Ruangan**: Mengelola data ruangan (Create, Read, Update, Delete).
* **Validasi Data**: Mencegah input data yang tidak logis (misal: kapasitas negatif).
* **Data Seeding**: Database otomatis terisi data awal saat dijalankan.
* **Dokumentasi API**: Menggunakan Swagger UI.

## 🛠️ Teknologi yang Digunakan
* **Framework**: .NET 8.0 (ASP.NET Core Web API)
* **Database**: SQLite (Development)
* **ORM**: Entity Framework Core

## 📦 Cara Install & Menjalankan

1. **Clone Repository**
   ```bash
   git clone [https://github.com/archtka/2026-peminjaman-ruangan-backend.git](https://github.com/archtka/2026-peminjaman-ruangan-backend.git)
   cd 2026-peminjaman-ruangan-backend

   5.  **Save** (`Ctrl + S` / `Cmd + S`).

---

#### 2. Bikin File `CHANGELOG.md`
1.  Klik kanan lagi di area kosong -> **New File**.
2.  Ketik nama: **`CHANGELOG.md`** (Huruf besar semua).
3.  **Copy-Paste isi ini:**

```markdown
# Changelog

Semua perubahan penting pada proyek ini akan didokumentasikan dalam file ini.

## [v1.0.0] - 2026-02-12

### 🚀 Fitur Baru (Features)
- Menambahkan **Modul Ruangan (Rooms)** lengkap dengan operasi CRUD.
- **POST**: Menambahkan ruangan baru dengan validasi input (Nama wajib, Kapasitas 1-1000).
- **GET**: Menampilkan daftar seluruh ruangan dan detail per ruangan.
- **PUT**: Mengupdate informasi ruangan (Nama, Deskripsi, Kapasitas).
- **DELETE**: Menghapus data ruangan dari database.
- Menambahkan **Data Seeding** otomatis (Lab Komputer & Aula).
- Implementasi **DTO (Data Transfer Object)** untuk keamanan input data.

### 🛠️ Teknis (Chores)
- Inisialisasi proyek menggunakan ASP.NET Core Web API (.NET 8).
- Setup database SQLite dengan Entity Framework Core.
- Dokumentasi API menggunakan Swagger UI.