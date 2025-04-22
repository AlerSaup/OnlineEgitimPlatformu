using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _231103015_Ali_Yahya_Atasever.DAL;
using _231103015_Ali_Yahya_Atasever.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace _231103015_Ali_Yahya_Atasever.Controllers
{
    [Authorize(Roles = "2")]
    public class OgretmenController : Controller
    {
        private readonly EgitimContext _context;
        private readonly IWebHostEnvironment _environment;

        public OgretmenController(EgitimContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: Ogretmen/Index
        public IActionResult Index()
        {
            ViewBag.Categories = _context.Categories?.ToList() ?? new List<Category>();
            ViewBag.Bolumler = _context.Bolums?.ToList() ?? new List<Bolum>();
            ViewBag.Siniflar = _context.Sinifs?.ToList() ?? new List<Sinif>();

            var videos = _context.Videos
                                 .Include(v => v.Category)
                                 .ToList() ?? new List<Video>();

            var izlenmeList = _context.Izlenmes
                                .Include(i => i.Ogrencibilgi)
                                    .ThenInclude(o => o.Bolum)
                                .Include(i => i.Ogrencibilgi)
                                    .ThenInclude(o => o.Sinif)
                                .OrderBy(i => i.Ogrencibilgi.Ad)
                                .ToList();

            ViewBag.Izlenmeler = izlenmeList;

            var ogrenciler = _context.Izlenmes
            .Include(i => i.Ogrencibilgi)
                .ThenInclude(o => o.Bolum)
            .Include(i => i.Ogrencibilgi)
                .ThenInclude(o => o.Sinif)
            .Select(i => i.Ogrencibilgi)
            .Distinct()
            .ToList();

            ViewBag.Ogrenciler = ogrenciler;

            return View(videos);
        }

        public async Task<IActionResult> StudentDetails(int studentId)
        {
            var student = await _context.Ogrencibilgis
                .Include(s => s.Bolum)
                .Include(s => s.Sinif)
                .FirstOrDefaultAsync(s => s.Id == studentId);

            if (student == null)
                return NotFound();

            var studentVideos = await _context.Izlenmes
                .Include(i => i.Video)
                .Where(i => i.OgrencibilgiId == studentId)
                .ToListAsync();

            var viewModel = new StudentDetailViewModel
            {
                Student = student,
                Videos = studentVideos
            };

            return View(viewModel);
        }

        // POST: Ogretmen/Index (Video Yükleme)
        [HttpPost]
        public async Task<IActionResult> Index(IFormFile videoFile, int categoryId, string videotitle, int bolumId, int sinifId)
        {
            ViewBag.Categories = _context.Categories?.ToList() ?? new List<Category>();
            ViewBag.Bolumler = _context.Bolums?.ToList() ?? new List<Bolum>();
            ViewBag.Siniflar = _context.Sinifs?.ToList() ?? new List<Sinif>();

            if (videoFile != null && videoFile.Length > 0)
            {
                string uploadsFolder = Path.Combine(_environment.WebRootPath, "video");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(videoFile.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await videoFile.CopyToAsync(fileStream);
                }

                Video video = new Video
                {
                    Videolink = uniqueFileName,
                    CategoryId = categoryId,
                    Videotitle = videotitle,
                    SinifId = sinifId // <-- EKLENDİ
                };

                _context.Videos.Add(video);
                await _context.SaveChangesAsync();

                // SADECE SEÇİLEN BÖLÜM ve SINIFA AİT ÖĞRENCİLER
                var ogrenciList = _context.Ogrencibilgis
                    .Where(o => o.BolumId == bolumId && o.SinifId == sinifId)
                    .ToList();

                foreach (var ogrenci in ogrenciList)
                {
                    Izlenme izlenmeKaydi = new Izlenme
                    {
                        OgrencibilgiId = ogrenci.Id,
                        VideoId = video.Id,
                        Izlenmemiktari = 0
                    };
                    _context.Izlenmes.Add(izlenmeKaydi);
                }

                await _context.SaveChangesAsync();
            }

            var videosList = _context.Videos.Include(v => v.Category).ToList() ?? new List<Video>();
            var ogrenciList2 = _context.Ogrencibilgis
                                    .Include(o => o.Bolum)
                                    .Include(o => o.Sinif)
                                    .ToList();
            ViewBag.Ogrenciler = ogrenciList2;

            return View(videosList);
        }
    }
}
