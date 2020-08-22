using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Vein.asset", menuName = "Data/Vein")]
public class Vein : ScriptableObject
{
  [SerializeField]
  private Tile tile;
  public Tile Tile { get { return tile; } }

  [SerializeField]
  private int minDepth = 5;
  public int MinDepth { get { return minDepth; } }

  [SerializeField]
  private int maxDepth = 500;
  public int MaxDepth { get { return maxDepth; } }

  [SerializeField]
  private AnimationCurve frequency = AnimationCurve.Linear(0, 0, 1, 0);
  public AnimationCurve Frequency { get { return frequency; } }

  [SerializeField, Range(0, 100)]
  private float expansionRate = 50;
  public float ExpansionRate { get { return expansionRate; } }

  [SerializeField, Range(0, 100)]
  private float expansionDropoff = 5;
  public float ExpansionRateDropoff { get { return expansionDropoff; } }

  [AssetIcon]
  private Sprite TileTexture()
  {
    if (tile == null)
      return null;

    return tile.Texture;
  }
}