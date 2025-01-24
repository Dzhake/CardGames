using CardGames.Console;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoPlus.Input;

namespace CardGames;


public class Engine : Game
{
    public static Engine Instance;

    public static GraphicsDeviceManager graphicsManager;
    public static SpriteBatch spriteBatch;
    public static GraphicsDevice graphics;
    public static GameWindow gameWindow;

    public static float DeltaTime;
    public static float TotalTime;

    public static SpriteFont Font;

    public Engine()
    {
        Instance = this;
        graphicsManager = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        gameWindow = Window;
        Window.AllowUserResizing = true;
    }

    protected override void Initialize()
    {
        Input.Initialize(this);
        ConsoleCore.Initialize();
        base.Initialize();
    }

    protected void LoadDefault()
    {
        Font = Content.Load<SpriteFont>("EditUndo");
    }

    protected override void LoadContent()
    {
        graphics = graphicsManager.GraphicsDevice;
        spriteBatch = new SpriteBatch(GraphicsDevice);
        LoadDefault();
    }

    protected override void Update(GameTime gameTime)
    {
        DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        TotalTime = (float)gameTime.TotalGameTime.TotalSeconds;
        Input.Update();
        ConsoleCore.Update();
        Input.PostUpdate();
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        ConsoleCore.Draw();
        base.Draw(gameTime);
    }
}