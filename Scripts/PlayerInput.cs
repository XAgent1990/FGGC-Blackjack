using Godot;
using System;
using static Godot.GD;

public partial class PlayerInput : MultiplayerSynchronizer
{
	[Export] public bool boost = false;
	[Export] public bool ascend = false;
	[Export] public bool descend = false;
	[Export] public Vector2 inputDirection = new();
	[Export] public Vector2 mouseDirection = new();
	[Export] public bool authorized = false;

	private bool changed;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){
		authorized = GetMultiplayerAuthority() == Multiplayer.GetUniqueId();
		SetProcess(authorized);
		if(authorized)
			Input.MouseMode = Input.MouseModeEnum.Captured;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta){
		inputDirection = Input.GetVector("MoveLeft", "MoveRight", "MoveForward", "MoveBackward");
		boost = Input.IsActionPressed("Boost");
		ascend = Input.IsActionPressed("Ascend");
		descend = Input.IsActionPressed("Descend");
		if(changed)
			changed = false;
		else
			mouseDirection = Vector2.Zero;
	}

	public override void _Input(InputEvent @event){
		base._Input(@event);
		if(authorized && @event.GetType() == typeof(InputEventMouseMotion)){
			mouseDirection = ((InputEventMouseMotion)@event).Relative;
			changed = true;
		}
	}



}
