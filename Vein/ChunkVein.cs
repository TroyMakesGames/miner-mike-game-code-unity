//#define debugLogVeinMath

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ChunkVein
{
  public Vein Vein { get; private set; }
  public float Chance { get; private set; }

  public ChunkVein(ChunkVein chunkVein)
  {
    Vein = chunkVein.Vein;
    Chance = chunkVein.Chance;
  }

  public ChunkVein(Vein vein, int Ypos)
  {
    Vein = vein;

    float minDepth = Mathf.Abs(vein.MinDepth);
    float maxDepth = Mathf.Abs(vein.MaxDepth);
    float depth = Mathf.Abs(Ypos);

    float graphPoint = (depth - minDepth) / (maxDepth - minDepth);
    Chance = vein.Frequency.Evaluate(graphPoint);

#if debugLogVeinMath
    Debug.Log("Depth = " + depth + "   Min depth = " + minDepth + "  Max depth= " + maxDepth);
    Debug.Log("Depth sits at " + graphPoint + " between min and max");
    Debug.Log("Chance at that pos = " + Chance);
#endif
  }
}