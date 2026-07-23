using UnityEngine;

public class BattleAlien : MonoBehaviour
{
    [Header("クラゲデータ")]
    [SerializeField] private int level;

    [SerializeField] private float fallSpeed = 5f;

    [SerializeField] private float attackInterval = 1.0f;

    [Header("HP")]
    [SerializeField] private int maxHP = 10;
    [SerializeField] private int currentHP;
    [SerializeField] private int attack = 1;

    private bool canAttack = false;
    private float attackTimer = 0f;

    private Vector3 targetPosition;
    private bool isMoving = false;

    private Planet targetPlanet;

    public System.Action<BattleAlien> OnReachedTarget;

    public void Initialize(int level, Planet planet)
    {
        this.level = level;
        targetPlanet = planet;
        ApplyStatsByLevel();
        currentHP = maxHP;

        Debug.Log($"BattleAlien Lv.{level} Initialized");
        Debug.Log($"Attack : {attack}");
        Debug.Log($"BattleAlien HP : {currentHP} / {maxHP}");
    }

    private void ApplyStatsByLevel()
    {
        switch (level)
        {
            case 1:
                attack = 1;
                maxHP = 10;
                break;

            case 2:
                attack = 2;
                maxHP = 20;
                break;
            
            case 3:
                attack = 3;
                maxHP = 30;
                break;

            default:
                // 仮実装
                attack = level;
                maxHP = level * 10;
                break;
        }
    }

    public void StartMove(Vector3 target)
    {
        targetPosition = target;
        isMoving = true;
    }

    public void BeginBattle()
    {
        canAttack = true;
        attackTimer = 0f;
    }

    private void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                fallSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                transform.position = targetPosition;
                isMoving = false;

                Debug.Log("BattleAlien 到着");
                OnReachedTarget?.Invoke(this);
            }
        }

        if (canAttack)
        {
            attackTimer += Time.deltaTime;

            if (attackTimer >= attackInterval)
            {
                attackTimer = 0f;

                Attack();
            }
        }
    }

    private void Attack()
    {
        Debug.Log($"{gameObject.name} が攻撃！");

        // 惑星へ攻撃し、反撃ダメージを受け取る
        int counterDamage = targetPlanet.TakeDamage(attack);
        // 惑星から反撃ダメージを受ける
        TakeDamage(counterDamage);
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        // HPが０未満にならないようにする
        currentHP = Mathf.Max(currentHP, 0);

        Debug.Log($"BattleAlien HP : {currentHP} / {maxHP}");

        if (currentHP == 0)
        {
            Die();
        }
    }

    private void Die()
    {
        canAttack = false;

        Debug.Log("BattleAlien撃破");

        BattleManager.Instance.OnAlienDestroyed(this);
    }

    public void StopBattle()
    {
        canAttack = false;
    }
}