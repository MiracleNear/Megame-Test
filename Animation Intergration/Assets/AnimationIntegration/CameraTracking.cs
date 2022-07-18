using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracking : MonoBehaviour
{
    public Transform Target;
    public Vector3 Offset;

    private void OnValidate()
    {
        transform.position = Target.position + Offset;
    }

    public void LateUpdate()
    {
        transform.position = Target.position + Offset;
    }
}
