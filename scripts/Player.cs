using Godot;
using System;

public class Player : KinematicBody2D
{

	private int gravity = 1000;
	private Vector2 velocity = Vector2.Zero;
	private int maxHorizontalSpeed = 140;
	private int jumpSpeed = 350;
	private int horizontalAcceleration = 2000;
	private int jumpTerminationMultiplier = 4;
	
	public override void _Ready()
	{
		
	}

	public override void _Process(float delta)
	{
		var moveVector = Vector2.Zero;
		moveVector.x = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left");
		moveVector.y = Input.IsActionJustPressed("jump") ? -1 : 0;
		
		// velocity.x = moveVector.x * maxHorizontalSpeed;
		velocity.x += moveVector.x * horizontalAcceleration * delta;
		if (moveVector.x == 0) 
		{
			// velocity.x = Mathf.Lerp(velocity.x, 0, 0.1f);
			velocity.x = Mathf.Lerp(0, velocity.x, Mathf.Pow(2, -50 * delta));
		}
		
		velocity.x = Mathf.Clamp(velocity.x, -maxHorizontalSpeed, maxHorizontalSpeed);
		
		if (moveVector.y < 0 && IsOnFloor()) {
			velocity.y = moveVector.y * jumpSpeed;	
		}
		
		if(velocity.y < 0 && !Input.IsActionPressed("jump")) 
		{
			velocity.y += gravity * jumpTerminationMultiplier * delta;
		} else 
		{
			velocity.y += gravity * delta;	
		}
		
		velocity = MoveAndSlide(velocity, Vector2.Up);
	}
}
