MvcProjeWriter

ASP.NET MVC (katmanlı mimari) ile geliştirilmiş “Yazar Paneli / Mesajlaşma” uygulaması. Proje; EntityLayer, DataAccessLayer, BusinessLayer ve MvcProjeWriter (UI) katmanlarından oluşur. 

Özellikler
Mesajlaşma Modülü: Gelen kutusu, giden kutusu, taslaklar, çöp kutusu

Zengin Metin Editörü: Summernote ile içerik düzenleme (kalın, renkli arka plan vb.)

Klasörleme & Filtreleme: Arama ve kategori bazlı gezinme

Galeri / Lightbox: Ekko Lightbox ile görsel önizleme

Doğrulamalar: FluentValidation ile alan kontrolleri; (opsiyonel) reCAPTCHA entegrasyonu

Raporlama / Sayfalandırma: Listeleme performansı ve okunabilirlik

Arayüz: AdminLTE 3 tabanlı, responsive tema

Not: Depodaki Talents.png görseli “Yetenek Kartı” (Talent Card) özelliği için örnek/çalışma görselidir. 

Mimarî
Presentation (MvcProjeWriter - UI)
        ↑
BusinessLayer (Managers, Validation, Rules)
        ↑
DataAccessLayer (EF / Repositories / Context)
        ↑
EntityLayer (POCO Entities / DTOs)
