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
    public class ProjectRole
    {
        //[BindProperty]
        //public InputModel Input { get; set; }

        //public class InputModel
        //{
        //    [Required]
        //    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        //    [Display(Name = "roleCheck")]

        //    public string roleCheck { get; set; }
        //}

        public int Id { get; set; }

        [Required]
        //[Compare("Role", ErrorMessage = "test")]
        [StringLength(15, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        [Display(Name = "Role")]
        public string RoleName { get; set; }
    }
}