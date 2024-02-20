namespace SDK;
 
public class Menu(string header, params MenuChoice[] menuChoices)
{
    public async Task Show(bool back = true)
    {
        this.Render(back);
        while(true)
        {
            ConsoleKeyInfo menuChoice = Console.ReadKey(true);

            if(menuChoice.Key == ConsoleKey.Escape)
            {
                Environment.Exit(0);
            }

            if(back && menuChoice.KeyChar is 'x' or 'X')
            {
                break;
            }
            
            if(menuChoice.KeyChar >= '1' && menuChoice.KeyChar <= menuChoices.Length.ToString().First())
            {
                await menuChoices.ElementAt(int.Parse(menuChoice.KeyChar.ToString()) - 1).Operation();
                this.Render(back);
            }
        }
    }

    private void Render(bool back)
    {
        int choice = 1;

        Console.WriteLine();
        Console.WriteLine(header);
        Console.WriteLine("======");
        foreach(var menuChoice in menuChoices)
        {
            Console.WriteLine($"{choice++}. {menuChoice.Text}");
        }

        if(back)
        {
            Console.WriteLine("X. Back");
        }
        Console.WriteLine();
    }
}
