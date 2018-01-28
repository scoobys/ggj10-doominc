using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Navigation
{
    public class MainMenu : MonoBehaviour
    {
        private MenuState _state;
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
            _state = MenuState.Main;
            Navigate(_state);
        }

        private void Navigate (MenuState newState)
        {
            _credits.SetActive(false);
            _main.SetActive(false);
            _new.SetActive(false);
            switch (newState)
            {
                case MenuState.Main:
                    _main.SetActive(true);
                    break;
                case MenuState.New:
                    _new.SetActive(true);
                    break;
                case MenuState.Credits:
                    _credits.SetActive(true);
                    break;
            }
        }

    }
}
