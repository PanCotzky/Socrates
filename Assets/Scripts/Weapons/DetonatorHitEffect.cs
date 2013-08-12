using UnityEngine;
using System.Collections;

public class DetonatorHitEffect : HitEffect
{

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void Fire()
    {
        var detonatorComponent = GetComponent<Detonator>();
        detonatorComponent.enabled = true;
        detonatorComponent.Explode();
		var camera = GameObject.Find("MainCamera");//.GetComponent<Camera>();
        iTween.ShakePosition(camera, new Vector3(0.2f, 0, 0.2f), 0);
    }
}
