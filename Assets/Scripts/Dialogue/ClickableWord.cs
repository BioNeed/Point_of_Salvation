public class ClickableWord
{
    public ClickableWord(string word)
    {
        Word = word;
    }

    public string Word { get; private set; }

    public bool IsFound { get; private set; } = false;

    public void MarkAsFound()
    {
        IsFound = true;
    }
}
