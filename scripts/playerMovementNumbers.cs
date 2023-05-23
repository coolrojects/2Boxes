using Godot;
using System;

public partial class playerMovementNumbers : Node2D {
    
	int speed = 400;
    Vector2 velocity = Vector2.Zero;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        Vector2 moveDirection = Input.GetVector("NumLeft", "NumRight", "NumUp", "NumDown");
        velocity = moveDirection.Normalized() * speed * (float)delta;

        Position += velocity;
    }
}
