using NSGameState;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Player;
using System.Collections.Generic;
using SpriteList;
using System.IO;
using System;
using System.Numerics;
using GameInput;
using System.Drawing;
using Microsoft.Xna.Framework;
using GameWindow;
using Global;
using System.ComponentModel;
using Tools;
using System.Net.NetworkInformation;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Emit;
using Microsoft.Xna.Framework.Audio;
using SoundList;
using System.Threading;
using System.Security.Cryptography.X509Certificates;

namespace Sprite;

public class Sprite()
{
    public static void renderSprite(SpriteBatch spriteBatch, Dictionary<string, object> a, Effect effect_to_use)
    {
        bool has_sprite = a.ContainsKey("sprite");
        bool has_rotation = a.ContainsKey("rotation");
        bool has_position = a.ContainsKey("position");
        bool has_scroll = a.ContainsKey("scroll_allowed");
        bool has_size = a.ContainsKey("size");
        bool has_tilt_frames = a.ContainsKey("tilt_frame");
        bool has_loop_animation = a.ContainsKey("loop_animation");
        bool has_score_display = a.ContainsKey("display_score");
        bool score_in_resources = GlobalStuff.resources.ContainsKey("score");
        bool has_health_bar = a.ContainsKey("health_bar");
        bool has_flash_effect = a.ContainsKey("hurt_flash");
        bool has_HP = a.ContainsKey("HP");
        bool has_particle_color_curve = a.ContainsKey("particle_color_curve");
        bool has_lifetime = a.ContainsKey("lifetime");
        bool has_particle_size_curve = a.ContainsKey("particle_size_curve");
        bool has_hp_decay_effect = a.ContainsKey("hp_decay_effect");
        bool has_ninepatch = a.ContainsKey("ninepatch");

        //Draw sprite.
        if (has_sprite && has_position)
        {

            float[] pos = (float[])a["position"];
            float rot = 0;
            float[] cam_offset = new float[2] { 0, 0 };
            if (has_rotation)
            {
                rot = (float)a["rotation"];
            }

            if (has_scroll)
            {
                if ((bool)a["scroll_allowed"] == true) { cam_offset = new float[2] { GlobalStuff.view[0], GlobalStuff.view[1] }; }
            }

            string sprite = (string)a["sprite"];

            //Mandragora.
            if (sprite == "mandragora_sprite")
            {
                spriteBatch.Begin(samplerState: SamplerState.PointClamp, effect: effect_to_use, blendState: BlendState.NonPremultiplied);
                SpriteEffects[] effects = [SpriteEffects.None, SpriteEffects.None, SpriteEffects.FlipHorizontally];
                int frame_idx = 0;
                if (has_tilt_frames)
                {
                    frame_idx = (int)a["tilt_frame"];
                }



                spriteBatch.Draw(SpriteLibrary.player_tex,
                new Microsoft.Xna.Framework.Rectangle(
                (int)pos[0] - (int)cam_offset[0], (int)pos[1] - (int)cam_offset[1], 16, 16),
                new Microsoft.Xna.Framework.Rectangle(Math.Abs(Math.Sign(frame_idx)) * 16, 0, 16, 16),
                Microsoft.Xna.Framework.Color.White, rot + (float)Math.PI / 2,
                new Microsoft.Xna.Framework.Vector2(8, 8),
                effects[frame_idx + 1], layerDepth: 1);
                spriteBatch.End();
            }

            //Copernicus
            if (sprite == "copernicus_sprite")
            {
                spriteBatch.Begin(samplerState: SamplerState.PointClamp, effect: effect_to_use, blendState: BlendState.NonPremultiplied);
                SpriteEffects[] effects = [SpriteEffects.None, SpriteEffects.None, SpriteEffects.FlipHorizontally];
                int frame_idx = 0;
                if (has_tilt_frames)
                {
                    frame_idx = (int)a["tilt_frame"];
                }



                spriteBatch.Draw(SpriteLibrary.player2_tex,
                new Microsoft.Xna.Framework.Rectangle(
                (int)pos[0] - (int)cam_offset[0], (int)pos[1] - (int)cam_offset[1], 16, 16),
                new Microsoft.Xna.Framework.Rectangle(Math.Abs(Math.Sign(frame_idx)) * 16, 0, 16, 16),
                Microsoft.Xna.Framework.Color.White, rot + (float)Math.PI / 2,
                new Microsoft.Xna.Framework.Vector2(8, 8),
                effects[frame_idx + 1], layerDepth: 1);
                spriteBatch.End();
            }

            //Mondragon.
            if (sprite == "mondragon_sprite")
            {
                spriteBatch.Begin(samplerState: SamplerState.PointClamp, effect: effect_to_use, blendState: BlendState.NonPremultiplied);
                SpriteEffects[] effects = [SpriteEffects.None, SpriteEffects.None, SpriteEffects.FlipHorizontally];
                int frame_idx = 0;
                if (has_tilt_frames)
                {
                    frame_idx = (int)a["tilt_frame"];
                }



                spriteBatch.Draw(SpriteLibrary.player3_tex,
                new Microsoft.Xna.Framework.Rectangle(
                (int)pos[0] - (int)cam_offset[0], (int)pos[1] - (int)cam_offset[1], 16, 16),
                new Microsoft.Xna.Framework.Rectangle(Math.Abs(Math.Sign(frame_idx)) * 16, 0, 16, 16),
                Microsoft.Xna.Framework.Color.White, rot + (float)Math.PI / 2,
                new Microsoft.Xna.Framework.Vector2(8, 8),
                effects[frame_idx + 1], layerDepth: 1);
                spriteBatch.End();
            }

            if (sprite == "pottenger_sprite")
            {
                spriteBatch.Begin(samplerState: SamplerState.PointClamp, effect: effect_to_use, blendState: BlendState.NonPremultiplied);
                SpriteEffects[] effects = [SpriteEffects.None, SpriteEffects.None, SpriteEffects.FlipHorizontally];
                int frame_idx = 0;
                if (has_tilt_frames)
                {
                    frame_idx = (int)a["tilt_frame"];
                }



                spriteBatch.Draw(SpriteLibrary.player4_tex,
                new Microsoft.Xna.Framework.Rectangle(
                (int)pos[0] - (int)cam_offset[0], (int)pos[1] - (int)cam_offset[1], 16, 16),
                new Microsoft.Xna.Framework.Rectangle(Math.Abs(Math.Sign(frame_idx)) * 16, 0, 16, 16),
                Microsoft.Xna.Framework.Color.White, rot + (float)Math.PI / 2,
                new Microsoft.Xna.Framework.Vector2(8, 8),
                effects[frame_idx + 1], layerDepth: 1);
                spriteBatch.End();
            }

            //Orangebox.
            if (sprite == "orangebox_sprite")
            {
                spriteBatch.Begin(samplerState: SamplerState.PointClamp, effect: effect_to_use, blendState: BlendState.NonPremultiplied);
                spriteBatch.Draw(SpriteLibrary.orangebox_tex,
                new Microsoft.Xna.Framework.Rectangle(
                (int)pos[0] - (int)cam_offset[0], (int)pos[1] - (int)cam_offset[1], 12, 8),
                new Microsoft.Xna.Framework.Rectangle(0, 0, 12, 8),
                Microsoft.Xna.Framework.Color.White, rot + (float)Math.PI / 2,
                new Microsoft.Xna.Framework.Vector2(6, 4), SpriteEffects.None, layerDepth: 1);
                spriteBatch.End();
            }

            //Biviolum
            if (sprite == "biviolum_sprite")
            {
                spriteBatch.Begin(samplerState: SamplerState.PointClamp, effect: effect_to_use, blendState: BlendState.NonPremultiplied);
                spriteBatch.Draw(SpriteLibrary.biviolum_tex,
                new Microsoft.Xna.Framework.Rectangle(
                (int)pos[0] - (int)cam_offset[0], (int)pos[1] - (int)cam_offset[1], 20, 16),
                new Microsoft.Xna.Framework.Rectangle(0, 0, 20, 16),
                Microsoft.Xna.Framework.Color.White, rot + (float)Math.PI / 2,
                new Microsoft.Xna.Framework.Vector2(10, 8),
                SpriteEffects.None, layerDepth: 1);
                spriteBatch.End();
            }

            //Bullets of the Mandragora.
            if (sprite == "bullets_01")
            {
                spriteBatch.Begin(samplerState: SamplerState.PointClamp, effect: effect_to_use, blendState: BlendState.NonPremultiplied);
                //Process the animation.
                int frame = 0;
                if (has_loop_animation)
                {
                    float[] anim_loop = (float[])a["loop_animation"];
                    frame = (int)anim_loop[0];
                }


                spriteBatch.Draw(SpriteLibrary.bullet_atlas,
                new Microsoft.Xna.Framework.Rectangle(
                (int)pos[0] - (int)cam_offset[0], (int)pos[1] - (int)cam_offset[1], 6, 10),
                new Microsoft.Xna.Framework.Rectangle(frame * 6, 32, 6, 10),
                Microsoft.Xna.Framework.Color.White, rot + (float)Math.PI / 2,
                new Microsoft.Xna.Framework.Vector2(3, 5),
                SpriteEffects.None, layerDepth: 1);
                spriteBatch.End();
            }

            //Bullets of the Copernicus.
            if (sprite == "bullets_02")
            {
                spriteBatch.Begin(samplerState: SamplerState.PointClamp, effect: effect_to_use, blendState: BlendState.NonPremultiplied);
                spriteBatch.Draw(SpriteLibrary.bullet_atlas,
                new Microsoft.Xna.Framework.Rectangle(
                (int)pos[0] - (int)cam_offset[0], (int)pos[1] - (int)cam_offset[1], 5, 7),
                new Microsoft.Xna.Framework.Rectangle(0, 8, 5, 7),
                Microsoft.Xna.Framework.Color.White, rot + (float)Math.PI / 2,
                new Microsoft.Xna.Framework.Vector2(3, 7),
                SpriteEffects.None, layerDepth: 1);
                spriteBatch.End();
            }

            //Bullets of the Mondragon.
            if (sprite == "bullets_03")
            {
                spriteBatch.Begin(samplerState: SamplerState.PointClamp, effect: effect_to_use, blendState: BlendState.NonPremultiplied);
                spriteBatch.Draw(SpriteLibrary.bullet_atlas,
                new Microsoft.Xna.Framework.Rectangle(
                (int)pos[0] - (int)cam_offset[0], (int)pos[1] - (int)cam_offset[1], 8, 10),
                new Microsoft.Xna.Framework.Rectangle(33, 0, 8, 10),
                Microsoft.Xna.Framework.Color.White, rot + (float)Math.PI / 2,
                new Microsoft.Xna.Framework.Vector2(4, 5),
                SpriteEffects.None, layerDepth: 1);
                spriteBatch.End();
            }

            //Bullets of the Pottenger.
            if (sprite == "bullets_04")
            {

                //Process the animation.
                int frame = 0;
                if (has_loop_animation)
                {
                    float[] anim_loop = (float[])a["loop_animation"];
                    frame = (int)anim_loop[0];
                }

                spriteBatch.Begin(samplerState: SamplerState.PointClamp, effect: effect_to_use, blendState: BlendState.NonPremultiplied);
                spriteBatch.Draw(SpriteLibrary.bullet_atlas,
                new Microsoft.Xna.Framework.Rectangle(
                (int)pos[0] - (int)cam_offset[0], (int)pos[1] - (int)cam_offset[1], 10, 12),
                new Microsoft.Xna.Framework.Rectangle(42 + (frame * 10), 0, 10, 12),
                Microsoft.Xna.Framework.Color.White, rot + (float)Math.PI / 2,
                new Microsoft.Xna.Framework.Vector2(5, 6),
                SpriteEffects.None, layerDepth: 1);
                spriteBatch.End();
            }

            //Fire particles.
            if (sprite == "explosion_particle")
            {

                int coords = 0;
                if (has_loop_animation)
                {
                    float[] loop = (float[])a["loop_animation"];
                    coords = (int)loop[0];
                }


                spriteBatch.Begin(samplerState: SamplerState.PointClamp, effect: effect_to_use, blendState: BlendState.NonPremultiplied);
                spriteBatch.Draw(SpriteLibrary.explosion,
                new Microsoft.Xna.Framework.Rectangle(
                (int)pos[0] - (int)cam_offset[0], (int)pos[1] - (int)cam_offset[1], 24, 24),
                new Microsoft.Xna.Framework.Rectangle(coords * 24, 0, 24, 24),
                Microsoft.Xna.Framework.Color.White, rot + (float)Math.PI / 2,
                new Microsoft.Xna.Framework.Vector2(12, 12), SpriteEffects.None, layerDepth: 1);
                spriteBatch.End();
            }
            
            //Enemy bullet.
            if (sprite == "en_bullet")
            {
                int[] frame_offsets = new int[4] {0, 1, 2, 1};
                float[] loop = new float[3] { 0, 0, 0 };

                if (has_loop_animation) { loop = (float[])a["loop_animation"]; }

                int coords = (int)loop[0];


                spriteBatch.Begin(samplerState: SamplerState.PointClamp, effect: effect_to_use, blendState: BlendState.NonPremultiplied);
                spriteBatch.Draw(SpriteLibrary.bullet_atlas,
                new Microsoft.Xna.Framework.Rectangle(
                (int)pos[0] - (int)cam_offset[0], (int)pos[1] - (int)cam_offset[1], 6, 6),
                new Microsoft.Xna.Framework.Rectangle(48 + frame_offsets[coords] * 6, 32, 6, 6),
                Microsoft.Xna.Framework.Color.White, rot + (float)Math.PI / 2,
                new Microsoft.Xna.Framework.Vector2(3, 3), SpriteEffects.None, layerDepth: 1);
                spriteBatch.End();
            }

        }
    }
}