using PathOfSolace.Entities;
using PathOfSolace.Core;
namespace PathOfSolace.Combat;

public static class Combat
{
    public static void Resolve(Entity A, Entity B)
    {
        // A attacks B
        B.Die();
        Log.Write("Attack Resolved.");
        Log.Write("Enemy Status now Alive: " + B.Alive);
    }
}