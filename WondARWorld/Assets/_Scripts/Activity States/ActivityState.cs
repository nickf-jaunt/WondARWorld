using System;

public abstract class ActivityState
{
    public ActivityState()
    {
        Cleanup();
    }

    public abstract void Init ();
    public abstract void Tick ();
    public abstract bool SetNextState (ActivityState inputNextState);

    public abstract void Cleanup();
}
