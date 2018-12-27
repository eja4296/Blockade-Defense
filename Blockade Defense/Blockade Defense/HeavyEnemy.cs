﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Blockade_Defense
{
    public class HeavyEnemy : Enemy
    {
        // attributes
        int health;
        int enemySpeed;
        int originalHealth;

        // constructor
        public HeavyEnemy(int x, int y, Texture2D enText, Rectangle enRec, int waveNum)
            :base(x,y, enText, enRec)
        {
            //ALL SPEED MUST BE DIVISIBLE BY 80!
            enemySpeed = 2;
            health = 100 + (waveNum * 25);
            originalHealth = health;
        }
        //properties
        public int EnemySpeed
        {
            get { return enemySpeed; }
            set { enemySpeed = value; }
        }

        public int Health
        {
            get { return health; }
            set { health = value; }
        }
        public int OriginalHealth
        {
            get { return originalHealth; }
            set { originalHealth = value; }
        }

        public override void Move()
        {
            if (enemyRectangle.X < NextMoveX)
            {
                enemyRectangle.X += EnemySpeed;
            }
            if (enemyRectangle.Y < NextMoveY)
            {
                enemyRectangle.Y += EnemySpeed;
            }
            if (enemyRectangle.X > NextMoveX)
            {
                enemyRectangle.X -= EnemySpeed;
            }
            if (enemyRectangle.Y > NextMoveY)
            {
                enemyRectangle.Y -= EnemySpeed;
            }
        }


        public override void NextMove(int x, int y)
        {
            NextMoveX = x;
            NextMoveY = y;
        }

        public override bool TakeDamage(int damageTaken)
        {
            health -= damageTaken;
            if (health <= 0)
            {
                Active = false;
                return Active;
            }
            return Active;
        }

        public override int GetOriginalHealth()
        {
            return originalHealth;
        }
        public override int GetHealth()
        {
            return health;
        }

    }
}