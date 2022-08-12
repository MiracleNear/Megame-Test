using InputSystem;
using UnityEngine;

public class MouseAndKeyboardInput : IInput
{
    public string Name { get; }
    public InputType InputType { get; } = InputType.MouseAndKeyboard;
    private Camera _mainCamera => Camera.main;
    
    public MouseAndKeyboardInput(string name)
    {
        Name = name;
    }

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
