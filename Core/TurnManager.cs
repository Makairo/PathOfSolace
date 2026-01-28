using PathOfSolace.Entities;
using Microsoft.Xna.Framework.Input;
using PathOfSolace.World;
using PathOfSolace.Combat;
using PathOfSolace.Core;
namespace PathOfSolace.Core;

public static class TurnManager
{
    public static TurnState State { get; private set; } = TurnState.Player;

    private static Map _mapRef;
    private static Player _playerRef;
    private static Enemy _enemyRef;

    public static void Update()
    {
        switch(State)
        {
            case TurnState.Player:
            // player turn
                RunPlayerTurn();
                //State = TurnState.AI; // Wait until player has made a move.
                break;

            case TurnState.AI:
                RunAITurn();
                State = TurnState.Player;
                break;
        }
    }

    public static void EndPlayerTurn()
    {
        State = TurnState.AI;
    }

    public static void EndAITurn()
    {
        State = TurnState.Player;
    }

    public static void SetPlayer(Player player)
    {
        _playerRef = player;
    }
    public static void SetMap(Map map)
    {
        _mapRef = map;
    }

    public static void SetEnemy(Enemy enemy)
    {
        _enemyRef = enemy;
    }

    public static void RunPlayerTurn()
    {
        int dx = 0;
        int dy = 0;

        if(Input.KeyWasPressed(Keys.NumPad5))       { EndPlayerTurn();  }     
        else if (Input.KeyWasPressed(Keys.NumPad1)) { dx = -1; dy =  1; } // Move SW
        else if (Input.KeyWasPressed(Keys.NumPad2)) { dx =  0; dy =  1; } // Move S 
        else if (Input.KeyWasPressed(Keys.NumPad3)) { dx =  1; dy =  1; } // Move SE
        else if (Input.KeyWasPressed(Keys.NumPad4)) { dx = -1; dy =  0; } // Move W 
        else if (Input.KeyWasPressed(Keys.NumPad6)) { dx =  1; dy =  0; } // Move E 
        else if (Input.KeyWasPressed(Keys.NumPad7)) { dx = -1; dy = -1; } // Move NW 
        else if (Input.KeyWasPressed(Keys.NumPad8)) { dx =  0; dy = -1; } // Move N
        else if (Input.KeyWasPressed(Keys.NumPad9)) { dx =  1; dy = -1; } // Move NE
        
        if (dx != 0 || dy != 0)
        {
            int nx = _playerRef.X + dx;
            int ny = _playerRef.Y + dy;

            Tile Target = _mapRef.getTile(nx, ny);
            Tile CurTile = _mapRef.getTile(_playerRef.X, _playerRef.Y);

            if(!Target.IsWalkable) { /* do nothing */ }
            
            if(Target.Occupant != null)
            {
                // Interaction code
                if(Target.Occupant.Relationship == Relationship.Friendly)
                {
                    // Swap Positions
                }
                if(Target.Occupant.Relationship == Relationship.Neutral)
                {
                    // Swap Positions
                }
                if(Target.Occupant.Relationship == Relationship.Hostile)
                {
                    // Attack
                    Combat.Combat.Resolve(_playerRef, Target.Occupant);
                }
            }
            
            if (_mapRef.IsWalkable(nx, ny))
            {
                CurTile.RemOccupant();
                _playerRef.Move(dx, dy);
                Target.SetOccupant(_playerRef);
                Log.Write($"Player moved to {_playerRef.X},{_playerRef.Y}");
                EndPlayerTurn();
            }
        }
    }

    public static void RunAITurn()
    {
        if(_enemyRef == null || !_enemyRef.Alive) return;

        int dx = -1;
        int dy = 0;
        int nx = _enemyRef.X + dx;
        int ny = _enemyRef.Y + dy;

        Tile Target = _mapRef.getTile(nx, ny);
        Tile CurTile = _mapRef.getTile(_enemyRef.X, _enemyRef.Y);

        CurTile.RemOccupant();
        Target.SetOccupant(_enemyRef);

        // naive collision: just stay in bounds 1..Width-2
        if (nx >= 1)
        {
            _enemyRef.Move(dx, dy);
            Log.Write($"Enemy moved to {_enemyRef.X},{_enemyRef.Y}");
        }
    }
}