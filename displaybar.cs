using System;
using System.Drawing;
using Microsoft.Xna.Framework.Graphics;
using SpriteList;

namespace HUD;

class Bars
{
    public static void draw_bar(SpriteBatch spriteBatch, int at_x, int at_y, int bar_length, int progress)
    {
        for (int i = 0; i < bar_length; i++)
        {
            spriteBatch.Draw(SpriteLibrary.display_bar_tex,
            new Microsoft.Xna.Framework.Rectangle(at_x + i, at_y, 1, 2), new Microsoft.Xna.Framework.Rectangle(0, 7, 1, 2),
            Microsoft.Xna.Framework.Color.White);
        }

        for (int i = 0; i < progress; i++)
        {
            spriteBatch.Draw(SpriteLibrary.display_bar_tex,
            new Microsoft.Xna.Framework.Rectangle(at_x + i, at_y, 1, 2), new Microsoft.Xna.Framework.Rectangle(2, 7, 1, 2),
            Microsoft.Xna.Framework.Color.White);
        }

    }

    public static void draw_bar2(SpriteBatch spriteBatch, int at_x, int at_y, int bar_length, int progress)
    {
        for (int i = 0; i < bar_length; i++)
        {
            spriteBatch.Draw(SpriteLibrary.display_bar_tex,
            new Microsoft.Xna.Framework.Rectangle(at_x + i, at_y, 5, 5), new Microsoft.Xna.Framework.Rectangle(0, 11, 5, 5),
            Microsoft.Xna.Framework.Color.White);
        }

        for (int i = 0; i < progress; i++)
        {
            spriteBatch.Draw(SpriteLibrary.display_bar_tex,
            new Microsoft.Xna.Framework.Rectangle(at_x + i, at_y, 5, 5), new Microsoft.Xna.Framework.Rectangle(0, 16, 5, 5),
            Microsoft.Xna.Framework.Color.White);
        }

    }

    public static void draw_bar3(SpriteBatch spriteBatch, int at_x, int at_y, int bar_length, int progress)
    {
        for (int i = 0; i < bar_length; i++)
        {
            spriteBatch.Draw(SpriteLibrary.display_bar_tex,
            new Microsoft.Xna.Framework.Rectangle(at_x + i, at_y, 3, 3), new Microsoft.Xna.Framework.Rectangle(0, 3, 3, 3),
            Microsoft.Xna.Framework.Color.White);
        }

        for (int i = 0; i < progress; i++)
        {
            spriteBatch.Draw(SpriteLibrary.display_bar_tex,
            new Microsoft.Xna.Framework.Rectangle(at_x + i, at_y, 3, 3), new Microsoft.Xna.Framework.Rectangle(3, 0, 3, 3),
            Microsoft.Xna.Framework.Color.White);
        }

    }
}

