using Godot;

public partial class Player1 : Player
{

    public override void _PhysicsProcess(double delta)
    {
        crouch = "crouch";
        dash = "dash";
        jump = "ui_accept";
        booright = false;
        booleft = true;
        Gright = 50;
        Gleft = -335;

        velocity = Velocity;
        direction = Input.GetAxis("ui_left", "ui_right");

        HandleGravity(ref delta);
        HandleShoot("p1_shoot");
        Movement(ref delta);
        Restart();



        Velocity = velocity;
        MoveAndSlide();


    }

}

