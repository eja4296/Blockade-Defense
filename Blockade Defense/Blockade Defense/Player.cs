using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blockade_Defense
{
    class Player
    {
        // attributes
        string name;
        int levelScore;
        int totalScore;
        int money;
        int numberOfLives;
        int currentWave;

        // constructor
        public Player(string nm, int lives, int mny)
        {
            name = nm;
            levelScore = 0;
            totalScore = 0;
            money = mny;
            numberOfLives = lives;
            currentWave = 1;
        }

        // properties
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public int LevelScore
        {
            get { return levelScore; }
            set { levelScore = value; }
        }
        public int TotalScore
        {
            get { return totalScore; }
            set { totalScore = value; }
        }
        public int Money
        {
            get { return money; }
            set { money = value; }
        }
        public int NumberOfLives
        {
            get { return numberOfLives; }
            set { numberOfLives = value; }
        }
        public int CurrentWave
        {
            get { return currentWave; }
            set { currentWave = value; }
        }
    }
}
