using Godot;
using System;

public partial class Potato : CharacterBody3D
{
	[Export]
	public float Speed = 5;
	
	private NavigationAgent3D navAgent;
	private Vector3 targetPosition;
	private CharacterBody3D player;

	public override void _Ready()
	{
		base._Ready();
		navAgent = GetNode<NavigationAgent3D>("NavigationAgent3D");
		
		player = GetTree().GetNodesInGroup("Player")[0] as CharacterBody3D;
		GD.Print(player);
		targetPosition = player.GlobalTransform.Origin;
		_UpdateTargetPosition(targetPosition);
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		
		// Look at the target
		this.LookAt(player.GlobalTransform.Origin);
		
		// Keep the potato from looking at the floor
		var tempRotation = Rotation;
		tempRotation.X = 0;
		tempRotation.Z = 0;
		Rotation = tempRotation;

		if (Position.DistanceTo(targetPosition) > 0.5)
		{
			Vector3 currentPosition = GlobalTransform.Origin;
			Vector3 nextPosition = navAgent.GetNextPathPosition();
			Vector3 nextVelocity = (nextPosition - currentPosition).Normalized() * Speed;
			targetPosition.Y = Position.Y;
			Velocity = nextVelocity;
			MoveAndSlide();
		}
		
		targetPosition = player.GlobalTransform.Origin;
		_UpdateTargetPosition(targetPosition);
	   
	}

	private void _UpdateTargetPosition(Vector3 targetPosition)
	{
		navAgent.SetTargetPosition(targetPosition);
	}
}
