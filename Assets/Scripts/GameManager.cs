using UnityEngine;

public enum GameState
{
    Preparing,
    Transfer
}

public class GameManager : MonoBehaviour
{
    [Header("ゲーム状態")]
    [SerializeField] private GameState currentState;

    [Header("シーン管理")]
    [SerializeField] private SceneLoader sceneLoader;

    [Header("グリッド管理")]
    [SerializeField] private GridManager gridManager;

    private void Awake()
    {
        ChangeState(GameState.Preparing);

        Debug.Log("GameManager Ready");
    }

    private void ChangeState(GameState newState)
    {
        currentState = newState;
        Debug.Log("Current State : " + currentState);
    }

    public void StartTransfer()
    {
        ChangeState(GameState.Transfer);

        SaveBattleData();

        TransferToBattle();
    }

    private void SaveBattleData()
    {
        // 前回のデータを消す
        BattleData.aliens.Clear();

        // 空いていないマスを順番に確認

        foreach (GridCell cell in gridManager.GetAllCells())
        {
            if (!cell.IsOccupied)
            {
                continue;
            }

            Alien alien = cell.currentAlien.GetComponent<Alien>();

            BattleAlienData data = new BattleAlienData();

            data.level = alien.level;
            data.gridPosition = cell.GridPosition;

            BattleData.aliens.Add(data);

        }

        Debug.Log("保存したクラゲ数 : " + BattleData.aliens.Count);
    }

    private void TransferToBattle()
    {
        sceneLoader.LoadBattleScene();
    }
}