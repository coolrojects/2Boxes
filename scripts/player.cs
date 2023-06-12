using Godot;
using System;
using System.Threading;

public partial class Player : CharacterBody2D
{
    [Export] float jumpHeight, jumpTimeToPeak, jumpTimeToDecend;

    float jumpVelocity, jumpGravity, fallGravity;

    private int speed = 400, mass = 20;
    public String name;
    //it is the value of how much distance will the player be moved per second
    const float JUMP_FORCE = 200, jumpSpeed = 200;
    float force, gravity = 200;
	public Vector2 myVelocity = Vector2.Zero;
	Vector2 jumpStartPos = Vector2.Zero;
    bool canJump = true, reachedJumpHeight = false;
    Vector2 moveDirection;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		speed = 400;
        jumpStartPos = Position;
        //myVelocity.Y = gravity;

        jumpVelocity = ((2.0f * jumpHeight) / jumpTimeToPeak) * -1.0f;
        jumpGravity = ((-2.0f * jumpHeight) / jumpTimeToPeak * jumpTimeToPeak) * -1.0f;
        fallGravity = ((-2.0f * jumpHeight) / jumpTimeToDecend * jumpTimeToDecend) * -1.0f;
        GD.Print(fallGravity);
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public  float getGravity()
	{
        if(Velocity.Y < 0.0)
        {
            return jumpGravity;
        }
        return fallGravity;
	}

    private void jump(float delta)
    {
        // if space is pressed then keep moving up till there is no more force to push youif( up
        if (Input.IsActionJustPressed("Jump"))
        {
            force = JUMP_FORCE;
            canJump = false;
            myVelocity.Y = jumpVelocity;
        }
    
       
    }


    private void movement()
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
    public void move()
    {
        Velocity = myVelocity;
        MoveAndSlide();
    }

    public override void _PhysicsProcess(double delta)
    {
        myVelocity.Y += getGravity() * (float)(delta);
        movement();
        jump((float)delta);
        move();
    }

    public void applyGravity()
    {

    }
    public int getMass()
    {
        return this.mass;
    }

}
