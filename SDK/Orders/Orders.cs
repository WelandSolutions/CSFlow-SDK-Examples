namespace SDK.Orders;

public static class Orders
{
    public static Task Execute()
    {
        Menu menu = new("Orders", new MenuChoice("Create complete", Complete.Execute), new MenuChoice("Create with individual items", IndividualItems.Execute),
            new MenuChoice("Monitor outcome", Monitor.Execute), new MenuChoice("Poll outcome", Poll.Execute),
            new MenuChoice("Orders outcome", OrdersOutcome.Execute), new MenuChoice("Validation", Validation.Execute),
            new MenuChoice("Item validation", ItemValidation.Execute));

        return(menu.Show());
    }
}
