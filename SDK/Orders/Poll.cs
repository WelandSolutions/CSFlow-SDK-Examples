using CompactStoreFlow.SDK;
using CompactStoreFlow.SDK.Entities;

namespace SDK.Orders;

public static class Poll
{
    public static async Task Execute()
    {
        ICompactStore compactStore = new CompactStore(new() { Loopback = true, Logging = true });
        Order order = new()
        {
            OrderNo = "EXT123",
            Type = OrderType.Inventory,
            Start = true,
            Feedback = true,
            Items =
            [
                new OrderItem { ItemNo = "121076", Quantity = 1 },
                new OrderItem { ItemNo = "121106", Quantity = 5 },
                new OrderItem { ItemNo = "121579", Quantity = 3 }
            ]
        };
        OrderOutcome outcome = null;

        try
        {
            await compactStore.Orders.Add(order);

            while (outcome == null)
            {
                await Task.Delay(TimeSpan.FromSeconds(1));
                outcome = await compactStore.Orders.Order(order.OrderNo).Outcome();
                if (outcome == null)
                {
                    Console.WriteLine($"Order {order.OrderNo} not finished");
                }
            }

            Console.WriteLine($"Order {order.OrderNo} finished");
            foreach (var itemOutcome in outcome.Items)
            {
                Console.WriteLine($"{itemOutcome.ItemNo} {itemOutcome.Quantity}");
            }
        }
        catch (CompactStoreException e)
        {
            if (e.Details != null)
            {
                Console.WriteLine(e.Details);
                if (e.Details.Errors != null)
                {
                    Console.WriteLine(e.Details.Errors.Description.First());
                }
            }
            else if (e.StatusCode.HasValue)
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
