﻿using System.ComponentModel.DataAnnotations;

namespace HomeBankingV9.Models
{
    public class Client
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}