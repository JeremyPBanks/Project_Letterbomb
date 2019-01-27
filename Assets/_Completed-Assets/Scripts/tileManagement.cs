using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITileCheckable<T>
{
    T tileCheck(Vector2 current_location, Vector2 dest_location, int grid_size);
    float findDistance(Vector2 v1, Vector2 v2);
}
