using PaginationAndSearch.Helpers;
using PaginationAndSearch.Models;
using System.Threading.Tasks;
using static PaginationAndSearch.Helpers.DataTableHelper;

namespace PaginationAndSearch.Service
{
    public interface IUserService
    {
        public  Task<DataTableHelper.Result<UserViewModel>> GetUsers (Parameters param);
    }
}
