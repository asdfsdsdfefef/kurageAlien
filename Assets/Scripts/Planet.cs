using UnityEngine;
using UnityEngine.UI;

public class Planet : MonoBehaviour
{
    [Header("HP")]
    [SerializeField] private int maxHP = 100;
    [SerializeField] private int currentHP;

    [SerializeField] private int defense = 0;
    [SerializeField] private int counterAttack = 1;

    [Header("UI")]
    [SerializeField] private Slider hpBar;

    public System.Action OnDestroyed;

    private void Awake()
    {
        currentHP = maxHP;
        InitializeHPBar();

        Debug.Log("Planet Ready");
        Debug.Log($"Planet HP : {currentHP} / {maxHP}");
    }

    public int TakeDamage(int attackPower)
    {
       // 防御力を考慮した実ダメージを計算
       int damage = Mathf.Max(1, attackPower - defense);
       UpdateHPBar();
       // HPを減らす
       currentHP -= damage;
       // HPが０未満にならないようにする
       if (currentHP < 0)
       {
           currentHP = 0;
       }

       Debug.Log($"Planetに{damage}ダメージ　残りHP:{currentHP}");
       // HPが０になったら撃破イベント
       if (currentHP == 0)
       {
           OnDestroyed?.Invoke();
       }

       // クラゲへ与える反撃ダメージを返す
       return counterAttack;
    }

    private void InitializeHPBar()
    {
        hpBar.maxValue = maxHP;
        hpBar.value = currentHP;
    }

    private void UpdateHPBar()
    {
        hpBar.value = currentHP;
    }
}