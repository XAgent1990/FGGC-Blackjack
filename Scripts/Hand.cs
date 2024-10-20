using Godot;
using System;
using FGGCBlackJack;
using static FGGCBlackJack.Utility;
using Vector3 = FGGCBlackJack.Vector3;
using System.Linq;

public partial class Hand : Node {
	static readonly Vector3 OFFSET = new(0, 0.0001f, 0.034f);
	Vector3 nextCardPos = new();
	Label3D valueText;
	byte aces = 0;
	byte cards = 0;
	bool changed = true;

	public static Hand NewInstance { get => Instantiate<Hand>("res://Prefabs/Hand.tscn"); }

	public byte Value {
		get {
			aces = 0;
			cards = 0;
			byte value = 0;
			foreach (Node node in GetChildren()) {
				if (node.GetType() != typeof(Card))
					continue;
				Card card = (Card)node;
				switch (card.value) {
					case CardValue.Ace:
						value += 11;
						aces++;
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
				cards++;
			}
			while (value > 21 && aces > 0) {
				value -= 10;
				aces--;
			}
			return value;
		}
	}

	public override void _Ready() {
		valueText = GetChild<Label3D>(0);
	}

	public override void _Process(double delta) {
		if (changed) {
			UpdateText();
			changed = false;
		}
	}

	public void AddCard(Card card) {
		if (Value > 21) return;
		AddChild(card);
		card.Position = nextCardPos;
		nextCardPos += OFFSET;
		changed = true;
	}

	public void AddRandom() {
		Card card = Instantiate<Card>("res://Prefabs/Card.tscn");
		AddCard(card);
		card.SetRandom();
		card.Reveal();
	}

	public void Reset() {
		foreach (Node node in GetChildren()) {
			if (node.GetType() != typeof(Card))
				continue;
			RemoveChild(node);
			node.QueueFree();
		}
		nextCardPos = Vector3.Zero;
		aces = 0;
		changed = true;
	}

	public void UpdateText() {
		byte v = Value;
		if (cards == 2 && v == 21) {
			valueText.Text = "BJ";
			return;
		}
		valueText.Text = v.ToString();
		if (aces > 0)
			valueText.Text += " / " + (v - 10).ToString();
	}

	public override string ToString() {
		string t = $"";
		foreach (Node node in GetChildren()) {
			if (node.GetType() != typeof(Card))
				continue;
			t += $"{((Card)node).ToString()}\n";
		}
		return t;
	}
}
