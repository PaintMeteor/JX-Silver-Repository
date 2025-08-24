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
using AI;

namespace Enemy;

public class EnemyLibrary
{
    public static Dictionary<string, object> OrangeBox()
    {

        Dictionary<string, object> res = new Dictionary<string, object>();
        Random r = GlobalStuff.resources.ContainsKey("spawner_seed") ? (Random)GlobalStuff.resources["spawner_seed"] : new Random();
        res.Add("HP", new int[2] { 12, 12 });
        res.Add("sprite", "orangebox_sprite");

        res.Add("topdown_movement", new Dictionary<string, object>() { { "speed", (float)20 + r.NextSingle() * 50 },
            { "initial_speed", new float[2] {0,0}},
            { "acceleration", (float)1000},
            { "deceleration", (float)200} });

        res.Add("hp_decay_effect", new Dictionary<string, object>()
        {
            {"color", new float[3] {1, 0, 0}}
        });

        res.Add("death_explosion", null);

        res.Add("death_sound", "enemy_die");

        //Add globally unique identifier.
        Guid guid = Guid.NewGuid();
        res.Add("UID", guid.ToString());

        //Add remove at edge.
        res.Add("delete_at_margin", new float[4] { 30, 10, 20, 20 });


        //Add HP bar.
        Dictionary<string, object> HPBar = new Dictionary<string, object>()
        {
            {"offset", new int[2] {-10, -14}},
            {"width", (int)12},
            {"color", "red"},
            {"under_color", "black"},
            {"type", "slant"}

        };

        //Add position and rotation.
        res.Add("position", new float[2] { 6 + r.NextSingle() * 186, 0 });
        res.Add("rotation", (float)-Math.PI / 2);

        res.Add("controls", new float[2] { 0, 1 });

        //Add AI.
        Dictionary<string, object> ai = AI.AI.Enemy1AI();
        ai.Add("aim_at_player", null);
        res.Add("AI1", ai);

        //Add groups
        res.Add("groups", new HashSet<string>() { "bad", "spacecraft" });

        res.Add("points_on_death", (int)100);

        res.Add("firing_sound", "enemy_shoot");

        //Add hurt sound.
        res.Add("hurt_sound", "enemy_hurt");

        //Add weapon.
        res.Add("weapon", "enemy_bullet1");

        //Add flash.
        Dictionary<string, object> fls = new Dictionary<string, object>()
        {
            //First index is initial, second index is max time.
            {"time", new float[2] { 0, 0.07f } },
            {"color", new float[3] { 1, 1, 1 } },
            {"strength", (float)1.0},
        };
        res.Add("hurt_flash", fls);

        //Add hitbox.
        Dictionary<string, object> h_box = new Dictionary<string, object>() {
            {"rect", new float[4] { 8 ,8 , 10, 10} },
            {"colliding_with", new HashSet<string>()},
            {"objects", new List<Dictionary<string, object>>()}
        };
        res.Add("hitbox", h_box);


        return res;
    }
    
    public static Dictionary<string, object> Biviolum()
    {

        Dictionary<string, object> res = new Dictionary<string, object>();
        Random r = GlobalStuff.resources.ContainsKey("spawner_seed") ? (Random)GlobalStuff.resources["spawner_seed"] : new Random();
        res.Add("HP", new int[2] { 50, 50 });
        res.Add("sprite", "biviolum_sprite");

        res.Add("topdown_movement", new Dictionary<string, object>() { { "speed", (float)120 + r.NextSingle() * 10 },
            { "initial_speed", new float[2] {0,0}},
            { "acceleration", (float)1000},
            { "deceleration", (float)200} });

        res.Add("hp_decay_effect", new Dictionary<string, object>()
        {
            {"color", new float[3] {1, 0, 0}}
        });

        res.Add("death_explosion", null);

        res.Add("death_sound", "enemy_die");

        //Add globally unique identifier.
        Guid guid = Guid.NewGuid();
        res.Add("UID", guid.ToString());

        //Add remove at edge.
        res.Add("delete_at_margin", new float[4] { 55, 20, 20, 55 });


        //Add HP bar.
        Dictionary<string, object> HPBar = new Dictionary<string, object>()
        {
            {"offset", new int[2] {-10, -14}},
            {"width", (int)12},
            {"color", "red"},
            {"under_color", "black"},
            {"type", "slant"}
            
        };

        //Add position and rotation.
        float spawn_margin = 10;
        float[] sides = [-spawn_margin, WindowProperties.window_width + spawn_margin];
        int random_num_side = (int)Math.Round(r.NextSingle());

        res.Add("position", new float[2] { sides[random_num_side], 75 });
        res.Add("rotation", (float)-Math.PI / 2);

        res.Add("controls", new float[2] { 1 - random_num_side * 2, 0 });

        //Add AI.
        Dictionary<string, object> ai = AI.AI.Enemy2AI();
        //ai.Add("aim_at_player", null);
        res.Add("AI2", ai);

        //Add groups
        res.Add("groups", new HashSet<string>() { "bad", "spacecraft" });

        res.Add("points_on_death", (int)700);

        res.Add("firing_sound", "enemy_shoot");

        //Add hurt sound.
        res.Add("hurt_sound", "enemy_hurt");

        //Add weapon.
        res.Add("weapon", "enemy_bullet1");

        //Add flash.
        Dictionary<string, object> fls = new Dictionary<string, object>()
        {
            //First index is initial, second index is max time.
            {"time", new float[2] { 0, 0.07f } },
            {"color", new float[3] { 1, 1, 1 } },
            {"strength", (float)1.0},
        };
        res.Add("hurt_flash", fls);

        //Add hitbox.
        Dictionary<string, object> h_box = new Dictionary<string, object>() {
            {"rect", new float[4] { 10, 8, 16, 12} },
            {"colliding_with", new HashSet<string>()},
            {"objects", new List<Dictionary<string, object>>()}
        };
        res.Add("hitbox", h_box);


        return res;
    }

}