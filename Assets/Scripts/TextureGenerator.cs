using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TextureGenerator {

    public static Texture2D TextureFromColourMap(Color[] colourMap,int mapWidth, int mapHeight)
    {
        Texture2D texture = new Texture2D(mapWidth * 2 + 1, mapHeight * 2);
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.SetPixels(colourMap);

        for(int x = 0; x < mapWidth; x++)
        {
            for(int y = 0; y < mapHeight; y++)
            {

                if(y % 2 == 0)
                {
                    texture.SetPixel(x * 2, y * 2, colourMap[y * mapHeight + x]);
                    texture.SetPixel(x * 2 + 1, y * 2, colourMap[y * mapHeight + x]);
                    texture.SetPixel(x * 2, y * 2 + 1, colourMap[y * mapHeight + x]);
                    texture.SetPixel(x * 2 + 1, y * 2 + 1, colourMap[y * mapHeight + x]);

                    /*for(int i = 1; i < 19; i++)
                    {
                        for(int j = 1; j < 19; j++)
                        {
                            texture.SetPixel(x * 20 + i, y * 20 + j, colourMap[y * mapHeight + x]);
                        }
                    }*/

                }
                else
                {
                    texture.SetPixel(x * 2 + 1, y * 2, colourMap[y * mapHeight + x]);
                    texture.SetPixel(x * 2 + 2, y * 2, colourMap[y * mapHeight + x]);
                    texture.SetPixel(x * 2 + 1, y * 2 + 1, colourMap[y * mapHeight + x]);
                    texture.SetPixel(x * 2 + 2, y * 2 + 1, colourMap[y * mapHeight + x]);

                    /*for (int i = 1; i < 19; i++)
                    {
                        for (int j = 1; j < 19; j++)
                        {
                            texture.SetPixel(x * 20 + i + 10, y * 20 + j, colourMap[y * mapHeight + x]);
                        }
                    }*/

                }

            }
        }

        texture.Apply();
        return texture;

        //mod colour map to make each pixel 2x2, then offset for each alternating row to create hex field;
        //track the type of each possible location in a 2d array instead of creating one object per location;
        //implement unit movement and line of sight
        //track array of visible locations and place translucent filter over unknowns
    }

    public static Texture2D TextureFromHeightMap(float[,] noiseMap)
    {
        int width = noiseMap.GetLength(0);
        int height = noiseMap.GetLength(1);

        Color[] colourMap = new Color[width * height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                colourMap[y * width + x] = Color.Lerp(Color.black, Color.white, noiseMap[x, y]);
            }
        }

        return TextureFromColourMap(colourMap, width, height);
    }

}
