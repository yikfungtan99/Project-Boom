[System.Serializable]
public class ShellInventory
{
    public ShellObject shellObject;
    public int count;

    public ShellInventory(ShellObject shellObject, int count)
    {
        this.shellObject = shellObject;
        this.count = count;
    }
}
