namespace CardGames.Console;

public abstract class Menu
{
    public abstract void Start();
    public virtual void Do(int action) {}
    public virtual void Update() {}
    public virtual void Proceed() {}
}
