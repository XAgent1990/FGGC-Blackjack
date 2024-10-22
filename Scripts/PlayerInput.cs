using Godot;
using System;
using static Godot.GD;

public partial class PlayerInput : MultiplayerSynchronizer {
    [Export] public bool boost = false;
    [Export] public bool ascend = false;
    [Export] public bool descend = false;
    [Export] public Vector2 inputDirection = new();
    [Export] public Vector2 mouseDirection = new();
    [Export] public bool authorized = false;

    bool freeMouse = true;

    private bool changed;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready() {
        authorized = GetMultiplayerAuthority() == Multiplayer.GetUniqueId();
        SetProcess(authorized);
        if (authorized) {
            ToggleMouseMode();
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta) {
        if (Input.IsActionJustPressed("ToggleMouseMode")) {
            ToggleMouseMode();
        }
        if (!freeMouse) {
            inputDirection = Input.GetVector("MoveLeft", "MoveRight", "MoveForward", "MoveBackward");
            boost = Input.IsActionPressed("Boost");
            ascend = Input.IsActionPressed("Ascend");
            descend = Input.IsActionPressed("Descend");
            if (changed)
                changed = false;
            else
                mouseDirection = Vector2.Zero;
        }



    }

    void ToggleMouseMode() {
        if (!freeMouse) {
            Input.MouseMode = Input.MouseModeEnum.Visible;
            freeMouse = true;
        }
        else {
            Input.MouseMode = Input.MouseModeEnum.Captured;
            freeMouse = false;
        }
    }

    public override void _Input(InputEvent @event) {
        base._Input(@event);
        if (!freeMouse && authorized && @event.GetType() == typeof(InputEventMouseMotion)) {
            mouseDirection = ((InputEventMouseMotion)@event).Relative;
            changed = true;
        }
    }



}
