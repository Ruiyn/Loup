using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using MoreMountains.Feedbacks;

namespace MoreMountains.TopDownEngine
{

    public class BattleSystem : MonoBehaviour
    {

        public Camera worldCamera;
        public Character _player;
        public GameObject battle;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void StartBattle()
        {
            _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
            _player.Freeze();
            Object.Instantiate(battle, transform);
            worldCamera.gameObject.SetActive(false);
        }
    }
}
