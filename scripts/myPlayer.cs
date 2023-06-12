using Godot;
using System;
using System.Threading;

public partial class myPlayer : CharacterBody2D
{
    [Export]
    float gravity = 4000;
    private int speed = 400, mass = 20;
    public String name;
    //it is the value of how much distance will the player be moved per second
    const float JUMP_FORCE = 1200, jumpSpeed = 200;
    float force, 
          isStandingCheckTime = 0;
    public Vector2 myVelocity = Vector2.Zero;
    Vector2 jumpStartPos = Vector2.Zero;
    bool updateGravity = true,
        isStanding = false,
        reachedJumpHeight = false,
        collisionAbovePlayer = false;
    Vector2 moveDirection,
            prevPos = Vector2.Zero;
    int counter = 0;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        speed = 400;
        jumpStartPos = Position;
        prevPos = Position;
    }

    private bool jump(float delta)
    {
       
        // if space is pressed then keep moving up till there is no more force to push youif( up
        if (Input.IsActionJustPressed("Jump"))
        {
            force = JUMP_FORCE;
            return true;
        }

        return false;
    }
    /// <summary>
    /// if player is on a static object, then stop increasing gravity
    /// </summary>
    /// <param name="delta">
    /// delta time
    /// </param>
    void controlGravity(float delta, bool pressedJump)
    {
        bool colliding = false, applyConstGravity = false;

        if (!pressedJump)
        {
            for (int i = 0; i < GetSlideCollisionCount(); i++)
            {
                KinematicCollision2D collision2D = GetSlideCollision(i);
                if (collision2D != null)
                {
                    Node2D collidingObject = (Node2D)collision2D.GetCollider();
                    colliding = collidingObject.Name.ToString().ToLower().Contains("static");
                    if (colliding)
                    {

                        //is player on a wall
                        if (Position.Y < collidingObject.Position.Y)
                        {
                            GD.Print("Player Pos: " + Position.Y + "wall Position" + collidingObject.Position.Y);
                            applyConstGravity = true;
                            collisionAbovePlayer = false;
                            
                        }
                        //did the collision happen above the player
                        else if(Position.Y > collidingObject.Position.Y & !collisionAbovePlayer)
                        {
                            force = 0;
                            collisionAbovePlayer = true;
                        }
                        
                    }
                }
            }
        }     

        //if not on a wall, then change velocity (aka gravity)
        if (!applyConstGravity)
        {
            myVelocity.Y = -force;
            force -= gravity * delta;
        }
        else
        {
            myVelocity.Y = gravity * delta;
        }
    }



    private void movement(float delta)
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
        Velocity = myVelocity;
        MoveAndSlide();
    }

    public override void _PhysicsProcess(double delta)
    {
        
        movement((float)delta);      
        controlGravity((float)delta, jump((float)delta));
        move((float)delta);
        
    }

}
