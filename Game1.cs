using System.Security.Cryptography;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PathOfSolace.Core;
using PathOfSolace.Entities;
using PathOfSolace.World;

namespace PathOfSolace;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private const int TILE_SIZE = 32;
    private GameContext context;
    private Texture2D _pixel;


    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        Log.Clear();
        Map map = new Map(25, 15);
        Player player = new Player(5,5);
        Enemy enemy = new Enemy("Test Enemy 1",10,5);
        Enemy enemy2 = new Enemy("Test Enemy 2",11,7);
        Enemy enemy3 = new Enemy("Test Enemy 3",12,9);

        map.getTile(5,5).SetOccupant(player);
        map.getTile(10,5).SetOccupant(enemy);
        
        context = new GameContext(map, player);
        context.Enemies.Add(enemy);
        context.Enemies.Add(enemy2);
        context.Enemies.Add(enemy3);

        TurnManager.Init(context);
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        Input.Update();
        //Log.Write("System Tick.");
        if (Input.KeyWasPressed(Keys.Escape))
        {
            Log.Write("Proper exit reached. Exiting game.");
            Exit();
        }

        TurnManager.Update(); // Runs each frame of the game.

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        _spriteBatch.Begin();

        DrawMap();
        DrawEntities();

        _spriteBatch.End();
        base.Draw(gameTime);
    }

    private void DrawMap()
    {
        for (int x = 0; x < context.Map.Width; x++)
        {
            for (int y = 0; y < context.Map.Height; y++)
            {
                Tile tile = context.Map.Tiles[x, y];

                Color color = tile.IsWalkable
                    ? Color.DarkSlateGray
                    : Color.Gray;

                _spriteBatch.Draw(
                    GetPixel(),
                    new Rectangle(
                        x * TILE_SIZE,
                        y * TILE_SIZE,
                        TILE_SIZE,
                        TILE_SIZE),
                    color
                );
            }
        }
    }

    private void DrawEntities()
    {
        // Draw enemies
        foreach (var enemy in context.Enemies)
        {
            if (!enemy.Alive)
                continue;

            _spriteBatch.Draw(
                GetPixel(),
                new Rectangle(
                    enemy.X * TILE_SIZE,
                    enemy.Y * TILE_SIZE,
                    TILE_SIZE,
                    TILE_SIZE),
                Color.Red
            );
        }

        // Draw player last so they’re on top
        var player = context.Player;
        _spriteBatch.Draw(
            GetPixel(),
            new Rectangle(
                player.X * TILE_SIZE,
                player.Y * TILE_SIZE,
                TILE_SIZE,
                TILE_SIZE),
            Color.Green
        );
    }



    private Texture2D GetPixel()
    {
        if(_pixel == null)
        {
            _pixel = new Texture2D(GraphicsDevice, 1, 1);
            _pixel.SetData(new[] { Color.White });
        }
        return _pixel;
    }
}
