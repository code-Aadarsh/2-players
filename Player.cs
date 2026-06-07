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
    protected float falling = 500f;
    protected int idle = 0;
    protected Sprite2D sprite;
    protected string crouch;
    protected string dash;
    protected string jump;
    protected bool booright;
    protected bool booleft;
    protected float Gright;
    protected float Gleft;
    public override void _Ready()
    {
        gunPoint = GetNode<Marker2D>("GunPoint");
        sprite = GetNode<Sprite2D>("Sprite2D");
    }
    protected void HandleGravity(ref double delta)
    {
        if (!IsOnFloor())
        {
            velocity += GetGravity() * (float)delta;
        }
    }
    protected void Movement(ref double delta)
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
                sprite.FlipH = booright;
                facing = 1;
                gunPoint.Position = new Vector2(Gright, 0);
                velocity.X = direction * MainSpeed;
            }
            else
            {
                sprite.FlipH = booleft;
                facing = -1;
                gunPoint.Position = new Vector2(Gleft, 0);
                velocity.X = direction * MainSpeed;
            }
        }

        else
        {
            velocity.X = idle;
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
        if (GlobalPosition.Y > falling)
        {
            GetTree().ReloadCurrentScene();
        }
    }
}