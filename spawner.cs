using System.Collections.Generic;
using Enemy;

namespace Tools;

public class Spawner
{
    public static Dictionary<string, object> getEntity(string name)
    {
        Dictionary<string, Dictionary<string, object>> nameMap = new Dictionary<string, Dictionary<string, object>>()
        {
            {"orangebox", EnemyLibrary.OrangeBox()},
            {"biviolum", EnemyLibrary.Biviolum()}
        };

        return nameMap[name];
    }
}