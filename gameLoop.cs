namespace GameProcess;

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
using GameWindow;
using Global;
using Tools;
using System.Reflection.Emit;
using Microsoft.Xna.Framework.Audio;
using SoundList;
using System.Threading;

public class LoopTools
{
    public static void update(float dt)
    {

        int actorLoopIDX = 0;
        foreach (Dictionary<string, object> a in GlobalStuff.sceneSpace.ToArray())
        {

            bool has_firing_sound = a.ContainsKey("firing_sound");
            bool has_input_controls = a.ContainsKey("input_controls");
            bool has_position = a.ContainsKey("position");
            bool has_topdown_movement = a.ContainsKey("topdown_movement");
            bool has_rotation = a.ContainsKey("rotation");
            bool has_tilt_frames = a.ContainsKey("tilt_frame");
            bool has_bounds = a.ContainsKey("bounds");
            bool has_lifetime = a.ContainsKey("lifetime");
            bool has_weapon = a.ContainsKey("weapon");
            bool has_custom_velocity = a.ContainsKey("custom_velocity");
            bool has_input_firing = a.ContainsKey("firing_input");
            bool has_loop_animation = a.ContainsKey("loop_animation");
            bool has_auto_fire = a.ContainsKey("fire_time");

            //Animation Loop;

            if (has_loop_animation)
            {
                float[] list = (float[])a["loop_animation"];
                list[0] += list[2] * dt;

                //Loop this around.
                list[0] = list[0] % list[1];

                a["loop_animation"] = new float[3] { list[0], list[1], list[2] };

            }

            //Velocity.
            if (has_custom_velocity && has_position)
            {
                float[] pos = (float[])a["position"];
                float[] vel = (float[])a["custom_velocity"];
                float[] normal = FunctionLibrary.normalize(vel[0], vel[1]);
                float vel_length = FunctionLibrary.length(vel[0], vel[1]);


                //Normalize.
                float nx = vel_length > 0 ? Math.Abs(normal[0]) * vel[0] : vel[0];
                float ny = vel_length > 0 ? Math.Abs(normal[1]) * vel[1] : vel[1];
                pos[0] += nx * dt;
                pos[1] += ny * dt;

                a["position"] = new float[2] { pos[0], pos[1] };


            }

            //Firing input.
            if (has_input_firing)
            {
                bool key_down = Keyboard.GetState().IsKeyDown(InputMap.keybinds["K_FIRE"]);
                bool[] input = (bool[])a["firing_input"];
                input[0] = key_down;
            }

            //Input controls.
            if (has_input_controls)
            {
                float left = 0;
                float right = 0;
                float up = 0;
                float down = 0;

                left = FunctionLibrary.boolToFloat(Keyboard.GetState().IsKeyDown(InputMap.keybinds["K_LEFT"]));
                right = FunctionLibrary.boolToFloat(Keyboard.GetState().IsKeyDown(InputMap.keybinds["K_RIGHT"]));
                up = FunctionLibrary.boolToFloat(Keyboard.GetState().IsKeyDown(InputMap.keybinds["K_UP"]));
                down = FunctionLibrary.boolToFloat(Keyboard.GetState().IsKeyDown(InputMap.keybinds["K_DOWN"]));


                a["input_controls"] = new float[2] { right - left, down - up };

                if (has_tilt_frames) { a["tilt_frame"] = (int)right - (int)left; }
            }

            //Movement.
            if (has_topdown_movement && has_position)
            {
                Dictionary<string, object> attributes = (Dictionary<string, object>)a["topdown_movement"];
                float[] input_control = new float[2] { 0, 0 };
                bool has_initial_speed = attributes.ContainsKey("initial_speed");
                bool has_acceleration = attributes.ContainsKey("acceleration");
                bool has_deceleration = attributes.ContainsKey("deceleration");


                //Use input controls.
                if (has_input_controls)
                {
                    input_control = (float[])a["input_controls"];
                    float[] pos = (float[])a["position"];
                    float[] normal = FunctionLibrary.normalize(input_control[0], input_control[1]);
                    float length = Math.Abs(normal[0]) + Math.Abs(normal[1]);
                    if (has_initial_speed && has_acceleration)
                    {
                        float[] initial = (float[])attributes["initial_speed"];
                        float acceleration = (float)attributes["acceleration"];
                        float speed = (float)attributes.GetValueOrDefault("speed", 0);


                        //X acceleration.
                        if (input_control[0] == 0)
                        {
                            if (has_deceleration)
                            {
                                initial[0] = FunctionLibrary.moveToward(initial[0], 0,
                                (float)attributes["deceleration"] * dt);
                            }
                            else
                            {
                                initial[0] = 0;
                            }
                        }
                        else
                        {
                            initial[0] += (float)normal[0] * acceleration * dt;
                        }

                        //Y acceleration.
                        if (input_control[1] == 0)
                        {
                            if (has_deceleration)
                            {
                                initial[1] = FunctionLibrary.moveToward(initial[1], 0,
                                (float)attributes["deceleration"] * dt);
                            }
                            else
                            {
                                initial[1] = 0;
                            }
                        }
                        else
                        {
                            initial[1] += (float)normal[1] * acceleration * dt;
                        }
                        float initial_length = (float)Math.Sqrt(initial[0] * initial[0] + initial[1] * initial[1]);
                        float[] initial_normal = FunctionLibrary.normalize(initial[0], initial[1]);

                        //Normalize Speed.
                        float nx = initial_length > 0 ? Math.Abs(initial_normal[0] * speed) : speed;
                        float ny = initial_length > 0 ? Math.Abs(initial_normal[1] * speed) : speed;


                        initial[0] = Math.Clamp((float)initial[0], -nx, nx);
                        initial[1] = Math.Clamp((float)initial[1], -ny, ny);



                        pos[0] += initial[0] * dt;
                        pos[1] += initial[1] * dt;

                        attributes["initial_speed"] = new float[2] { initial[0], initial[1] };

                        a["position"] = new float[2] { pos[0], pos[1] };
                    }
                    else
                    {
                        if (input_control[0] != 0 || input_control[1] != 0)
                        {
                            float speed = (float)attributes.GetValueOrDefault("speed", 0);

                            pos[0] += (float)normal[0] * speed * dt;
                            pos[1] += (float)normal[1] * speed * dt;

                            a["position"] = new float[2] { pos[0], pos[1] };
                        }
                    }

                }
            }
            //Bounds.
            if (has_bounds && has_position)
            {
                float[] bounds = (float[])a["bounds"];
                float[] pos = (float[])a["position"];

                pos[0] = Math.Clamp(pos[0], bounds[0], bounds[0] + bounds[2]);
                pos[1] = Math.Clamp(pos[1], bounds[1], bounds[1] + bounds[3]);

                a["position"] = new float[2] { pos[0], pos[1] };


            }

            //Lifetime.
            if (has_lifetime)
            {
                a["lifetime"] = (float)a["lifetime"] - dt;

                if ((float)a["lifetime"] <= 0)
                {
                    //Delete.
                    GlobalStuff.sceneSpace.RemoveAt(actorLoopIDX);
                }
            }

            //Weapon.
            if (has_weapon && has_position)
            {
                float[] position = (float[])a["position"];

                //Use input firing.
                if (has_input_firing)
                {
                    bool[] firing_input = (bool[])a["firing_input"];
                    if (firing_input[0] == true && firing_input[1] == false)
                    {

                        Dictionary<string, object> b = FunctionLibrary.fire_weapon((string)a["weapon"]);
                        b["position"] = new float[2] { position[0], position[1] };
                        GlobalStuff.sceneSpace.Add(b);

                        if (has_firing_sound) { SoundLibrary.sounds[(string)a["firing_sound"]].Play(); }
                    }




                }
            }

            //After processing of input trigger.
            if (has_input_firing)
            {
                bool[] input = (bool[])a["firing_input"];
                if (input[0] == true)
                {
                    input[1] = true;

                    if (has_auto_fire)
                    {
                        float[] time_array = (float[])a["fire_time"];
                        float[] position = (float[])a["position"];
                        //Add time multiplied by the second index which is the fire rate.
                        time_array[0] += time_array[1] * dt;

                        if (time_array[0] > 1)
                        {
                            time_array[0] = 0;
                            Dictionary<string, object> b = FunctionLibrary.fire_weapon((string)a["weapon"]);
                            b["position"] = new float[2] { position[0], position[1] };
                            GlobalStuff.sceneSpace.Add(b);

                            if (has_firing_sound)
                            {
                                SoundLibrary.sounds[(string)a["firing_sound"]].Play();
                            }
                        }
                        a["fire_time"] = new float[2] { time_array[0], time_array[1] };


                    }
                }
                else
                {
                    input[1] = false;
                    float[] time_array = (float[])a["fire_time"];
                    a["fire_time"] = new float[2] { 0, time_array[1] };

                }
            }
            //Increment loop.
            actorLoopIDX++;

        }
    }

