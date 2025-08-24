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
using System.Diagnostics;

namespace AI;

public class AI
{

    //AI1 creator.
    public static Dictionary<string, object> Enemy1AI()
    {
        Dictionary<string, object> res = new Dictionary<string, object>();
        Random r = GlobalStuff.resources.ContainsKey("spawner_seed") ? (Random)GlobalStuff.resources["spawner_seed"] : new Random();
        res.Add("timer_1", new float[2] { 0, 0.88f + r.NextSingle() * 2.1f });
        res.Add("timer_2", new float[2] { 0, 3.4f });
        res.Add("state", "enter");
        res.Add("turn_dir", (float)1 - (float)Math.Round(r.NextSingle()) * 2);



        return res;
    }

    //AI1 processor.
    public static void process_AI1(ref Dictionary<string, object> entity, float dt)
    {
        string weapon = (string)entity.GetValueOrDefault("weapon", "");
        float bullet_speed = 100;
        if (entity.TryGetValue("AI1", out object Value))
        {
            Dictionary<string, object> att = (Dictionary<string, object>)Value;
            Dictionary<string, object> movement = (Dictionary<string, object>)entity["topdown_movement"];
            const float second_speed = 120;
            const float second_acceleration = 30;
            const float second_deceleration = 50;


            float[] timer = (float[])att.GetValueOrDefault("timer_1", new float[2] { 0, 1 });
            float[] timer2 = (float[])att.GetValueOrDefault("timer_2", new float[2] { 0, 1 });
            string state = (string)att.GetValueOrDefault("state", "");
            float[] controls = (float[])entity.GetValueOrDefault("controls", new float[2] { 0, 0 });
            float direction = (float)att.GetValueOrDefault("turn_dir", (float)0);
            float[] pos = (float[])entity.GetValueOrDefault("position", new float[2] { 0, 0 });
            string shoot_sound = (string)entity.GetValueOrDefault("firing_sound", "");

            bool aim = att.ContainsKey("aim_at_player");

            Dictionary<string, object> player = FunctionLibrary.get_objects_in_group("player")[0] ?? null;

            float[] player_pos = (float[])player?.GetValueOrDefault("position", new float[2] { 0, 0 });

            float b_angle = aim ? (float)Math.Atan2(player_pos[1] - pos[1], player_pos[0] - pos[0]) : (float)Math.PI / 2;



            if (state == "enter")
            {
                timer[0] += dt;
                if (timer[0] >= timer[1])
                {
                    timer[0] = 0; state = "turn";

                    Dictionary<string, object> b = FunctionLibrary.fire_weapon(weapon);
                    b["position"] = new float[2] { pos[0], pos[1] };
                    b["custom_velocity"] = new float[2] { (float)Math.Cos(b_angle) * bullet_speed, (float)Math.Sin(b_angle) * bullet_speed };
                    GlobalStuff.gameStateStack.Peek().sceneSpace.Add(b);

                    if (shoot_sound != "") { SoundLibrary.sounds[shoot_sound].Play(); }

                    controls[0] = direction;
                    controls[1] = 0.2f;
                    if (movement.ContainsKey("speed")) { movement["speed"] = (float)movement["speed"] + second_speed; }
                    if (movement.ContainsKey("acceleration")) { movement["acceleration"] = second_acceleration; }
                    if (movement.ContainsKey("deceleration")) { movement["deceleration"] = second_deceleration; }
                }
            }
            if (state == "turn")
            {
                timer[1] += dt;
                if (timer2[0] >= timer2[1])
                {
                    timer2[0] = 0;

                    Dictionary<string, object> b = FunctionLibrary.fire_weapon(weapon);
                    b["position"] = new float[2] { pos[0], pos[1] };
                    b["custom_velocity"] = new float[2] { (float)Math.Cos(b_angle) * bullet_speed, (float)Math.Sin(b_angle) * bullet_speed };
                    GlobalStuff.gameStateStack.Peek().sceneSpace.Add(b);

                    if (shoot_sound != "") { SoundLibrary.sounds[shoot_sound].Play(); }
                }
            }

        }
    }
    //AI2 creator.
    public static Dictionary<string, object> Enemy2AI()
    {
        Dictionary<string, object> res = new Dictionary<string, object>();
        Random r = GlobalStuff.resources.ContainsKey("spawner_seed") ? (Random)GlobalStuff.resources["spawner_seed"] : new Random();
        res.Add("direction_timer", new float[2] { 0, 0.3f + r.NextSingle() * 0.38f });
        res.Add("shoot_timer", new float[2] { 0, 1.2f });
        res.Add("shooting_delay", (float)0.44f);
        res.Add("shoot_count", new int[2] { 0, 3 });
        res.Add("state", "enter");
        res.Add("gun_state", "in_use");



        return res;
    }


