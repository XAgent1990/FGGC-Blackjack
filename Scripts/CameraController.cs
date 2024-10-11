using Godot;
using System;
using Vector3 = FGGCBlackJack.Scripts.FGGC.Vector3;

public partial class CameraController : Camera3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		float x, z;
		x = z = 0;
		x -= Input.IsKeyPressed(Key.A) ? 1 : 0;
		x += Input.IsKeyPressed(Key.D) ? 1 : 0;
		z -= Input.IsKeyPressed(Key.S) ? 1 : 0;
		z += Input.IsKeyPressed(Key.W) ? 1 : 0;
		Vector3 d = new(x, z, 0);
		if(d.Length > 0.5)
			Translate((Input.IsKeyPressed(Key.Shift) ? 2 : 1)*(float)delta * d.Normalized);
	}
}
