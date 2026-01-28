namespace PathOfSolace.Entities;

public class Enemy : Entity
{
    public Enemy(int x, int y)
    {
        X = x;
        Y = y;
        Relationship = Relationship.Hostile;
    }

    public void Move(int dx, int dy)
    {
        X += dx;
        Y += dy;
    }
}