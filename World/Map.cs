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
}