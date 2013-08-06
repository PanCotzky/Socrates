using UnityEngine;
using System.Collections;

public class Bolt : MonoBehaviour
{
    private bool _firedOff = false;
    public float _maxDistance = 100;
    private Hashtable _flightParams;
    public float _velocity = 50;
    protected Transform _onHitExplosion;
    public Transform HitEffect;

    public float Power;

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

    public void Fire(float spread, Transform target)
    {
        Target = target;
        if (Target == null)
        {
            Destroy(gameObject);
            return;
        }

        var particles = GetComponentsInChildren<ParticleSystem>();

        foreach (ParticleSystem particle in particles)
        {
            particle.Play();
        }

		_flightParams = new Hashtable();


        _flightParams.Add("orienttopath", true);
        //parameters.Add("movetopath", true);
        _flightParams.Add("oncompletetarget", gameObject);
        _flightParams.Add("oncomplete", "OnComplete");
        _flightParams.Add("speed", _velocity);
        _flightParams.Add("easetype", "Linear");

        Vector3 pos = Target.position;//transform.forward * 100;
        float distance = Vector3.Distance(transform.position, pos);
        float disp = distance*0.1f;

        pos.Set(pos.x + Random.Range(-disp, disp), 0, pos.z + Random.Range(-disp, disp));
        //new Vector3(Mathf.Cos(transform.rotation.y)*100, 0, Mathf.Sin(transform.rotation.y)*100);

        _flightParams.Add("position", pos);
        iTween.MoveTo(gameObject, _flightParams);
		_firedOff = true;
    }

    private void OnComplete()
    {
        //var exp = transform.FindChild("HitEffect");
        Vector3 position = new Vector3(transform.position.x, 1, transform.position.z);
        var newExp = Instantiate(HitEffect, position, transform.rotation) as Transform;
        var hitEffect = newExp.GetComponent<HitEffect>();

        if (hitEffect)
        {
            hitEffect.Fire();
        }
        //else
        //{
        //    var particleComponent = newExp.GetComponent<ParticleSystem>();
        //    var observable = newExp.GetComponent<ObservableSpriteSystem>();
        //    observable.enabled = true;
        //    particleComponent.Play();
        //}
        

        //var camera = GameObject.Find("MainCamera");//.GetComponent<Camera>();
        //iTween.ShakePosition(camera, new Vector3(0.2f, 0, 0.2f), 0);
		
		
        Destroy(gameObject);
        
        var ship = Target.GetComponent<Ship>();
        ship.HitPointsLeft -= Power;
    }
}
