using Godot;
public partial class Bullet : RigidBody2D
{
    [Export] public float speed = 1000f;
    public Vector2 direction = Vector2.Right;

    public override void _Ready()
    {
        ContactMonitor = true;
        MaxContactsReported = 1;
        LinearVelocity = speed * direction;
    }
    public void OnBodyEntered(Node body)
    {
        QueueFree();
    }
}
