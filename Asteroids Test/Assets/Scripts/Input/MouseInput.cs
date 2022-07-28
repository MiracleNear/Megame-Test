using UnityEngine;

public class MouseInput : IInput
{
    private Camera _mainCamera => Camera.main;

    public string Name { get; private set; } = "Mouse and Keyboard Input";

    public void Update(Player player)
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Mouse1))
        {
            player.Accelerate();
        }
        else
        {
            player.SlowDown();
        }

        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0))
        {
            player.Shoot();
        }
        
        
        player.LookAt(_mainCamera.ScreenToWorldPoint(Input.mousePosition));
    }
}
