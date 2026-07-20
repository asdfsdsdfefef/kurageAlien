using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BattleAlienData
{
    public int level;
    public Vector2Int gridPosition;
}

public static class BattleData
{
    // 出撃するクラゲ情報
    public static List<BattleAlienData> aliens = new List<BattleAlienData>();
}