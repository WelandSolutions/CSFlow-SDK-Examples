namespace SDK.Items;

public static class Items
{
    public static Task Execute()
    {
        Menu menu = new("Items", new MenuChoice("Add", Add.Execute), new MenuChoice("Update", Update.Execute),
            new MenuChoice("Balance", Balance.Execute), new MenuChoice("Validation", Validation.Execute));

        return(menu.Show());
    }
}

