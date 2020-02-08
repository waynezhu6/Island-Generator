using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Vector2 translation = new Vector2(0.01f, 0.01f);
    public bool animate = false;

    void Start () {

        animate = false;
        GameObject MapGenerator = GameObject.Find("Map Generator");
        MapGenerator MapGen = MapGenerator.GetComponent<MapGenerator>();
        MapGen.GenerateMap();

	}
	
	// Update is called once per frame
	void Update () {

        GameObject MapGenerator = GameObject.Find("Map Generator");
        MapGenerator MapGen = MapGenerator.GetComponent<MapGenerator>();

        if(animate)
        {
            MapGen.offset = MapGen.offset + translation;
            MapGen.GenerateMap();
        }

    }

    public void ButtonPress()
    {
        GameObject MapGenerator = GameObject.Find("Map Generator");
        MapGenerator MapGen = MapGenerator.GetComponent<MapGenerator>();
        MapGen.seed = Random.Range(-100000, 100000);
        MapGen.GenerateMap();
    }

    public void AnimateTrigger()
    {
        animate = !animate;
    }

}
