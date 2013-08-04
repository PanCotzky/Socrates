using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    public Transform Target;
	public float CoolDown = 1;
	public float CoolDownLeft {get; protected set;}
		
	// Use this for initialization
	void Start ()
    {
	    CoolDownLeft = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(Target)
	    {
            iTween.LookUpdate(gameObject, Target.position, 0);
	    }
	    UpdateCoolDown(Time.deltaTime);
    }

    private void UpdateCoolDown(float p)
    {
        if (CoolDownLeft > 0)
        {
            var deltaTimeInSec = p;
            CoolDownLeft = (deltaTimeInSec > CoolDownLeft) ? 0 : CoolDownLeft - deltaTimeInSec;
        }
    }

    public void LockOn(Transform target)
    {
        Target = target;
    }

    public void AimTarget(Vector3 target)
    {
        if(Target == null)
        {
            iTween.LookUpdate(gameObject, target, 0);
        }
        //iTween.LookTo(gameObject, target, 0);
    }

    public void AimDirection(Quaternion direction)
    {

    }

    public void Fire()
    {
		if(CoolDownLeft<=0)
		{
			var bolt = transform.FindChild("Bolt");
	        var newBolt = Instantiate(bolt, bolt.position, transform.rotation) as Transform;
	        var boltComponent = newBolt.GetComponent<Bolt>();
	
			boltComponent.enabled= true;
	        boltComponent.Fire();
			ResetCoolDown();
		}
        
    }

    private void ResetCoolDown()
    {
        CoolDownLeft = CoolDown;
    }

    public void Fire(Vector3 direction)
    {
        
    }

    internal void Fire(ActorController target)
    {
        throw new System.NotImplementedException();
    }
}
