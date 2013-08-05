using System;
using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour
{
    #region Fields

    private Hashtable _moveStreightToParams;
    #endregion

    #region Events

    public event EventHandler<EventArgs> Finished;

    #endregion

    public Vector3 Target;

    

	// Use this for initialization
	void Start ()
    {
        _moveStreightToParams = new Hashtable();


        _moveStreightToParams.Add("orienttopath", true);
        //parameters.Add("movetopath", true);
        _moveStreightToParams.Add("oncompletetarget", gameObject);
        _moveStreightToParams.Add("oncomplete", "OnFinished");
        _moveStreightToParams.Add("speed", 50);
        _moveStreightToParams.Add("path", null);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void MoveStraightTo(Vector3 waypoint)
    {
        if (waypoint == null) return;

        Target = waypoint;
        Target.Set(Target.x, 0, Target.z);

        _moveStreightToParams["path"] = new [] { transform.position, Target };
        
        iTween.MoveTo(gameObject, _moveStreightToParams);
    }

    protected void OnFinished()
    {
        if (Finished != null) Finished(this, EventArgs.Empty);
    }
}
