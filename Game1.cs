using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NSGameState;
using states;
using SpriteList;
using System;
using GameWindow;
using Global;
using SoundList;
using Microsoft.Xna.Framework.Audio;
using EffectsList;

namespace JXSilver;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Rectangle screen_rect;

    private RenderTarget2D _mainTarget;

    private Stack<GameState> states;
    public Game1()
    {
        WindowProperties.window_width = 192;
        WindowProperties.window_height = 400;
        _graphics = new GraphicsDeviceManager(this);
        this._graphics.PreferredBackBufferWidth = 192;
        this._graphics.PreferredBackBufferHeight = 400;
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        this.Window.AllowUserResizing = true;
        this._graphics.ApplyChanges();
        this.Window.ClientSizeChanged += new EventHandler<EventArgs>(OnClientSizeChanged);

        //We will be managing game scenes using states.
        this.states = new Stack<GameState>();
        GlobalStuff.gameStateStack = this.states;

    }

    private void OnClientSizeChanged(object sender, EventArgs e)
    {
           setRectangleDestination();
    }

    public void setRectangleDestination()
    {
        var screenSize = GraphicsDevice.PresentationParameters.Bounds;

        float scaleX = (float)screenSize.Width / _mainTarget.Width;
        float scaleY = (float)screenSize.Height / _mainTarget.Height;
        float scale = Math.Min(scaleX, scaleY);

        int newWidth = (int)(_mainTarget.Width * scale);
        int newHeight = (int)(_mainTarget.Height * scale);

        int posX = (screenSize.Width - newWidth) / 2;
        int posY = (screenSize.Height - newHeight) / 2;
        this.screen_rect = new Rectangle(posX, posY, newWidth, newHeight);

    }

    protected override void Initialize()
    {
        
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        //Load sprites.
        SpriteLibrary.player_tex = Content.Load<Texture2D>("MandragoraSmall");
        SpriteLibrary.logo_tex = Content.Load<Texture2D>("PixelPaintMeteorSprite");
        SpriteLibrary.game_font = Content.Load<SpriteFont>("GameFont");
        SpriteLibrary.bullet_atlas = Content.Load<Texture2D>("JXMiniBullets");
        SpriteLibrary.player2_tex = Content.Load<Texture2D>("CopernicusSmall");
        SpriteLibrary.player3_tex = Content.Load<Texture2D>("Mondragon");
        SpriteLibrary.player4_tex = Content.Load<Texture2D>("Pottenger");
        SpriteLibrary.orangebox_tex = Content.Load<Texture2D>("OrangeboxSmall");
        SpriteLibrary.biviolum_tex = Content.Load<Texture2D>("BiviolumSmall");
        SpriteLibrary.display_bar_tex = Content.Load<Texture2D>("JXDisplayBar");
        SpriteLibrary.explosion = Content.Load<Texture2D>("JXSilverExplosion1");

        //Load sounds.
        SoundLibrary.sounds.Add("mand_shoot_sound", Content.Load<SoundEffect>("Sounds/PlayerShoot1"));
        SoundLibrary.sounds.Add("coper_shoot_sound", Content.Load<SoundEffect>("Sounds/PlayerShoot2"));
        SoundLibrary.sounds.Add("mon_shoot_sound", Content.Load<SoundEffect>("Sounds/PlayerShoot3"));
        SoundLibrary.sounds.Add("pott_shoot_sound", Content.Load<SoundEffect>("Sounds/PlayerShoot4"));
        SoundLibrary.sounds.Add("enemy_hurt", Content.Load<SoundEffect>("Sounds/EnemyHit"));
        SoundLibrary.sounds.Add("enemy_die", Content.Load<SoundEffect>("Sounds/EnemyDie1"));

        //Load effects.
        EffectsLibrary.flash_effect = Content.Load<Effect>("Effects/FlashShader");
        
        this._mainTarget = new RenderTarget2D(GraphicsDevice, WindowProperties.window_width, WindowProperties.window_height);
        setRectangleDestination();

        
        FirstState s = new FirstState();
        states.Push(s);
        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        states.Peek().update(dt);
        states.Peek().collide();
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        
        GraphicsDevice.SetRenderTarget(_mainTarget);
        GraphicsDevice.Clear(Color.Olive);
        
        
        //Draw the main game.
        states.Peek().draw(this._spriteBatch);

        GraphicsDevice.SetRenderTarget(null);
        GraphicsDevice.Clear(Color.Black);
        
        _spriteBatch.Begin();
        _spriteBatch.Draw(_mainTarget, this.screen_rect, Color.White);
        _spriteBatch.End();

        // TODO: Add your drawing code here
        
        base.Draw(gameTime);
    }
}
