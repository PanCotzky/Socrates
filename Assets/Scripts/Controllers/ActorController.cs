using System;
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
    public int ActionPoints;

    public MovementController MovementController { get; protected set; }
    public WeaponsController WeaponsController { get; protected set; }

    public Weapon Weapon { get; set; }
    public Transform Target;

	// Use this for initialization
	void Start ()
    {
        //var obj = GameObject.Find("PathMap");
		//_pathFinder = 
	    Commands = new List<AICommand>();

        MovementController = GetComponent<MovementController>();
		if (!MovementController)
        {
            Debug.LogError("Unity.Start()" + name + "has no Movement Controller!");
            enabled = false;
        }
	    MovementController.Finished += OnCommandFinished;

        WeaponsController = transform.Find("Weaponry").GetComponent<WeaponsController>();
        if (!WeaponsController)
        {
            Debug.LogError("Unity.Start()" + name + "has no Movement Controller!");
            enabled = false;
        }

        //var gun = transform.Find("Cannon");
        //Weapon = gun.GetComponent<Weapon>();

        //if (!Weapon)
        //{
        //    Debug.LogError("Unity.Start()" + name + "No cannons found!");
        //    enabled = false;
        //}
    }
	
	// Update is called once per frame
	void Update ()
    {
        //if(Commands.Count > 0)
        //{
        //    if(!Commands[0].Enabled) Commands[0].Run();

        //    if(Commands[0].Process())
        //    {
        //        PopCommand();
        //    }
        //}
	}

    private void PopCommand()
    {
        // If there are commands, pop the first of them
		if(Commands.Count > 0)
	    {
			Commands.RemoveAt(0);
	    }

        // If there is still some commands, run the first of them
        if (Commands.Count > 0)
        {
            // If can't run, pop this command
            if (!Commands[0].Run())
                PopCommand();
        }
        
    }

    public void PushCommand(AICommand command)
    {
        PopCommand();
        command.Character = this;
        Commands.Add(command);

        // If new command is the first command in the queue, run it
        if(Commands.Count == 1)
        {
            // If can't run, pop this command
            if(!Commands[0].Run())
                PopCommand();
        }
    }

    public void OnComplete()
    {
        if(Commands.Count > 0)
	    {
			Commands[0].OnComplete();
	    }
    }



    public void Aim(Vector3 target)
    {
        if (WeaponsController)
        {
            WeaponsController.AimPoint(target);
        }
    }

    //public void Fire(Transform target)
    //{
    //    if (Weapon)
    //    {
    //        Weapon.Fire();
    //    }
    //}

    public void OnCommandFinished(object s, EventArgs e)
    {
        PopCommand();
    }
}
