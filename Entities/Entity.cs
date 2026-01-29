using PathOfSolace.World;
using PathOfSolace.Core;
namespace PathOfSolace.Entities;
public abstract class Entity
{
    public string Name { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public bool Alive { get; protected set; } = true; 
    public Tile CurrentTile { get; protected set; }

    public virtual bool BlocksMovement => true;
    public virtual Relationship Relationship { get; protected set; } = Relationship.Neutral;

    public virtual void Die() { Alive = false; }
    public void Move(int nx, int ny)
    {
        X = nx;
        Y = ny;
    }
    
    
}