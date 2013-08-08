using System;
using UnityEngine;
using System.Collections;

public class Explosion2 : MonoBehaviour
{
    protected Detonator _detonator;
    private Hashtable _cameraTiltParams;
    public float TiltPower = 2;

	// Use this for initialization
	void Start ()
    {
        _detonator = GetComponent<Detonator>();
        if(_detonator)
        {
            _detonator.Exploded += OnExplosion;
        }
        else
        {
            Destroy(gameObject);
        }
        _cameraTiltParams = new Hashtable();

        _cameraTiltParams.Add("amount", new Vector3(TiltPower, 0, TiltPower));
        //_cameraTiltParams.Add("delay", 3);
        //.GetComponent<Camera>();
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!_detonator)
        {
            Destroy(gameObject);
        }
	}

    private void OnExplosion(object s, EventArgs e)
    {
        var camera = GameObject.Find("MainCamera");
        iTween.ShakePosition(camera, _cameraTiltParams);
    }
}
