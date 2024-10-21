using Godot;
using System;
using FGGCBlackJack;
using static FGGCBlackJack.Utility;
using Vector3 = FGGCBlackJack.Vector3;
using System.Linq;

public partial class Hand : Node3D {
    static readonly Vector3 OFFSET = new(0, 0.0001f, 0.034f);
    [Export] public bool dealer = false;
    [Export] public bool finished = true;
    Vector3 nextCardPos = new();
    Label3D valueText;
    byte aces = 0;
    byte cards = 0;
    bool changed = true;
    Hand split1;
    Hand split2;

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
                cards++;
                if (card.Frame == 60)
                    continue;
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
        if (GetParent().Name == "Dealer") {
            dealer = true;
            finished = false;
            return;
        }
        try {
            split1 = GetNodeOrNull<Hand>($"../{Name}1");
            split2 = GetNodeOrNull<Hand>($"../{Name}2");
        }
        catch (InvalidCastException) { }
        if (!Visible) SetProcess(false);
    }

    public override void _Process(double delta) {
        if (dealer) {
            Camera3D cam = GetViewport().GetCamera3D();
            Vector3 pos = cam.GlobalPosition;
            if(pos.X != 0 || pos.Z != 0){
                Vector3 r = cam.GlobalRotation;
                r.X = 0;
                Rotation = r;
            }
            if(Input.IsActionJustPressed("DealerCard"))
                AddRandom();
            if(Input.IsActionJustPressed("DealerReset"))
                Reset();
        }
        if (changed) {
            UpdateText();
            changed = false;
            if (Value > 21 && !dealer)
                finished = true;
        }
    }

    public void AddCard(Card card) {
        if (finished) return;
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

    public void Split() {
        if (!Visible) return;
        if (split1 == null || split2 == null) return;
        split1.SetActive(true);
        split2.SetActive(true);
        split1.finished = split2.finished = false;
        this.SetActive(false);
        finished = true;
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
