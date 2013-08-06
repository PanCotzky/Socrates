using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour
{
    protected Detonator[] _explosions;
	
    protected float timer = 0;
    private Hashtable _cameraTiltParams;

	// Use this for initialization
	public void Start ()
	{
        _explosions = GetComponentsInChildren<Detonator>();
		
        _cameraTiltParams = new Hashtable();

        _cameraTiltParams.Add("amount", new Vector3(5, 0, 5));
        _cameraTiltParams.Add("delay", 3);
	    var camera = GameObject.Find("MainCamera");//.GetComponent<Camera>();
        iTween.ShakePosition(camera, _cameraTiltParams);
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

    public void Detonate()
    {
		
        
        foreach (Detonator explosion in _explosions)
        {
            explosion.Explode();
        }
    }
}
