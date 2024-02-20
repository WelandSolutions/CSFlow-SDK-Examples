namespace SDK;

public class MenuChoice(string text, Func<Task> operation)
{
    public string Text { get; } = text;
    public Func<Task> Operation { get; } = operation;
}
