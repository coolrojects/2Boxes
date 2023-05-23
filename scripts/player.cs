using Godot;
using System;
using System.Threading;

public partial class player : CharacterBody2D
{
	int speed = 400;
    int gravity = 26000;
    float force = 500;
    //it is the value of how much distance will the player be moved per second
    const float JUMP_FORCE = 3000;
	Vector2 myVelocity = Vector2.Zero;
	Vector2 playerPos = Vector2.Zero;
    bool canJump = true, reachedJumpHeight = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		speed = 400;
        playerPos = Position;
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
				
	}

    public override void _PhysicsProcess(double delta)
    {
        Vector2 moveDirection = Vector2.Zero;
        if (Input.IsPhysicalKeyPressed(Key.A) )
        {
            moveDirection.X = -1;
        }
        if (Input.IsPhysicalKeyPressed(Key.D))
        {
            moveDirection.X = 1;
        }
        // if space is pressed then keep moving up till there is no more force to push youif( up
        if (Input.IsActionJustPressed("Jump") && canJump)
        {
            GD.Print(gravity * (float)delta);
            force = JUMP_FORCE;
            moveDirection.Y = -1 * force;
            playerPos = Position;
            canJump = false;
            
        }
        else if(!canJump)
        {
            moveDirection.Y = -1 * force;
            //Decrease the amount of distance the force can move
            force -= gravity * (float)delta;

        }
        else if (canJump)
        {
            moveDirection.Y += gravity * (float)delta;
        }
        moveDirection.X *= speed;
		myVelocity = moveDirection;
		Velocity = myVelocity;
        MoveAndSlide();
        if((int)Position.Y >= (int)playerPos.Y)
        {
            canJump = true;
        }
    }

}
