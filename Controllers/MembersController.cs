
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
        public async Task<IActionResult> Index(string searchString)
        {
            // Get all members initially
            var members = await crudAppDbContext.Members.ToListAsync();

            // Check if a search string is provided
            if (!string.IsNullOrEmpty(searchString))
            {
                // Filter members based on the search string
                members = members.Where(m =>
                    m.FirstName.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                    m.SurName.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                ).ToList();
            }

            // Pass the search string to the view so that it can be displayed in the search input field
            ViewData["CurrentFilter"] = searchString;

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
                MiddleName = addMemberRequest.MiddleName,
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
                    MiddleName = member.MiddleName,
                    SurName = member.SurName,
                    Age = member.Age

                };


                return await Task.Run(() => View("View", viewModel));

            }
            
            return RedirectToAction("Index");


        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateMemberViewModel model)
        {
            var member = await crudAppDbContext.Members.FindAsync(model.Id);

            if(member != null)
            {
                member.FirstName = model.FirstName;
                member.SurName = model.SurName;
                member.MiddleName = model.MiddleName;
                member.Age = model.Age;

                await crudAppDbContext.SaveChangesAsync();

                return RedirectToAction("Index");

            };

            return RedirectToAction("Index");

        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateMemberViewModel model)
        {
            var member = await crudAppDbContext.Members.FindAsync(model.Id);

            if(member != null)
            {
                crudAppDbContext.Members.Remove(member);

                await crudAppDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");


        }


    }
    
}
