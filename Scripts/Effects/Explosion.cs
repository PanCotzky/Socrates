using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour
{
    protected Detonator[] _explosions;
    protected float timer = 0;
    private Hashtable _cameraTiltParams;
    public float TiltPower = 2;
    public float Delay = 3;

	// Use this for initialization
	public void Start ()
	{
        _explosions = GetComponentsInChildren<Detonator>();
		
        _cameraTiltParams = new Hashtable();

        _cameraTiltParams.Add("amount", new Vector3(TiltPower, 0, TiltPower));
        _cameraTiltParams.Add("delay", Delay);
	    var camera = GameObject.Find("MainCamera");//.GetComponent<Camera>();
        iTween.ShakePosition(camera, _cameraTiltParams);
	}
	
	// Update is called once per frame
	void Update ()
	{
	    var comp = GetComponentsInChildren<Detonator>();
        if(comp==null || comp.Length==0)
        {
            Destroy(gameObject);
        }
	}

    public void Detonate()
    {
        foreach (Detonator explosion in _explosions)
        {
            explosion.Explode();
        }
    }
}
