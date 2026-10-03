using UnityEngine;

public class Life : MonoBehaviour
{
    public float amount;

    void Update()
    {
        if (amount <= 0)
        {
            Destroy(gameObject);
        }
    }
}