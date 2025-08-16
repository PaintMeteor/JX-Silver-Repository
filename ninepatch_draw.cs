using System;
using System.Drawing;
using Microsoft.Xna.Framework.Graphics;
using SpriteList;

namespace HUD;

class Ninepatch
{
    public static void draw_rect(SpriteBatch spriteBatch, int at_x, int at_y, int size_x, int size_y, float[] color)
    {

        int size_min = 5;

        int y = Math.Max(1, size_y - size_min);
        int x = Math.Max(1, size_x - size_min);

        int ctr_x = x;
        int ctr_y = y;

        int ctr_w = x;
        int ctr_h = y;


        if (color.Length != 3) { throw new Exception("Element list of 'color' must be exactly 3"); }
        Microsoft.Xna.Framework.Color color1 = new Microsoft.Xna.Framework.Color(color[0], color[1], color[2]);

        //Top left.
        spriteBatch.Draw(SpriteLibrary.UI_tex,
            new Microsoft.Xna.Framework.Rectangle(at_x, at_y, 2, 2), new Microsoft.Xna.Framework.Rectangle(0, 16, 2, 2),
            color1);

        //Middle left.
        spriteBatch.Draw(SpriteLibrary.UI_tex,
            new Microsoft.Xna.Framework.Rectangle(at_x, at_y + 2, 2, y), new Microsoft.Xna.Framework.Rectangle(0, 18, 2, 1),
            color1);

        //Bottom left.
        spriteBatch.Draw(SpriteLibrary.UI_tex,
            new Microsoft.Xna.Framework.Rectangle(at_x, at_y + 2 + y, 2, 2), new Microsoft.Xna.Framework.Rectangle(0, 19, 2, 2),
            color1);

        //Top middle.
        spriteBatch.Draw(SpriteLibrary.UI_tex,
            new Microsoft.Xna.Framework.Rectangle(at_x + 2, at_y, x, 2), new Microsoft.Xna.Framework.Rectangle(2, 16, 1, 2),
            color1);

        //Center.
        spriteBatch.Draw(SpriteLibrary.UI_tex,
            new Microsoft.Xna.Framework.Rectangle(at_x + 2, at_y + 2, x, y), new Microsoft.Xna.Framework.Rectangle(2, 18, 1, 1),
            color1);

        //Bottom middle.
        spriteBatch.Draw(SpriteLibrary.UI_tex,
            new Microsoft.Xna.Framework.Rectangle(at_x + 2, at_y + 2 + y, x, 2), new Microsoft.Xna.Framework.Rectangle(2, 19, 1, 2),
            color1);

        //Top right.
        spriteBatch.Draw(SpriteLibrary.UI_tex,
            new Microsoft.Xna.Framework.Rectangle(at_x + 2 + x, at_y, 2, 2), new Microsoft.Xna.Framework.Rectangle(3, 16, 2, 2),
            color1);

        //Middle right.
        spriteBatch.Draw(SpriteLibrary.UI_tex,
            new Microsoft.Xna.Framework.Rectangle(at_x + 2 + x, at_y + 2, 2, y), new Microsoft.Xna.Framework.Rectangle(3, 18, 2, 1),
            color1);

        //Bottom right.
        spriteBatch.Draw(SpriteLibrary.UI_tex,
            new Microsoft.Xna.Framework.Rectangle(at_x + 2 + x, at_y + 2 + y, 2, 2), new Microsoft.Xna.Framework.Rectangle(3, 19, 2, 2),
            color1);
        

        

    }
}

