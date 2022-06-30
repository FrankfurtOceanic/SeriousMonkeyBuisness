using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// List all the actions in the game
/// </summary>
public enum MonkeyKey
{
    SelectLeft,
    SelectRight,
    ToggleBuild,
    PlaceTurret
}

public class InputManager : MonoBehaviour
{
    public static bool VR_GetDown(MonkeyKey k)
    {
        return OVRInput.GetDown(k switch
        {
            MonkeyKey.SelectLeft => OVRInput.RawButton.LThumbstickLeft,
            MonkeyKey.SelectRight => OVRInput.RawButton.LThumbstickRight,
            MonkeyKey.ToggleBuild => OVRInput.RawButton.Y,
            MonkeyKey.PlaceTurret => OVRInput.RawButton.LIndexTrigger,
            _ => throw new System.NotImplementedException(),
        });
    }

    public static bool PC_GetDown(MonkeyKey k)
    {
        return Input.GetKeyDown(k switch
        {
            MonkeyKey.SelectLeft => KeyCode.LeftArrow,
            MonkeyKey.SelectRight => KeyCode.RightArrow,
            MonkeyKey.ToggleBuild => KeyCode.Y,
            MonkeyKey.PlaceTurret => KeyCode.Return,
            _ => throw new System.NotImplementedException(),
        });
    }

    public static bool GetDown(MonkeyKey k)
    {
        return VR_GetDown(k) || PC_GetDown(k);
    }
}
