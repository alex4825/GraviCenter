using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public static class CoordEditor
{
    public static Vector3 RoundToHalf(Vector3 position)
    {
        position.x = Mathf.Round(position.x * 2) / 2;
        position.y = Mathf.Round(position.y * 2) / 2;
        position.z = Mathf.Round(position.z * 2) / 2;
        return position;
    }
}
