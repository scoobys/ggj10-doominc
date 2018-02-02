using Assets.Scripts.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Menu
{
    public class ButtonBase : MonoBehaviour
    {
        public AudioSource ClickSound;
        public Scenes TargetScene = Scenes.MainMenu;

        public void LoadScene()
        {
            if (ClickSound != null)
            {
                ClickSound.Play();
            }
            SceneManager.LoadScene((int)TargetScene);
        }

        public void Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit ();
#endif
        }
    }
}
