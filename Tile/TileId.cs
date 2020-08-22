using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public enum TileId : ushort
{
  Empty,
  Grass,
  Dirt,
  Gold,
  Sky,
}

public static class TileIdExtensions
{
  public static World World { private get; set; }

  public static Tile Tile(this TileId tile)
  {
    return World.GetTile(tile);
  }
}