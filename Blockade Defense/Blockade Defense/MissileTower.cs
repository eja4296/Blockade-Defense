using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Blockade_Defense
{
    class MissileTower : Tower
    {
        int attackPower;
        int attackRadius;
        int attackFrequency;
        Rectangle attackRectangle;
        Rectangle radiusRec;
        Texture2D towerTexture;
        Texture2D attackTexture;
        int cost;
        float coolDown;
        bool isAttacking;
        int attackTracker1;
        int attackTracker2;
        int attackTrackerX;
        int attackTrackerY;
        int[] targetLocation;
        bool isTracking;
        int attackSpeed;

        public MissileTower(int x, int y, int atkPwr, int atkRad, int atkFreq, int cst, Texture2D text, Texture2D atkText)
            : base(x, y)
        {
            attackPower = atkPwr;
            attackRadius = atkRad;
            attackFrequency = atkFreq;
            towerTexture = text;
            attackTexture = atkText;
            attackRectangle.X = (TowerRec.X + 32);
            attackRectangle.Y = (TowerRec.Y + 32);
            attackRectangle.Width = 0;
            attackRectangle.Height = 0;
            radiusRec.X = (TowerRec.X - 160);
            radiusRec.Y = (TowerRec.Y - 160);
            radiusRec.Width = 400;
            radiusRec.Height = 400;
            cost = cst;
            coolDown = 0;
            isAttacking = false;
            attackTracker1 = 0;
            attackTracker2 = 0;
            attackTrackerX = 1;
            attackTrackerY = 1;
            targetLocation = new int[2];
            isTracking = false;
            attackSpeed = 15;
        }
        public int AttackPower
        {
            get { return attackPower; }
            set { attackPower = value; }
        }
        public int AttackRadius
        {
            get { return attackRadius; }
            set { attackRadius = value; }
        }
        public int AttackFrequency
        {
            get { return attackFrequency; }
            set { attackFrequency = value; }
        }
        public Texture2D TowerTexture
        {
            get { return towerTexture; }
            set { TowerTexture = value; }
        }
        public int Cost
        {
            get { return cost; }
            set { cost = value; }
        }
        public Rectangle AttackRectangle
        {
            get { return attackRectangle; }
            set { attackRectangle = value; }
        }
        public float CoolDown
        {
            get { return coolDown; }
            set { coolDown = value; }
        }
        public int AttackTracker1
        {
            get { return attackTracker1; }
            set { attackTracker1 = value; }
        }
        public int AttackTracker2
        {
            get { return attackTracker2; }
            set { attackTracker2 = value; }
        }
        public int AttackTrackerX
        {
            get { return attackTrackerX; }
            set { attackTrackerX = value; }
        }
        public int AttackTrackerY
        {
            get { return attackTrackerY; }
            set { attackTrackerY = value; }
        }
        public bool IsAttacking
        {
            get { return isAttacking; }
            set { isAttacking = value; }
        }
        public Texture2D AttackTexture
        {
            get { return attackTexture; }
            set { attackTexture = value; }
        }
        public Rectangle RadiusRec
        {
            get { return radiusRec; }
            set { radiusRec = value; }
        }
        public int[] TargetLocation
        {
            get { return targetLocation; }
            set { targetLocation = value; }
        }
        public bool IsTracking
        {
            get { return isTracking; }
            set { isTracking = value; }
        }
        public int AttackSpeed
        {
            get { return attackSpeed; }
            set { attackSpeed = value; }
        }

    }
}
