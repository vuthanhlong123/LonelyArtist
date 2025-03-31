using System;
using UnityEngine;

namespace Game.Runtimes.Characters
{
    [Serializable]
    public class CharacterKernel 
    {
        protected Character character;

        public void StartUp(Character character)
        {
            this.character = character;
        }

        public virtual void Awake()
        {

        }

        public virtual void Start()
        {

        }

        public virtual void Update()
        {

        }

        public virtual void FixedUpdate()
        {

        }

        public virtual void Enable()
        {

        }

        public virtual void Disable()
        {

        }
    }
}


