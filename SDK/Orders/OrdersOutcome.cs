using CompactStoreFlow.SDK;
using CompactStoreFlow.SDK.Entities;

namespace SDK.Orders;

public static class OrdersOutcome
{
    public static async Task Execute()
    {
        ICompactStore compactStore = new CompactStore(new() { Loopback = true, Logging = true });
        Order order1 = new()
        {
            OrderNo = "EXT123",
            Type = OrderType.Output,
            Start = true,
            Feedback = true,
            Items =
            [
                new OrderItem { ItemNo = "121076", Quantity = 1 },
                new OrderItem { ItemNo = "121106", Quantity = 5 },
                new OrderItem { ItemNo = "121579", Quantity = 3 }
            ]
        };
        Order order2 = new()
        {
            OrderNo = "EXT124",
            Type = OrderType.Output,
            Start = true,
            Feedback = true,
            Items =
            [
                new OrderItem { ItemNo = "121076", Quantity = 1 },
            ]
        };
        Order order3 = new()
        {
            OrderNo = "EXT125",
            Type = OrderType.Output,
            Start = true,
            Feedback = true,
            Items =
            [
                new OrderItem { ItemNo = "121106", Quantity = 1 },
            ]
        };

        try
        {
            await compactStore.Orders.Add(order1);
            await compactStore.Orders.Add(order2);
            await compactStore.Orders.Add(order3);

            Console.WriteLine("Waiting to orders to finish");
            await Task.Delay(TimeSpan.FromSeconds(10));

            IEnumerable<OrderOutcome> outcomes = await compactStore.Orders.Outcome();
            foreach (var orderOutcome in outcomes)
            {
                Console.WriteLine($"Order {orderOutcome.OrderNo}:");
                foreach (var itemOutcome in orderOutcome.Items)
                {
                    Console.WriteLine($"{itemOutcome.ItemNo} {itemOutcome.Quantity}");
                }
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
