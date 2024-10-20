using FGGCBlackJack;
using Godot;

public partial class ComPlayer : Node3D {
    /*
    Has imfos about its cards
    the dealer card
    the deck situation
    
    can do standard actions
    
    
    
    */

    int value;//Money/Chips owned
    int bet;

    #region Vault

    void New() {
        value = 25;
    }

    void Load() {

    }
    #endregion

    #region Game
    void Bet() {
        bet = Mathf.FloorToInt(value / 2);
    }

    void Double_Down() {
        if (value < (bet * 2)) return;

    }

    void Draw_Card() {

    }

    void Stand() {

    }

    void Split() {

    }
    #endregion
}