using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosFlecha : MonoBehaviour
{
    public Vector2 PointerPosisition {  get; set; }

    private void Update()
    {
        transform.right = (PointerPosisition - (Vector2)transform.position).normalized;
    }
}
