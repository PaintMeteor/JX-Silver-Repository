
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using NSGameState;


namespace Global;

public static class GlobalStuff
{
    public static float[] view = new float[2] { 0, 0 };
    public static Dictionary<string, object> resources = new Dictionary<string, object>();
    public static Stack<GameState> gameStateStack = new Stack<GameState>(); //List to store game states.
}