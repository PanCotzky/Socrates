using UnityEngine;
using System.Collections;

public class Bolt : MonoBehaviour
{
    private bool _firedOff = false;
    public float _maxDistance = 100;
    private Hashtable _flightParams;
    public float _velocity = 50;
    protected Transform _onHitExplosion;
    public int Power;

    [HideInInspector]
    public Transform Target;

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

        Vector3 pos = Target.position;//transform.forward * 100;

        pos.Set(pos.x + Random.Range(-6f, 6f), 0, pos.z + Random.Range(-6f, 6f));
        //new Vector3(Mathf.Cos(transform.rotation.y)*100, 0, Mathf.Sin(transform.rotation.y)*100);

        _flightParams.Add("position", pos);
        iTween.MoveTo(gameObject, _flightParams);
		_firedOff = true;
    }

    private void OnComplete()
    {
        var exp = transform.FindChild("OnHitExplosion");
        Vector3 position = new Vector3(transform.position.x, 1, transform.position.z);
        var newExp = Instantiate(exp, position, transform.rotation) as Transform;
        var detonatorComponent = newExp.GetComponent<Detonator>();

        detonatorComponent.enabled = true;

        var camera = GameObject.Find("MainCamera");//.GetComponent<Camera>();
        iTween.ShakePosition(camera, new Vector3(0.2f, 0, 0.2f), 0);

        Destroy(gameObject);
        detonatorComponent.Explode();
        var ship = Target.GetComponent<Ship>();
        ship.HitPointsLeft -= Power;
    }
}
