using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class TerrainGeneration : MonoBehaviour
{
    int WORLD_SIZE = 32; // in chunks
    int CHUNKS_SIZE = 32;
    int WORLD_Y_SIZE = 8; // in chunks
    int SEED = 0;
    int SEED2 = 0;



    private PerlinNoise NoiseFunctions;
    private SpriteManager SpriteManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void GenerateChunk(Vector2Int ChunkPosition)
    {
        //Debug.Log("test");
        for (int x = 0; x < CHUNKS_SIZE; x++)
        {
            float Continentalness = PerlinNoise.PerlinNoise2D(x + ChunkPosition.x * CHUNKS_SIZE + SEED, 0, 0.01f, 0.5f, 2f, 3);
            int Height = Mathf.FloorToInt(Continentalness * 100+100);

            for (int y = 0; y < CHUNKS_SIZE; y++)
            {
                if (y + ChunkPosition.y * CHUNKS_SIZE > Height) { continue; }
                float CheeseCaves = PerlinNoise.PerlinNoise2D(x + ChunkPosition.x * CHUNKS_SIZE + SEED, y + ChunkPosition.y * CHUNKS_SIZE + SEED, 0.01f, 0.5f, 2f, 1);
                float SpaghettiCaves = Mathf.Abs((PerlinNoise.PerlinNoise2D(x + ChunkPosition.x * CHUNKS_SIZE + SEED2, y + ChunkPosition.y * CHUNKS_SIZE + SEED2, 0.005f, 0.5f, 2f, 1)-0.5f)*2);
                 if (CheeseCaves < 0.12f) { continue; }
                if (SpaghettiCaves < 0.55f && SpaghettiCaves > 0.45f) {  continue; }

                string BlockSprite = "Stone";

                if (y + ChunkPosition.y * CHUNKS_SIZE > Height - 5)        { BlockSprite = "Dirt"; }
                if (y + ChunkPosition.y * CHUNKS_SIZE == Height)           { BlockSprite = "Grass";} 
                
                SpriteManager.SpawnWithSpriteName(BlockSprite, new Vector2(x + ChunkPosition.x * CHUNKS_SIZE, y + ChunkPosition.y * CHUNKS_SIZE));


                }
            }
    }



    void Start()
    {
        SEED = Random.Range(1, 1000);
        SEED2 = Random.Range(1, 1000);
        Debug.Log("start");
        for (int x =0; x < WORLD_SIZE; x++)
        {
            for (int y = 0; y < WORLD_Y_SIZE; y++)
            {
                GenerateChunk(new Vector2Int(x, y));
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}

