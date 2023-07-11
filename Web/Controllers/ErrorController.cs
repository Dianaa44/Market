using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {

            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry the resourse not found";
                    // return View("NotFound");
                    break;
                case 500:
                    ViewBag.ErrorMessage = "Sorry Internal Server Error";
                    return View("InternalServerError");
            }
            return View("NotFound");
        }
    }
}
