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
using GameProcess;
using System.Security.Cryptography.X509Certificates;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using HUD;
using EffectsList;


namespace NSGameState;
public interface GameState
{
   public void update(float dt)
    {
        Random rng = new Random();

        int actorLoopIDX = 0;

        for (int i = 0; i < GlobalStuff.sceneSpace.ToArray().Length; i++)
        {
            Dictionary<string, object> a = (Dictionary<string, object>)GlobalStuff.sceneSpace[i];

            bool has_firing_sound = a.ContainsKey("firing_sound");
            bool has_input_controls = a.ContainsKey("input_controls");
            bool has_position = a.ContainsKey("position");
            bool has_z_index = a.ContainsKey("z-index");
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
            bool has_en_spawner = a.ContainsKey("en_spawner");
            bool has_hitbox = a.ContainsKey("hitbox");
            bool has_dmg_trigger = a.ContainsKey("damage_trigger");
            bool has_hp = a.ContainsKey("HP");
            bool has_index = a.ContainsKey("entity_index");
            bool has_damage_target = a.ContainsKey("damage_target");
            bool has_flash_effect = a.ContainsKey("hurt_flash");
            bool has_delete_on_damage = a.ContainsKey("delete_on_damage");
            bool has_delete_at_margin = a.ContainsKey("delete_at_margin");
            bool has_death_explosion = a.ContainsKey("death_explosion");
            bool has_death_sound = a.ContainsKey("death_sound");


            //Index.
            if (has_index) { a["entity_index"] = (int)i; }

            //Animation Loop;
            if (has_loop_animation)
            {
                float[] list = (float[])a["loop_animation"];
                list[0] += list[2] * dt;

                //Loop this around.
                list[0] = list[0] % list[1];

                a["loop_animation"] = new float[3] { list[0], list[1], list[2] };

            }

            //Hurt flash effect.
            if (has_flash_effect)
            {
                Dictionary<string, object> attributes = (Dictionary<string, object>)a["hurt_flash"];
                bool has_time = attributes.ContainsKey("time");
                if (has_time)
                {
                    //Get the time attribute.
                    float[] time = (float[])attributes["time"];
                    //Subtract delta.
                    time[0] -= dt;
                    //Limit the time from going under zero.
                    time[0] = Math.Max(time[0], 0);
                }
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

            //Enemy spawner.
            if (has_en_spawner)
            {
                Dictionary<string, object> attributes = (Dictionary<string, object>)a["en_spawner"];
                Random r;
                float spawn_chance = 1.0f;
                bool has_time = attributes.ContainsKey("time");
                bool has_cooldown = attributes.ContainsKey("cooldown");
                bool has_objects = attributes.ContainsKey("objects");
                bool has_frequency = attributes.ContainsKey("frequency");
                bool has_seed = GlobalStuff.resources.ContainsKey("spawner_seed");
                r = has_seed ? (Random)GlobalStuff.resources["spawner_seed"] : new Random();
                spawn_chance = has_frequency ? (float)attributes["frequency"] : 1.0f;

                if (has_objects && has_cooldown)
                {

                    float[] cd = (float[])attributes["cooldown"];
                    cd[0] += dt;


                    if (cd[0] >= cd[1])
                    {
                        //Do something.
                        if (r.NextSingle() <= spawn_chance)
                        {
                            Dictionary<string, object> config = new Dictionary<string, object>()
                            {
                                {"level", 1}
                            };
                            Dictionary<string, object> obox = Enemy.EnemyLibrary.OrangeBox(config);
                            GlobalStuff.sceneSpace.Add(obox);

                        }
                        cd[0] = 0;

                    }
                    attributes["cooldown"] = new float[2] { cd[0], cd[1] };
                }
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

            //Delete at edge.
            if (has_delete_at_margin && has_position)
            {

                float[] margin = (float[])a["delete_at_margin"];
                float[] pos = (float[])a["position"];

                //Outside?
                if (pos[0] < -margin[2] || pos[0] > WindowProperties.window_width + margin[3] || pos[1] < -margin[0] || pos[1] > WindowProperties.window_height + margin[1])
                {
                    GlobalStuff.sceneSpace.Remove(a);
                }
            }

            //Damage target.
                if (has_damage_target)
                {
                    Dictionary<string, object> att = (Dictionary<string, object>)a["hitbox"];
                    bool has_objects = att.ContainsKey("objects");
                    int dmg = a.ContainsKey("damage") ? (int)a["damage"] : 1;
                    HashSet<string> dmg_tgt = (HashSet<string>)a["damage_target"];

                    if (has_objects)
                    {
                        List<Dictionary<string, object>> objects = (List<Dictionary<string, object>>)att["objects"];

                        for (int j = 0; j < objects.ToArray().Length; j++)
                        {
                            HashSet<string> matches = (HashSet<string>)objects[j]["groups"];
                            Dictionary<string, object> victim = objects[j];
                            //Is there a similar item inside the array?
                            if (matches.Intersect(dmg_tgt).ToArray().Length > 0)
                            {

                                FunctionLibrary.damage_entity(ref victim, dmg);
                                if (has_delete_on_damage)
                                {
                                    GlobalStuff.sceneSpace.Remove(a);
                                    continue;
                                }

                            }
                        }

                    }
                }

            //HP and death.
                if (has_hp)
                {
                    int[] hp = (int[])a["HP"];
                if (hp[0] <= 0)
                {
                    GlobalStuff.sceneSpace.Remove(a);

                    int pts = a.ContainsKey("points_on_death") ? (int)a["points_on_death"] : 0;
                    if (GlobalStuff.resources.ContainsKey("score"))
                    {
                        GlobalStuff.resources["score"] = (int)GlobalStuff.resources["score"] + pts;
                    }

                    if (has_death_explosion && has_position)
                    {
                        float[] pos = (float[])a["position"];
                        FunctionLibrary.explode(pos[0], pos[1], 9);
                    }

                    if (has_death_sound)
                    {
                        SoundLibrary.sounds[(string)a["death_sound"]].Play();
                    }

                }
                }
            //Lifetime.
            if (has_lifetime)
            {
                a["lifetime"] = (float)a["lifetime"] - dt;

                if ((float)a["lifetime"] <= 0)
                {
                    //Delete.
                    GlobalStuff.sceneSpace.Remove(a);
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

    public void collide()
    {
        for (int i = 0; i < GlobalStuff.sceneSpace.ToArray().Length; i++)
        {
            Dictionary<string, object> actor = GlobalStuff.sceneSpace[i];
            HashSet<string> groups_collided = new HashSet<string>();
            bool has_hitbox = actor.ContainsKey("hitbox");
            if (!has_hitbox) { continue; }
            Dictionary<string, object> main_hbox = (Dictionary<string, object>)actor["hitbox"];
            bool has_objects_colliding = main_hbox.ContainsKey("objects");

            //Get a list of actors colliding, excluding itself.
            List<Dictionary<string, object>> colliding_actors = GlobalStuff.sceneSpace.Where(
                a => a.ContainsKey("position") && a.ContainsKey("hitbox") && FunctionLibrary.objectCollideWithOther(actor, a) && !a.Equals(actor)
            ).ToList();

            for (int j = 0; j < colliding_actors.ToArray().Length; j++)
            {
                bool has_groups = colliding_actors[j].ContainsKey("groups");
                if (has_groups) { groups_collided.UnionWith((HashSet<string>)colliding_actors[j]["groups"]); }
            }

            bool has_colliding_groups = main_hbox.ContainsKey("colliding_with");
            if (has_colliding_groups) { main_hbox["colliding_with"] = groups_collided; }
            if (has_objects_colliding) { main_hbox["objects"] = colliding_actors; }
        }
    }
    public void draw(SpriteBatch spriteBatch)
    {

        //Z indexes.
        Dictionary<Dictionary<string, object>, int> z_indexes = new Dictionary<Dictionary<string, object>, int>();
        foreach (Dictionary<string, object> a in GlobalStuff.sceneSpace.ToArray())
        {
            bool has_z_index = a.ContainsKey("z-index");

            z_indexes.Add(a, has_z_index ? (int)a["z-index"] : 0);
        }

        z_indexes = z_indexes.OrderBy(obj => obj.Value).ToDictionary();


        //Draw scene.
        for (int i = 0; i < z_indexes.Keys.ToArray().Length; i++)
        {
            Dictionary<string, object> a = (Dictionary<string, object>)z_indexes.Keys.ToArray()[i];
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

            Effect effect_to_use = null;

            //Hurt effect.
            if (has_flash_effect)
            {

                Dictionary<string, object> attributes = (Dictionary<string, object>)a["hurt_flash"];
                bool has_color = attributes.ContainsKey("color");
                bool has_strength = attributes.ContainsKey("strength");
                bool has_time = attributes.ContainsKey("time");

                if (has_time)
                {
                    effect_to_use = EffectsLibrary.flash_effect.Clone();
                    float[] time = (float[])attributes["time"];
                    float[] color = has_color ? (float[])attributes["color"] : new float[3] { 1, 1, 1 };
                    float strength = has_strength ? (float)attributes["strength"] : 1.0f;

                    if (time[1] == 0)
                    {
                        throw new Exception("Max time cannot be zero.");
                    }

                    float mix_value = (time[0] / time[1]) * strength;
                    effect_to_use.Parameters["color_r"].SetValue(color[0]);
                    effect_to_use.Parameters["color_g"].SetValue(color[1]);
                    effect_to_use.Parameters["color_b"].SetValue(color[2]);
                    effect_to_use.Parameters["amount"].SetValue(mix_value);
                }


            }

            //Draw score display.
            if (has_score_display && has_position && has_score_display)
            {
                spriteBatch.Begin(samplerState: SamplerState.PointClamp, effect: effect_to_use, blendState: BlendState.NonPremultiplied);
                float[] pos = (float[])a["position"];
                spriteBatch.DrawString(SpriteLibrary.game_font, GlobalStuff.resources["score"].ToString(), new Microsoft.Xna.Framework.Vector2(
                    pos[0], pos[1]), Microsoft.Xna.Framework.Color.White);

                spriteBatch.End();

            }

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
                    Microsoft.Xna.Framework.Color.White, rot - (float)Math.PI / 2,
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

            }
            
            //Draw health bar.
            if (has_health_bar && has_position && has_HP)
            {
                spriteBatch.Begin(samplerState: SamplerState.PointClamp);
                float[] pos = (float[])a["position"];
                int[] hp = (int[])a["HP"];

                Dictionary<string, object> attributes = (Dictionary<string, object>)a["health_bar"];
                bool has_offset = attributes.ContainsKey("offset");
                bool has_width = attributes.ContainsKey("width");
                int[] offset = has_offset ? (int[])attributes["offset"] : new int[2] { 0, 0 };

                if (has_width)
                {
                    int width = (int)attributes["width"];
                    HUD.Bars.draw_bar2(spriteBatch, (int)pos[0] + offset[0] - width, (int)pos[1] + offset[1], width * 2, (int)(((float)hp[0] / (float)hp[1]) * width * 2));
                }
                
                spriteBatch.End();
            }
        }



    }
}