    public static void process_AI2(ref Dictionary<string, object> entity, float dt)
    {
        string weapon = (string)entity.GetValueOrDefault("weapon", "");
        float bullet_speed = 70;
        if (entity.TryGetValue("AI2", out object Value))
        {
            Dictionary<string, object> att = (Dictionary<string, object>)Value;
            Dictionary<string, object> movement = (Dictionary<string, object>)entity["topdown_movement"];
            const float second_speed = 40;


            float[] turn_timer = (float[])att.GetValueOrDefault("direction_timer", new float[2] { 0, 1 });
            int[] shoot_count = (int[])att.GetValueOrDefault("shoot_count", new float[2] { 0, 1 });
            float shoot_delay = (float)att.GetValueOrDefault("shooting_delay", 0.1f);
            float[] shoot_timer = (float[])att.GetValueOrDefault("shoot_timer", new float[2] { 0, 1 });
            string state = (string)att.GetValueOrDefault("state", "");
            float[] controls = (float[])entity.GetValueOrDefault("controls", new float[2] { 0, 0 });
            float[] pos = (float[])entity.GetValueOrDefault("position", new float[2] { 0, 0 });
            string shoot_sound = (string)entity.GetValueOrDefault("firing_sound", "");
            string gun_state = (string)att.GetValueOrDefault("gun_state", "");

            bool aim = att.ContainsKey("aim_at_player");

            Dictionary<string, object> player = FunctionLibrary.get_objects_in_group("player")[0] ?? null;

            float[] player_pos = (float[])player?.GetValueOrDefault("position", new float[2] { 0, 0 });

            float b_angle = aim ? (float)Math.Atan2(player_pos[1] - pos[1], player_pos[0] - pos[0]) : (float)Math.PI / 2;


            //Process state.
            if (state == "enter")
            {
                turn_timer[0] += dt;
                if (turn_timer[0] >= turn_timer[1])
                {
                    turn_timer[0] = 0; state = "forward";
                    controls[0] = 0;
                    controls[1] = 1;
                    if (movement.ContainsKey("speed")) { movement["speed"] = second_speed; }
                }
            }


            //Process shoot timers.
            shoot_timer[0] += dt;
            if (shoot_timer[0] > shoot_timer[1] && gun_state == "in_use")
            {
                float spread = (float)Math.PI / 6;
                Dictionary<string, object> b = FunctionLibrary.fire_weapon(weapon);
                b["position"] = new float[2] { pos[0], pos[1] };
                b["custom_velocity"] = new float[2] { (float)Math.Cos(b_angle) * bullet_speed, (float)Math.Sin(b_angle) * bullet_speed };
                GlobalStuff.gameStateStack.Peek().sceneSpace.Add(b);

                Dictionary<string, object> b2 = FunctionLibrary.fire_weapon(weapon);
                b2["position"] = new float[2] { pos[0], pos[1] };
                b2["custom_velocity"] = new float[2] { (float)Math.Cos(b_angle - spread) * bullet_speed, (float)Math.Sin(b_angle - spread) * bullet_speed };
                GlobalStuff.gameStateStack.Peek().sceneSpace.Add(b2);

                Dictionary<string, object> b3 = FunctionLibrary.fire_weapon(weapon);
                b3["position"] = new float[2] { pos[0], pos[1] };
                b3["custom_velocity"] = new float[2] { (float)Math.Cos(b_angle + spread) * bullet_speed, (float)Math.Sin(b_angle + spread) * bullet_speed };
                GlobalStuff.gameStateStack.Peek().sceneSpace.Add(b3);

                if (shoot_sound != "") { SoundLibrary.sounds[shoot_sound].Play(); }

                if (shoot_count[0] <= shoot_count[1] && att.ContainsKey("shoot_count"))
                {
                    shoot_timer[0] -= shoot_delay; shoot_count[0]++;
                    if (shoot_count[0] == shoot_count[1])
                    {
                        shoot_timer[0] = 0;
                        att["gun_state"] = "done";
                    }

                }

                if (!att.ContainsKey("shoot_count"))
                {
                    att["gun_state"] = "done";
                }

            }
        }
    }
}