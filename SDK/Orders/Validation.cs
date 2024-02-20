using CompactStoreFlow.SDK;
using CompactStoreFlow.SDK.Entities;

namespace SDK.Orders;

public static class Validation
{
    public static async Task Execute()
    {
        ICompactStore compactStore = new CompactStore(new() { Loopback = true, Logging = true });
        Order order = new()
        {
            OrderNo = "",
            Type = OrderType.Input,
            Items =
            [
                new OrderItem { ItemNo = "121076", Quantity = 1 },
                new OrderItem { ItemNo = "121106", Quantity = 5 },
                new OrderItem { ItemNo = "121579", Quantity = 3 }
            ]
        };

        try
        {
            await compactStore.Orders.Add(order);
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
