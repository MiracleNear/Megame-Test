using UnityEngine;

public class KeyboardInput : MonoBehaviour
{
    public bool IsAccelerateButtonPressed => Input.GetAxisRaw("Vertical") > 0;

    public bool IsShotButtonPressed => Input.GetKeyDown(KeyCode.Space);
    public float InverseDirectionRotation => -Input.GetAxisRaw("Horizontal");
    
}
