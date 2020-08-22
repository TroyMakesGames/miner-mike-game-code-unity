//#define debugLogPlayerChunkMovement

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChunkGrid
{
  [SerializeField]
  private List2D<Chunk> chunks;

  [SerializeField]
  private readonly ushort chunkSize;

  [SerializeField]
  private int seed;

  private readonly ushort drawDistance;
  private readonly Vein[] veinDatabase;

  private Transform chunkBehaviourParent;
  private Vector2Int playerChunkPosition;
  private Vector2Int previousChunkPositon;

  public ChunkGrid(ushort chunkSize, ushort drawDistance, int seed, Vein[] veinDatabase)
  {
    chunks = new List2D<Chunk>();
    this.chunkSize = chunkSize;
    this.drawDistance = drawDistance;
    this.seed = seed;
    this.veinDatabase = veinDatabase;
    chunkBehaviourParent = new GameObject("Chunk Grid").transform;

    playerChunkPosition = new Vector2Int(0, 0);
    DrawChunksAroundPlayer();
  }

  public void Tick(Vector2 playerWorldPosition)
  {
    Vector2Int playerChunkPosition = ChunkPosition(playerWorldPosition);

    // Check if player has moved chunks.
    if (!this.playerChunkPosition.Equals(playerChunkPosition))
    {
#if debugLogPlayerChunkMovement
    Debug.Log(string.Format("Player is now in chunk ({0}, {1})", playerChunkPosition.X, playerChunkPosition.Y));
#endif

      previousChunkPositon = this.playerChunkPosition;
      this.playerChunkPosition = playerChunkPosition;
      DrawChunksAroundPlayer();
    }
  }

  private ChunkBehaviour CreateChunkBehaviour()
  {
    ChunkBehaviour newChunkBehaviour = new GameObject().AddComponent<ChunkBehaviour>();
    newChunkBehaviour.transform.SetParent(chunkBehaviourParent);
    newChunkBehaviour.Setup(chunkSize);
    return newChunkBehaviour;
  }

  private void DrawChunksAroundPlayer()
  {
    Vector2Int playerVelocity = PlayerChunkVelocity();

    // todo, when moving diagonal, add another to either side of draw distance

    // Undraw chunks.
    Queue<ChunkBehaviour> disableChunkBehavours = new Queue<ChunkBehaviour>();
    if (playerVelocity.y == -1)
    {
      for (int x = -drawDistance; x < drawDistance + 1; x++)
      {
        Vector2Int toUndrawChunkPosition = playerChunkPosition + new Vector2Int(x, drawDistance + 1);

        if (chunks[toUndrawChunkPosition.x, toUndrawChunkPosition.y] != null && chunks[toUndrawChunkPosition.x, toUndrawChunkPosition.y].Drawn)
        {
          disableChunkBehavours.Enqueue(chunks[toUndrawChunkPosition.x, toUndrawChunkPosition.y].UnDraw());
        }
      }
    }
    else if (playerVelocity.y == +1)
    {
      for (int x = -drawDistance; x < drawDistance + 1; x++)
      {
        Vector2Int toUndrawChunkPosition = playerChunkPosition + new Vector2Int(x, -drawDistance - 1);

        if (chunks[toUndrawChunkPosition.x, toUndrawChunkPosition.y] != null && chunks[toUndrawChunkPosition.x, toUndrawChunkPosition.y].Drawn)
        {
          disableChunkBehavours.Enqueue(chunks[toUndrawChunkPosition.x, toUndrawChunkPosition.y].UnDraw());
        }
      }
    }
    if (playerVelocity.x == -1)
    {
      for (int y = -drawDistance; y < drawDistance + 1; y++)
      {
        Vector2Int toUndrawChunkPosition = playerChunkPosition + new Vector2Int(drawDistance + 1, y);

        if (chunks[toUndrawChunkPosition.x, toUndrawChunkPosition.y] != null && chunks[toUndrawChunkPosition.x, toUndrawChunkPosition.y].Drawn)
        {
          disableChunkBehavours.Enqueue(chunks[toUndrawChunkPosition.x, toUndrawChunkPosition.y].UnDraw());
        }
      }
    }
    else if (playerVelocity.x == + 1)
    {
      for (int y = -drawDistance; y < drawDistance + 1; y++)
      {
        Vector2Int toUndrawChunkPosition = playerChunkPosition + new Vector2Int(-drawDistance - 1, y);

        if (chunks[toUndrawChunkPosition.x, toUndrawChunkPosition.y] != null && chunks[toUndrawChunkPosition.x, toUndrawChunkPosition.y].Drawn)
        {
          disableChunkBehavours.Enqueue(chunks[toUndrawChunkPosition.x, toUndrawChunkPosition.y].UnDraw());
        }
      }
    }

    // Corner check.
    Vector2Int topLeftCorner = playerChunkPosition + new Vector2Int(-drawDistance - 1, -drawDistance - 1);
    if (chunks[topLeftCorner.x, topLeftCorner.y] != null && chunks[topLeftCorner.x, topLeftCorner.y].Drawn)
    {
      disableChunkBehavours.Enqueue(chunks[topLeftCorner.x, topLeftCorner.y].UnDraw());
    }
    Vector2Int topRightCorner = playerChunkPosition + new Vector2Int(-drawDistance - 1, +drawDistance + 1);
    if (chunks[topRightCorner.x, topRightCorner.y] != null && chunks[topRightCorner.x, topRightCorner.y].Drawn)
    {
      disableChunkBehavours.Enqueue(chunks[topRightCorner.x, topRightCorner.y].UnDraw());
    }
    Vector2Int bottomLeftCorner = playerChunkPosition + new Vector2Int(+drawDistance + 1, -drawDistance - 1);
    if (chunks[bottomLeftCorner.x, bottomLeftCorner.y] != null && chunks[bottomLeftCorner.x, bottomLeftCorner.y].Drawn)
    {
      disableChunkBehavours.Enqueue(chunks[bottomLeftCorner.x, bottomLeftCorner.y].UnDraw());
    }
    Vector2Int bottomRightCorner = playerChunkPosition + new Vector2Int(+drawDistance + 1, +drawDistance + 1);
    if (chunks[bottomRightCorner.x, bottomRightCorner.y] != null && chunks[bottomRightCorner.x, bottomRightCorner.y].Drawn)
    {
      disableChunkBehavours.Enqueue(chunks[bottomRightCorner.x, bottomRightCorner.y].UnDraw());
    }


    // For each chunk in player draw distance.
    for (int x = -drawDistance; x <= drawDistance; x++)
    {
      for (int y = -drawDistance; y <= drawDistance; y++)
      {
        Vector2Int chunkPosition = playerChunkPosition + new Vector2Int(x, y);

        // Generate chunk if they doesn't exist yet.
        if (chunks[chunkPosition.x, chunkPosition.y] == null)
        {
          chunks[chunkPosition.x, chunkPosition.y] = new Chunk(chunkSize, chunkPosition, seed, veinDatabase);
        }

        // Draw
        if (chunks[chunkPosition.x, chunkPosition.y].Drawn == false)
        {
          chunks[chunkPosition.x, chunkPosition.y].Draw(disableChunkBehavours.Count != 0 ? disableChunkBehavours.Dequeue() : CreateChunkBehaviour());
        }
      }
    }

    // Remove any remaining un-drawn chunks.
    for (int i = 0; i < disableChunkBehavours.Count; i++)
    {
      GameObject.Destroy(disableChunkBehavours.Dequeue());
    }
  }

  private Vector2Int PlayerChunkVelocity()
  {
    Vector2Int velocity = Vector2Int.Zero;

    if (playerChunkPosition.y > previousChunkPositon.y)
    {
      velocity.y = 1;
    }
    else if (playerChunkPosition.y < previousChunkPositon.y)
    {
      velocity.y = -1;
    }

    if (playerChunkPosition.x > previousChunkPositon.x)
    {
      velocity.x = 1;
    }
    else if (playerChunkPosition.x < previousChunkPositon.x)
    {
      velocity.x = -1;
    }

    return velocity;
  }

  private Vector2Int ChunkPosition(Vector2 worldPosition)
  {
    Vector2Int tilePosition = TilePosition(worldPosition);
    return ChunkPosition(tilePosition);
  }

  private Vector2Int ChunkPosition(Vector2Int tilePosition)
  {
    return new Vector2Int(tilePosition.x / chunkSize, tilePosition.y / chunkSize);
  }

  private Vector2Int TilePosition(Vector2 worldPosition)
  {
    return new Vector2Int(Mathf.RoundToInt(worldPosition.x), Mathf.RoundToInt(worldPosition.y));
  }
}