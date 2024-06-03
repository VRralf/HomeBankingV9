namespace HomeBankingV9.Models
{
    public class DBInitializer
    {
        public static void Initialize(HomeBankingContext context)
        {
            if (!context.Clients.Any())
            {
                var clients = new Client[]
                {
                    new Client{FirstName="Eduardo",LastName="Mendoza",Email="edu@gmail.com",Password="123"},
                    new Client{FirstName="Juan",LastName="Perez",Email="juan@gmail.com",Password="123"},
                    new Client{FirstName="Maria",LastName="Lopez",Email="maria@gmail.com",Password="123" },
                    new Client{FirstName="Pedro",LastName="Gomez",Email="pedro@gmail.com",Password="123" }
                };

                context.Clients.AddRange(clients);
                // Guardar los cambios en la base de datos
                context.SaveChanges();
            }

            if (!context.Accounts.Any())
            {
                Client eduClient = context.Clients.FirstOrDefault(cl => cl.Email == "edu@gmail.com");
                if(eduClient != null)
                {
                    var eduAccounts = new Account[]
                    {
                        new Account{Number="VIN001",CreationDate=DateTime.Now,Balance=100000,ClientId=eduClient.Id},
                        new Account{Number="VIN002",CreationDate=DateTime.Now,Balance=200000,ClientId=eduClient.Id}
                    };

                    context.Accounts.AddRange(eduAccounts);
                    context.SaveChanges();
                }
            }
            if (!context.Transactions.Any())
            {
                Account account1 = context.Accounts.FirstOrDefault(a => a.Number == "VIN001");
                if (account1 != null)
                {

                    var transactions = new Transaction[]
                     {
                        new Transaction{Type=TransactionType.CREDIT.ToString(),Amount=10000,Description="Deposito",Date=DateTime.Now.AddHours(-5),AccountId=account1.Id},
                        new Transaction{Type=TransactionType.DEBIT.ToString(),Amount=5000,Description="Retiro",Date=DateTime.Now.AddHours(-6),AccountId=account1.Id},
                        new Transaction{Type=TransactionType.CREDIT.ToString(),Amount=20000,Description="Deposito",Date=DateTime.Now.AddHours(-4),AccountId=account1.Id}
                    };
                    context.Transactions.AddRange(transactions);
                    context.SaveChanges();
                }
            }
            if (!context.Loans.Any())
            {
                var loans = new Loan[]
                {
                    new Loan { Name = "Hipotecario", MaxAmount = 500000, Payments = "12,24,36,48,60" },
                    new Loan { Name = "Personal", MaxAmount = 100000, Payments = "6,12,24" },
                    new Loan { Name = "Automotriz", MaxAmount = 300000, Payments = "6,12,24,36" }
                };
                context.Loans.AddRange(loans);
                context.SaveChanges();
            }
            if(!context.ClientLoans.Any())
            {
                Client client = context.Clients.FirstOrDefault(c => c.Email == "edu@gmail.com");
                if (client != null)
                {
                    Loan loan1 = context.Loans.FirstOrDefault(l => l.Name == "Hipotecario");
                    if (loan1 != null)
                    {
                        ClientLoan clientLoan1 = new ClientLoan
                        {
                            Amount = 400000,
                            ClientId = client.Id,
                            LoanId = loan1.Id,
                            Payments = "60"
                        };
                        context.ClientLoans.Add(clientLoan1);
                    }
                    Loan loan2 = context.Loans.FirstOrDefault(l => l.Name == "Personal");
                    if (loan2 != null)
                    {
                        ClientLoan clientLoan2 = new ClientLoan
                        {
                            Amount = 50000,
                            ClientId = client.Id,
                            LoanId = loan2.Id,
                            Payments = "12"
                        };
                        context.ClientLoans.Add(clientLoan2);
                    }
                    Loan loan3 = context.Loans.FirstOrDefault(l => l.Name == "Automotriz");
                    if (loan3 != null)
                    {
                        ClientLoan clientLoan3 = new ClientLoan
                        {
                            Amount = 100000,
                            ClientId = client.Id,
                            LoanId = loan3.Id,
                            Payments = "24"
                        };
                        context.ClientLoans.Add(clientLoan3);
                    }
                    context.SaveChanges();
                }
            }
        }
    }
}
