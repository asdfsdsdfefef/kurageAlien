using UnityEngine;

public class Planet : MonoBehaviour
{
    [Header("HP")]
    [SerializeField] private int maxHP = 100;
    [SerializeField] private int currentHP;

    public System.Action OnDestroyed;

    private void Awake()
    {
        currentHP = maxHP;

        Debug.Log("Planet Ready");
        Debug.Log($"Planet HP : {currentHP} / {maxHP}");
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        // HPが0未満にならないようにする
        currentHP = Mathf.Max(currentHP, 0);

        Debug.Log($"Planet HP : {currentHP} / {maxHP}");

        if (currentHP <=0)
        {
            Debug.Log("Planet Ddestroyed!");

            OnDestroyed?.Invoke();
        }
    }
}