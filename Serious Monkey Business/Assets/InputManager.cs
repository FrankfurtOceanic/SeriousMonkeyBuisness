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
    PlaceTurret,
    SwitchWeapon,
    Fire
}

public class InputManager : MonoBehaviour
{
    public static bool VR_GetDown(MonkeyKey k)
    {
        return OVRInput.GetDown(ToOVR(k));
    }

    public static bool PC_GetDown(MonkeyKey k)
    {
        return Input.GetKeyDown(ToPC(k));
    }

    public static bool VR_GetUp(MonkeyKey k)
    {
        return OVRInput.GetUp(ToOVR(k));
    }

    public static bool PC_GetUp(MonkeyKey k)
    {
        return Input.GetKeyUp(ToPC(k));
    }

    public static bool VR_Get(MonkeyKey k)
    {
        return OVRInput.Get(ToOVR(k));
    }

    public static bool PC_Get(MonkeyKey k)
    {
        return Input.GetKey(ToPC(k));
    }
    private static OVRInput.RawButton ToOVR(MonkeyKey k)
    {
        return k switch
        {
            MonkeyKey.SelectLeft => OVRInput.RawButton.LThumbstickLeft,
            MonkeyKey.SelectRight => OVRInput.RawButton.LThumbstickRight,
            MonkeyKey.ToggleBuild => OVRInput.RawButton.Y,
            MonkeyKey.SwitchWeapon => OVRInput.RawButton.X,
            MonkeyKey.PlaceTurret => OVRInput.RawButton.LIndexTrigger,
            MonkeyKey.Fire => OVRInput.RawButton.RIndexTrigger,
            _ => throw new System.NotImplementedException(),
        };
    }

    private static KeyCode ToPC(MonkeyKey k)
    {
        return k switch
        {
            MonkeyKey.SelectLeft => KeyCode.LeftArrow,
            MonkeyKey.SelectRight => KeyCode.RightArrow,
            MonkeyKey.ToggleBuild => KeyCode.Y,
            MonkeyKey.SwitchWeapon => KeyCode.X,
            MonkeyKey.PlaceTurret => KeyCode.Return,
            MonkeyKey.Fire => KeyCode.LeftArrow,
            _ => throw new System.NotImplementedException(),
        };
    }

    public static bool GetDown(MonkeyKey k)
    {
        return VR_GetDown(k) || PC_GetDown(k);
    }


    public static bool GetUp(MonkeyKey k)
    {
        return VR_GetUp(k) || PC_GetUp(k);
    }
    public static bool Get(MonkeyKey k)
    {
        return VR_Get(k) || PC_Get(k);
    }
}
