using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAppProject.Models;
using System.ComponentModel.DataAnnotations;

namespace WebAppProject.Models
{
    public class UserRoleViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public bool IsSelected { get; set; }
        public string RoleName { get; set; }

        public UserRoleViewModel()
        {

        }
    }
}