using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Blockade_Defense
{
    class BasicTower : Tower 
    {
        //attributes
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
       

        public BasicTower(int x, int y, int atkPwr, int atkRad, int atkFreq, int cst, Texture2D text, Texture2D atkText)
            : base(x, y)
        {
            attackPower = atkPwr;
            attackRadius = atkRad;
            attackFrequency = atkFreq;
            towerTexture = text;
            attackRectangle.X = (TowerRec.X - 80);
            attackRectangle.Y = (TowerRec.Y - 80);
            attackRectangle.Width = 0;
            attackRectangle.Height = 0;
            radiusRec.X = (TowerRec.X - 80);
            radiusRec.Y = (TowerRec.Y - 80);
            radiusRec.Width = 240;
            radiusRec.Height = 240;
            attackTexture = atkText;
            cost = cst;
            coolDown = 0;
            isAttacking = false;
            attackTracker1 = 0;
            attackTracker2 = 0;
            
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
        public Rectangle RadiusRec
        {
            get { return radiusRec; }
            set { radiusRec = value; }
        }
        public float CoolDown
        {
            get { return coolDown; }
            set { coolDown = value; }
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
        

    }
}
