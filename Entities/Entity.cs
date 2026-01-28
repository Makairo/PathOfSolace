namespace PathOfSolace.Entities;
public abstract class Entity
{
    public int X { get; set; }
    public int Y { get; set; }
    public bool Alive { get; protected set; } = true; 

    public virtual bool BlocksMovement => true;
    public virtual Relationship Relationship { get; protected set; } = Relationship.Neutral;

    public virtual void Die() { Alive = false; }
}