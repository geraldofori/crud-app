
using Microsoft.AspNetCore.Mvc;

namespace CRUD_App.Controllers
{
    public class MembersController: Controller
    {
        [HttpGet]
        public IActionResult Add(){
            return View();
        }
    }
    
}
