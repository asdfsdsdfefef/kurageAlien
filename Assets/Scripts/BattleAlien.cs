using UnityEngine;

public class BattleAlien : MonoBehaviour
{
    [Header("クラゲデータ")]
    [SerializeField] private int level;

    [SerializeField] private float fallSpeed = 5f;

    [SerializeField] private float attackInterval = 1.0f;

    private bool canAttack = false;
    private float attackTimer = 0f;

    private Vector3 targetPosition;
    private bool isMoving = false;

    public System.Action<BattleAlien> OnReachedTarget;

    public void Initialize(int level)
    {
        Debug.Log($"BattleAlien Lv.{level} 生成");
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
    }
}