using PathOfSolace.World;
using PathOfSolace.Entities;
using System.Collections.Generic;
namespace PathOfSolace.Core;
public class GameContext
{
    public Map Map { get; private set; }
    public int MapID { get; private set; }
    public Player Player { get; }
    public List<Enemy> Enemies { get; } = new();

    public TurnState TurnState { get; set; }

    public GameContext(Map map, Player player)
    {
        Map = map;
        MapID = 0;
        Player = player;
        TurnState = TurnState.Player;
    }

    public void SetMap(Map map, int mapID)
    {
        Map = map;
        MapID = mapID;
    }
}