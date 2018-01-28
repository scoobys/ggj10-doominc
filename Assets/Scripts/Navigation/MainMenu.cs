using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Navigation
{
    public class MainMenu : MonoBehaviour
    {
        private GameObject _main;
        private GameObject _credits;
        private GameObject _new;

        public void Awake()
        {
            _main = GameObject.FindGameObjectWithTag("Main");
            _credits = GameObject.FindGameObjectWithTag("Credits");
            _new = GameObject.FindGameObjectWithTag("New");
        }
        public void Start()
        {
            if(_new.activeSelf && _credits.activeSelf)
            {
                Navigate(0);
            }
        }
        public void Navigate (int newState)
        {
            _credits.SetActive(false);
            _main.SetActive(false);
            _new.SetActive(false);
            switch (newState)
            {
                case (int)MenuState.Main:
                    _main.SetActive(true);
                    break;
                case (int)MenuState.New:
                    _new.SetActive(true);
                    break;
                case (int)MenuState.Credits:
                    _credits.SetActive(true);
                    break;
                default:
                    _main.SetActive(true);
                    break;
            }
        }

    }
}
