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
    private Player _player;
    private Enemy _enemy;
    private Map _map;
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
        _map = new Map(25, 15);
        _player = new Player(5,5);
        _enemy = new Enemy(10,5);
        TurnManager.SetPlayer(_player);
        TurnManager.SetEnemy(_enemy);
        TurnManager.SetMap(_map);

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

        //Draw the map.
        for(int x  = 0 ; x < _map.Width ; x++)
        {
            for(int y = 0 ; y < _map.Height ; y++)
            {
                Color color = _map.Tiles[x,y].IsWalkable ? Color.DarkSlateGray : Color.Gray;

                _spriteBatch.Draw(GetPixel(), new Rectangle( x * TILE_SIZE, y * TILE_SIZE, TILE_SIZE, TILE_SIZE), color);
            }
        }

        // Draw enemy(ies)
        _spriteBatch.Draw(
            GetPixel(),
            new Microsoft.Xna.Framework.Rectangle(_enemy.X * TILE_SIZE, _enemy.Y * TILE_SIZE, TILE_SIZE, TILE_SIZE),
            Microsoft.Xna.Framework.Color.Red
        );

        //Draw player
        _spriteBatch.Draw(GetPixel(), new Rectangle(_player.X * TILE_SIZE, _player.Y * TILE_SIZE, TILE_SIZE, TILE_SIZE), Color.Green);

        
        //Stop Drawing
        _spriteBatch.End();
        base.Draw(gameTime);
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
