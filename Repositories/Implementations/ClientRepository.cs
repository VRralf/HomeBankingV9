﻿using HomeBankingV9.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeBankingV9.Repositories.Implementations
{
    public class ClientRepository : RepositoryBase<Client>, IClientRepository
    {
        public ClientRepository(HomeBankingContext repositoryContext) : base(repositoryContext)
        {
        }

        public Client FindByEmail(string email)
        {
            return FindByCondition(c => c.Email.ToLower() == email.ToLower())
                .Include(c => c.Accounts)
                .Include(c => c.ClientLoans)
                .ThenInclude(cl => cl.Loan)
                .Include(c => c.Cards)
                .FirstOrDefault();
        }

        public Client FindById(long id)
        {
            return FindByCondition(c => c.Id == id)
                .Include(c => c.Accounts)
                .Include(c => c.ClientLoans)
                .ThenInclude(cl => cl.Loan)
                .Include(c => c.Cards)
                .FirstOrDefault();
        }

        public IEnumerable<Client> GetAllClients()
        {
            return FindAll()
                .Include(c => c.Accounts)
                .Include(c => c.ClientLoans)
                .ThenInclude(cl => cl.Loan)
                .Include(c => c.Cards)
                .ToList();
        }

        public void Save(Client client)
        {
            Create(client);
            SaveChanges();
        }
    }
}
