using FGGCBlackJack;
using static FGGCBlackJack.Utility;
using Godot;
using static Godot.GD;
using System.Collections.Generic;

public partial class Field : Node3D {
    int players = 8;
    List<Hand> hands = new List<Hand>();
    Deck deck;

    public Field(int playerAmount, int deckSize) {
        players = playerAmount;
        for (int x = 0; x < players + 1; x++) {
            hands.Add(Hand.NewInstance);
        }
        deck = new Deck((byte)deckSize);
    }

    public void BeginRound() {
        //Print(deck);
        for (int iCards = 0; iCards < 2; iCards++) {
            for (int x = 0; x < players + 1; x++) {
                if (x == 0 && iCards == 0) {
                    //dealer karte verdeckt halten
                }
                hands[x].AddCard(deck.Draw_Card());
                //delay
            }
        }
        string text = $"";
        foreach (Hand h in hands) {
            text += $"{h}\n";
        }
        Print(text);
        Print(deck);
    }

}