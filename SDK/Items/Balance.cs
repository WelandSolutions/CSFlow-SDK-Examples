using CompactStoreFlow.SDK;
using CompactStoreFlow.SDK.Entities;

namespace SDK.Items;

public static class Balance
{
    public static async Task Execute()
    {
        ICompactStore compactStore = new CompactStore(new() { Loopback = true, Logging = true });

        try
        {
            IEnumerable<ItemBalance> balance = await compactStore.Items.Item("121076").Balance();

            Console.WriteLine("Balance for item 121076:");
            foreach(var itemBalance in balance)
            {
                Console.WriteLine($"{itemBalance.StorageLocation} {itemBalance.Quantity}");
            }
            Console.WriteLine($"Total: {balance.Sum(b => b.Quantity)}");
        }
        catch(CompactStoreException e)
        {
            if(e.Details != null)
            {
                Console.WriteLine(e.Details);
                if(e.Details.Errors != null)
                {
                    Console.WriteLine(e.Details.Errors.Description.First());
                }
            }
            else if(e.StatusCode.HasValue)
            {
                Console.WriteLine(e.StatusCode.Value);
            }
            else
            {
                Console.Write(e.InnerException);
            }
        }
    }
}
