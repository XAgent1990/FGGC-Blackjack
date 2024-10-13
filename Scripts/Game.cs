using Godot;
using System;
using static Godot.GD;

public partial class Game : Node3D
{
	public static Game Instance;
	public Node3D playerContainer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Instance = this;
		playerContainer = GetNode<Node3D>("PlayerContainer");

		Print("Game ready");

		if(!Multiplayer.IsServer())
			return;

		Multiplayer.PeerConnected += AddPlayer;
		Multiplayer.PeerDisconnected += DeletePlayer;

		foreach(int id in Multiplayer.GetPeers())
			AddPlayer(id);

		if(!OS.HasFeature("dedicated_server"))
			AddPlayer(1);
	}

	public void AddPlayer(long id){
		Print($"Add Player: {id}");
		Player player = Load<PackedScene>("res://Prefabs/Player.tscn").Instantiate<Player>();
		player.ID = id;
		player.Name = id.ToString();
		playerContainer.AddChild(player, true);
	}

	public void DeletePlayer(long id){
		string name = id.ToString();
		if(!playerContainer.HasNode(name))
			return;
		
		playerContainer.GetNode(name).QueueFree();
	}

	public override void _ExitTree()
	{
		base._ExitTree();
		if(!Multiplayer.IsServer())
			return;
		Multiplayer.PeerConnected -= AddPlayer;
		Multiplayer.PeerDisconnected -= DeletePlayer;
	}
}
