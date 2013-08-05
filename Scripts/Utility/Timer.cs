using System;
using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour
{
    public float TickTime = 1;

    [HideInInspector]
    public float TimeLeft;

    public event EventHandler Tick;

    public bool Enabled = false;

	// Use this for initialization
	void Start ()
	{
        TimeLeft = TickTime;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(Enabled)
	    {
	        TimeLeft -= Time.deltaTime;
	    }
        if(TimeLeft<=0)
        {
			OnTick();
            Enabled = false;

        }
	}

    protected void OnTick()
    {
        if (Tick != null) Tick(this, EventArgs.Empty);
    }
}
