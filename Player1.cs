using Godot;

public partial class Player1 : CharacterBody2D
{
    [Export] private float speed = 500f;
    private float dashTimer = 0f;
    [Export] private float DashDuration = 0.3f;
    [Export] private float DashSpeed = 1000f;
    [Export] private float jumpForce = -600f;
    private float cooldownTimer = 0f;
    [Export] private float cooldownDuration = 2f;

    [Export] public PackedScene BulletScene;
    private Marker2D gunPoint;
    private float facing = 1f;
    public override void _Ready()
    {
        gunPoint = GetNode<Marker2D>("GunPoint");
    }
    public override void _PhysicsProcess(double delta)
    {

        Vector2 velocity = Velocity;
        float direction = Input.GetAxis("ui_left", "ui_right");

        HandleShoot();
        HandleGravity(ref velocity, ref delta);
        Movement(ref velocity, ref delta, ref direction);

        Velocity = velocity;
        MoveAndSlide();


    }
    private void HandleGravity(ref Vector2 velocity, ref double delta)
    {
        if (!IsOnFloor())
        {
            velocity += GetGravity() * (float)delta;
        }
    }
    private void Movement(ref Vector2 velocity, ref double delta, ref float direction)
    {
        if (Input.IsActionPressed("crouch2"))
        {
            speed = 100f;
        }
        else
        {
            speed = 500f;
        }
        if (Input.IsActionJustPressed("dash") && dashTimer <= 0 && cooldownTimer <= 0)
        {
            cooldownTimer = cooldownDuration;
            dashTimer = DashDuration;
        }
        if (cooldownTimer > 0)
        {
            cooldownTimer -= (float)delta;
        }
        if (dashTimer > 0)
        {
            dashTimer -= (float)delta;
            velocity.X = direction * DashSpeed;

        }
        else if (direction != 0)
        {
            velocity.X = direction * speed;
            if (direction > 0)
            {
                GetNode<Sprite2D>("Sprite2D").FlipH = false;
                facing = 1;
                gunPoint.Position = new Vector2(50, 0);
            }
            else
            {
                GetNode<Sprite2D>("Sprite2D").FlipH = true;
                facing = -1;
                gunPoint.Position = new Vector2(-50, 0);

            }
        }
        else
        {
            velocity.X = 0;
        }


        if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
        {
            velocity.Y = jumpForce;
        }

    }
    private void HandleShoot()
    {
        if (Input.IsActionJustPressed("p1_shoot"))
        {
            Bullet bullet = BulletScene.Instantiate<Bullet>();
            bullet.GlobalPosition = gunPoint.GlobalPosition;
            bullet.direction = new Vector2(facing, 0);
            GetTree().Root.AddChild(bullet);
            bullet.AddCollisionExceptionWith(this);
        }
    }
}

