﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Khdoum.Api.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "اسم المستخدم مطلوب")]
        public string Username { get; set; }

        [Required(ErrorMessage = "كلمة المرور مطلوبة")]
        public string Password { get; set; }

        [Required(ErrorMessage = "الاسم مطلوب")]
        public string Name { get; set; }

    }
}
