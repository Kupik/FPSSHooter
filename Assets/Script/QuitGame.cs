using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QuitGame : MonoBehaviour
{
    public Button quitButton; // Referință la butonul de închidere

    void Start()
    {
        if (quitButton != null)
        {
            quitButton.onClick.AddListener(Quit);
        }
    }

   public void Quit()
    {
#if UNITY_EDITOR
        // Închide jocul doar în editorul Unity
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Închide jocul în build
        Application.Quit();
#endif
    }
}
