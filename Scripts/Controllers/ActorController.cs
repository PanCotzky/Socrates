using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class ActorController : MonoBehaviour
{
    public Collider Collider;
    public List<AICommand> Commands;
    public float MoveSpeed = 100F;
    public float NextWaypointDistance = 3;
	public AstarPath _pathFinder;
    public Weapon Weapon { get; set; }
    public Transform Target;

	// Use this for initialization
	void Start ()
    {
        //var obj = GameObject.Find("PathMap");
		//_pathFinder = 
	    Commands = new List<AICommand>();

        var gun = transform.Find("Cannon");
        Weapon = gun.GetComponent<Weapon>();

        if (!Weapon)
        {
            Debug.LogError("Unity.Start()" + name + "No cannons found!");
            enabled = false;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if(Commands.Count > 0)
	    {
			if(!Commands[0].Enabled) Commands[0].Run();

	        if(Commands[0].Process())
	        {
	            PopCommand();
	        }
	    }
	}

    private void PopCommand()
    {
		if(Commands.Count > 0)
	    {
			Commands.RemoveAt(0);
	    }
        
    }

    public void PushCommand(AICommand command)
    {
        PopCommand();
        command.Character = this;
        Commands.Add(command);
    }

    public void OnComplete()
    {
        if(Commands.Count > 0)
	    {
			Commands[0].OnComplete();
	    }
    }



    public void Aim(Vector3 transform)
    {
        if(Weapon)
        {
            Weapon.AimTarget(transform);
        }
    }

    public void Fire(Transform transform)
    {
        if (Weapon)
        {
            Weapon.Fire();
        }
    }
}
