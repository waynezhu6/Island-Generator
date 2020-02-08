using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

    public enum DrawMode { noiseMap, colourMap }
    public DrawMode drawMode;

    public int mapWidth;
    public int mapHeight;
    public float noiseScale;
    public bool autoUpdate;
    public int octaves;
    [Range(0,1)]
    public float persistence;
    public float lacunarity;
    public int seed;
    public Vector2 offset;
    public TerrainType[] regions;

    public void GenerateMap()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, seed, noiseScale, octaves, persistence, lacunarity, offset);
        Color[] colourMap = new Color[mapWidth * mapHeight];

        for(int y = 0; y < mapHeight; y++)
        {
            for(int x = 0; x < mapWidth; x++)
            {
                float currentHeight = noiseMap[x, y];
                
                for(int i = 0; i < regions.Length; i++)
                {
                    if(currentHeight <= regions[i].height)
                    {
                        colourMap[y * mapWidth + x] = regions[i].color;
                        break;
                    }
                }
            }
        }

        MapDisplay display = FindObjectOfType<MapDisplay>();

        if(drawMode == DrawMode.noiseMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap));
        }
        else if(drawMode == DrawMode.colourMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromColourMap(colourMap, mapWidth, mapHeight));
        }

        /*for(int x = 0; x < mapWidth; x++)
        {
            for(int y = 0; y < mapHeight; y++)
            {
                if (y % 2 == 1)
                {

                }

                Vector3 instantiateLocation = new Vector3(x * 10, y * 10, -10);
                GameObject tile = Instantiate(deepWater, instantiateLocation, Quaternion.identity);
                tile.transform.SetParent(GameObject.Find("TileHolder").transform);

            }
        }*/

    }

    public void DrawModeToggle()
    {
        if(drawMode == DrawMode.colourMap)
        {
            drawMode = DrawMode.noiseMap;
        }
        else
        {
            drawMode = DrawMode.colourMap;
        }
        GenerateMap();

    }

    void OnValidate()
    {
        if(mapWidth < 1)
        {
            mapWidth = 1;
        }
        if(mapHeight < 1)
        {
            mapHeight = 1;
        }
        if(lacunarity < 1)
        {
            lacunarity = 1;
        }
        if(octaves < 0)
        {
            octaves = 0;
        }
        
    }

    [System.Serializable]
    public struct TerrainType
    {
        public string name;
        public float height;
        public Color color;
    }
    
}
