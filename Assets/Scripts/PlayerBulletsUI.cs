using UnityEngine;
using TMPro;

public class PlayerBulletsUI : MonoBehaviour
{
    public PlayerShooting targetShooting;
    private TMP_Text text;

    void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    void Update()
    {
        if (targetShooting != null)
            text.text = "Bullets: " + targetShooting.bulletsAmount;
    }
}
