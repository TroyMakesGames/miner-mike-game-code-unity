using UnityEngine;

[System.Serializable]
public struct Vector2Int
{
  [SerializeField]
  public int x;

  [SerializeField]
  public int y;

  public Vector2Int(int x, int y)
  {
    this.x = x;
    this.y = y;
  }

  public static Vector2Int Zero
  {
    get
    {
      return new Vector2Int(0, 0);
    }
  }

  public override bool Equals(object o)
  {
    if (!o.GetType().Equals(typeof(Vector2Int)))
      return false;

    Vector2Int other = (Vector2Int)o;

    if (x != other.x) return false;
    if (y != other.y) return false;
    return true;
  }

  public static Vector2Int operator +(Vector2Int value1, Vector2Int value2)
  {
    return new Vector2Int(value1.x + value2.x, value1.y + value2.y);
  }

  public static Vector2Int operator -(Vector2Int value1, Vector2Int value2)
  {
    return new Vector2Int(value1.x - value2.x, value1.y - value2.y);
  }

  public static Vector2Int operator *(Vector2Int value1, Vector2Int value2)
  {
    return new Vector2Int(value1.x * value2.x, value1.y * value2.y);
  }

  public static Vector2Int operator /(Vector2Int value1, Vector2Int value2)
  {
    return new Vector2Int(value1.x / value2.x, value1.y / value2.y);
  }

  public static Vector2Int operator *(Vector2Int value1, int value2)
  {
    return new Vector2Int(value1.x * value2, value1.y * value2);
  }

  public static Vector2Int operator /(Vector2Int value1, int value2)
  {
    return new Vector2Int(value1.x / value2, value1.y / value2);
  }

  public Vector2 ToVector2()
  {
    return new Vector2(x, y);
  }
}