using UnityEngine;
using System.Collections;

public class Pseudo2DModel : MonoBehaviour
{



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        //if (!OT.isValid) return;

        //// check if we have to initialize
        //if (!initialized)
        //    Initialize();

        //// only scroll when left mouse button pressed
        //if (Input.GetMouseButton(0))
        //{
        //    // get delta position relative to screen center
        //    Vector2 delta = (Input.mousePosition - new Vector3(Screen.width / 2, Screen.height / 2, 0));
        //    delta = new Vector2(delta.x / Screen.width, delta.y / Screen.height);
        //    // set scroll speed of layers - the more backwards the less scroll
        //    SetScroll("filled 1", delta * 60);
        //    SetScroll("filled 2", delta * 50);
        //    SetScroll("filled 3", delta * 40);
        //    SetScroll("filled 4", delta * 30);
        //    scrolling = true;
        //}
        //else
        //{
        //    // check if we are scrolling
        //    if (scrolling)
        //    {
        //        // stop scrolling
        //        SetScroll("filled 1", Vector2.zero);
        //        SetScroll("filled 2", Vector2.zero);
        //        SetScroll("filled 3", Vector2.zero);
        //        SetScroll("filled 4", Vector2.zero);
        //        scrolling = false;
        //    }
        //}
	}

    // Set scroll speed for a specific FilledSprite providing its name
    void SetScroll(string spriteName, Vector2 scrollSpeed)
    {
        // lookup sprite
        OTObject sprite = OT.ObjectByName(spriteName);
        // set scroll speed
        if (sprite != null)
            (sprite as OTFilledSprite).scrollSpeed = scrollSpeed;
    }
}
