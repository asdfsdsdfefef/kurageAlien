using UnityEngine;

public enum BattleState
{
    Idle,
    Preparing,
    Battle,
    Finished
}

public class BattleManager : MonoBehaviour
{
    [Header("戦闘対象")]
    [SerializeField] private Planet planet;

    [Header("戦闘状態")]
    [SerializeField] private BattleState currentState = BattleState.Idle;

    private void Awake()
    {
        currentState = BattleState.Idle;

        Debug.Log("BattleManager Ready");
        Debug.Log("Current State : " + currentState);

    }

    private void ChangeState(BattleState newState)
    {
        currentState = newState;
        Debug.Log("Current State : " + currentState);
    }

    private void StartBattle()
    {
        ChangeState(BattleState.Preparing);
    }

    private void BeginBattle()
    {
        ChangeState(BattleState.Battle);
    }

    private void EndBattle()
    {
        ChangeState(BattleState.Finished);
    }

}