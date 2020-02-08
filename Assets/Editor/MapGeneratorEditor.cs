using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapGenerator))]
public class MapGeneratorEditor : Editor {

    public override void OnInspectorGUI()
    {
        MapGenerator MapGen = (MapGenerator)target;

        if(DrawDefaultInspector())
        {
            if(MapGen.autoUpdate)
            {
                MapGen.GenerateMap();
            }
        }

        if(GUILayout.Button("Generate"))
        {
            MapGen.GenerateMap();
        }

        if (GUILayout.Button("Animate"))
        {
            GameObject Camera = GameObject.Find("MainCamera");
            GameManager GM = Camera.GetComponent<GameManager>();
            GM.animate = true;
        }
        else
        {
            GameObject Camera = GameObject.Find("MainCamera");
            GameManager GM = Camera.GetComponent<GameManager>();
            GM.animate = false;
        }
    }
}
