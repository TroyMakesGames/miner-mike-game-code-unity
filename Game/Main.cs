using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
  [SerializeField]
  private PlayerBehaviour playerPrefab;

  [SerializeField]
  private string seed = "";

  [SerializeField]
  private float tickDelay = 1f;

  [SerializeField]
  private ushort chunkSize = 16;

  [SerializeField]
  private ushort drawDistance = 2;

  [SerializeField]
  private Tile[] tiles;

  [SerializeField]
  private Vein[] veins;

  private World world;
  private float tickTimer = 0;

  private void Awake()
  {
    Debug.Assert(playerPrefab, "Player prefab not set.");
    Debug.Assert(tiles.Length > 0, "No tiles.");
    Debug.Assert(veins.Length > 0, "No veins.");
    Debug.Assert(tiles.Length > 0, "Chunk size too small.");
  }

  private void Start()
  {
    int seed = !string.IsNullOrEmpty(this.seed) ? this.seed.GetHashCode() : Random.Range(int.MinValue, int.MaxValue);
    world = new World(chunkSize, drawDistance, tiles, veins, seed, playerPrefab);
    world.Tick();
  }

  private void Update()
  {
    tickTimer += Time.deltaTime;
    if (tickTimer >= tickDelay)
    {
      world.Tick();
      tickTimer = 0;
    }
  }
}