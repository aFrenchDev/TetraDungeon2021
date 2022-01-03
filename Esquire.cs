using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;
using System;
using System.Collections.Generic;
using System.Text;

namespace TetraDungeon
{
    class Esquire
    {

        // Spawn
        private Vector2 SpawnPosition;

        // Infos
        private Vector2 position;
        private Vector2 direction;
        private Vector2 destination;
        private string _animation;
        private AnimatedSprite _esquire;
        private float _vitesseEsquire = 75;


        public Esquire(AnimatedSprite _esquire, Vector2 spawnPosition, Vector2 destination)
        {
            this._esquire = _esquire;
            this.SpawnPosition = spawnPosition;
            this.Destination = destination;
        }

        public Vector2 Position
        {
            get
            {
                return this.Position;
            }

            set
            {
                this.Position = value;
            }
        }

        public Vector2 Destination
        {
            get
            {
                return this.destination;
            }

            set
            {
                this.destination = value;
            }
        }

        public void Behavior(Vector2 stevePosition, float deltaTime)
        {
            this.Position = new Vector2(160, 160);

            float walkSpeed = deltaTime * _vitesseEsquire;
            
            this._esquire.Play("Idle");
        }
    }
}
