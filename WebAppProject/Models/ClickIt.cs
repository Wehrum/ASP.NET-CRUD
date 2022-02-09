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
    public class ClickIt
    {
        public int Id { get; set; }

        public int Click { get; set; }

        public ClickIt()
        {

        }
    }

}
