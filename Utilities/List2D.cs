using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class List2D<T>
{
  [SerializeField]
  private List<List<T>> posXposY;

  [SerializeField]
  private List<List<T>> posXnegY;

  [SerializeField]
  private List<List<T>> negXposY;

  [SerializeField]
  private List<List<T>> negXnegY;

  public List2D()
  {
    posXposY = new List<List<T>>();
    posXnegY = new List<List<T>>();
    negXposY = new List<List<T>>();
    negXnegY = new List<List<T>>();
  }

  public T this[int x, int y]
  {
    get
    {
      if (x >= 0)
      {
        if (y >= 0)
        {
          if (posXposY.Count <= x)
          {
            for (int i = posXposY.Count; i <= x; i++)
            {
              posXposY.Add(new List<T>());
            }
          }

          if (posXposY[x].Count <= y)
          {
            for (int i = posXposY[x].Count; i <= y; i++)
            {
              posXposY[x].Add(default(T));
            }
          }

          return posXposY[x][y];
        }
        else
        {
          int negY = (y * -1) - 1;

          if (posXnegY.Count <= x)
          {
            for (int i = posXnegY.Count; i <= x; i++)
            {
              posXnegY.Add(new List<T>());
            }
          }

          if (posXnegY[x].Count <= negY)
          {
            for (int i = posXnegY[x].Count; i <= negY; i++)
            {
              posXnegY[x].Add(default(T));
            }
          }

          return posXnegY[x][negY];
        }
      }
      else
      {
        int negX = (x * -1) - 1;

        if (y >= 0)
        {
          if (negXposY.Count <= negX)
          {
            for (int i = negXposY.Count; i <= negX; i++)
            {
              negXposY.Add(new List<T>());
            }
          }

          if (negXposY[negX].Count <= y)
          {
            for (int i = negXposY[negX].Count; i <= y; i++)
            {
              negXposY[negX].Add(default(T));
            }
          }

          return negXposY[negX][y];
        }
        else
        {
          int negY = (y * -1) - 1;

          if (negXnegY.Count <= negX)
          {
            for (int i = negXnegY.Count; i <= negX; i++)
            {
              negXnegY.Add(new List<T>());
            }
          }

          if (negXnegY[negX].Count <= negY)
          {
            for (int i = negXnegY[negX].Count; i <= negY; i++)
            {
              negXnegY[negX].Add(default(T));
            }
          }

          return negXnegY[negX][negY];
        }
      }
    }
    set
    {
      if (x >= 0)
      {
        if (y >= 0)
        {
          if (posXposY.Count <= x)
          {
            for (int i = posXposY.Count; i <= x; i++)
            {
              posXposY.Add(new List<T>());
            }
          }

          if (posXposY[x].Count <= y)
          {
            for (int i = posXposY[x].Count; i <= y; i++)
            {
              posXposY[x].Add(default(T));
            }
          }
          else
            posXposY[x][y] = value;
        }
        else
        {
          int negY = (y * -1) - 1;

          if (posXnegY.Count <= x)
          {
            for (int i = posXnegY.Count; i <= x; i++)
            {
              posXnegY.Add(new List<T>());
            }
          }

          if (posXnegY[x].Count <= negY)
          {
            for (int i = posXnegY[x].Count; i <= negY; i++)
            {
              posXnegY[x].Add(default(T));
            }
          }
          else
            posXnegY[x][negY] = value;
        }
      }
      else
      {
        int negX = (x * -1) - 1;

        if (y >= 0)
        {
          if (negXposY.Count <= negX)
          {
            for (int i = negXposY.Count; i <= negX; i++)
            {
              negXposY.Add(new List<T>());
            }
          }

          if (negXposY[negX].Count <= y)
          {
            for (int i = negXposY[negX].Count; i <= y; i++)
            {
              negXposY[negX].Add(default(T));
            }
          }
          else
            negXposY[negX][y] = value;
        }
        else
        {
          int negY = (y * -1) - 1;

          if (negXnegY.Count <= negX)
          {
            for (int i = negXnegY.Count; i <= negX; i++)
            {
              negXnegY.Add(new List<T>());
            }
          }

          if (negXnegY[negX].Count <= negY)
          {
            for (int i = negXnegY[negX].Count; i <= negY; i++)
            {
              negXnegY[negX].Add(default(T));
            }
          }
          else
            negXnegY[negX][negY] = value;
        }
      }
    }
  }
}