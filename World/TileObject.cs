namespace PathOfSolace.World;
public abstract class TileObject
{
    public int X { get; set; }
    public int Y { get; set; }

    public virtual bool BlocksMovement => true;
    public virtual bool Interactable => true;
}
    
