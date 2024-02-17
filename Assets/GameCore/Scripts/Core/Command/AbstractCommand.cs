using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractCommand
{
    protected float _commandTime;
    public float _commandTime
    {
        get { return _commandTime; }
    }
    public virtual void execute() { }
    public virtual void undo() { }

}
