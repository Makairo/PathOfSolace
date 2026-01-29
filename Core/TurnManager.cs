using PathOfSolace.Entities;
using Microsoft.Xna.Framework.Input;
using PathOfSolace.World;
using PathOfSolace.Core;
using System;
using Microsoft.Xna.Framework;
namespace PathOfSolace.Core;

public static class TurnManager
{
    private static GameContext context;

    public static void Init(GameContext gameContext)
    {
        context = gameContext;
    }
    public static void Update()
    {
        switch(context.TurnState)
        {
            case TurnState.Player:
                RunPlayerTurn();
                break;
            case TurnState.AI:
                RunAITurn();
                break;
        }
    }

    public static void RunPlayerTurn()
    {
        if (Input.IsWaitPressed())
        {
            EndPlayerTurn();
            return;
        }

        if (!Input.GetMovementInput(out int dx, out int dy))
            return;

        bool acted = MovementSystem.TryMove(context.Player, dx, dy, context);

        if (acted)
            EndPlayerTurn();
    }


    public static void RunAITurn()
    {
        foreach (Enemy enemy in context.Enemies)
        {
            if (!enemy.Alive)
                continue;

            RunEnemyAI(enemy);
        }
        EndAITurn();
    }

    public static void RunEnemyAI(Enemy enemy)
    {
        enemy.VisionCheck(context.Player);

        if (enemy.Aggro)
            RunAggroAI(enemy);
        else
            RunIdleAI(enemy);
    }

    private static void RunAggroAI(Enemy enemy)
    {
        int dx = Math.Sign(context.Player.X - enemy.X);
        int dy = Math.Sign(context.Player.Y - enemy.Y);

        MovementSystem.TryMove(enemy, dx, dy, context);
    }

    public static void RunIdleAI(Enemy enemy)
    {
        // do nothing
    }

    public static void EndPlayerTurn()
    {
        context.TurnState = TurnState.AI;
    }

    public static void EndAITurn()
    {
        context.TurnState = TurnState.Player;
    }
    public static void SetMap(Map map, int ID)
    {
        context.SetMap(map, ID);
    }
}