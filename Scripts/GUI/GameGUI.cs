using UnityEngine;
using System.Collections;

public class GameGUI : MonoBehaviour
{
    public PlayerController PlayerController;

	// Use this for initialization
	void Start ()
	{
	    
	    if(!PlayerController)
	    {
            PlayerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
            if(!PlayerController)
            {
                Debug.LogError("Unity.Start()" + name + "has no Player Controller!");
                enabled = false;
            }
            
	    }
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
    
    void OnGUI()
    {
        if(PlayerController.SelectedCharacter != null)
        {

            //if (GUI.Button(new Rect(Screen.width - 110, Screen.height - 50, 100, 40), "I am a button"))
            //{
            //    print("You clicked the button!");
            //}
        }
        
    }
}
