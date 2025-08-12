using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace GameInput;


public class InputMap
{
    public static Dictionary<string, Keys> keybinds = new Dictionary<string, Keys>
    {
        {"K_UP", Keys.W},
        {"K_DOWN", Keys.S},
        {"K_LEFT", Keys.A},
        {"K_RIGHT", Keys.D},
        {"K_FIRE", Keys.Space},
        {"K_F1", Keys.F1}
    };
}