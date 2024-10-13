using Godot;
using System;
using static Godot.GD;
using Vector3 = FGGCBlackJack.Scripts.FGGC.Vector3;
using Math = FGGCBlackJack.Scripts.FGGC.Math;

public partial class Player : CharacterBody3D {
	public const float Speed = 5.0f;
	public const float MouseSpeed = 0.05f;
	public const float DegreesLimit = 85.0f;
	public PlayerInput input;
	private long id = 1;
	[Export]
	public long ID {
		get => id;
		set {
			id = value;
			GetNode<PlayerInput>("PlayerInput").SetMultiplayerAuthority((int)id);
		}
	}

	public override void _Ready() {
		base._Ready();
		input = GetNode<PlayerInput>("PlayerInput");
		if (id == Multiplayer.GetUniqueId())
			GetNode<Camera3D>("Camera3D").MakeCurrent();
	}

	public override void _Process(double delta) {
		if (input.mouseDirection.X == 0 && input.mouseDirection.Y == 0)
			return;
		Vector3 rotation = RotationDegrees;
		rotation.X = Mathf.Clamp(rotation.X - input.mouseDirection.Y * MouseSpeed, -DegreesLimit, DegreesLimit);
		rotation.Y -= input.mouseDirection.X * MouseSpeed;
		RotationDegrees = rotation;
	}

	public override void _PhysicsProcess(double delta) {
		Vector3 velocity = Velocity;
		float y = (input.ascend ? 1 : 0) - (input.descend ? 1 : 0);
		Vector3 direction = new Vector3(input.inputDirection.X, y, input.inputDirection.Y).Normalized;

		if (direction == Vector3.Zero) {
			Velocity = Vector3.Zero;
			return;
		}

		Vector3 forward = -Transform.Basis.Z;
		forward.Y = 0;
		float angle = Vector3.Angle(forward, -Vector3.UnitZ);
		if (forward.X < 0)
			angle *= -1;
		Vector3 temp = new() {
			X = Mathf.Cos(angle) * direction.X - Mathf.Sin(angle) * direction.Z,
			Y = direction.Y,
			Z = Mathf.Sin(angle) * direction.X + Mathf.Cos(angle) * direction.Z
		};
		direction = temp;

		velocity.X = direction.X * Speed;
		velocity.Y = direction.Y * Speed;
		velocity.Z = direction.Z * Speed;
		if (input.boost)
			velocity *= 2;

		Velocity = velocity;
		MoveAndSlide();
	}
}
