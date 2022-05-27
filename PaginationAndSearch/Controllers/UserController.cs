using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PaginationAndSearch.Models;
using PaginationAndSearch.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using static PaginationAndSearch.Helpers.DataTableHelper;

namespace PaginationAndSearch.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private IUserService _userService;
        public UserController(ILogger<UserController> logger, IConfiguration iconfiguration,IUserService userService)
        {
            _logger = logger;
            _userService=userService;
        }

        [HttpPost]

        public async Task<JsonResult> GetUserList([FromBody] Parameters param)
        {
            var userlist= await _userService.GetUsers(param);
            return Json(userlist);
        }
        public IActionResult Index()
        {
            return View();
        }
       
    }
}
