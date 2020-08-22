using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tile.asset", menuName = "Data/Tile")]
public class Tile : ScriptableObject
{
  [SerializeField]
  private TileId tileId;
  public TileId TileId { get { return tileId; } }

  [SerializeField, AssetIcon]
  private Sprite texture;
  public Sprite Texture { get { return texture; } }

  [SerializeField]
  private bool collision = true;
  public bool Collision { get { return collision; } }
}