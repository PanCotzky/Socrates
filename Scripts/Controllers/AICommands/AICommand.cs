using UnityEngine;
using System.Collections;

public class AICommand
{
    public ActorController Character;
	public bool Enabled = false;
    public bool Finished = true;
	
	public AICommand()
	{
	}
	
    /// <summary>
    /// Processes the command 
    /// </summary>
    /// <returns></returns>
    public virtual bool Process()
    {
        return Finished;
    }
	
	
	public virtual void Run()
	{}

    public virtual void OnPop()
    {
    }
	
	public virtual void OnComplete()
	{
	    Finished = true;
	}
}
