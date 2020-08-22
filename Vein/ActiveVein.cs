using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ActiveVein
{
  public Vein Vein { get; private set; }
  public float CurrentExpansionRate { get; private set; }

  public ActiveVein(Vein vein)
  {
    Vein = vein;
    CurrentExpansionRate = vein.ExpansionRate;
  }

  public void DropCurrentExpansionRate()
  {
    CurrentExpansionRate -= Vein.ExpansionRateDropoff;
  }
}