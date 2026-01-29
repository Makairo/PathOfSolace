using PathOfSolace.Entities;
using PathOfSolace.Core;
namespace PathOfSolace.Core;

public static class CombatSystem
{
    public static void Resolve(Entity A, Entity B)
    {
        // A attacks B
        B.Die();
        Log.Write("Attack Resolved.");
        Log.Write("Enemy Status now Alive: " + B.Alive);
    }
}