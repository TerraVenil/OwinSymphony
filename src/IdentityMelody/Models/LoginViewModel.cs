﻿using System.ComponentModel.DataAnnotations;

namespace IdentityMelody.Models
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}