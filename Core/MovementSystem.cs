using PathOfSolace.Entities;
using PathOfSolace.World;
using Microsoft.Xna.Framework;
namespace PathOfSolace.Core;

public class MovementSystem
{
    public static bool TryMove(Entity entity, int dx, int dy, GameContext context)
    {
        int nx = entity.X + dx;
        int ny = entity.Y + dy;

        if(!context.Map.Inbounds(nx, ny)) return false;

        Tile Target = context.Map.getTile(nx, ny);

        if (!Target.IsWalkable) return false;

        // Occupied tile
        if (Target.Occupant != null && Target.Occupant.Alive)
        {
            HandleInteraction(entity, Target.Occupant, context);
            return true; // interaction consumes turn
        }

        context.Map.MoveEntity(entity, nx, ny);
        Log.Write($"{entity.Name} moved to [{nx},{ny}]");
        return true;
    }

    public static void HandleInteraction(Entity entity, Entity other, GameContext Context)
    {
        Log.Write($"Interaction Attempted. [{entity.Name} -> {other.Name}]");
        switch (other.Relationship)
        {
            case Relationship.Hostile:
                CombatSystem.Resolve(entity, other);
                break;

            case Relationship.Friendly:
            case Relationship.Neutral:
                // Optional: swap / push / block
                break;
        }
    }
}