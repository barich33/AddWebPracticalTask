using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PaginationAndSearch.API.Models;
using PaginationAndSearch.API.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using static PaginationAndSearch.API.Helpers.DataTableHelper;
using System.Linq;
using PaginationAndSearch.API.AppContext;
using Microsoft.EntityFrameworkCore;

namespace PaginationAndSearch.API.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {       
        private ApplicationDbContext _dbContext;
        public UserController(IConfiguration iconfiguration, ApplicationDbContext dbContext)
        {           
            _dbContext = dbContext;
        }

        [HttpPost("GetUserList")]
        public IActionResult GetUserList([FromBody] Parameters param)
        {
            var data = new UserViewModel();            

            string StoredProc = "exec [dbo].[SP_GetUserList] " +
                    "@SearchVal = '" + param.Search.Value +"'," + 
                    "@Page = " + param.Start + "," +
                    "@PageSize = " + param.Length + "," +
                    "@OrderBy= '" + param.SortOrder + "'";

            var result = _dbContext.Users.FromSqlRaw(StoredProc).ToList();

            foreach (var item in result)
            {
                UserViewModel model = new UserViewModel();
                model.Email = item.Email.ToString();
                model.FirstName = item.FirstName.ToString();
                model.LastName = item.LastName.ToString();
                model.Address = item.Address.ToString();
                model.DateOfBirth = item.DateOfBirth.ToString("yyyy-mm-dd");
                data.UserList.Add(model);

            }
            int totalUsers=_dbContext.Users.Count();
            data.Total = totalUsers;
            var helper = new DataTableHelper.Result<UserViewModel>()
            {
                Draw = param.Draw,
                Data = data.UserList.ToList(),
                RecordsFiltered = data.Total,
                RecordsTotal = data.Total
            };

            return Ok(helper);
        }


    }
}
