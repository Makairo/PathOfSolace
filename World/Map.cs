using PathOfSolace.Entities;
using System;
namespace PathOfSolace.World;

public class Map
{
    public int Width { get; }
    public int Height { get; }

    public Tile[,] Tiles { get; }

    public Map(int width, int height)
    {
        Width = width;
        Height = height;
        Tiles = new Tile[Width, Height];

        Generate();
    }

    private void Generate()
    {
        for(int x = 0 ; x < Width ; x++)
        {
            for(int y = 0 ; y < Height ; y++)
            {
                bool isWall = ( x == 0 || y == 0 || x == Width - 1 || y == Height - 1);
                Tiles[x,y] = new Tile(!isWall);
            }
        }
    }

    public Tile getTile(int x, int y)
    {
        return Tiles[x,y];
    }

    public bool IsWalkable(int x, int y)
    {
        return Tiles[x,y].IsWalkable;
    }

    public bool Inbounds(int x, int y)
    {
        if(x > 0 && x < Width - 1 && y > 0 && y < Height - 1)
        {
            return true;
        }
        return false;
    }

    public void MoveEntity(Entity entity, int newX, int newY)
{
    // Safety: bounds
    if (!Inbounds(newX, newY))
        throw new ArgumentOutOfRangeException("MoveEntity out of bounds");

    Tile target = getTile(newX, newY);

    // Safety: tile must be walkable
    if (!target.IsWalkable)
        throw new InvalidOperationException("Target tile is not walkable");

    // Safety: no living occupant
    if (target.Occupant != null && target.Occupant.Alive)
        throw new InvalidOperationException("Target tile is occupied");

    // Remove from current tile (if any)
    if (Inbounds(entity.X, entity.Y))
    {
        Tile current = getTile(entity.X, entity.Y);
        if (current.Occupant == entity)
            current.RemOccupant();
    }

    // Place in new tile
    target.SetOccupant(entity);

    // Update entity coordinates LAST
    entity.Move(newX, newY);
}

}