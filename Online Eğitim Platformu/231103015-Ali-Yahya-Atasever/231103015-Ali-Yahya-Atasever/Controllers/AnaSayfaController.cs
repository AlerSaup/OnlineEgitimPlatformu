using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _231103015_Ali_Yahya_Atasever.DAL;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace _231103015_Ali_Yahya_Atasever.Controllers
{
    [Authorize]
    public class AnaSayfaController : Controller
    {
        private readonly EgitimContext _context;

        public AnaSayfaController(EgitimContext context)
        {
            _context = context;
        }

        // GET: AnaSayfa/Index
        public IActionResult Index()
        {
            var username = User.Identity.Name;
            var login = _context.Logins
                .Include(l => l.Ogrencibilgi)
                .FirstOrDefault(l => l.Username == username);

            if (login != null && login.Ogrencibilgi != null)
            {
                if (login.Ogrencibilgi.BolumId == null)
                    return View(new List<Video>());

                int bolumId = login.Ogrencibilgi.BolumId.Value;
                int sinifId = login.Ogrencibilgi.SinifId.Value;

                var categories = _context.Categories
                    .Where(c => c.BolumId == bolumId)
                    .ToList();

                ViewBag.Categories = categories;
                ViewBag.OgrenciId = login.OgrencibilgiId;

                var student = _context.Ogrencibilgis
                    .Include(o => o.Bolum)
                    .Include(o => o.Sinif)
                    .FirstOrDefault(o => o.Id == login.OgrencibilgiId);

                ViewBag.Student = student;

                // ID'leri ayıkla ve null'ları temizle
                var categoryIds = categories
                    .Select(c => c.Id)
                    .ToList();

                var videos = _context.Videos
                    .Include(v => v.Category)
                    .Include(v => v.Sinif)
                    .Where(v =>
                        v.Category.BolumId == bolumId &&
                        v.SinifId == sinifId)
                    .ToList();

                var studentProgress = _context.Izlenmes
                    .Where(i => i.OgrencibilgiId == login.OgrencibilgiId)
                    .ToDictionary(i => i.VideoId, i => i.Izlenmemiktari);

                ViewBag.StudentProgress = studentProgress;

                return View(videos);
            }

            // Giriş başarısızsa
            ViewBag.Categories = new List<Category>();
            ViewBag.OgrenciId = 0;
            ViewBag.Student = null;
            ViewBag.StudentProgress = new Dictionary<int, int>();
            return View(new List<Video>());
        }



        [HttpPost]
        public IActionResult UpdateIzlenme([FromBody] IzlenmeUpdateModel model)
        {
            var izlenmeKaydi = _context.Izlenmes
                .FirstOrDefault(i => i.VideoId == model.VideoId && i.OgrencibilgiId == model.OgrencibilgiId);
            if (izlenmeKaydi != null)
            {
                // Yeni ilerleme değeri mevcut değerden yüksekse güncelle
                if (model.Izlenmemiktari > izlenmeKaydi.Izlenmemiktari)
                {
                    izlenmeKaydi.Izlenmemiktari = model.Izlenmemiktari;
                    _context.SaveChanges();
                }
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        public class IzlenmeUpdateModel
        {
            public int VideoId { get; set; }
            public int OgrencibilgiId { get; set; }
            public int Izlenmemiktari { get; set; }
        }

    }
}
