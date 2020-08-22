using UnityEngine;

public class ChunkBehaviour : MonoBehaviour
{
  private TileBehaviour[,] tileBehaviours;

  public void Setup(ushort chunkSize)
  {
    tileBehaviours = new TileBehaviour[chunkSize, chunkSize];

    for (int x = 0; x < chunkSize; x++)
    {
      for (int y = 0; y < chunkSize; y++)
      {
        tileBehaviours[x, y] = new GameObject(string.Format("Tile ({0}, {1})", x, y)).AddComponent<TileBehaviour>();
        tileBehaviours[x, y].transform.SetParent(transform);
        tileBehaviours[x, y].transform.localPosition = new Vector2(x, y);
        tileBehaviours[x, y].gameObject.AddComponent<SpriteRenderer>();
        tileBehaviours[x, y].gameObject.AddComponent<BoxCollider2D>().size = new Vector2(1, 1);
      }
    }

    gameObject.name = "Chunk (empty)";
    gameObject.SetActive(false);
  }

  public void AssignChunk(Chunk chunk, Vector2 chunkPos)
  {
    gameObject.name = string.Format("Chunk ({0}, {1})", chunkPos.x, chunkPos.y);
    transform.position = chunkPos;

    for (int x = 0; x < chunk.Tiles.GetLength(0); x++)
    {
      for (int y = 0; y < chunk.Tiles.GetLength(1); y++)
      {
        tileBehaviours[x, y].GetComponent<SpriteRenderer>().sprite = chunk.Tiles[x, y].Tile().Texture;
        tileBehaviours[x, y].GetComponent<BoxCollider2D>().enabled = chunk.Tiles[x, y].Tile().Collision;
      }
    }

    gameObject.SetActive(true);
  }
}