using System;
namespace PathOfSolace.Entities;

public class Enemy : Entity
{
    public int VisionRadius { get; } = 6;
    public bool Aggro { get; private set; } = false;
    public void VisionCheck(Player player)
    {
        int dx = Math.Abs(player.X - X);
        int dy = Math.Abs(player.Y - Y);
        if(dx + dy <= VisionRadius)
        {
            Aggro = true;
        }
    }
    public Enemy(string n, int x, int y)
    {
        Name = n;
        X = x;
        Y = y;
        Relationship = Relationship.Hostile;
    }    

}