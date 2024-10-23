using FGGCBlackJack;
using Godot;
using static Godot.GD;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

public partial class Deck : Node3D {
    public Stack<Card> cards = new();
    byte bundles = 1;

    public Deck(byte size) {
        bundles = size;
        Reset();
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready() {
        Reset();
        //Print(this);
    }

    public Card Draw_Card() {
        return cards.Pop();
    }

    public override string ToString() {
        string text = $"Max Cards in this Deck: {bundles * 52}\n";
        text += $"Cards in this Deck: {cards.Count}\n";
        foreach (Card crd in cards) {
            text += $"{crd}\n";
        }
        return text;
    }


    public void Reset() {
        for (byte i = 0; i < bundles; i++) {
            for (byte ii = 0; ii < 4; ii++) {
                for (byte iii = 0; iii < 13; iii++) {
                    Card card = new();
                    card.SetCard(Enum.Parse<Suit>(ii.ToString()), Card.IndexToValue(iii));
                    cards.Push(card);
                }
            }
        }
        Shuffle();
    }

    public void Shuffle() {
        List<Card> list = cards.ToList();
        cards.Clear();
        RandomNumberGenerator rng = new();
        while (list.Count > 0) {
            int r = rng.RandiRange(0, list.Count - 1);
            cards.Push(list[r]);
            list.RemoveAt(r);
        }
    }
}
