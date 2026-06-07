using Godot;
using GodotPlugins.Game;


public partial class Player : CharacterBody2D
{
    [Export] protected float speed = 500f;
    protected float MainSpeed;
    protected float crouchspeed = 100f;
    protected float dashTimer = 0f;
    [Export] protected float DashDuration = 0.3f;
    [Export] protected float DashSpeed = 1200f;
    [Export] protected float jumpForce = -600f;
    protected float cooldownTimer = 0f;
    [Export] protected float cooldownDuration = 2f;

    [Export] public PackedScene BulletScene;
    protected Marker2D gunPoint;
    protected float facing = 1f;
    protected Vector2 velocity;
    protected float direction;
    protected float dashDirection;
    protected float Standing;

    public override void _Ready()
    {
        gunPoint = GetNode<Marker2D>("GunPoint");
    }
    protected void HandleGravity(ref double delta)
    {
        if (!IsOnFloor())
        {
            velocity += GetGravity() * (float)delta;
        }
    }
    protected void Movement(ref double delta, string crouch, string dash, string jump, bool booright, bool booleft, float Gright, float Gleft)
    {

        if (Input.IsActionPressed(crouch))
        {
            MainSpeed = crouchspeed;
        }
        else
        {
            MainSpeed = speed;
        }

        if (Input.IsActionJustPressed(dash) && dashTimer <= 0 && cooldownTimer <= 0)
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
            if (direction > 0)
            {
                GetNode<Sprite2D>("Sprite2D").FlipH = booright;
                facing = 1;
                gunPoint.Position = new Vector2(Gright, 0);
                velocity.X = direction * MainSpeed;
            }
            else
            {
                GetNode<Sprite2D>("Sprite2D").FlipH = booleft;
                facing = -1;
                gunPoint.Position = new Vector2(Gleft, 0);
                velocity.X = direction * MainSpeed;
            }
        }

        else
        {
            velocity.X = 0;
        }


        if (Input.IsActionJustPressed(jump) && IsOnFloor())
        {
            velocity.Y = jumpForce;
        }

    }
    protected void HandleShoot(string shoot)
    {
        if (Input.IsActionJustPressed(shoot))
        {
            Bullet bullet = BulletScene.Instantiate<Bullet>();
            bullet.GlobalPosition = gunPoint.GlobalPosition;
            bullet.direction = new Vector2(facing, 0);
            GetTree().Root.AddChild(bullet);
            bullet.AddCollisionExceptionWith(this);
        }
    }
    protected void Restart()
    {
        if (GlobalPosition.Y > 500f)
        {
            GetTree().ReloadCurrentScene();
        }
    }
}