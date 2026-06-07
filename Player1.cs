using Godot;

public partial class Player1 : Player
{

    public override void _PhysicsProcess(double delta)
    {
        velocity = Velocity;
        direction = Input.GetAxis("ui_left", "ui_right");

        HandleGravity(ref delta);
        HandleShoot("p1_shoot");
        Movement(ref delta, "crouch", "dash", "ui_accept", false, true, 50, -335);
        Restart();



        Velocity = velocity;
        MoveAndSlide();


    }

}

