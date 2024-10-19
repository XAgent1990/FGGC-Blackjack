using FGGCBlackJack;
using Godot;
using System;
using System.Collections.Generic;
using static Godot.GD;

public partial class Card : Sprite3D {
	public Suit suit;
	public CardValue value;

	public static byte ValueToIndex(CardValue value) {
		return value switch {
			CardValue.Ace => 0,
			CardValue.Jack => 10,
			CardValue.Queen => 11,
			CardValue.King => 12,
			_ => (byte)(value - 1)
		};
	}

	public static CardValue IndexToValue(byte index) {
		return index switch {
			0 => CardValue.Ace,
			10 => CardValue.Jack,
			11 => CardValue.Queen,
			12 => CardValue.King,
			_ => Enum.Parse<CardValue>((index + 1).ToString())
		};
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		Frame = 60;
		// Dictionary<CardValue, int> counting = new();
		// Card card = new();
		// foreach(CardValue value in Enum.GetValues<CardValue>())
		// 	counting.Add(value, 0);
		// Print(counting);
		// for(int i = 0; i < 1000000; i++){
		// 	card.SetRandom();
		// 	counting[card.value]++;
		// }
		// foreach(CardValue value in Enum.GetValues<CardValue>())
		// 	Print($"{value}: {counting[value]}");
	}

	public void SetCard(Suit suit, CardValue value) {
		this.suit = suit;
		this.value = value;
	}

	public void SetRandom() {
		RandomNumberGenerator rng = new();
		int rSuit = rng.RandiRange(0, 3);
		int rValue = rng.RandiRange(1, 13);
		Print($"Suit:{rSuit} | Value:{rValue}");
		suit = Enum.Parse<Suit>(rSuit.ToString());
		byte v = (byte)(rValue - 1);
		value = IndexToValue(v);
		Print($"Suit:{suit} | Value:{value}");
	}

	public void Reveal() {
		Frame = (int)suit * 13 + ValueToIndex(value);
	}
}
