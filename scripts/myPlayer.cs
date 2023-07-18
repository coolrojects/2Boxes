using Godot;
using System;
using System.Threading;

public partial class myPlayer : CharacterBody2D
{
	[Export] float gravity = 4000;
	//it is the value of how much distance will the player be moved per second
	public const float JUMP_FORCE = 1200;
	public int speed = 400;
	public float force;  
	public bool
		collisionAbovePlayer = false, 
		canJump = true,
		isOnWall = false,
		bumpedWithaWallonTop = false,
		stopMovement = false;
	public Vector2 myVelocity = Vector2.Zero;
	public Vector2 moveDirection;
	CharacterBody2D player2;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		try
		{
			player2 = (CharacterBody2D)GetNode("/root/Level1/player2");
		}
		catch (Exception e)
		{
			GD.Print("Failed to get player 2." + e.Message);
		}
	}


	public virtual bool jump()
	{
	   
		// if space is pressed then keep moving up till there is no more force to push youif( up
		if (Input.IsPhysicalKeyPressed(Key.Space) & isOnWall)
		{
			force = JUMP_FORCE;
			return true;
		}

		return false;
	}
	/// <summary>
	/// Controls player jumping if jump() returns true. Else controls gravity accordingly
	/// </summary>
	/// <param name="delta">
	/// delta time
	/// </param>
	void controlGravity(bool jump, float delta)
	{

		if (jump)
		{
			myVelocity.Y -= force;
		}
		else if (bumpedWithaWallonTop)
		{
			bumpedWithaWallonTop = false;
			myVelocity.Y = 0;
		}
		else if (isOnWall)
		{
			myVelocity.Y = gravity * delta;
		}
		else
		{
			myVelocity.Y += gravity * delta;

		}
	}



	public virtual void movement()
	{
		moveDirection = Vector2.Zero;
		if (Input.IsPhysicalKeyPressed(Key.A))
		{
			moveDirection.X = -1;
		}
		if (Input.IsPhysicalKeyPressed(Key.D))
		{
			moveDirection.X = 1;
		}
		moveDirection.X *= speed;
		myVelocity.X = moveDirection.X;
	}


	 
	public void move(float delta)
	{
		if(!stopMovement)
		{
			Velocity = myVelocity;
			MoveAndSlide();
		}       
		
	}

	

	public override void _PhysicsProcess(double delta)
	{
		
		movement();      
		controlGravity(jump(), (float)delta);
		move((float)delta);
		
	}

	public void characterEnteredJumpArea(Area2D area)
	{
		if (area.Name.Equals("bottom"))
		{
			bumpedWithaWallonTop = true;
		}
		else if (area.GetParent().Name.Equals("Home"))
		{
			this.stopMovement = true;
		}
		else
		{
			isOnWall = true;
		}
		
	}

	public void playerIsNotOnWall(Area2D area)
	{
		isOnWall = false;
	}



}
