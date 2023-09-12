
using System.Data;
using CRUD_App.Data;
using CRUD_App.Migrations;
using CRUD_App.Models;
using CRUD_App.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CRUD_App.Controllers
{
    public class ClientsController: Controller
    {
        private readonly IConfiguration configuration;


        public ClientsController(IConfiguration configuration )
        {
            this.configuration = configuration;
            
        }

        public  IActionResult Index()
        {
            DataTable dtbl = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(configuration.GetConnectionString("CrudConnectionString")))
            {
                sqlConnection.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("ClientViewAll", sqlConnection);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.Fill(dtbl);
            }
            return View(dtbl);
            

        }


        public IActionResult Add()
        {
            ClientViewModel clientViewModel = new ClientViewModel();

            return View(clientViewModel);
        }

        public IActionResult Add(int id,[Bind("Id, FirstName, SurName, Age")] ClientViewModel clientViewModel  )
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection sqlConnection= new SqlConnection(configuration.GetConnectionString("CrudConnectionString")))
                {
                    sqlConnection.Open();
                    SqlCommand sqlCmd = new SqlCommand("ClientAdd", sqlConnection);
                    sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("Id", clientViewModel.Id);
                    sqlCmd.Parameters.AddWithValue("FirstName", clientViewModel.FirstName);
                    sqlCmd.Parameters.AddWithValue("SurName", clientViewModel.SurName);
                    sqlCmd.Parameters.AddWithValue("Age", clientViewModel.Age);
                    sqlCmd.ExecuteNonQuery();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(clientViewModel);

        }

        public IActionResult View(Guid id)
        {

            
            
            return RedirectToAction("Index");


        }

        public IActionResult View(UpdateMemberViewModel model)
        {
            

            return RedirectToAction("Index");

        }

        public  IActionResult Delete(UpdateMemberViewModel model)
        {
            

            return RedirectToAction("Index");


        }


    }
    
}
