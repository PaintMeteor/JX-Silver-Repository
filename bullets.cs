using System;
using System.Collections.Generic;


namespace Bullets;

public class BulletCreator
{
    public static Dictionary<string, object> MandragoraBullet()
    {
        Dictionary<string, object> res = new Dictionary<string, object>();

        //Add globally unique identifier.
        Guid guid = Guid.NewGuid();
        res.Add("UID", guid.ToString());

        //Add sprite.
        res.Add("sprite", "bullets_01");

        res.Add("custom_velocity", new float[2] { 0, -400 });

        res.Add("position", new float[2] { 0, 0 });

        res.Add("rotation", (float)-Math.PI / 2);

        res.Add("damage", (int)5);

        res.Add("delete_on_damage", null);

        res.Add("damage_target", new HashSet<string>() { "bad" });

        //Add index.
        res.Add("entity_index", 0);

        res.Add("loop_animation", new float[3] { 0, 6, 60 });

        //Add hitbox.
        Dictionary<string, object> h_box = new Dictionary<string, object>() {
            {"rect", new float[4] { 8 ,8 , 10, 10} },
            {"colliding_with", new HashSet<string>()},
            {"objects", new List<Dictionary<string, object>>()}
        };
        res.Add("hitbox", h_box);

        res.Add("groups", new HashSet<string>() { "good", "bullets" });

        res.Add("lifetime", (float)0.7);


        return res;
    }

    public static Dictionary<string, object> CopernicusBullet()
    {
        Dictionary<string, object> res = new Dictionary<string, object>();

        //Add globally unique identifier.
        Guid guid = Guid.NewGuid();
        res.Add("UID", guid.ToString());

        //Add sprite.
        res.Add("sprite", "bullets_02");

        res.Add("position", new float[2] { 0, 0 });

        res.Add("rotation", (float)-Math.PI / 2);

        res.Add("custom_velocity", new float[2] { 0, -320 });

        res.Add("damage", (int)8);

        //Add a hitbox.
        Dictionary<string, object> h_box = new Dictionary<string, object>() {
            {"rect", new float[4] { 8 ,8 , 10, 10} },
            {"colliding_with", new List<string>()}
        };
        res.Add("damage_trigger", h_box);

        //Add index.
        res.Add("entity_index", 0);

        res.Add("groups", new List<string>() { "good", "bullets" });

        res.Add("lifetime", (float)0.7);

        return res;
    }

    public static Dictionary<string, object> MondragonBullet()
    {
        Dictionary<string, object> res = new Dictionary<string, object>();

        //Add globally unique identifier.
        Guid guid = Guid.NewGuid();
        res.Add("UID", guid.ToString());

        //Add sprite.
        res.Add("sprite", "bullets_03");

        res.Add("position", new float[2] { 0, 0 });

        res.Add("rotation", (float)-Math.PI / 2);

        res.Add("custom_velocity", new float[2] { 0, -223 });

        res.Add("damage", (int)8);

        //Add a damage trigger.
        Dictionary<string, object> h_box = new Dictionary<string, object>() {
            {"rect", new float[4] { 8 ,8 , 10, 10} }
        };
        res.Add("damage_trigger", h_box);

        res.Add("groups", new List<string>() { "good", "bullets" });

        res.Add("lifetime", (float)1);

        return res;
    }

    public static Dictionary<string, object> PottengerBullet()
    {
        Dictionary<string, object> res = new Dictionary<string, object>();

        //Add globally unique identifier.
        Guid guid = Guid.NewGuid();
        res.Add("UID", guid.ToString());

        //Add sprite.
        res.Add("sprite", "bullets_04");

        res.Add("position", new float[2] { 0, 0 });

        res.Add("rotation", (float)-Math.PI / 2);

        res.Add("custom_velocity", new float[2] { 0, -400 });

        res.Add("damage", (int)8);

        res.Add("loop_animation", new float[3] { 0, 5, 30 });

        //Add a damage trigger.
        Dictionary<string, object> h_box = new Dictionary<string, object>() {
            {"rect", new float[4] { 8 ,8 , 10, 10} }
        };
        res.Add("damage_trigger", h_box);

        res.Add("groups", new List<string>() { "good", "bullets" });

        res.Add("lifetime", (float)0.7);


        return res;
    }
    
    public static Dictionary<string, object> EnemyBullet()
    {
        Dictionary<string, object> res = new Dictionary<string, object>();

        //Add globally unique identifier.
        Guid guid = Guid.NewGuid();
        res.Add("UID", guid.ToString());

        //Add sprite.
        res.Add("sprite", "en_bullet");

        res.Add("position", new float[2] { 0, 0 });

        res.Add("z-index", 2);

        res.Add("rotation", (float)-Math.PI / 2);

        res.Add("custom_velocity", new float[2] { 0, 0 });

        res.Add("damage", (int)5);

        res.Add("delete_on_damage", null);

        res.Add("loop_animation", new float[3] { 0, 4, 20 });

        res.Add("damage_target", new HashSet<string>() { "player" });

        //Add hitbox.
        Dictionary<string, object> h_box = new Dictionary<string, object>() {
            {"rect", new float[4] { 8 ,8 , 10, 10} },
            {"colliding_with", new HashSet<string>()},
            {"objects", new List<Dictionary<string, object>>()}
        };
        res.Add("hitbox", h_box);

        res.Add("groups", new HashSet<string>() {"bullets"});

        res.Add("lifetime", (float)4);


        return res;
    }
}