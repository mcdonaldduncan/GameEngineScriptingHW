using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Monster
{
    public class MonsterInstantiator : MonoBehaviour
    {
        public static MonsterInstantiator SharedInstance;

        public GameObject sunlionPrefab;
        public GameObject strawbunnyPrefab;
        public GameObject advodoggoPrefab;
        public GameObject raccornPrefab;

        private void Awake()
        {
            SharedInstance = this;
        }

        public void InstantiateLion()
        {
            Instantiate(sunlionPrefab);
        }

        public void InstantiateStrawbunny()
        {
            Instantiate(strawbunnyPrefab);
        }

        public void InstantiateAdvodoggo()
        {
            Instantiate(advodoggoPrefab);
        }

        public void InstantiateRaccorn()
        {
            Instantiate(raccornPrefab);
        }

        public void InstantiateMonster(string key)
        {
            switch (key)
            {
                case ("SunflowerLion"):
                    InstantiateLion();
                    break;
                case ("Strawbunny"):
                    InstantiateStrawbunny();
                    break;
                case ("Advodoggo"):
                    InstantiateAdvodoggo();
                    break;
                case ("Raccorn"):
                    InstantiateRaccorn();
                    break;
            }
        }
    }
}
