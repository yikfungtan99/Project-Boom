using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ModeType
{
    DRIVE,
    ARTILLERY,
    DRONE
}

public abstract class PlayerMode : NetworkBehaviour
{
    public ModeType mode;
    protected bool modeActive = false;

    public virtual void EnterMode()
    {
        modeActive = true;
    }

    public virtual void ExitMode()
    {
        modeActive = false;
    }

    public virtual void ToggleMode()
    {
        if (!modeActive)
        {
            EnterMode();
        }
        else
        {
            ExitMode();
        }
    }
}
