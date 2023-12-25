﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace R54_M11_Class_06_Work_01.DataApi.ViewModels.Identity
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; } = default!;
        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6)]
        public string Password { get; set; } = default!;
    }
}
