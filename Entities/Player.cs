using PathOfSolace.Core;
using Microsoft.Xna.Framework.Input;
using PathOfSolace.World;
namespace PathOfSolace.Entities;

public class Player : Entity
{
    public Player(int x, int y)
    {
        Name = "Player";
        X = x;
        Y = y;
    }

    public void Move(int dx, int dy)
    {
        X += dx;
        Y += dy;
    }

}
