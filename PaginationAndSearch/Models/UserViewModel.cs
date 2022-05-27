using System;
using System.Collections.Generic;

namespace PaginationAndSearch.Models
{
    public class UserViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string DateOfBirth { get; set; }
        public int Total { get; set; }
        public List<UserViewModel> UserList { get; set; }
        public UserViewModel()
        {
            UserList = new List<UserViewModel>();
        }
    }
}