    public void draw(ref SpriteBatch spriteBatch)
    {
        //Draw scene.
        foreach (Dictionary<string, object> a in GlobalStuff.sceneSpace.ToArray())
        {

            bool has_sprite = a.ContainsKey("sprite");
            bool has_rotation = a.ContainsKey("rotation");
            bool has_position = a.ContainsKey("position");
            bool has_scroll = a.ContainsKey("scroll_allowed");
            bool has_tilt_frames = a.ContainsKey("tilt_frame");
            bool has_loop_animation = a.ContainsKey("loop_animation");


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
                }

                //Copernicus
                if (sprite == "copernicus_sprite")
                {
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
                }

                //Mondragon.
                if (sprite == "mondragon_sprite")
                {
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
                }

                if (sprite == "pottenger_sprite")
                {
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
                }

                //Bullets of the Mandragora.
                if (sprite == "bullets_01")
                {
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
                }

                //Bullets of the Copernicus.
                if (sprite == "bullets_02")
                {

                    spriteBatch.Draw(SpriteLibrary.bullet_atlas,
                    new Microsoft.Xna.Framework.Rectangle(
                    (int)pos[0] - (int)cam_offset[0], (int)pos[1] - (int)cam_offset[1], 5, 7),
                    new Microsoft.Xna.Framework.Rectangle(0, 8, 5, 7),
                    Microsoft.Xna.Framework.Color.White, rot + (float)Math.PI / 2,
                    new Microsoft.Xna.Framework.Vector2(3, 7),
                    SpriteEffects.None, layerDepth: 1);
                }

                //Bullets of the Mondragon.
                if (sprite == "bullets_03")
                {

                    spriteBatch.Draw(SpriteLibrary.bullet_atlas,
                    new Microsoft.Xna.Framework.Rectangle(
                    (int)pos[0] - (int)cam_offset[0], (int)pos[1] - (int)cam_offset[1], 8, 10),
                    new Microsoft.Xna.Framework.Rectangle(33, 0, 8, 10),
                    Microsoft.Xna.Framework.Color.White, rot + (float)Math.PI / 2,
                    new Microsoft.Xna.Framework.Vector2(4, 5),
                    SpriteEffects.None, layerDepth: 1);
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


                    spriteBatch.Draw(SpriteLibrary.bullet_atlas,
                    new Microsoft.Xna.Framework.Rectangle(
                    (int)pos[0] - (int)cam_offset[0], (int)pos[1] - (int)cam_offset[1], 10, 12),
                    new Microsoft.Xna.Framework.Rectangle(42 + (frame * 10), 0, 10, 12),
                    Microsoft.Xna.Framework.Color.White, rot + (float)Math.PI / 2,
                    new Microsoft.Xna.Framework.Vector2(5, 6),
                    SpriteEffects.None, layerDepth: 1);
                }
            }
        }

    }
}