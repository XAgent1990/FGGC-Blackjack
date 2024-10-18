using FGGCBlackJack;
using Godot;
using System;
using System.Collections.Generic;
using static Godot.GD;

public partial class Card : Sprite3D {
	public Suit suit;
	public CardValue value;

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
		// 	Print($"{value}: {counting[card.value]}");
	}

	public void SetCard(Suit suit, CardValue value){
		this.suit = suit;
		this.value = value;
	}

	public void SetRandom(){
		RandomNumberGenerator rng = new();
		suit = Enum.Parse<Suit>(Mathf.Floor(rng.Randf() * 4).ToString());
		byte v = (byte)Mathf.Ceil(rng.Randf() * 13);
		value = v switch {
			1 => CardValue.Ace,
			11 => CardValue.Jack,
			12 => CardValue.Queen,
			13 => CardValue.King,
			_ => Enum.Parse<CardValue>(v.ToString()),
		};
	}

	public void Reveal(){
		Frame = (int)((int)suit * 13 + value);
	}
}
