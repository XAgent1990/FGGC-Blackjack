using Godot;
using System;
using static Godot.GD;
using static FGGCBlackJack.Utility;

public partial class Game : Node3D {
	public static Game Instance;
	public static Node3D playerContainer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		Instance = this;
		playerContainer = GetNode<Node3D>("PlayerContainer");

		Print("Game ready");

		if (!OS.HasFeature("dedicated_server"))
			AddPlayer(1);

		RpcId(1, "HelloIJoined", Startup.Username, Startup.Color);

		if (!Multiplayer.IsServer())
			return;

		Multiplayer.PeerConnected += AddPlayer;
		Multiplayer.PeerDisconnected += DeletePlayer;

		foreach (int id in Multiplayer.GetPeers())
			AddPlayer(id);
	}

	public void AddPlayer(long id) {
		Print($"Add Player: {id}");
		Player player = Instantiate<Player>("res://Prefabs/Player.tscn");
		player.ID = id;
		player.Name = id.ToString();
		playerContainer.AddChild(player, true);
	}

	[Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = true)]
	public void HelloIJoined(string name, Color color) {
		Label3D label = GetNode<Label3D>($"Seats/Seat{playerContainer.GetChildCount()}/UsernameLabel");
		label.Text = name;
		label.Modulate = label.OutlineModulate = color;
	}

	public void DeletePlayer(long id) {
		string name = id.ToString();
		if (!playerContainer.HasNode(name))
			return;

		playerContainer.GetNode(name).QueueFree();
	}

	public override void _ExitTree() {
		base._ExitTree();
		if (!Multiplayer.IsServer())
			return;
		Multiplayer.PeerConnected -= AddPlayer;
		Multiplayer.PeerDisconnected -= DeletePlayer;
	}
}
