//#define debugLog

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Chunk
{
  [SerializeField]
  private TileId[,] tiles;
  public TileId[,] Tiles { get { return tiles; } }

  [SerializeField]
  private Vector2Int position;

  private ChunkBehaviour chunkBehaviour = null;
  public bool Drawn { get { return chunkBehaviour; } }

  public Chunk(ushort chunkSize, Vector2Int position, int seed, Vein[] veinDatabase)
  {
    this.position = position;
    GenerateChunk(chunkSize, seed, veinDatabase);
  }

  private void GenerateChunk(ushort chunkSize, int seed, Vein[] veinDatabase)
  {
#if debugLog
    Debug.Log(string.Format("[Chunk] Generating new chunk at ({0}, {1})", position.X, position.Y));
#endif

    UnityEngine.Random.InitState(string.Format("{0}{1}{2}", position.x, position.y, seed).GetHashCode());

    tiles = new TileId[chunkSize, chunkSize];
    Vector2Int chunkWorldPos = new Vector2Int((position.x * chunkSize), (position.y * chunkSize));
    List<ChunkVein> avaliableVeins = AvaliableVeins(veinDatabase, chunkWorldPos);

    for (int x = 0; x < chunkSize; x++)
    {
      for (int y = 0; y < chunkSize; y++)
      {
        if (tiles[x, y] == TileId.Empty)
        {
          tiles[x, y] = GenerateTile(new Vector2Int(x, y), new Vector2Int(chunkWorldPos.x + x, chunkWorldPos.y + y), avaliableVeins, chunkSize);
        }
      }
    }
  }

  private List<ChunkVein> AvaliableVeins(Vein[] veinDatabase, Vector2Int chunkWorldPos)
  {
    List<ChunkVein> avaliableVeins = new List<ChunkVein>();
    for (int i = 0; i < veinDatabase.Length; i++)
    {
      if (chunkWorldPos.y <= veinDatabase[i].MinDepth && chunkWorldPos.y >= veinDatabase[i].MaxDepth)
      {
        avaliableVeins.Add(new ChunkVein(veinDatabase[i], chunkWorldPos.y));
      }
    }

    return avaliableVeins;
  }

  private TileId GenerateTile(Vector2Int tileChunkPos, Vector2Int tileWorldPos, List<ChunkVein> veins, ushort chunkSize)
  {
    // Air
    if (tileWorldPos.y > 0)
      return TileId.Sky;

    // Grass
    if (tileWorldPos.y == 0)
      return TileId.Grass;

    // Veins
    bool startVein = false;
    TileId vein = StartVein(tileChunkPos, tileChunkPos, veins, chunkSize, out startVein);

    // Dirt or vein.
    if (startVein)
      return vein;
    else
      return TileId.Dirt;
  }

  private TileId StartVein(Vector2Int tileChunkPos, Vector2Int chunkPos, List<ChunkVein> veins, ushort chunkSize,  out bool startVein)
  {
    Vein vein = PickVein(veins);

    if (vein != null)
    {
      startVein = true;
      SpreadVein(vein, tileChunkPos, chunkSize);
      return vein.Tile.TileId;
    }
    else
    {
      startVein = false;
      return TileId.Empty;
    }
  }

  private Vein PickVein(List<ChunkVein> veins)
  {
    float randomNum = UnityEngine.Random.Range(0, 100);
    float runningTotal = 0;

    for (int i = 0; i < veins.Count; i++)
    {
      runningTotal += veins[i].Chance;
      if (runningTotal > randomNum)
      {
        return veins[i].Vein;
      }
    }

    return null;
  }

  private void SpreadVein(Vein vein, Vector2Int position, ushort chunkSize)
  {
    ActiveVein activeVein = new ActiveVein(vein);
    Vector2Int lastRandomDirection = Vector2Int.Zero;

    while ((int)UnityEngine.Random.Range(0, 101) <= activeVein.CurrentExpansionRate)
    {
      Vector2Int randomDirection = RandomDirection();
      while (randomDirection.Equals(lastRandomDirection))
      {
        randomDirection = RandomDirection();
      }
      Vector2Int nextPosition = position + randomDirection;

      if (nextPosition.x >= 0 && nextPosition.x < chunkSize && nextPosition.y >= 0 && nextPosition.y < chunkSize)
      {
        if (tiles[nextPosition.x, nextPosition.y] == TileId.Dirt || tiles[nextPosition.x, nextPosition.y] == TileId.Empty)
        {
          tiles[nextPosition.x, nextPosition.y] = vein.Tile.TileId;
          position = nextPosition;
        }
      }

      activeVein.DropCurrentExpansionRate();
    }
  }

  private Vector2Int RandomDirection()
  {
    int offset = UnityEngine.Random.Range(0, 2) == 0 ? -1 : 1;
    if (UnityEngine.Random.Range(0, 2) == 0)
      return new Vector2Int(offset, 0);
    else
      return new Vector2Int(0, offset);
  }

  public void Draw(ChunkBehaviour chunkBehaviour)
  {
    Vector2 chunkPos = new Vector2((position.x * tiles.GetLength(0)), (position.y * tiles.GetLength(1)));
    chunkBehaviour.AssignChunk(this, chunkPos);
    this.chunkBehaviour = chunkBehaviour;
  }

  public ChunkBehaviour UnDraw()
  {
    ChunkBehaviour removingChunkBehavour = chunkBehaviour;
    chunkBehaviour = null;
    return removingChunkBehavour;
  }
}