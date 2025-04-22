using _231103015_Ali_Yahya_Atasever.DAL;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace _231103015_Ali_Yahya_Atasever.Controllers
{
    public class HomeController : Controller
    {
        EgitimContext _blogContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HomeController(EgitimContext db,
            IHttpContextAccessor httpContextAccessor)
        {
            _blogContext = db;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Deneme()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Index(string username, string password)
        {

            var KullaniciVarMi =
                _blogContext.Logins
                .Any(h =>
                h.Username.Equals(username)
                &&
                h.Password.Equals(password)
                );

            if (KullaniciVarMi)
            {
                // Giriþ baþarýlý, kullanýcý bilgilerini context'e yerleþtir.
                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, username),
                    };

                var identity = new ClaimsIdentity(
                    claims,
                    "custom",
                    ClaimTypes.Name,
                    ClaimTypes.Role);

                var principal = new ClaimsPrincipal(identity);

                _httpContextAccessor.HttpContext.SignInAsync(principal);

                return RedirectToAction("Index", "AnaSayfa");
            }

            TempData["mesaj"] = "Kullanýcýadý þifre hatalý";
            return View();
        }
    }
}
