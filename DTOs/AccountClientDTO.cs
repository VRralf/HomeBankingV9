﻿using HomeBankingV9.Models;

namespace HomeBankingV9.DTOs
{
    public class AccountClientDTO
    {
        public long Id { get; set; }
        public string Number { get; set; }
        public DateTime CreationDate { get; set; }
        public double Balance { get; set; }
        public AccountClientDTO(Account account)
        {
            Id = account.Id;
            Number = account.Number;
            CreationDate = account.CreationDate;
            Balance = account.Balance;
        }
    }
}