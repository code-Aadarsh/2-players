using Godot;

public partial class Player2 : Player
{
	public override void _PhysicsProcess(double delta)
	{
		crouch = "crouch2";
		dash = "dash2";
		jump = "jump";
		booright = true;
		booleft = false;
		Gright = -335;
		Gleft = 50;

		velocity = Velocity;
		direction = Input.GetAxis("left", "right");

		HandleGravity(ref delta);
		HandleShoot("p2_shoot");
		Movement(ref delta);
		Restart();




		Velocity = velocity;
		MoveAndSlide();


	}
}

