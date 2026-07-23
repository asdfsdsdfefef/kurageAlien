using UnityEngine;
using System.Collections.Generic;

public enum BattleState
{
    Battle,
    Finished
}

public enum BattleResult
{
    None,
    Victory,
    Defeat
}

public class BattleManager : MonoBehaviour
{
    [Header("戦闘対象")]
    [SerializeField] private Planet planet;

    [Header("戦闘状態")]
    [SerializeField] private BattleState currentState;
    [SerializeField] private BattleResult currentResult = BattleResult.None;

    [Header("戦闘用クラゲ")]
    [SerializeField] private BattleAlien battleAlienPrefab;

    [Header("移動設定")]
    [SerializeField] private Transform battleTarget;

    public static BattleManager Instance {get; private set; }
    private int reachedAlienCount = 0;
    private int totalAlienCount = 0;
    private readonly List<BattleAlien> battleAliens = new();
    private bool battleEnded = false;

    private void Awake()
    {
        Debug.Log("BattleManager Ready");
        LoadBattleData();
        SpawnBattleAliens();
        Instance = this;

        planet.OnDestroyed += OnPlanetDestroyed;
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
        battleAliens.Clear();
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
    
                alien.Initialize(data.level, planet);

                alien.OnReachedTarget += OnAlienReached;

                totalAlienCount++;

                alien.StartMove(battleTarget.position);

                battleAliens.Add(alien);
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
        if (battleEnded)
        {
            return;
        }

        battleEnded = true;

        foreach (BattleAlien alien in battleAliens)
        {
            alien.StopBattle();
        }
        
        ChangeState(BattleState.Finished);

        Debug.Log($"Battle Result : {currentResult}");
    }

    private void OnAlienReached(BattleAlien alien)
    {
        reachedAlienCount++;

        Debug.Log($"到着 {reachedAlienCount} / {totalAlienCount}");

        if (reachedAlienCount >= totalAlienCount)
        {
            OnAllAliensReached();
        }
    }

    private void OnAllAliensReached()
    {
        Debug.Log("全クラゲ到着！");

        BeginBattle();
    }

    private void BeginBattle()
    {
        currentResult = BattleResult.None;

        battleEnded = false;

        ChangeState(BattleState.Battle);

        Debug.Log("戦闘開始！");

        foreach (BattleAlien alien in battleAliens)
        {
            alien.BeginBattle();
        }
    }

    public void OnAlienDestroyed(BattleAlien alien)
    {
        Debug.Log($"{alien.name}が撃破されました。");

        battleAliens.Remove(alien);

        if (battleAliens.Count == 0)
        {
            Debug.Log("クラゲが全滅しました。");
            SetBattleResult(BattleResult.Defeat);
            EndBattle();
        }
    }
    
    private void OnPlanetDestroyed()
    {
        SetBattleResult(BattleResult.Victory);
        EndBattle();
    }

    private void OnDestroy()
    {
        if (planet != null)
        {
            planet.OnDestroyed -= OnPlanetDestroyed;
        }
    }

    private void SetBattleResult(BattleResult result)
    {
        currentResult = result;
    }
}