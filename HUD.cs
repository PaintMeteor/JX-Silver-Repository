
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace HUD;

public class GameUI
{
    public static void drawGameUI(SpriteBatch spriteBatch, Dictionary<string, object> actor)
    {
        //The 7 main components for the game UI.
        bool has_score_ui = actor.ContainsKey("score_display_interface");
        bool has_player_hp_ui = actor.ContainsKey("health_player_interface");
        bool has_missile_ui = actor.ContainsKey("missile_interface");
        bool has_bomb_ui = actor.ContainsKey("bomb_interface");
        bool has_exp_ui = actor.ContainsKey("experience_ui");
        bool has_money_ui = actor.ContainsKey("money_interface");
        bool has_boss_hp_ui = actor.ContainsKey("boss_hp_interface");

        
    }
}