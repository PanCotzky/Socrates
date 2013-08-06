using UnityEngine;
using System.Collections;

public class SpriteHitEffect : HitEffect
{

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void Fire()
    {
        var particleComponent = GetComponent<ParticleSystem>();
        var observable = GetComponent<ObservableSpriteSystem>();
        observable.enabled = true;
        particleComponent.Play();
    }
}
