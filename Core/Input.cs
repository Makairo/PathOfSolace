using System.Security.Cryptography;
using Microsoft.Xna.Framework.Input;

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
}