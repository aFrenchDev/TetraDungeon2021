using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;
using System;
using System.Collections.Generic;
using System.Text;

namespace TetraDungeon
{
    class Player
    {
        // Steve
        private AnimatedSprite _steve;
        private float vitesse = 75;
        private float timerFootStep;
        private int life;
        private Vector2 position;

        public Player(AnimatedSprite steve)
        {
            _steve = steve;
        }

        public Vector2 Position
        {
            get
            {
                return this.position;
            }

            set
            {
                this.position = value;
            }
        }

        public int Life
        {
            get
            {
                return this.life;
            }

            set
            {
                this.life = value;
            }
        }

        public void MovementSteve(float deltaSeconds, TiledMapTileLayer tiledMapTileLayer, TiledMap tiledMap, string animationSteve, ref Vector2 _stevePosition, ref string tempIdle, SoundEffect steveFootStep)
        {
            // GESTION DU CLAVIER
            KeyboardState keyboardState = Keyboard.GetState();
            float walkSpeed = deltaSeconds * vitesse;

            animationSteve = tempIdle;

            // -------------------------------------------------------
            //                   DEPLACEMENT STEVE
            // -------------------------------------------------------

            if (keyboardState.IsKeyDown(Keys.Z))
            {
                ushort tx = (ushort)(_stevePosition.X / tiledMap.TileWidth);
                ushort ty = (ushort)(_stevePosition.Y / tiledMap.TileHeight - 0.1);
                if (!Collision.IsCollision(tx, ty, tiledMapTileLayer))
                    _stevePosition.Y -= walkSpeed;

                tempIdle = "idleBack";
                animationSteve = "walkBack";
            }
            if (keyboardState.IsKeyDown(Keys.S))
            {
                ushort tx = (ushort)(_stevePosition.X / tiledMap.TileWidth);
                ushort ty = (ushort)(_stevePosition.Y / tiledMap.TileHeight + 0.5);
                if (!Collision.IsCollision(tx, ty, tiledMapTileLayer))
                    _stevePosition.Y += walkSpeed;

                tempIdle = "idleFront";
                animationSteve = "walkFront";
            }
            if (keyboardState.IsKeyDown(Keys.Q))
            {
                ushort tx = (ushort)(_stevePosition.X / tiledMap.TileWidth - 0.5);
                ushort ty = (ushort)(_stevePosition.Y / tiledMap.TileHeight);
                if (!Collision.IsCollision(tx, ty, tiledMapTileLayer))
                    _stevePosition.X -= walkSpeed;

                tempIdle = "idleF";
                animationSteve = "walkF";
            }
            if (keyboardState.IsKeyDown(Keys.D))
            {
                ushort tx = (ushort)((_stevePosition.X / tiledMap.TileWidth + 0.5));
                ushort ty = (ushort)((_stevePosition.Y / tiledMap.TileHeight));
                if (!Collision.IsCollision(tx, ty, tiledMapTileLayer))
                    _stevePosition.X += walkSpeed;

                tempIdle = "idle";
                animationSteve = "walk";
            }

            if (animationSteve == "walkF" || animationSteve == "walk" || animationSteve == "walkFront" || animationSteve == "walkBack")
            {
                timerFootStep += deltaSeconds;
                if (timerFootStep >= 0.35)
                {
                    timerFootStep = 0;
                    steveFootStep.Play();
                }
            }

            _steve.Play(animationSteve);
        }
    }
}
