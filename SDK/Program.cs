using SDK;
using SDK.Items;
using SDK.Orders;

Menu menu = new("Menu", new MenuChoice("Orders", Orders.Execute), new MenuChoice("Items", Items.Execute));

await menu.Show(false);
