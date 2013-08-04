using Pathfinding;
using UnityEngine;
using System.Collections;

public class LevelSettings : MonoBehaviour
{
    public int Width;
    public int Height;
    private Transform _invisibleFloor;
    private AstarPath _pathMap;

	// Use this for initialization
	void Start ()
    {
	    _pathMap = GameObject.Find("PathMap").GetComponent<AstarPath>();
		var graph = (_pathMap.graphs[0] as GridGraph);
		
		_invisibleFloor = GameObject.Find("InvisibleFloor").transform;
		
		_invisibleFloor.localScale = new Vector3(graph.width * 4, 0, graph.depth*4);
        //_invisibleFloor.localScale.Set(graph.width * 4, 0, graph.depth*4);
		//_pathMap.Scan();
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
