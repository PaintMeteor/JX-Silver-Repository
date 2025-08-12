
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using Global;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework.Graphics;
using System.Xml.XPath;
using Bullets;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using SoundList;
using Microsoft.Xna.Framework.Audio;
using System.Linq;
using System.Security.Cryptography;

namespace Tools;

public class FunctionLibrary()
{
    //Convert boolean to an integer value.
    public static int boolToInt(bool val)
    {
        return val ? 1 : 0;
    }

    public static List<Dictionary<string, object>> get_objects_in_group(string name)
    {
        List<Dictionary<string, object>> li = new List<Dictionary<string, object>>();

        foreach (Dictionary<string, object> a in GlobalStuff.sceneSpace.ToArray())
        {
            if (a.ContainsKey("groups"))
            {
                List<string> groups = (List<string>)a["groups"];

                if (groups.Contains(name))
                {
                    li.Add(a);
                }
            }
        }
        return li;
    }

    public static bool rectCollide(float x1, float y1, float w1, float h1, float x2, float y2, float w2, float h2)
    {

        return x1 < x2 + w2 &&
        x1 + w1 > x2 &&
        y1 < y2 + h2 &&
        y1 + h1 > y2;
    }
    public static bool is_object_in_group(Dictionary<string, object> a, string name)
    {
        if (a.ContainsKey("groups"))
        {
            List<string> groups = (List<string>)a["groups"];

            if (groups.Contains(name))
            {
                return true;
            }
        }
        return false;
    }

    public static bool objectCollideWithOther(Dictionary<string, object> actor_a, Dictionary<string, object> actor_b)
    {
        bool has_hitbox = actor_a.ContainsKey("hitbox");
        bool other_has_hitbox = actor_b.ContainsKey("hitbox");

        bool has_position = actor_a.ContainsKey("position");
        bool other_has_position = actor_b.ContainsKey("position");

        if (!(has_hitbox && other_has_hitbox)) { return false; }
        if (!(has_position && other_has_position)) { return false; }

        Dictionary<string, object> hbox_att_a = (Dictionary<string, object>)actor_a["hitbox"];
        Dictionary<string, object> hbox_att_b = (Dictionary<string, object>)actor_b["hitbox"];

        float[] r1 = (float[])hbox_att_a["rect"];
        float[] r2 = (float[])hbox_att_b["rect"];

        float[] p1 = (float[])actor_a["position"];
        float[] p2 = (float[])actor_b["position"];

        return rectCollide(r1[0] + p1[0], r1[1] + p1[1], r1[2], r1[3], r2[0] + p2[0], r2[1] + p2[1], r2[2], r2[3]);
    }

    public static float length(float x, float y)
    {
        return (float)Math.Sqrt(x * x + y * y);
    }

    public static Dictionary<string, object> fire_weapon(string val)
    {
        Dictionary<string, object> def = new Dictionary<string, object>();

        if (val == "mandragora_bullets")
        {
            Dictionary<string, object> b = Bullets.BulletCreator.MandragoraBullet();
            b.Add("position", new float[2] { 0, 0 });
            b.Add("rotation", (float)-Math.PI / 2);

            return b;
        }

        if (val == "copernicus_bullets")
        {
            Dictionary<string, object> b = Bullets.BulletCreator.CopernicusBullet();
            b.Add("position", new float[2] { 0, 0 });
            b.Add("rotation", (float)-Math.PI / 2);

            return b;
        }

        if (val == "mondragon_bullets")
        {
            Dictionary<string, object> b = Bullets.BulletCreator.MondragonBullet();
            b.Add("position", new float[2] { 0, 0 });
            b.Add("rotation", (float)-Math.PI / 2);

            return b;
        }

        if (val == "pottenger_bullets")
        {
            Dictionary<string, object> b = Bullets.BulletCreator.PottengerBullet();
            b.Add("position", new float[2] { 0, 0 });
            b.Add("rotation", (float)-Math.PI / 2);

            return b;
        }
        return def;
    }

    public static float[] normalize(float x, float y)
    {
        float len = (float)Math.Sqrt(Math.Abs(x * x) + Math.Abs(y * y));
        if (len == 0.0) { return new float[2] { 0, 0 }; }
        return new float[2] { x / len, y / len };
    }

    public static float boolToFloat(bool val)
    {
        return val ? 1 : 0;
    }

    public static float moveToward(float start, float end, float dt)
    {
        if (start < end)
        {
            start += dt;
            start = Math.Min(start, end);
        }
        if (start > end)
        {
            start -= dt;
            start = Math.Max(start, end);
        }


        return start;
    }

    public static List<Rectangle> getRectangles(Rectangle rect, int through)
    {
        List<Rectangle> rectangles = new List<Rectangle>();
        for (int i = 0; i < through; i++)
        {
            Rectangle rectangle = rect;
            rect.X += through * rect.Width;
            rectangles.Add(rectangle);
        }
        return rectangles;
    }

    public static void explode(float at_x, float at_y, int size)
    {
        

        int count = 4 * size;

        for (int i = 0; i < count; i++)
        {
            Random r = new Random();

            float[] pos = new float[2] { at_x, at_y };

            double random_circle_angle = r.NextDouble() * Math.PI * 2.0;

            float radius = (r.NextSingle() * (size) * 0.3f);

            pos[0] = at_x + (float)Math.Cos(random_circle_angle) * radius;
            pos[1] = at_y + (float)Math.Sin(random_circle_angle) * radius;

            float speed = (9 * r.NextSingle() * (float)size);

            float lf = (float)Math.Min(0.03f * (float)size, 1 + r.NextSingle() * 0.68);
            Dictionary<string, object> ptc = new Dictionary<string, object>();
            ptc.Add("position", pos);
            ptc.Add("lifetime", lf);
            ptc.Add("custom_velocity", new float[2] { (float)Math.Cos(random_circle_angle) * speed, (float)Math.Sin(random_circle_angle) * speed });

            ptc.Add("delete_at_margin", new float[4] { 50, 50, 50, 50 });

            ptc.Add("sprite", "explosion_particle");
            ptc.Add("loop_animation", new float[3] { 0, 12, (12 / lf) });
            ptc.Add("z-index", 20);

            Guid guid = Guid.NewGuid();
            ptc.Add("UID", guid.ToString());

            GlobalStuff.sceneSpace.Add(ptc);
        
        }
    }

    public static float spike_function_1(float x)
    {
        return 0 < 0.1 ? 10 * x : -1.1f * x + 0.1f;
    }

    public static void damage_entity(ref Dictionary<string, object> e, int dmg)
    {
        bool has_hp = e.ContainsKey("HP");
        bool has_hurt_flash = e.ContainsKey("hurt_flash");
        bool has_hurt_sound = e.ContainsKey("hurt_sound");

        if (!has_hp) { return; } // Cancel function if there is no HP.
        int[] hp = (int[])e["HP"];

        hp[0] -= dmg;

        e["HP"] = hp;

        if (has_hurt_sound)
        {
            SoundLibrary.sounds[(string)e["hurt_sound"]].Play();
        }

        if (!has_hurt_flash) { return; } //Cancel function if there is no flash.
        Dictionary<string, object> hf_attributes = (Dictionary<string, object>)e["hurt_flash"]; //Access the hurt flash component's attributes.
        bool has_time = hf_attributes.ContainsKey("time");

        if (has_time)
        {
            float[] time = (float[])hf_attributes["time"];
            time[0] = time[1];
            hf_attributes["time"] = new float[2] { time[0], time[1] };
        }
    }
}