using UnityEngine;

public class Alien : MonoBehaviour
{
    [Header("基本データ")]
    public int level = 1;

    public int attack = 1;

    public int maxHP = 5;

    public int currentHP;

    public GridCell currentCell;

    private void Awake()
    {
        currentHP = maxHP;
    }
}