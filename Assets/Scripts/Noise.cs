using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise
{
    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, int seed, float scale, int octaves, float persistence, float lacunarity, Vector2 offset)
    {

        System.Random prng = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[octaves];
        
        for(int i = 0; i < octaves; i++)
        {
            float offsetX = prng.Next(-100000, 100000);
            float offsetY = prng.Next(-100000, 100000);
            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }

        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        float halfWidth = mapWidth / 2f;
        float halfHeight = mapHeight / 2f;

        float[,] noiseMap = new float[mapWidth, mapHeight];
        float[,] noiseMapOffset = new float[mapWidth, mapHeight];

        float maxDistance = Mathf.Sqrt(Mathf.Pow(halfWidth, 2) + Mathf.Pow(halfHeight, 2));

        for (int x = 0; x < mapWidth; x++)
        {
            for(int y = 0; y < mapHeight; y++)
            {
                float distance = Mathf.Sqrt(Mathf.Pow((x - halfWidth), 2) + Mathf.Pow((y - halfHeight), 2));
                noiseMapOffset[x, y] = Mathf.InverseLerp(maxDistance, 0, distance);
                //noiseMapOffset[x, y] = Mathf.Pow(0.4f, noiseMapOffset[x, y]);
                //noiseMapOffset[x, y] = distance / maxDistance;
            }
        }

        if (scale <= 0)
        {
            scale = 0.0001f;
        }

        for(int y = 0; y < mapHeight; y++)
        {
            for(int x = 0; x < mapWidth; x++)
            {
                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 1;

                for(int i = 0; i < octaves; i++)
                {
                    float sampleX = (x - halfWidth) / scale * frequency + octaveOffsets[i].x + offset.x;
                    float sampleY = (y - halfHeight) / scale * frequency + octaveOffsets[i].y + offset.y;

                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                    noiseHeight += perlinValue * amplitude;
                    noiseMap[x, y] = perlinValue;

                    amplitude *= persistence;
                    frequency *= lacunarity;
                }

                if(noiseHeight > maxNoiseHeight)
                {
                    maxNoiseHeight = noiseHeight;
                }
                else if(noiseHeight < minNoiseHeight)
                {
                    minNoiseHeight = noiseHeight;
                }

                noiseMap[x, y] = noiseHeight;

            }
        }

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);
                noiseMap[x, y] = noiseMap[x, y] * noiseMapOffset[x, y];
            }

        }

        return noiseMap;

    }

}
