namespace MenuSystem;

public class MenuItem
{
    private string? _title;
    private string? _shortcut;

    public Func<string>? MenuItemAction { get; set; }
    
    public string Title
    {
        get => _title!;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Title cannot be null or empty.");
            }
            _title = value;
            
        } 
    }

    public string Shortcut
    {
        get => _shortcut!;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Shortcut cannot be null or empty.");
            }
            if (value.Length > 1)
            {
                throw new ArgumentException("Shortcut cannot be longer than 1.");
            }
            _shortcut = value;
        }
    }


    public override string ToString()
    {
        return Shortcut + ")" + Title;
    }
}