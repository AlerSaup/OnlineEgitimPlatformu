using _231103015_Ali_Yahya_Atasever.DAL;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;

namespace BlogApp.Controllers
{
    public class LoginController : Controller
    {
        EgitimContext _blogContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginController(EgitimContext db, IHttpContextAccessor httpContextAccessor)
        {
            _blogContext = db;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string username, string password)
        {
            // Rank'ı da dahil ederek kullanıcıyı alıyoruz
            var user = _blogContext.Logins
                .Include(u => u.Rank)
                .FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user != null)
            {
                // Role claim olarak RankId'yi string olarak ekliyoruz (ya da Rank.Name istersen oraya geçeriz)
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, user.RankId.ToString()) // string olarak verilmeli
                };

                var identity = new ClaimsIdentity(claims, "custom", ClaimTypes.Name, ClaimTypes.Role);
                var principal = new ClaimsPrincipal(identity);
                await _httpContextAccessor.HttpContext.SignInAsync(principal);

                // RankId'ye göre yönlendirme yapıyoruz
                switch (user.RankId)
                {
                    case 1: // Öğrenci
                        return RedirectToAction("Index", "AnaSayfa");
                    case 2: // Öğretmen
                        return RedirectToAction("Index", "Ogretmen");
                    case 3: // Admin
                        return RedirectToAction("Index", "Logins"); // varsa bir admin paneli
                    default:
                        return RedirectToAction("Index", "AnaSayfa");
                }
            }

            TempData["mesaj"] = "Kullanıcı adı veya şifre hatalı";
            return View();
        }
    }
}
