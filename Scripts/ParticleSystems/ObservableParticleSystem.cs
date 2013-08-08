using System;
using UnityEngine;
using System.Collections;

public class ObservableParticleSystem : MonoBehaviour
{
    public bool DieOnFinished = false;
    public ParticleSystem _system;

    public event EventHandler Died;

	// Use this for initialization
	void Start ()
	{
	    _system = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!_system.IsAlive())
        {
            Destroy(gameObject);
        }
	}

    protected void OnDied()
    {
        if (Died != null) Died(this, EventArgs.Empty);
    }
}
