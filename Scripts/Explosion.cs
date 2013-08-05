using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour
{
    protected Detonator[] _explosions;
    protected float timer = 0;

	// Use this for initialization
	void Start ()
	{
        _explosions = GetComponentsInChildren<Detonator>();

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
