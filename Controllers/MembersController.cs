
using CRUD_App.Data;
using CRUD_App.Models;
using CRUD_App.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CRUD_App.Controllers
{
    public class MembersController: Controller
    {
        private readonly CrudAppDbContext crudAppDbContext;

        public MembersController(CrudAppDbContext crudAppDbContext)
        {
            this.crudAppDbContext = crudAppDbContext;
            
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var members = await crudAppDbContext.Members.ToListAsync();
            return View(members);

        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddMemberViewModel addMemberRequest)
        {
            var member = new Member()
            {
                Id = Guid.NewGuid(),
                FirstName = addMemberRequest.FirstName,
                SurName = addMemberRequest.SurName,
                Age = addMemberRequest.Age

            };

            await crudAppDbContext.Members.AddAsync(member);
            await crudAppDbContext.SaveChangesAsync();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var member =  await crudAppDbContext.Members.FirstOrDefaultAsync(x => x.Id == id);

            if(member != null)
            {
                var viewModel = new UpdateMemberViewModel()
                {
                    Id = member.Id,
                    FirstName = member.FirstName,
                    SurName = member.SurName,
                    Age = member.Age

                };


                return View(viewModel);

            }
            
            return RedirectToAction("Index");


        }

    }
    
}
