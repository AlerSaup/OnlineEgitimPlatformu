# 🎓 Online Eğitim Platformu

Bu proje, öğretmenlerin ders videolarını ilgili bölüm ve sınıflara yükleyebildiği, öğrencilerin bu videoları izleyebildiği ve yöneticilerin (admin) tüm sistemi kontrol edebildiği bir **online video eğitim platformudur**.

## 🧩 Özellikler

- 👨‍🏫 **Öğretmen Paneli**
  - Ders videosu yükleyebilir
  - İlgili Sınıf ve Bölüme Ait Öğrencilerin Videolar Hakkında Bilgilerini Görebilir

- 👩‍🎓 **Öğrenci Paneli**
  - Kendi bölüm ve sınıfına atanmış videoları izleyebilir
  - İzleme yüzdesi sistemi ile öğretmenler ne kadar izlendiğini görebilir

- 🛠️ **Admin Paneli**
  - Öğrenci, öğretmen ve videolar üzerinde tam yetki
  - Tüm sistemin genel kontrolü

---

## 🚀 Kurulum

### 1. Dosya İçerisinde Bulunan egitim.dacpac dosyasını
SQL Server Management Studio ya Yüklüyoruz

### 2. (Gerekirse) Araçlar / NuGet Paket Yöneticisi /
 Paket Yöneticisi Konsolu Kısmına Gittikten Sonra
 Scaffold-DbContext"Server=.\SQLEXPRESS;Database=egitim;Trusted_Connection=True;TrustServerCertificate=True;"Microsoft.EntityFrameworkCore.SqlServerOutputDir DAL-force
 Komutu İle SQL Bağlantımızı Yapıyoruz


- Bilgisayar Programcılığı 1. Sınıf Öğrencisi İçin
 Kullanıcı Adı : bilgisayarprogramciligi1@gmail.com
 Şifre : deneme123

- Bilgisayar Programcılığı 2. Sınıf Öğrencisi İçin
 Kullanıcı Adı : bilgisayarprogramciligi2@gmail.com
 Şifre : deneme123

- Admin Girişi İçin
 Kullanıcı Adı : admin@gmail.com
 Şifre : deneme123

 (Hali Hazırda Veritabanında Bulunan Video Bilgisayar
 Programcılığı 2. Sınıf Öğrencisi İçin Hazırlanmıştır
 Öğretmen Hesabından Giriş Sağlayarak İlgili Bölüme
 Ve Sınıflara Video Yüklemesi Yapabilirsiniz)

## 🖼️ Görseller

> Aşağıda Örnek Fotoğrafları Görebilirsiniz

### 🎥 Öğretmen Video Yükleme Sayfası

![Öğretmen Video Sayfası](https://i.imgur.com/kBpLNe0.png)

### 👨‍🏫 Öğretmenlerin Öğrencilerin Listesini Görebildiği Sayfa

![Öğretmen Öğrenci Sayfası](https://i.imgur.com/Pm9Tjty.png)

### 👨‍🏫 Öğretmenlerin Öğrencilerin Analizlerini Görebildiği Sayfa

![Öğretmen Öğrencilerin Analizleri Sayfası](https://i.imgur.com/5ChBCJU.png)

### 📁 Öğrencilerin Videoları Gördüğü Sayfa

![Öğrencilerin Videolar Sayfası](https://i.imgur.com/rW6tHo4.png)

### 🛠️ Admin Panel

![Admin Sayfası](https://i.imgur.com/GLHJd1S.png)

## 🔧 Teknolojiler

- ASP.NET Core MVC
- Entity Framework Core
- SQL Server
- Bootstrap 5
- JavaScript / AJAX

---
