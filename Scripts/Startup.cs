using Godot;
using System;
using static Godot.GD;

public partial class Startup : Node
{
	const int PORT = 8080;
	const int MAX_CLIENTS = 8;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){
	}
	
	public void OnHostPressed(){
		Print("host pressed");
		ENetMultiplayerPeer peer = new();
		peer.CreateServer(PORT, MAX_CLIENTS);
		Multiplayer.MultiplayerPeer = peer;
		StartGame();
	}

	public void StartGame(){
		Control UI = GetNode<Control>("UI");
		UI.Hide();

		if(Multiplayer.IsServer()){
			LoadGame(Load<PackedScene>("res://Scenes/Game.tscn"));
			// CallDeferred("LoadGame", Load<PackedScene>("res://Scenes/Game.tscn"));
		}
	}

	public void LoadGame(PackedScene scene){
		Node gameContainer = GetNode("GameContainer");
		foreach(Node child in gameContainer.GetChildren()){
			gameContainer.RemoveChild(child);
			child.QueueFree();
		}
		gameContainer.AddChild(scene.Instantiate());
	}
	
	public void OnJoinPressed(){
		Print("join pressed");
		string ip = GetNode<LineEdit>("UI/VBoxContainer/HBoxContainer/IPLineEdit").Text;
		if(ip == "")
			ip = "127.0.0.1";
		ENetMultiplayerPeer peer = new();
		peer.CreateClient(ip, PORT);
		Multiplayer.MultiplayerPeer = peer;
		StartGame();
	}
}