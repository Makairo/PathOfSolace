namespace PathOfSolace.World;
using PathOfSolace.Entities;
using PathOfSolace.World;

public class Tile
{
    public bool IsWalkable { get; protected set; }
    public Entity? Occupant { get; protected set; }

    public TileObject? Object { get; protected set; }

    public Tile(bool isWalkable)
    {
        IsWalkable = isWalkable;
    }

    public void RemOccupant()
    {
        Occupant = null;
    }

    public void SetOccupant(Entity occ)
    {
        Occupant = occ;
    }
}