using HomeBankingV9.Models;

namespace HomeBankingV9.Repositories
{
    public interface IAccountRepository
    {
        Account FindById(long id);
        IEnumerable<Account> GetAllAccounts();
        void Save(Account account);
    }
}
