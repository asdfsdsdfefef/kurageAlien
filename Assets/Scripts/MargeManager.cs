using UnityEngine;

public class MergeManager : MonoBehaviour
{
    public static MergeManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public bool TryMerge(Alien movingAlien, Alien targetAlien)
    {
        if (movingAlien.level != targetAlien.level)
        {
            return false;
        }

        Debug.Log("マージ成功！");

        return true;
    }
}