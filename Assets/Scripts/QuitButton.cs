using UnityEngine;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour
{
    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(Quit);
    }

    void Quit()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }
}
