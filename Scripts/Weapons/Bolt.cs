using UnityEngine;
using System.Collections;

public class Bolt : MonoBehaviour
{
    private bool _firedOff = false;
    public float _maxDistance = 100;
    private Hashtable _flightParams;
    public float _velocity = 50;

	// Use this for initialization
	void Start ()
    {
        

	}
	
	// Update is called once per frame
	void Update ()
    {
	}

    public void Fire()
    {
        var particle = GetComponentInChildren<ParticleSystem>();
        particle.Play();

		_flightParams = new Hashtable();


        _flightParams.Add("orienttopath", true);
        //parameters.Add("movetopath", true);
        _flightParams.Add("oncompletetarget", gameObject);
        _flightParams.Add("oncomplete", "OnComplete");
        _flightParams.Add("speed", _velocity);
        _flightParams.Add("easetype", "Linear");
        
        Vector3 pos = transform.forward * 100;
		pos.Set(pos.x, 0, pos.z);
        //new Vector3(Mathf.Cos(transform.rotation.y)*100, 0, Mathf.Sin(transform.rotation.y)*100);

        _flightParams.Add("position", pos);
        iTween.MoveTo(gameObject, _flightParams);
		_firedOff = true;
    }

    private void OnComplete()
    {
        Destroy(gameObject);
    }
}
