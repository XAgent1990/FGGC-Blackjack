using Godot;
using System;
using FGGCBlackJack;
using static FGGCBlackJack.Utility;
using Vector3 = FGGCBlackJack.Vector3;
using System.Linq;

public partial class Hand : Node {
	static readonly Vector3 OFFSET = new(0, 0.0001f, 0.034f);
	Vector3 nextCardPos = new();

	public byte Value {
		get {
			byte aces = 0;
			byte value = 0;
			foreach(Card card in GetChildren().Cast<Card>()) {
				switch(card.value){
					case CardValue.Ace:
						aces++;
						value += 11;
						break;
					case CardValue.Jack:
					case CardValue.Queen:
					case CardValue.King:
						value += 10;
						break;
					default:
						value += (byte)card.value;
						break;
				}
			}
			while(value > 21 && aces > 0){
				value -= 10;
				aces--;
			}
			return value;
		}
	}

	public void AddCard(Card card) {
		AddChild(card);
		card.Position = nextCardPos;
		nextCardPos += OFFSET;
	}

	public void AddRandom(){
		Card card = Instantiate<Card>("res://Prefabs/Card.tscn");
		AddCard(card);
		card.SetRandom();
		card.Reveal();
	}

	public void Reset() {
		foreach (Node node in GetChildren()) {
			if(node.GetType() != typeof(Card))
				continue;
			RemoveChild(node);
			node.QueueFree();
		}
		nextCardPos = Vector3.Zero;
	}
}
