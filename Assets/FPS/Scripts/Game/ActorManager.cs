using System;
using UnityEngine;
using System.Collections.Generic;

namespace Unity.FPS.Game
{
    public class ActorManager : MonoBehaviour
    {
        public List<Actor> Actors { get; private set; }
        public GameObject Player { get; private set; }

        private void Awake()
        {
            Actors = new List<Actor>();
        }
        
        public void SetPlayer(GameObject player) => Player = player;
    }
}

