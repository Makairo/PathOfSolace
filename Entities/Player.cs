namespace PathOfSolace.Entities;

public class Player : Entity
{
    public Player(int x, int y)
    {
        X = x;
        Y = y;
    }

    public void Move(int dx, int dy)
    {
        X += dx;
        Y += dy;
    }
}