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

namespace states;

public class FirstState : GameState
{

    public FirstState()
    {
        Dictionary<string, object> p = Player.PlayerCreator.Mandragora();
        GlobalStuff.sceneSpace.Add(p);
        Dictionary<string, object> enemy_spawner = new Dictionary<string, object>();
        Dictionary<string, object> spawner_data = new Dictionary<string, object>();


        //Create enemy spawner.
        spawner_data.Add("cooldown", new float[2] { 0, 0.4f });
        spawner_data.Add("time", 60);
        spawner_data.Add("frequency", 0.33f);
        spawner_data.Add("objects", new string[] { "orangebox", "orangebox", "orangebox", "orangebox", "biviolum" });

        enemy_spawner.Add("name", "enemy_spawner");
        enemy_spawner.Add("en_spawner", (Dictionary<string, object>)spawner_data);
        //Add globally unique identifier.
        Guid guid = Guid.NewGuid();
        enemy_spawner.Add("UID", guid.ToString());
        GlobalStuff.sceneSpace.Add(enemy_spawner);
        
        GlobalStuff.resources.Add("score", (int)0);
        GlobalStuff.resources.Add("difficulty", (int)1);
        GlobalStuff.resources.Add("spawner_seed", new Random(1103100));
        GlobalStuff.resources.Add("combo", (int)0);

        Dictionary<string, object> score = new Dictionary<string, object>();
        //Add globally unique identifier.
        Guid guid2 = Guid.NewGuid();
        score.Add("UID", guid2.ToString());
        score.Add("display_score", true);
        score.Add("position", new float[2] { 8, 8 });
        score.Add("z-index", 999);
        GlobalStuff.sceneSpace.Add(score);
    }


    
}