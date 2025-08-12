using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Mime;
using System.Numerics;
using System.Threading.Tasks.Dataflow;
using GameWindow;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SoundList;
using SpriteList;
using Tools;
using GameInput;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using Global;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.ComponentModel;

namespace Player;

public class PlayerCreator
{
    public static Dictionary<string, object> Mandragora()
    {
        Dictionary<string, object> res = new Dictionary<string, object>();

        //Add health points.
        res.Add("HP", new int[2] { 50, 50 });

        //Add movement.
        res.Add("topdown_movement", new Dictionary<string, object>() { { "speed", (float)127 },
            { "initial_speed", new float[2] {0,0}},
            { "acceleration", (float)1800},
            { "deceleration", (float)1000} });

        //Add globally unique identifier.
        Guid guid = Guid.NewGuid();
        res.Add("UID", guid.ToString());

        //Add input.
        res.Add("input_controls", new float[2] { 0, 0 });

        //Add groups.
        res.Add("groups", new HashSet<string>() { "good", "player", "spacecraft" });

        //And a sprite.
        res.Add("sprite", "mandragora_sprite");
        res.Add("z-index", 4);

        res.Add("weapon", "mandragora_bullets");

        res.Add("firing_input", new bool[2] { false, false });
        res.Add("fire_time", new float[2] { 0, 10 });
        res.Add("firing_sound", "mand_shoot_sound");

        //Add position and rotation.
        res.Add("position", new float[2] { 80, 232 });
        res.Add("rotation", (float)-Math.PI / 2);

        //Follow camera.
        res.Add("scroll_allowed", true);

        //Add hitbox.
        Dictionary<string, object> h_box = new Dictionary<string, object>() {
            {"rect", new float[4] { 8 ,8 , 10, 10} },
            {"colliding_with", new HashSet<string>()},
            {"objects", new List<Dictionary<string, object>>()}
        };
        res.Add("hitbox", h_box);

        //Add index.
        res.Add("entity_index", 0);

        //Stay within the screen.
        res.Add("bounds", new float[4] { 0, 0, 192, 400 });

        //Allow the sprite to change frames with a tilt frame.
        res.Add("tilt_frame", (int)0);

        //Give it a name.
        res.Add("name", "Mandragora");
        return res;

    }
    //This is the first ship, the classic Mandragora. It fires its twin laser
    //and spread shots when powered up, at a high fire rate.
    
    public static Dictionary<string, object> Copernicus()
    {
        Dictionary<string, object> res = new Dictionary<string, object>();

        //Add health points.
        res.Add("HP", new int[2] { 50, 50 });

        //Add movement.
        res.Add("topdown_movement", new Dictionary<string, object>() { { "speed", (float)131 },
            { "initial_speed", new float[2] {0,0}},
            { "acceleration", (float)1000},
            { "deceleration", (float)200} });

        //Add globally unique identifier.
        Guid guid = Guid.NewGuid();
        res.Add("UID", guid.ToString());

        //Add input.
        res.Add("input_controls", new float[2] { 0, 0 });

        //Add groups.
        res.Add("groups", new List<string>() { "good", "player", "spacecraft" });

        //And a sprite.
        res.Add("sprite", "copernicus_sprite");

        res.Add("weapon", "copernicus_bullets");

        res.Add("firing_input", new bool[2] { false, false });
        res.Add("fire_time", new float[2] { 0, 3 });
        res.Add("firing_sound", "coper_shoot_sound");

        //Add position and rotation.
        res.Add("position", new float[2] { 80, 232 });
        res.Add("rotation", (float)-Math.PI / 2);

        //Follow camera.
        res.Add("scroll_allowed", true);

        //Stay within the screen.
        res.Add("bounds", new float[4] { 0, 0, 192, 400 });

        //Allow the sprite to change frames with a tilt frame.
        res.Add("tilt_frame", (int)0);

        //Give it a name.
        res.Add("name", "Copernicus");
        return res;

    }
    //Copernicus is a bit different from the Mandragora. It shoots slower than any ship;
    //it has the lowest fire rate. It moves slower than the Mandragora. It's bullets are
    //very explosive and targets the nearest enemy.

    public static Dictionary<string, object> Mondragon()
    {
        Dictionary<string, object> res = new Dictionary<string, object>();

        //Add health points.
        res.Add("HP", new int[2] { 50, 50 });

        //Add movement.
        res.Add("topdown_movement", new Dictionary<string, object>() { { "speed", (float)131 },
            { "initial_speed", new float[2] {0,0}},
            { "acceleration", (float)1000},
            { "deceleration", (float)200} });

        //Add globally unique identifier.
        Guid guid = Guid.NewGuid();
        res.Add("UID", guid.ToString());

        //Add input.
        res.Add("input_controls", new float[2] { 0, 0 });

        //Add groups.
        res.Add("groups", new List<string>() { "good", "player", "spacecraft" });

        //And a sprite.
        res.Add("sprite", "mondragon_sprite");

        res.Add("weapon", "mondragon_bullets");

        res.Add("firing_input", new bool[2] { false, false });
        res.Add("fire_time", new float[2] { 0, 17 });
        res.Add("firing_sound", "mon_shoot_sound");

        //Add position and rotation.
        res.Add("position", new float[2] { 80, 232 });
        res.Add("rotation", (float)-Math.PI / 2);

        //Follow camera.
        res.Add("scroll_allowed", true);

        //Stay within the screen.
        res.Add("bounds", new float[4] { 0, 0, 192, 400 });

        //Allow the sprite to change frames with a tilt frame.
        res.Add("tilt_frame", (int)0);

        //Give it a name.
        res.Add("name", "Mondragon");
        return res;

    }
    //The Mondragon is a versatile, fast moving ship with bullets that toast enemies to a
    //crisp. Named after Eva "Mondragon" Rice.
    
    public static Dictionary<string, object> Pottenger()
    {
        Dictionary<string, object> res = new Dictionary<string, object>();

        //Add health points.
        res.Add("HP", new int[2] { 50, 50 });

        //Add movement.
        res.Add("topdown_movement", new Dictionary<string, object>() { { "speed", (float)131 },
            { "initial_speed", new float[2] {0,0}},
            { "acceleration", (float)1000},
            { "deceleration", (float)200} });

        //Add globally unique identifier.
        Guid guid = Guid.NewGuid();
        res.Add("UID", guid.ToString());

        //Add input.
        res.Add("input_controls", new float[2] { 0, 0 });

        //Add groups.
        res.Add("groups", new List<string>() { "good", "player", "spacecraft" });

        //And a sprite.
        res.Add("sprite", "pottenger_sprite");

        res.Add("weapon", "pottenger_bullets");

        res.Add("firing_input", new bool[2] { false, false });
        res.Add("fire_time", new float[2] { 0, 11 });
        res.Add("firing_sound", "pott_shoot_sound");

        //Add position and rotation.
        res.Add("position", new float[2] { 80, 232 });
        res.Add("rotation", (float)-Math.PI / 2);

        //Follow camera.
        res.Add("scroll_allowed", true);

        //Stay within the screen.
        res.Add("bounds", new float[4] { 0, 0, 192, 400 });

        //Allow the sprite to change frames with a tilt frame.
        res.Add("tilt_frame", (int)0);

        //Give it a name.
        res.Add("name", "Pottenger");
        return res;

    }

    //Pottenger is the most interesting ship. Though its change in fire-rate and accuracy are chaotic,
    //It seems useful for damaging loosely grouped enemies from afar. 
    //This was named after the Pottenger's Cats study by Francis M. Pottenger.



}

