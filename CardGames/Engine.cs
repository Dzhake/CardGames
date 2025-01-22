using CardGames.Console;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoPlus.Input;

namespace CardGames;


public class Engine : Game
{
    public static Engine Instance;
    public static Drawer Drawer;

    public static GraphicsDeviceManager GraphicsManager;
    public static SpriteBatch SpriteBatch;
    public static GraphicsDevice Graphics;

    public static float DeltaTime;
    public static float TotalTime;

    public Engine()
    {
        Instance = this;
        GraphicsManager = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        Drawer = new();
    }

    protected override void Initialize()
    {
        Input.Initialize();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        SpriteBatch = new SpriteBatch(GraphicsDevice);
    }

    protected override void Update(GameTime gameTime)
    {
        DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        TotalTime = (float)gameTime.TotalGameTime.TotalSeconds;
        Input.Update();
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        Drawer.Draw();
        base.Draw(gameTime);
    }
}