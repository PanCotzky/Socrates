using System;
using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour
{
    #region Movement

    #region Fields

    /// <summary>
    ///  Parameters for movement streight to point
    /// </summary>

    private Hashtable _moveStreightToParams;

    #endregion

    #region Properties

    /// <summary>
    ///  Speed of engine
    /// </summary>

    #region Editable

    public float EngineSpeed = 50;

    public UnityEngine.Object DeathEffect;
    
    #endregion 

    public Vector3 Destenation { get; protected set; }

    #endregion

    #region Events

    public event EventHandler<EventArgs> MovementComplete;

    #endregion

    #region Functions

    public void MoveStraightTo(Vector3 waypoint)
    {
        if (waypoint == null) return;

        Destenation = waypoint;
        Destenation.Set(Destenation.x, 0, Destenation.z);

        _moveStreightToParams["speed"] = EngineSpeed;
        //var littleForvord = transform.position+transform.forward * 20;
        _moveStreightToParams["path"] = new[] { transform.position, Destenation };

        iTween.MoveTo(gameObject, _moveStreightToParams);
    }

    protected void OnMovementComplete()
    {
        if (MovementComplete != null) MovementComplete(this, EventArgs.Empty);
    }

    #endregion

    #endregion


    #region Weaponry

    #region Fields

    protected Weapon _weapon;

    #endregion

    #endregion

    #region Hull

    #region Properties

    public int HitPoints = 100;

    public float HitPointsLeft { get; set; }

    [HideInInspector]
    public bool Dead { get; protected set; }

    #endregion

    #endregion

    // Use this for initialization
	void Start ()
	{
	    Dead = false;
	    HitPointsLeft = HitPoints;
        var disapearTimer = GetComponent<Timer>();
	    disapearTimer.Tick += OnDisapear;

	    //_weapon = transform.Find("Cannon").GetComponent<Weapon>();

        _moveStreightToParams = new Hashtable();


        _moveStreightToParams.Add("orienttopath", true);
        //parameters.Add("movetopath", true);
        _moveStreightToParams.Add("oncompletetarget", gameObject);
        _moveStreightToParams.Add("oncomplete", "OnMovementComplete");

        // Configurable
        _moveStreightToParams.Add("speed", null);
        _moveStreightToParams.Add("path", null);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (HitPointsLeft <= 0 && !Dead)
	    {
	        Die();
	    }
	}

    private void Die()
    {
        Vector3 pos = new Vector3(transform.position.x, 1, transform.position.z);
        GameObject explosion = Instantiate(DeathEffect/*Resources.Load("Cruiser/CruiserExplosion")*/, pos, transform.rotation) as GameObject;
        var expComponent = explosion.GetComponent<Explosion>();
        expComponent.Start();
		expComponent.enabled = true;
        //expComponent.Detonate();
        var disapearTimer = GetComponent<Timer>();
        disapearTimer.Enabled = true;
        Dead = true;
    }



    public void OnDisapear(object s, EventArgs e)
    {
        var sprite = transform.Find("Sprite");
        Destroy(gameObject);
        //sprite.GetComponent<OTSprite>().renderer.enabled = false;
    }

    
}
