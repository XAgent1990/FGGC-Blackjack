using FGGCBlackJack;
using static FGGCBlackJack.Utility;
using Godot;
using static Godot.GD;
using System.Collections.Generic;
using System;

public partial class Field : Node3D {
	int players = 1;
	List<Hand> activeHands = new List<Hand>();
	Deck deck;


	public enum DeckSettings {
		Random,
		Deck
	}
	public Field(int playerAmount, int deckSize) {
		players = playerAmount;
		for (int x = 0; x < players + 1; x++) {
			activeHands.Add(Hand.NewInstance);
		}
		deck = new Deck((byte)deckSize);
	}

	public override void _Ready() {
		BeginRound();
	}
	public void BeginRound() {
		//Print(deck);
		for (int iCards = 0; iCards < 2; iCards++) {
			for (int x = 0; x < players + 1; x++) {
				if (x == 0 && iCards == 0) {
					//dealer karte verdeckt halten
				}
				activeHands[x].AddCard(deck.Draw_Card());
				//delay
			}
		}
		string text = $"";
		foreach (Hand h in activeHands) {
			text += $"{h}\n";
		}
		Print(text);
		Print(deck);
	}

	public void ResetRound() {
		if (deck.cards.Count <= 26) {
			//reset deck
		}
		//reset hands
	}
}
