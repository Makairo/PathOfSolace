using System.Security.Cryptography;
using Microsoft.Xna.Framework.Input;
using PathOfSolace.World;
using PathOfSolace.Entities;
using PathOfSolace.Core;

namespace PathOfSolace.Core;

public static class Input
{
    private static KeyboardState _current;
    private static KeyboardState _previous;

    public static void Update()
    {
        _previous = _current;
        _current = Keyboard.GetState();
    }

    public static bool KeyWasPressed(Keys key)
    {
        return _current.IsKeyDown(key) && !_previous.IsKeyDown(key);
    }

    public static bool GetMovementInput(out int dx, out int dy)
    {
        dx = 0;
        dy = 0;

        //if(Input.KeyWasPressed(Keys.NumPad5))       { EndPlayerTurn();  }     
        if (Input.KeyWasPressed(Keys.NumPad1))      { dx = -1; dy =  1; } // Move SW
        else if (Input.KeyWasPressed(Keys.NumPad2)) { dx =  0; dy =  1; } // Move S 
        else if (Input.KeyWasPressed(Keys.NumPad3)) { dx =  1; dy =  1; } // Move SE
        else if (Input.KeyWasPressed(Keys.NumPad4)) { dx = -1; dy =  0; } // Move W 
        else if (Input.KeyWasPressed(Keys.NumPad6)) { dx =  1; dy =  0; } // Move E 
        else if (Input.KeyWasPressed(Keys.NumPad7)) { dx = -1; dy = -1; } // Move NW 
        else if (Input.KeyWasPressed(Keys.NumPad8)) { dx =  0; dy = -1; } // Move N
        else if (Input.KeyWasPressed(Keys.NumPad9)) { dx =  1; dy = -1; } // Move NE
        else    return false;

        return true;
    }

    public static bool IsWaitPressed()
    {
        return Input.KeyWasPressed(Keys.NumPad5);
    }

}