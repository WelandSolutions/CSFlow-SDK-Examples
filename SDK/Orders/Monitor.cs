using CompactStoreFlow.SDK;
using CompactStoreFlow.SDK.Entities;

namespace SDK.Orders;

public static class Monitor
{
    public static async Task Execute()
    {
        ICompactStore compactStore = new CompactStore(new() { Loopback = true, Logging = true });
        Order order = new()
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
        Task finished = new(() => { });
        Func<string, Task> orderFinished = async orderNo =>
        {
            Console.WriteLine($"Order {orderNo} finished");
            OrderOutcome outcome = await compactStore.Orders.Order(orderNo).Outcome();

            foreach (var itemOutcome in outcome.Items)
            {
                Console.WriteLine($"{itemOutcome.ItemNo} {itemOutcome.Quantity}");
            }
            finished.Start();
        };

        try
        {
            if (await compactStore.Orders.Monitor(orderFinished))
            {
                await compactStore.Orders.Add(order);
            }
            await finished;
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
