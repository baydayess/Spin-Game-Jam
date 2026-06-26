using UnityEngine;

public class Submit_Bet_button : Bet_Button
{

    override public void Press()
    {
        if (GameManager.Instance.GamePlayState != EGamePlayState.BetScreen) return;

        Player player = FindFirstObjectByType<Player>();
        Betting_System bet = FindFirstObjectByType<Betting_System>();
        player.bets = bet.bets;
        player.multiplier_bets = bet.multiplier_bets;
        player.amount_bets = bet.amount_bets;
        float final_amount = 0;
        for (int i = 0; i < player.amount_bets.Count; i++)
        {
            final_amount += player.amount_bets[i];
        }
        player.Remove_Money(final_amount);
        GameManager.Instance.ChangeGamePlayState((int) EGamePlayState.Roulette);
    }
}
