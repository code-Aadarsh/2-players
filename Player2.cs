using Godot;

public partial class Player2 : Player
{
	public override void _PhysicsProcess(double delta)
	{
		velocity = Velocity;
		direction = Input.GetAxis("left", "right");

		HandleGravity(ref delta);
		HandleShoot("p2_shoot");
		Movement(ref delta, "crouch2", "dash2", "jump");
		Restart();





		Velocity = velocity;
		MoveAndSlide();


	}
}

