using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Blockade_Defense
{
    abstract public class Enemy
    {
        // attributes
        int enemyDirection;
        int enemyX;
        int enemyY;
        int nextMoveX; // used to help move enemy
        int nextMoveY; // used to help move enemy
        int nextXTrackerNum; // used to help move enemy
        int nextYTrackerNum; // used to help move enemy
        bool active;
        public Rectangle enemyRectangle;
        Texture2D enemyTexture;
        bool reachedEnd;
        bool firstNextMoveFound;
        int enemySpeed;

        // constuctor
        public Enemy(int x, int y, Texture2D enText, Rectangle enRec)
        {
            enemyDirection = 0;
            enemyX = x;
            enemyY = y;
            enemyRectangle = enRec;
            enemyRectangle.X = (enemyX*80);
            enemyRectangle.Y = (enemyY*80);
            enemyRectangle.Width = 80;
            enemyRectangle.Height = 80;
            enemyTexture = enText;
            nextMoveX = 0;
            nextMoveY = 0;
            nextXTrackerNum = 1;
            nextYTrackerNum = 1;
            reachedEnd = false;
            firstNextMoveFound = false;
        }

        public int EnemyDirection
        {
            get { return enemyDirection; }
            set { enemyDirection = value; }
        }
        public Texture2D EnemyTexture
        {
            get { return enemyTexture; }
            set { enemyTexture = value; }
        }
        public Rectangle EnemyRectangle
        {
            get { return enemyRectangle; }
            set { enemyRectangle = value; }
        }
        public int EnemyX
        {
            get { return enemyX; }
            set { enemyX = value; }
        }
        public int EnemyY
        {
            get { return enemyY; }
            set { enemyY = value; }
        }
        public bool Active
        {
            get { return active; }
            set { active = value; }
        }
        public int NextMoveX
        {
            get { return nextMoveX; }
            set { nextMoveX = value; }
        }
        public int NextMoveY
        {
            get { return nextMoveY; }
            set { nextMoveY = value; }
        }
        public int NextXTrackerNum
        {
            get { return nextXTrackerNum; }
            set { nextXTrackerNum = value; }
        }
        public int NextYTrackerNum
        {
            get { return nextYTrackerNum; }
            set { nextYTrackerNum = value; }
        }
        public bool ReachedEnd
        {
            get { return reachedEnd; }
            set { reachedEnd = value; }
        }
        public bool FirstNextMoveFound
        {
            get { return firstNextMoveFound; }
            set { firstNextMoveFound = value; }
        }

        virtual public void GetDirection()
        {
            // 1 is right
            // 2 is down
            // 3 is left
            // 4 is up

            if (enemyRectangle.X > nextMoveX && enemyRectangle.Y == nextMoveY)
            {               
                enemyDirection = 3; //left
                return;
            }
            if (enemyRectangle.X < nextMoveX && enemyRectangle.Y == nextMoveY)
            {
                enemyDirection = 1; //right
                return;
            }
            if (enemyRectangle.Y > nextMoveY && enemyRectangle.X == nextMoveX)
            {
                enemyDirection = 4; // up
                return;
            }
            if (enemyRectangle.Y < nextMoveY && enemyRectangle.X == nextMoveX)
            {
                enemyDirection = 2; // down
                return;
            }


        }
        abstract public bool TakeDamage(int damageTaken);

        abstract public void Move();

        abstract public void NextMove(int x, int y);

        abstract public int GetOriginalHealth();

        abstract public int GetHealth();
    }
}
