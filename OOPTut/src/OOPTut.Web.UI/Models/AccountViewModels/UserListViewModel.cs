using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace OOPTut.Web.UI.Models.AccountViewModels
{
    public class UserListViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<IdentityRole> UserRoles { get; set; }
    }
}
