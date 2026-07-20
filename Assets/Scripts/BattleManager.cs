using UnityEngine;

public enum BattleState
{
    Battle,
    Finished
}

public class BattleManager : MonoBehaviour
{
    [Header("戦闘対象")]
    [SerializeField] private Planet planet;

    [Header("戦闘状態")]
    [SerializeField] private BattleState currentState;

    [Header("戦闘用クラゲ")]
    [SerializeField] private BattleAlien battleAlienPrefab;

    private void Awake()
    {
        Debug.Log("BattleManager Ready");
        LoadBattleData();
        SpawnBattleAliens();
    }

    private void LoadBattleData()
    {
        Debug.Log("----- 出撃クラゲ一覧 -----");

        foreach (BattleAlienData data in BattleData.aliens)
        {
            Debug.Log($"Lv.{data.level} 位置({data.gridPosition.x},{data.gridPosition.y})");
        }

        Debug.Log("-------------------------");
    }

    private void SpawnBattleAliens()
    {
        float startX = -2.4f;
        float startY = 2.4f;
        float cellSize = 1.2f;
    
        foreach (BattleAlienData data in BattleData.aliens)
        {
            Vector3 spawnPosition = new Vector3(
                startX + data.gridPosition.x * cellSize,
                startY - data.gridPosition.y * cellSize,
                0f);
    
            BattleAlien alien = Instantiate(
                battleAlienPrefab,
                spawnPosition,
                Quaternion.identity);
    
            alien.Initialize(data.level);
        }
    }

    private void ChangeState(BattleState newState)
    {
        currentState = newState;
        Debug.Log("Current State : " + currentState);
    }

    private void StartBattle()
    {
        ChangeState(BattleState.Battle);
    }

    private void EndBattle()
    {
        ChangeState(BattleState.Finished);
    }

}