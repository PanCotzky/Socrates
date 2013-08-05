using System;
using UnityEngine;
using System.Collections;

public class WeaponsController : MonoBehaviour
{
    protected Weapon[] _weaponry;
    public Transform Target { get; set; }
	// Use this for initialization
	void Start ()
	{
	    _weaponry = GetComponentsInChildren<Weapon>();
	    foreach (Weapon weapon in _weaponry)
	    {
	        weapon.Controller = this;
	    }
	}
	
	// Update is called once per frame
	void Update ()
    {
	}

    public void AimPoint(Vector3 target)
    {
        foreach (Weapon weapon in _weaponry)
        {
            weapon.AimTarget(target);
        }
    }

    public void LockOn(Transform target)
    {
        Target = target;
    }

    private void OnTargetDestroied(object s, EventArgs e)
    {
        Target = null;
    }
}
