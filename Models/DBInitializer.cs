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
        }
    }
}
