using UnityEngine;

public class Submit_Bet_button : Bet_Button
{

    override public void Press()
    {
        Player player = FindFirstObjectByType<Player>();
        Betting_System bet = FindFirstObjectByType<Betting_System>();
        GameManager gamemanager = FindFirstObjectByType<GameManager>();
        player.bets = bet.bets;
        player.multiplier_bets = bet.multiplier_bets;
        player.amount_bets = bet.amount_bets;
    }
}
