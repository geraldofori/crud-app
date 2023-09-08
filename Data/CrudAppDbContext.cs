using Microsoft.EntityFrameworkCore;

namespace CRUD_App.Data 
{
    public class CrudAppDbContext: DbContext
    {
        public CrudAppDbContext(DbContextOptions options) :base(options)
        {
            

        }

    }

}