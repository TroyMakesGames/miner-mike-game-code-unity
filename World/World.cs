using System;
using UnityEngine;

[Serializable]
public class World
{
  private Tile[] tileDatabase;
  private ChunkGrid chunkGrid;
  private PlayerBehaviour player;

  public World(ushort chunkSize, ushort drawDistance, Tile[] tiles, Vein[] veins, int seed, PlayerBehaviour playerPrefab)
  {
    UnityEngine.Random.InitState(seed);

    TileIdExtensions.World = this;

    // Order and verify databases.
    OrderTileDatabase(tiles);
    VerifyVeinDatabase(veins);

    player = GameObject.Instantiate(playerPrefab, Vector2.zero, Quaternion.identity);
    chunkGrid = new ChunkGrid(chunkSize, drawDistance, seed, veins);
  }

  // Called every second (by default).
  public void Tick()
  {
    chunkGrid.Tick(player.transform.position);
  }

  // todo, move this into a tiledatabase class
  private void OrderTileDatabase(Tile[] unsortedTiles)
  {
    // Create tile array in order of all the tiles.
    tileDatabase = new Tile[Enum.GetValues(typeof(TileId)).Length];
    for (int i = 0; i < unsortedTiles.Length; i++)
    {
      if (tileDatabase[(int)unsortedTiles[i].TileId] != null)
      {
        Debug.LogWarning("Multiple tile classes for TileId: " + unsortedTiles[i].TileId);
      }
      tileDatabase[(int) unsortedTiles[i].TileId] = unsortedTiles[i];
    }

    VerifyTileDatabase();
  }

  // todo, move this into a tiledatabase class
  private void VerifyTileDatabase()
  {
    for (int i = 0; i < tileDatabase.Length; i++)
    {
      if (tileDatabase[i] == null)
      {
        Debug.LogWarning("No tile class for TileId: " + (TileId)i);
      }
    }
  }

  // todo, move this into a vein database class.
  private void VerifyVeinDatabase(Vein[] veins)
  {
    Debug.LogWarning("Todo, verify vein database.");
  }

  // todo, move this into a tiledatabase class
  public Tile GetTile(TileId id)
  {
    return tileDatabase[(int)id];
  }
}