using System;
using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    public Transform Target 
    {
        get
        {
            if(Controller!=null)
            {
                return Controller.Target;
            }
            return null;
        }
    }

	public float Cooldown = 1;
	public float CooldownLeft {get; protected set;}
    public WeaponsController Controller { get; set; }
    public event EventHandler TargetDestroyed;
    public float Spread = 0.1f;
    public string WeaponName;
    public Hashtable _rotationProps;

    //public float MinFireDelay=0;
    //public float MaxFireDelay;
    public int ShotsInRound = 1;

    protected int ShotsMade = 0;

    public bool OneRoundPerFire = true;


    public bool FireOrderGiven { get; set; }

    protected ParticleSystem _gunFireEffect;
	// Use this for initialization
	void Start ()
    {
	    CooldownLeft = 0;
        _rotationProps = new Hashtable();

        _rotationProps.Add("looktarget", null);
        _rotationProps.Add("axis", "Y");
        _rotationProps.Add("time", 1);

	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Target && FireOrderGiven && (OneRoundPerFire && ShotsMade < ShotsInRound))
        {
            _rotationProps["looktarget"] = Target.position;
            iTween.LookUpdate(gameObject, _rotationProps);
            Shoot();
	    }
	    UpdateCoolDown(Time.deltaTime);
    }

    private void UpdateCoolDown(float p)
    {
        if (CooldownLeft > 0)
        {
            var deltaTimeInSec = p;
            CooldownLeft = (deltaTimeInSec > CooldownLeft) ? 0 : CooldownLeft - deltaTimeInSec;
        }
    }

    //public void LockOn(Transform target)
    //{
    //    Target = target;
    //}

    public void AimTarget(Vector3 target)
    {
        if(Target == null)
        {
            iTween.LookUpdate(gameObject, target, 0);
        }
        //iTween.LookTo(gameObject, target, 0);
    }

    public void Shoot()
    {
        var ship = Target.GetComponent<Ship>();
        if (ship != null && ship.Dead)
        {
            OnTargetDestroyed();
            return;
        }

		if(CooldownLeft<=0)
		{
			var bolt = transform.FindChild("Shot");
	        var newBolt = Instantiate(bolt, bolt.position, transform.rotation) as Transform;
	        var boltComponent = newBolt.GetComponent<Bolt>();

		    var gunFire = transform.Find("GunFire");
		    _gunFireEffect = gunFire.GetComponent<ParticleSystem>();

		    boltComponent.Target = Target;
			boltComponent.enabled= true;
            _gunFireEffect.Play();//Emit(50);
            boltComponent.Fire(Spread, Target);
			ResetCoolDown();
		    ShotsMade++;
		}
        
    }

    public void Fire()
    {
        if(Target)
        {
            ShotsMade = 0;
            FireOrderGiven = true;
        }
    }

    private void ResetCoolDown()
    {
        CooldownLeft = Cooldown;
    }

    protected void OnTargetDestroyed()
    {
        FireOrderGiven = false;
        if (TargetDestroyed != null) TargetDestroyed(this, EventArgs.Empty);
    }

    
}
