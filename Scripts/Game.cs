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

		if (Multiplayer.IsServer())
			GetNode<Label3D>($"Seats/Seat1/UsernameLabel").Text = Startup.Username;
		else
			RpcId(1, "HelloIJoined", Startup.Username);

		if (!Multiplayer.IsServer())
			return;

		Multiplayer.PeerConnected += AddPlayer;
		Multiplayer.PeerDisconnected += DeletePlayer;

		foreach (int id in Multiplayer.GetPeers())
			AddPlayer(id);

		if (!OS.HasFeature("dedicated_server"))
			AddPlayer(1);
	}

	public void AddPlayer(long id) {
		Print($"Add Player: {id}");
		Player player = Instantiate<Player>("res://Prefabs/Player.tscn");
		player.ID = id;
		player.Name = id.ToString();
		playerContainer.AddChild(player, true);
	}

	[Rpc(MultiplayerApi.RpcMode.AnyPeer)]
	public void HelloIJoined(string name) {
		GetNode<Label3D>($"Seats/Seat{playerContainer.GetChildCount()}/UsernameLabel").Text = name;
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
