using Godot;

public partial class Player2 : Player
{
	public override void _PhysicsProcess(double delta)
	{
		Standing = -1;
		velocity = Velocity;
		direction = Input.GetAxis("left", "right");

		HandleGravity(ref delta);
		HandleShoot("p2_shoot");
		Movement(ref delta, "crouch2", "dash2", "jump", true, false, -335, 50);
		Restart();




		Velocity = velocity;
		MoveAndSlide();


	}
}

