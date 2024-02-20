using CompactStoreFlow.SDK;
using CompactStoreFlow.SDK.Entities;

namespace SDK.Items;

public static class Add
{
    public static async Task Execute()
    {
        ICompactStore compactStore = new CompactStore(new() { Loopback = true, Logging = true });
        Item item = new()
        {
            ItemNo = "121830",
            Description = "Rotary holder 30.4 mm",
            Comment = "A test item."
        };

        try
        {
            await compactStore.Items.AddOrUpdate(item);
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
