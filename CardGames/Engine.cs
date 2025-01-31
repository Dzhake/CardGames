using CardGames.Console;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoPlus.Graphics;
using MonoPlus.Input;
using MonoPlus.Time;
using SDL3;

namespace CardGames;


public class Engine : Game
{
    public static Engine? Instance;
    public static GameWindow? gameWindow;
    public static SpriteFont? Font;

    public static float WindowWidth => gameWindow?.ClientBounds.Width ?? 0;
    public static float WindowHeight => gameWindow?.ClientBounds.Height ?? 0;

    public Engine()
    {
        Instance = this;
        Content.RootDirectory = "Content";
        gameWindow = Window;
        Window.AllowUserResizing = true;
        Graphics.OnGameCreated(this);
        SDL.SDL_Init(SDL.SDL_InitFlags.SDL_INIT_VIDEO);
    }

    protected override void Initialize()
    {
        Graphics.Initialize(this);
        Input.Initialize(this);
        Shell.Initialize();
        base.Initialize();
    }

    protected void LoadDefault()
    {
        Font = Content.Load<SpriteFont>("Fonts/EditUndo");
    }

    protected override void LoadContent()
    {
        LoadDefault();
    }

    protected override void Update(GameTime gameTime)
    {
        Time.DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        Time.TotalTime = (float)gameTime.TotalGameTime.TotalSeconds;
        Input.Update();
        Shell.Update();
        Input.PostUpdate();
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        Shell.Draw();
        base.Draw(gameTime);
    }
}