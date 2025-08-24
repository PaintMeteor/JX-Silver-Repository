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
using EffectsList;
using Tools;

namespace states;

public class SplashScreen : GameState
{
    public event EventHandler finished;
    private float duration = 4;
    private float time_passed = 0;

    private float[] alpha = new float[3] { 1, 1, 1.44f };
    private float[] reveal = new float[3] { 0, 1, 2 };
    private Texture2D logo;


    public void init()
    {
        GlobalStuff.resources.Clear();
    }

    public override void update(float dt)
    {

        this.time_passed += dt;

        reveal[0] = FunctionLibrary.moveToward(reveal[0], reveal[1], reveal[2] * dt);
        alpha[0] = FunctionLibrary.moveToward(alpha[0], alpha[1], alpha[2] * dt);

        if (this.time_passed > 2.1)
        {
            alpha[1] = 0;
        }

        if (this.time_passed >= this.duration)
        {

            this.finished?.Invoke(this, EventArgs.Empty);
        }
    }

    public override void draw(SpriteBatch spriteBatch)
    {
        Effect effect = EffectsLibrary.effects.Clone();

        effect.Parameters["reveal_amount"].SetValue(reveal[0]);
        effect.Parameters["alpha"].SetValue(alpha[0]);
        spriteBatch.Begin(samplerState: SamplerState.PointClamp, effect: effect, blendState: BlendState.NonPremultiplied);
        spriteBatch.Draw(this.logo, new Microsoft.Xna.Framework.Vector2((WindowProperties.window_width / 2) - 32, (WindowProperties.window_height / 2) - 32), Color.White);
        spriteBatch.End();
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
        GlobalStuff.gameStateStack.Peek().Init();
    }
}