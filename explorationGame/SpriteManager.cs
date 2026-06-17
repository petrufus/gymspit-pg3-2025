using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    void Start()
    {
    
    }

    public static void SpawnWithSpriteName(string spriteName, Vector2 position)
    {
        Sprite loadedSprite = Resources.Load<Sprite>(spriteName);

        if (loadedSprite == null)
        {

            Debug.LogError($"Could not find '{spriteName}' inside the Resources folder!");
            return;
        }

        GameObject newBlock = new GameObject(spriteName);
        newBlock.transform.position = position;

        SpriteRenderer renderer = newBlock.AddComponent<SpriteRenderer>();
        renderer.sprite = loadedSprite;
    }
}
