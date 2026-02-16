using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public enum BattleState {
    START,
    PLAYERTURN,
    ENEMYTURN,
    WON,
    LOST
}

public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    Unit playerUnit;
    Unit enemyUnit;

    public BattleState state;
    public Text turnText;
    public Color blue;
    public Color red;

    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<Unit>();
        GameObject enemyGO = Instantiate(playerPrefab, playerBattleStation);
        Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<Unit>();


        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerAttack()
    {
        // DMG enemy
        bool isDead = enemyUnit.TakeDamage(playerUnit.STR);

        yield return new WaitForSeconds(2f);

        // Check if they're dead to change otherwise
        if (isDead){
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator PlayerHeal()
    {
        // Heal
        playerUnit.Heal();

        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator PlayerPass()
    {
        // Do nothing

        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator EnemyTurn()
    {
        turnText.text = "Enemy TURN";
        turnText.color = red;
        yield return new WaitForSeconds(1f);

        bool playerDead = playerUnit.TakeDamage(enemyUnit.STR);
        yield return new WaitForSeconds(1f);
        if (playerDead)
        {
            state = BattleState.LOST;
            EndBattle();
        } else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    void EndBattle()
    {
        if(state == BattleState.WON)
        {
            turnText.text = "You win";
        } else if (state == BattleState.LOST)
        {
            turnText.text = "You should not see this like idk";
        }
    }

    void PlayerTurn()
    {
        turnText.text = "PLAYER TURN";
        turnText.color = blue;
        // TODO: UI saying PLAYER ACTION
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN) return;

        StartCoroutine(PlayerAttack());
    }

    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN) return;

        StartCoroutine(PlayerHeal());
    }

    public void OnPassButton()
    {
        if (state != BattleState.PLAYERTURN) return;

        StartCoroutine(PlayerPass());
    }
}
