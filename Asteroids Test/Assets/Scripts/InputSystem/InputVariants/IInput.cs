using InputSystem;

public interface IInput
{
    public string Name { get; }
    public InputType InputType { get; }
    public void Update(Player player);
}
