using Godot;
using System;
using System.Threading;

public partial class Player : myPlayer
{

    public override bool jump()
    {

        // if space is pressed then keep moving up till there is no more force to push youif( up
        if (Input.IsPhysicalKeyPressed(Key.Kp0) & isOnWall)
        {
            force = JUMP_FORCE;
            return true;
        }

        return false;
    }
    public override void movement()
    {
        moveDirection = Vector2.Zero;
        if (Input.IsPhysicalKeyPressed(Key.Kp4))
        {
            moveDirection.X = -1;
        }
        if (Input.IsPhysicalKeyPressed(Key.Kp6))
        {
            moveDirection.X = 1;
        }
        moveDirection.X *= speed;
        myVelocity.X = moveDirection.X;
    }

}
