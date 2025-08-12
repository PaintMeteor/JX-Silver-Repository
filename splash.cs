using NSGameState;
using MonoGame;
using System.Reflection;
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using SpriteList;
using GameWindow;
using Global;
using states;

namespace states;

public class SplashScreen : GameState
{
    public event EventHandler finished;
    private float duration = 2;
    private float time_passed = 0;
    private Texture2D logo;



    public void update(float dt)
    {
        
        this.time_passed += dt;
        if (this.time_passed >= this.duration)
        {
            
            this.finished?.Invoke(this, EventArgs.Empty);
        }
    }

    public void draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(this.logo, new Microsoft.Xna.Framework.Vector2((192 / 2) - 32, (400 / 2) - 32), Color.White);
    }

    public SplashScreen()
    {
        this.logo = SpriteLibrary.logo_tex;
        this.finished += this.onFinished;
    }

    private void onFinished(object sender, EventArgs e)
    {
        this.time_passed = 0;
        GlobalStuff.gameStateStack.Push(new FirstState());
    }
}