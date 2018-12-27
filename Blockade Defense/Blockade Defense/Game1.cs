using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;
using System;

namespace Blockade_Defense
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        //Attributes
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // Different spritefonts for each Gamestate
        SpriteFont mySpriteFont;
        SpriteFont mySpriteFont2;
        SpriteFont mySpriteFont3;
        SpriteFont mySpriteFont4;

        //Grid of tile objects
        Tile[,] tileArray;
        PathTile[] pathTileArray;

        //Tile Objects
        EmptyTile emptyTile;
        PathTile pathTile;

        // Used to rotate enemies
        Vector2 rotationVector = new Vector2(40, 40);

        //Enemy Array to hold all enemy objects
        Enemy[] enemyArray;

        // Tower Arrays
        BasicTower[] basicTowerArray;
        LongRangeTower[] longRangeTowerArray;
        MissileTower[] missileTowerArray;
        SuperTower[] superTowerArray;

        // int for how many spaces are on the board that a tower can be placed on
        int numOfTowerSpaces;
        // int for tracking the number of towers in the tower array
        int towerArrayNumTracker = 0;
        //basic tower object

        // Random number generator; used when creating new enemies
        Random rgen = new Random();
        int randomNumber;



        // wave number tracks the current wave
        // numberOfEnemies is used to calculate the number of enemies that will spawn each round
        int waveNumber = 1;
        int numberOfEnemies = 0;

        // use to make sure a certain method is only called once at the beginning of a round
        int loadEnemiesOnce = 0;

        // int to only clear the towers from the screen if a new game is started (not after each round)
        int keepTowersOnScreen = 0;

        // arrays to hold the information for the path Tiles
        int[] pathTileX;
        int[] pathTileY;

        // used to move enemies only every ... seconds
        int enemyTimer;

        // used to create a new enemy only every ... seconds
        int enemyTimerT = 0;

        // used to track what position in the enemy array a new enemy will be placed
        int enemyArrayNumTracker = 0;

        // make sure certain methods are only called once when starting each round
        int onlyIncrementOnce = 0;
        
        // integers that change if the user has clicked on one of the towers
        int clickedOnTwr1 = 0;
        int clickedOnTwr2 = 0;
        int clickedOnTwr3 = 0;
        int clickedOnTwr4 = 0;

        //int pulseTracker;

        int[] enemyLocationTracker = new int[2];

        // Player object
        Player player1;

        // int to track the number of enemies to reach the end
        // if numOfEnemiesReached + player1.LevelScore == numOfEnemies, Go to next level
        int numOfEnemiesReachedEnd;

        // Window Forms object
        Name getName;
        LoadMap loading;
        //Map map1;
        bool mapHasBeenSelected;
        public string currentMap = "mapFile.txt";
        bool loadIsRunning;
        int mapHasBeenRead = 0;

        // HighScore stats
        string hsName = "";
        int hsWave = 1;
        int hsScore = 0;

        string newMapFile;

        // Textures
        Texture2D titleScreenTexture;
        Texture2D pressEnterTexture;
        Texture2D mainMenuTexture;
        Texture2D playGameButtonTexture;
        Texture2D quitGameButtonTexture;
        Texture2D helpButtonTexture;
        Texture2D gameTexture;
        Texture2D gameOverTexture;
        Texture2D mainMenuButtonTexture;
        Texture2D pathBlockTexture;
        Texture2D towerPlaceHolderTexture;
        Texture2D subPlaceHolderTexture;

        Texture2D defaultPlayGameButtonTexture;
        Texture2D playGameHoverTexture;
        Texture2D defaultQuitGameButtonTexture;
        Texture2D quitGameHoverTexture;
        Texture2D defaultHelpButtonTexture;
        Texture2D helpHoverTexture;
        Texture2D defaultMainMenuButtonTexture;
        Texture2D mainMenuHoverTexture;

        Texture2D startNextWaveHoverTexture;
        Texture2D endGameHoverTexture;
        Texture2D pressEnterBlinkTexture;
        Texture2D defaultPressEnterTexture;
        Texture2D emptyTileTexture;
        Texture2D towerPlaceHolder2Texture;
        Texture2D towerPlaceHolder3Texture;
        Texture2D superTowerTexture;
        Texture2D subPlaceHolder2Texture;
        Texture2D subPlaceHolder3Texture;
        Texture2D subPlaceHolder4Texture;

        Texture2D pulse;
        Texture2D testBullet;
        Texture2D missile;

        Texture2D finishedButtonTexture;
        Texture2D mapButtonTexture;
        Texture2D mapButtonHoverTexture;
        Texture2D mapSelectionScreenTexture;
        Texture2D defaultMapButtonTexture;
        Texture2D towerHoverTexture;

        // Rectangles for all of the Textures
        Rectangle screenRec;
        Rectangle pressEnterRec;
        Rectangle playGameRec;
        Rectangle quitGame1Rec;
        Rectangle helpRec;
        Rectangle mainMenuRec;
        Rectangle quitGame2Rec;
        Rectangle enemyPathRec;
        Rectangle startWaveRec;
        Rectangle endGameRec;
        Rectangle towerPlaceHolderRec;
        Rectangle towerPlaceHolder2Rec;
        Rectangle towerPlaceHolder3Rec;
        Rectangle superTowerRec;
        Rectangle subRectangle;
        Rectangle bigBoy;

        Rectangle finishedButtonRec;
        Rectangle mapButtonRec;

        Rectangle pulseRec;
        Rectangle tower1HoverRec;
        Rectangle tower2HoverRec;
        Rectangle tower3HoverRec;
        Rectangle tower4HoverRec;


        // MouseTracking ints
        int m = 0; // tracks for StartNextWaveHover
        int n = 0; // tracks for EndGamehover
        // p, q, r, and s are used for hover texture for clicking on towers
        int p = 0;
        int q = 0;
        int r = 0;
        int s = 0;
        int o = 0; // tracks so quitgame button doesn't go off imidately upon entering gameover gamestate from playgame gamestate aftering clicking end game
                   // the End game button and Quit game button are in the same area so 'o' prevents a small problem

        int finishedButtonTracker = 0;

        // KeyboardStates; used for title screen gamestate
        KeyboardState kbState;
        KeyboardState previousKbState;

        // use for 'press enter' blink
        double timer = 0;
        double x = 0;

        // tracks index of pathTileArray when creating map
        int pathTracker;


        //GameState enum method 
        public enum GameState
        {
            TitleScreen,
            MainMenu,
            Help,
            Map,
            PlayGame,
            GameOver
        }

        GameState currentGameState;
        //Constructor
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            // Make mouse visible
            IsMouseVisible = true;

            // Start at the title screen
            currentGameState = GameState.TitleScreen;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            // Load all Textures here
            titleScreenTexture = this.Content.Load<Texture2D>(@"Images/TitleScreen");
            pressEnterTexture = this.Content.Load<Texture2D>(@"Images/PressEnter");
            mainMenuTexture = this.Content.Load<Texture2D>(@"Images/MainMenu");
            playGameButtonTexture = this.Content.Load<Texture2D>(@"Images/PlayGameButton");
            quitGameButtonTexture = this.Content.Load<Texture2D>(@"Images/QuitGameButton");
            helpButtonTexture = this.Content.Load<Texture2D>(@"Images/HelpButton");
            gameTexture = this.Content.Load<Texture2D>(@"Images/Game");
            gameOverTexture = this.Content.Load<Texture2D>(@"Images/GameOver");
            mainMenuButtonTexture = this.Content.Load<Texture2D>(@"Images/MainMenuButton");
            pathBlockTexture = this.Content.Load<Texture2D>(@"Images/PathBlock");
            towerPlaceHolderTexture = this.Content.Load<Texture2D>(@"Images/PulseTower");
            subPlaceHolderTexture = this.Content.Load<Texture2D>(@"Images/BasicSub");

            towerPlaceHolder2Texture = this.Content.Load<Texture2D>(@"Images/BasicTower");
            towerPlaceHolder3Texture = this.Content.Load<Texture2D>(@"Images/MissileTower");
            subPlaceHolder2Texture = this.Content.Load<Texture2D>(@"Images/FastSub");
            testBullet = this.Content.Load<Texture2D>(@"Images/TestBullet");
            subPlaceHolder3Texture = this.Content.Load<Texture2D>(@"Images/HeavySub");
            superTowerTexture = this.Content.Load<Texture2D>(@"Images/SuperTower");

            playGameHoverTexture = this.Content.Load<Texture2D>(@"Images/PlayGameButtonHover");
            defaultPlayGameButtonTexture = this.Content.Load<Texture2D>(@"Images/PlayGameButton");
            quitGameHoverTexture = this.Content.Load<Texture2D>(@"Images/QuitGameButtonHover");
            defaultQuitGameButtonTexture = this.Content.Load<Texture2D>(@"Images/QuitGameButton");
            helpHoverTexture = this.Content.Load<Texture2D>(@"Images/HelpButtonHover");
            defaultHelpButtonTexture = this.Content.Load<Texture2D>(@"Images/HelpButton");
            mainMenuHoverTexture = this.Content.Load<Texture2D>(@"Images/MainMenuButtonHover");
            defaultMainMenuButtonTexture = this.Content.Load<Texture2D>(@"Images/MainMenuButton");

            startNextWaveHoverTexture = this.Content.Load<Texture2D>(@"Images/StartNextWaveHover");
            endGameHoverTexture = this.Content.Load<Texture2D>(@"Images/EndGameHover");
            pressEnterBlinkTexture = this.Content.Load<Texture2D>(@"Images/PressEnterBlink");
            defaultPressEnterTexture = this.Content.Load<Texture2D>(@"Images/PressEnter");

            emptyTileTexture = this.Content.Load<Texture2D>(@"Images/EmptyTile");

            mySpriteFont = this.Content.Load<SpriteFont>(@"Images/mySpriteFont");
            mySpriteFont2 = this.Content.Load<SpriteFont>(@"Images/mySpriteFont2");
            mySpriteFont3 = this.Content.Load<SpriteFont>(@"Images/mySpriteFont3");
            mySpriteFont4 = this.Content.Load<SpriteFont>(@"Images/mySpriteFont4");

            pulse = this.Content.Load<Texture2D>(@"Images/Pulse1");
            missile = this.Content.Load<Texture2D>(@"Images/missile");
            subPlaceHolder4Texture = this.Content.Load<Texture2D>(@"Images/BigBoy");
            finishedButtonTexture = this.Content.Load<Texture2D>(@"Images/FinishedButton");
            mapButtonTexture = this.Content.Load<Texture2D>(@"Images/MapButton");
            mapButtonHoverTexture = this.Content.Load<Texture2D>(@"Images/MapButtonHover");
            mapSelectionScreenTexture = this.Content.Load<Texture2D>(@"Images/MapSelectionScreen");
            defaultMapButtonTexture = this.Content.Load<Texture2D>(@"Images/MapButton");

            towerHoverTexture = this.Content.Load<Texture2D>(@"Images/TowerHover");


            // Creates array for grid spaces
            tileArray = new Tile[9, 12];
            //pathTileArray = new PathTile[108];

            //Populate the 2D array with EmptyTile objects
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    emptyTile = new EmptyTile((80 * j), (80 * i), emptyTileTexture);
                    tileArray[i, j] = emptyTile;
                }
            }

            ReadMap();
            //map1 = new Map();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);

            // used for 'press enter' blink in title screeen
            timer = Math.Sin(x);
            x += .1;

            // mouse class stuff for hovering and clicking
            var mouseState = Mouse.GetState();
            var mousePosition = new Point(mouseState.X, mouseState.Y);



            // switch statement for circulating through each gamestate
            switch (currentGameState)
            {
                // Title Screen
                case GameState.TitleScreen:
                    previousKbState = kbState;
                    kbState = Keyboard.GetState();
                    // Goes to the main menu when user presses enter key
                    if (SingleKeyPress(Keys.Enter))
                    {
                        currentGameState = GameState.MainMenu;

                    }
                    break;
                // Main Menu
                case GameState.MainMenu:
                    // Creates a new PLayer when you get the the main menu
                    player1 = new Player("", 100, 250);
                    getName = new Name();
                    loading = new LoadMap();
                    player1.Name = "";
                    

                    // Play Game button hover effect
                    if (new Rectangle(mouseState.X, mouseState.Y, 1, 1).Intersects(playGameRec))
                    {
                        playGameButtonTexture = playGameHoverTexture;
                    }
                    else
                    {
                        playGameButtonTexture = defaultPlayGameButtonTexture;
                    }

                    // Quit Game button hover effect
                    if (new Rectangle(mouseState.X, mouseState.Y, 1, 1).Intersects(quitGame1Rec))
                    {
                        quitGameButtonTexture = quitGameHoverTexture;
                    }
                    else
                    {
                        quitGameButtonTexture = defaultQuitGameButtonTexture;
                    }

                    // Help button hover effect
                    if (new Rectangle(mouseState.X, mouseState.Y, 1, 1).Intersects(helpRec))
                    {
                        helpButtonTexture = helpHoverTexture;
                    }
                    else
                    {
                        helpButtonTexture = defaultHelpButtonTexture;
                    }

                    // Map Button Hover Effect
                    if (new Rectangle(mouseState.X, mouseState.Y, 1, 1).Intersects(mapButtonRec))
                    {
                        mapButtonTexture = mapButtonHoverTexture;
                    }
                    else
                    {
                        mapButtonTexture = defaultMapButtonTexture;
                    }

                    // If the user clicks on 'Play Game' it takes the user to the PlayGame gamestate
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        if (new Rectangle(mouseState.X, mouseState.Y, 1, 1).Intersects(playGameRec))
                        {
                            getName.Show();

                            currentGameState = GameState.PlayGame;

                        }

                    }

                    // Quits game
                    if (mouseState.LeftButton == ButtonState.Pressed && o > 30)
                    {
                        if (new Rectangle(mouseState.X, mouseState.Y, 1, 1).Intersects(quitGame1Rec))
                        {
                            // close the Windows Form
                            getName.Close();
                            Environment.Exit(1);
                        }

                    }

                    // Goes to Help menu
                    if (mouseState.LeftButton == ButtonState.Pressed && o > 30)
                    {
                        if (new Rectangle(mouseState.X, mouseState.Y, 1, 1).Intersects(helpRec))
                        {
                            o = 0;
                            currentGameState = GameState.Help;
                        }

                    }

                    // Goes to map selection screen
                    if (mouseState.LeftButton == ButtonState.Pressed && o > 30)
                    {
                        if (new Rectangle(mouseState.X, mouseState.Y, 1, 1).Intersects(mapButtonRec))
                        {
                            o = 0;
                            pathTileArray = new PathTile[108];
                            pathTracker = 0;
                            currentMap = "";
                            WriteMap();
                            tileArray = new Tile[9, 12];
                            
                            //Populate the 2D array with EmptyTile objects
                            for (int i = 0; i < 9; i++)
                            {
                                for (int j = 0; j < 12; j++)
                                {
                                    emptyTile = new EmptyTile((80 * j), (80 * i), emptyTileTexture);
                                    tileArray[i, j] = emptyTile;
                                }
                            }

                            if (mouseState.LeftButton == ButtonState.Pressed)
                            {
                                if (new Rectangle(mouseState.X, mouseState.Y, 1, 1).Intersects(mapButtonRec))
                                {
                                    loading.Show();
                                    //map1 = new Map();
                                    currentMap = "";
                                    mapHasBeenRead = 0;
                                    currentGameState = GameState.Map;

                                }

                            }
                            //currentGameState = GameState.Map;
                        }

                    }
                    o++;
                    break;

                case GameState.Map:
                    Rectangle mouseTracker2 = new Rectangle(mouseState.X, mouseState.Y, 1, 1);

                    //currentMap = "";
                    //testForm.ShowDialog();
                    loadIsRunning = loading.CheckIfRunning();
                    
                    currentMap = loading.GetNewMap();

                    if (currentMap == "")
                    {
                        currentMap = loading.GetSavedMap();
                    }
                    // Finsihed Button Hover
                    if (mouseTracker2.X >= (GraphicsDevice.Viewport.Width - 320) && mouseTracker2.Y >= (GraphicsDevice.Viewport.Height - 160))
                    {
                        finishedButtonTracker = 1;
                    }
                    else
                    {
                        finishedButtonTracker = 0;
                    }

                    // Goes back to Main Menu
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        if (new Rectangle(mouseState.X, mouseState.Y, 1, 1).Intersects(finishedButtonRec))
                        {                          
                            // Write in the map corrdinates that the user chose
                            if (mapHasBeenSelected == true)
                            {
                                WriteMap();
                            }
                            
                            // Reads in the text file to get the corrdinates of the enemy path
                            //loading.Close();
                            ReadMap();
                            o = 0;
                            mapHasBeenSelected = false;
                            currentGameState = GameState.MainMenu;
                        }

                    }
                    mapHasBeenSelected = loading.MapSelected();
                    // checks where player puts the path
                    if (loadIsRunning == false && mapHasBeenSelected == true && mouseState.LeftButton == ButtonState.Pressed && o > 30)
                    {
                        foreach (Tile tl in tileArray)
                        {
                            if (new Rectangle(mouseState.X, mouseState.Y, 1, 1).Intersects(tl.TileRectangle) && tl.Occupied == false)
                            {
                                tl.Occupied = true;
                                pathTile = new PathTile(tl.TileRectangle.X, tl.TileRectangle.Y, pathBlockTexture);
                                pathTileArray[pathTracker] = pathTile;
                                pathTracker++;
                            }
                        }
                    }

                    if(loadIsRunning == false && mapHasBeenSelected == false && mapHasBeenRead == 0)
                    {
                        ReadMap();
                        mapHasBeenRead++;
                    }

                    o++;
                    break;
                case GameState.Help:
                    if (new Rectangle(mouseState.X, mouseState.Y, 1, 1).Intersects(mainMenuRec))
                    {
                        mainMenuButtonTexture = mainMenuHoverTexture;
                    }
                    else
                    {
                        mainMenuButtonTexture = defaultMainMenuButtonTexture;
                    }
                    // Quit Game button hover effect
                    if (new Rectangle(mouseState.X, mouseState.Y, 1, 1).Intersects(quitGame2Rec))
                    {
                        quitGameButtonTexture = quitGameHoverTexture;
                    }
                    else
                    {
                        quitGameButtonTexture = defaultQuitGameButtonTexture;
                    }

                    // Goes back to main menu
                    if (mouseState.LeftButton == ButtonState.Pressed && o > 30)
                    {
                        if (new Rectangle(mouseState.X, mouseState.Y, 1, 1).Intersects(mainMenuRec))
                        {
                            o = 0;
                            currentGameState = GameState.MainMenu;
                        }

                    }
                    // Quits game
                    if (mouseState.LeftButton == ButtonState.Pressed && o > 30)
                    {
                        if (new Rectangle(mouseState.X, mouseState.Y, 1, 1).Intersects(quitGame2Rec))
                        {
                            getName.Close();
                            Environment.Exit(1);

                        }


                    }
                    o++;
                    break;

                // Play Game
                case GameState.PlayGame:
                    // Allows the Player to enter a new name

                    if (player1.Name == "")
                    {
                        player1.Name = getName.GetName();
                    }

                    

                    // creates mouseRectangle to track where the mouse is
                    Rectangle mouseTracker = new Rectangle(mouseState.X, mouseState.Y, 1, 1);

                    // checks to see if the Player wants to click on the button to start the next wave (for hover effect)
                    if (mouseTracker.X >= (GraphicsDevice.Viewport.Width - 320) && mouseTracker.X <= (GraphicsDevice.Viewport.Width - 160) && mouseTracker.Y >= (GraphicsDevice.Viewport.Height - 160) && mouseTracker.Y <= (GraphicsDevice.Viewport.Height))
                    {
                        m = 1;
                    }
                    else
                    {
                        m = 0;
                    }

                    // checks to see if the Player wants to click on the button to End the game (for hover effect)
                    if (mouseTracker.X >= (GraphicsDevice.Viewport.Width - 160) && mouseTracker.X <= (GraphicsDevice.Viewport.Width) && mouseTracker.Y >= (GraphicsDevice.Viewport.Height - 160) && mouseTracker.Y <= (GraphicsDevice.Viewport.Height))
                    {
                        n = 1;
                    }
                    else
                    {
                        n = 0;
                    }

                    if (mouseTracker.X >= (GraphicsDevice.Viewport.Width - 320) && mouseTracker.X <= (GraphicsDevice.Viewport.Width - 240) && mouseTracker.Y >= 240 && mouseTracker.Y <= 320)
                    {
                        p = 1;
                    }
                    else
                    {
                        p = 0;
                    }
                    if (mouseTracker.X >= (GraphicsDevice.Viewport.Width - 320) && mouseTracker.X <= (GraphicsDevice.Viewport.Width - 240) && mouseTracker.Y >= 320 && mouseTracker.Y <= 400)
                    {
                        q = 1;
                    }
                    else
                    {
                        q = 0;
                    }
                    if (mouseTracker.X >= (GraphicsDevice.Viewport.Width - 160) && mouseTracker.X <= (GraphicsDevice.Viewport.Width- 80) && mouseTracker.Y >= 240 && mouseTracker.Y <= 320)
                    {
                        r = 1;
                    }
                    else
                    {
                        r = 0;
                    }
                    if (mouseTracker.X >= (GraphicsDevice.Viewport.Width - 160) && mouseTracker.X <= (GraphicsDevice.Viewport.Width) && mouseTracker.Y >= 320 && mouseTracker.Y <= 400)
                    {
                        s = 1;
                    }
                    else
                    {
                        s = 0;
                    }

                    // checks if the user clicked on the "End Game button"
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        if (new Rectangle(mouseState.X, mouseState.Y, 1, 1).Intersects(endGameRec))
                        {
                            // Reset enemies and towers
                            ResetGameEnemies();
                            ResetGameTowers();
                            o = 0;
                            // Read in the previous highscore
                            ReadScore();
                            // check to see if the Player's score is greater than the previous high score
                            // if it is, write the new highscore
                            if (player1.TotalScore >= hsScore)
                            {
                                WriteScore(player1.Name, player1.CurrentWave, player1.TotalScore);
                            }
                            // Read the Score again so the most correct/most recent highscore is displayed on the end game screen
                            ReadScore();
                            currentGameState = GameState.GameOver;
                        }

                    }

                    // checks if the user clicked on the Start Next Wave button
                    if (mouseState.LeftButton == ButtonState.Pressed && loadEnemiesOnce == 0)
                    {
                        if (new Rectangle(mouseState.X, mouseState.Y, 1, 1).Intersects(startWaveRec))
                        {
                            // Reset the enemies (creates more enemies each round)
                            ResetGameEnemies();
                            // makes sure Player can only start a wave once the previous wave is completed
                            if (onlyIncrementOnce == 0)
                            {
                                CalculateNumOfEnemies();
                                randomNumber = rgen.Next(1, 100);
                                CreatNewEnemy(enemyArrayNumTracker, randomNumber);
                                enemyArrayNumTracker++;
                                loadEnemiesOnce++;
                                onlyIncrementOnce++;
                                keepTowersOnScreen++;
                            }
                        }

                    }

                    // next 2 if statements check if player clicked on first tower (assume they want to place that tower)
                    if (mouseState.LeftButton == ButtonState.Pressed && loadEnemiesOnce > 0)
                    {
                        if (new Rectangle(mouseState.X, mouseState.Y, 1, 1).Intersects(towerPlaceHolderRec))
                        {
                            clickedOnTwr1++;
                        }
                    }
                    // checks if the user clicked on an empty, valid tile in order to place the tower
                    if (clickedOnTwr1 > 0 && player1.Money >= 50 && loadEnemiesOnce > 0)
                    {
                        if (mouseState.LeftButton == ButtonState.Pressed)
                        {
                            foreach (Tile tl in tileArray)
                            {
                                if (new Rectangle(mouseState.X, mouseState.Y, 1, 1).Intersects(tl.TileRectangle) && tl.Occupied == false)
                                {
                                    tl.Occupied = true;
                                    BasicTower newTower = new BasicTower(tl.TileRectangle.X, tl.TileRectangle.Y, 1, 120, 30, 50, towerPlaceHolderTexture, pulse);
                                    basicTowerArray[towerArrayNumTracker] = newTower;
                                    towerArrayNumTracker++;
                                    player1.Money -= 50;
                                    clickedOnTwr1 = 0;
                                }
                            }


                        }
                    }

                    // Checks if player clicks on LongRangetower
                    if (mouseState.LeftButton == ButtonState.Pressed && loadEnemiesOnce > 0)
                    {
                        if (new Rectangle(mouseState.X, mouseState.Y, 1, 1).Intersects(towerPlaceHolder2Rec))
                        {
                            clickedOnTwr2++;
                        }
                    }
                    if (clickedOnTwr2 > 0 && player1.Money >= 100 && loadEnemiesOnce > 0)
                    {
                        if (mouseState.LeftButton == ButtonState.Pressed)
                        {
                            foreach (Tile tl in tileArray)
                            {
                                if (new Rectangle(mouseState.X, mouseState.Y, 1, 1).Intersects(tl.TileRectangle) && tl.Occupied == false)
                                {
                                    tl.Occupied = true;
                                    LongRangeTower newTower = new LongRangeTower(tl.TileRectangle.X, tl.TileRectangle.Y, 8, 200, 45, 100, towerPlaceHolder2Texture, testBullet);
                                    longRangeTowerArray[towerArrayNumTracker] = newTower;
                                    towerArrayNumTracker++;
                                    player1.Money -= 100;
                                    clickedOnTwr2 = 0;
                                }
                            }
                        }
                    }

                    // Checks if player clicks on Missiletower
                    if (mouseState.LeftButton == ButtonState.Pressed && loadEnemiesOnce > 0)
                    {
                        if (new Rectangle(mouseState.X, mouseState.Y, 1, 1).Intersects(towerPlaceHolder3Rec))
                        {
                            clickedOnTwr3++;
                        }
                    }
                    if (clickedOnTwr3 > 0 && player1.Money >= 150 && loadEnemiesOnce > 0)
                    {
                        if (mouseState.LeftButton == ButtonState.Pressed)
                        {
                            foreach (Tile tl in tileArray)
                            {
                                if (new Rectangle(mouseState.X, mouseState.Y, 1, 1).Intersects(tl.TileRectangle) && tl.Occupied == false)
                                {
                                    tl.Occupied = true;
                                    MissileTower newTower = new MissileTower(tl.TileRectangle.X, tl.TileRectangle.Y, 30, 200, 60, 75, towerPlaceHolder3Texture, missile);
                                    missileTowerArray[towerArrayNumTracker] = newTower;
                                    towerArrayNumTracker++;
                                    player1.Money -= 150;
                                    clickedOnTwr3 = 0;
                                }
                            }
                        }
                    }

                    // Checks if player clicks on SuperTower
                    if (mouseState.LeftButton == ButtonState.Pressed && loadEnemiesOnce > 0)
                    {
                        if (new Rectangle(mouseState.X, mouseState.Y, 1, 1).Intersects(superTowerRec))
                        {
                            clickedOnTwr4++;
                        }
                    }
                    if (clickedOnTwr4 > 0 && player1.Money >= 500 && loadEnemiesOnce > 0)
                    {
                        if (mouseState.LeftButton == ButtonState.Pressed)
                        {
                            foreach (Tile tl in tileArray)
                            {
                                if (new Rectangle(mouseState.X, mouseState.Y, 1, 1).Intersects(tl.TileRectangle) && tl.Occupied == false)
                                {
                                    tl.Occupied = true;
                                    SuperTower newTower = new SuperTower(tl.TileRectangle.X, tl.TileRectangle.Y, 5, 400, 15, 500, superTowerTexture, testBullet);
                                    superTowerArray[towerArrayNumTracker] = newTower;
                                    towerArrayNumTracker++;
                                    player1.Money -= 500;
                                    clickedOnTwr4 = 0;
                                }
                            }
                        }
                    }

                    // Moves the enemies along the path
                    if (loadEnemiesOnce > 0 && enemyTimer >= 1)
                    {
                        foreach (Enemy en in enemyArray)
                        {
                            if (en != null && en.Active == true && en.FirstNextMoveFound == false)
                            {
                                FindNextMove(en.NextXTrackerNum, en.NextYTrackerNum, en);
                                en.NextXTrackerNum++;
                                en.NextYTrackerNum++;
                                en.FirstNextMoveFound = true;
                            }
                        }
                        
                        // moves the enemy in the direction of its next coordinates
                        // Move all enemies enemies
                        foreach(Enemy en in enemyArray)
                        {
                            if (en != null && en.Active == true)
                            {
                                en.Move();
                                if (en.enemyRectangle.X == en.NextMoveX && en.enemyRectangle.Y == en.NextMoveY)
                                {
                                    en.EnemyX = en.enemyRectangle.X / 80;
                                    FindNextMove(en.NextXTrackerNum, en.NextYTrackerNum, en);
                                    en.NextXTrackerNum++;
                                    en.NextYTrackerNum++;
                                }
                            }
                        }
                        
                        if (waveNumber < 10)
                        {
                            if (enemyTimerT > 45 && enemyArrayNumTracker < numberOfEnemies)
                            {
                                randomNumber = rgen.Next(1, 100);
                                CreatNewEnemy(enemyArrayNumTracker, randomNumber);
                                enemyArrayNumTracker++;
                                enemyTimerT = 0;
                            }
                            enemyTimer = 0;
                        }
                        else if (waveNumber < 25 && waveNumber >= 10)
                        {
                            if (enemyTimerT > 30 && enemyArrayNumTracker < numberOfEnemies)
                            {
                                randomNumber = rgen.Next(1, 100);
                                CreatNewEnemy(enemyArrayNumTracker, randomNumber);
                                enemyArrayNumTracker++;
                                enemyTimerT = 0;
                            }
                            enemyTimer = 0;
                        }
                        else
                        {
                            if (enemyTimerT > 15 && enemyArrayNumTracker < numberOfEnemies)
                            {
                                randomNumber = rgen.Next(1, 100);
                                CreatNewEnemy(enemyArrayNumTracker, randomNumber);
                                enemyArrayNumTracker++;
                                enemyTimerT = 0;
                            }
                            enemyTimer = 0;
                        }
                        // creates a new enemy every second
                        
                    }
                    // checks to see if any of the enemies have reached the end of the path
                    if (loadEnemiesOnce > 0)
                    {
                        LoseLives();
                    }
                    // checks to see if there are no enemies left, in which case the wave was completed
                    if (loadEnemiesOnce > 0 && numberOfEnemies == (player1.LevelScore + numOfEnemiesReachedEnd))
                    {
                        NextLevel();
                    }
                    enemyTimer++;
                    enemyTimerT++;

                    // Increment Tower's cooldown attribute (tells when the tower can fire)
                    if (loadEnemiesOnce > 0)
                    {
                        foreach (BasicTower bTwr in basicTowerArray)
                        {
                            if (bTwr != null)
                            {
                                bTwr.CoolDown++;
                            }

                        }

                        foreach (LongRangeTower lrTwr in longRangeTowerArray)
                        {
                            if (lrTwr != null)
                            {
                                lrTwr.CoolDown++;
                            }
                        }
                        foreach (MissileTower mTwr in missileTowerArray)
                        {
                            if (mTwr != null)
                            {
                                mTwr.CoolDown++;
                            }
                        }
                        foreach (SuperTower sTwr in superTowerArray)
                        {
                            if (sTwr != null)
                            {
                                sTwr.CoolDown++;
                            }
                        }

                    }

                    // have all eligible towers attack
                    if (loadEnemiesOnce > 0)
                    {
                        Attack();
                    }

                    // Ends the game if the player's lives reach 0
                    if (player1.NumberOfLives <= 0)
                    {
                        // Reset enemies and towers
                        ResetGameEnemies();
                        ResetGameTowers();
                        o = 0;
                        // Read in the previous highscore
                        ReadScore();
                        // check to see if the Player's score is greater than the previous high score
                        // if it is, write the new highscore
                        if (player1.TotalScore >= hsScore)
                        {
                            WriteScore(player1.Name, player1.CurrentWave, player1.TotalScore);
                        }
                        // Read the Score again so the most correct/most recent highscore is displayed on the end game screen
                        ReadScore();
                        currentGameState = GameState.GameOver;
                    }
                    break;
                // Game Over
                case GameState.GameOver:
                    // Main Menu button hover effect
                    if (new Rectangle(mouseState.X, mouseState.Y, 1, 1).Intersects(mainMenuRec))
                    {
                        mainMenuButtonTexture = mainMenuHoverTexture;
                    }
                    else
                    {
                        mainMenuButtonTexture = defaultMainMenuButtonTexture;
                    }
                    // Quit Game button hover effect
                    if (new Rectangle(mouseState.X, mouseState.Y, 1, 1).Intersects(quitGame2Rec))
                    {
                        quitGameButtonTexture = quitGameHoverTexture;
                    }
                    else
                    {
                        quitGameButtonTexture = defaultQuitGameButtonTexture;
                    }

                    // Goes back to main menu
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        if (new Rectangle(mouseState.X, mouseState.Y, 1, 1).Intersects(mainMenuRec))
                        {
                            o = 0;
                            currentMap = "mapFile.txt";
                            currentGameState = GameState.MainMenu;
                        }

                    }
                    // Quits game
                    if (mouseState.LeftButton == ButtonState.Pressed && o > 50)
                    {
                        if (new Rectangle(mouseState.X, mouseState.Y, 1, 1).Intersects(quitGame2Rec))
                        {
                            getName.Close();
                            Environment.Exit(1);

                        }
                    }

                    // Prevents quit game from firing imediately upon entering GameOver gamestate
                    o++;
                    break;
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);

            spriteBatch.Begin();

            // set up different Rectangles
            screenRec = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            pressEnterRec = new Rectangle((GraphicsDevice.Viewport.Width / 2) - 343, GraphicsDevice.Viewport.Height - 125, 700, 100);
            playGameRec = new Rectangle(30, 200, 600, 200);
            quitGame1Rec = new Rectangle(675, 200, 600, 200);
            helpRec = new Rectangle(675, 450, 600, 200);
            mapButtonRec = new Rectangle(30, 450, 600, 200);
            mainMenuRec = new Rectangle(33, 550, 550, 150);
            quitGame2Rec = new Rectangle(700, 550, 550, 150);
            enemyPathRec = new Rectangle(0, 0, 80, 80);
            subRectangle = new Rectangle(0, 0, 80, 80);
            bigBoy = new Rectangle(0, 0, 80, 60);
            startWaveRec = new Rectangle(GraphicsDevice.Viewport.Width - 321, GraphicsDevice.Viewport.Height - 161, 160, 160);
            endGameRec = new Rectangle(GraphicsDevice.Viewport.Width - 161, GraphicsDevice.Viewport.Height - 161, 160, 160);
            towerPlaceHolderRec = new Rectangle(GraphicsDevice.Viewport.Width - 321, 240, 80, 80);
            towerPlaceHolder2Rec = new Rectangle(GraphicsDevice.Viewport.Width - 321, 320, 80, 80);
            pulseRec = new Rectangle(640, 640, 240, 240);
            towerPlaceHolder3Rec = new Rectangle(GraphicsDevice.Viewport.Width - 161, 240, 80, 80);
            finishedButtonRec = new Rectangle(GraphicsDevice.Viewport.Width - 321, GraphicsDevice.Viewport.Height - 161, 320, 160);
            superTowerRec = new Rectangle(GraphicsDevice.Viewport.Width - 161, 320, 80, 80);
            tower1HoverRec = new Rectangle(GraphicsDevice.Viewport.Width - 321, 240, 80, 80);
            tower2HoverRec = new Rectangle(GraphicsDevice.Viewport.Width - 321, 320, 80, 80);
            tower3HoverRec = new Rectangle(GraphicsDevice.Viewport.Width - 161, 240, 80, 80);
            tower4HoverRec = new Rectangle(GraphicsDevice.Viewport.Width - 161, 320, 80, 80);



            // if statements to track current gamestate
            if (currentGameState == GameState.TitleScreen)
            {

                spriteBatch.Draw(titleScreenTexture, screenRec, Color.White);
                // makes 'Press enter' blink
                if (timer >= 0)
                {
                    spriteBatch.Draw(pressEnterTexture, pressEnterRec, Color.White);
                }
                else
                {
                    spriteBatch.Draw(pressEnterBlinkTexture, pressEnterRec, Color.White);
                }

            }
            if (currentGameState == GameState.MainMenu)
            {
                // draws background texture and different buttons
                spriteBatch.Draw(mainMenuTexture, screenRec, Color.White);
                spriteBatch.Draw(playGameButtonTexture, playGameRec, Color.White);
                spriteBatch.Draw(quitGameButtonTexture, quitGame1Rec, Color.White);
                spriteBatch.Draw(mapButtonTexture, mapButtonRec, Color.White);
                spriteBatch.Draw(helpButtonTexture, helpRec, Color.White);


            }

            if (currentGameState == GameState.Map)
            {
                int XX = GraphicsDevice.Viewport.Width;

                

                spriteBatch.Draw(mapSelectionScreenTexture, screenRec, Color.White);

                // instructions for map creation and loading
                spriteBatch.DrawString(mySpriteFont4, "Instructions:", new Vector2(XX - 265, 5), Color.Black);

                spriteBatch.DrawString(mySpriteFont4, "To load an existing \nmap file, type it \nexactly as shown \nincluding the '.txt' \nat the end of \nthe file name.", new Vector2(XX - 320, 45), Color.White);

                spriteBatch.DrawString(mySpriteFont4, "To create a new \nmap file, give it a \ntitle in the \nspecified text box \nand then click or \nclick-drag on the \nintended path, \nand hit finish. \nWhen you click the 'map' \nbutton again, your \nmap will be \navailable in the \nload maps box.", new Vector2(XX - 320, 210), Color.White);

                if (finishedButtonTracker > 0)
                {
                    spriteBatch.Draw(finishedButtonTexture, finishedButtonRec, Color.White);
                }
                if (mapHasBeenSelected == true)
                foreach (PathTile pt in pathTileArray)
                    {
                        {
                            if (pt != null)
                            {
                                spriteBatch.Draw(pt.TileTexture, pt.TileRectangle, Color.White);
                            }
                        }
                    }
                
                if (loadIsRunning == false && mapHasBeenSelected == false)
                {
                    for (int i = 0; i < 9; i++)
                    {
                        for (int j = 0; j < 12; j++)
                        {
                            spriteBatch.Draw(tileArray[i, j].TileTexture, tileArray[i, j].TileRectangle, Color.White);
                        }
                    }
                }
            }

            if (currentGameState == GameState.Help)
            {
                spriteBatch.Draw(mainMenuTexture, screenRec, Color.White);
                spriteBatch.Draw(mainMenuButtonTexture, mainMenuRec, Color.White);
                spriteBatch.Draw(quitGameButtonTexture, quitGame2Rec, Color.White);
                spriteBatch.DrawString(mySpriteFont3, "Objective:", new Vector2(25, 150), Color.Black);
                spriteBatch.DrawString(mySpriteFont4, "Place ships in the open water to attack oncoming enemy\nsubmarines.  The number of enemies increases after each\nsuccessfully completed wave.  Your metal will increase\nwhen you destroy a submarine, as well as at the end of\neach wave.", new Vector2(290, 150), Color.LightGray);
                spriteBatch.DrawString(mySpriteFont3, "Instructions:", new Vector2(25, 350), Color.Black);
                spriteBatch.DrawString(mySpriteFont4, "First, click on the 'Start next wave' button to begin\na wave.  Once a wave has begun, you can click on a\nship on the side menu to select that ship.  Then, click\nagain on an unoccupied 'light blue' square to place\nthat ship.  You must re-click on a ship on the menu\neach time you want to place a new ship.", new Vector2(360, 350), Color.LightGray);

                //spriteBatch.DrawString(mySpriteFont2, "50", new Vector2(1090, 247), Color.Black);
            }
            if (currentGameState == GameState.PlayGame)
            {
                spriteBatch.Draw(gameTexture, screenRec, Color.White);
                

                // draws start next wave hover texture
                if (m == 1 && loadEnemiesOnce == 0)
                {
                    spriteBatch.Draw(startNextWaveHoverTexture, startWaveRec, Color.White);
                }
                else if (loadEnemiesOnce > 0)
                {
                    spriteBatch.Draw(startNextWaveHoverTexture, startWaveRec, Color.Gray);
                }
                // draws quit game hover texture
                if (n == 1)
                {
                    spriteBatch.Draw(endGameHoverTexture, endGameRec, Color.White);
                }
                if (p == 1 && loadEnemiesOnce > 0)
                {
                    spriteBatch.Draw(towerHoverTexture, tower1HoverRec, Color.White); ;
                }
                if (q == 1 && loadEnemiesOnce > 0)
                {
                    spriteBatch.Draw(towerHoverTexture, tower2HoverRec, Color.White); ;
                }
                if (r == 1 && loadEnemiesOnce > 0)
                {
                    spriteBatch.Draw(towerHoverTexture, tower3HoverRec, Color.White); ;
                }
                if (s == 1 && loadEnemiesOnce > 0)
                {
                    spriteBatch.Draw(towerHoverTexture, tower4HoverRec, Color.White); ;
                }

                // Display Towers to choose from
                spriteBatch.Draw(towerPlaceHolderTexture, towerPlaceHolderRec, Color.White);
                spriteBatch.Draw(towerPlaceHolder2Texture, towerPlaceHolder2Rec, Color.White);
                spriteBatch.Draw(towerPlaceHolder3Texture, towerPlaceHolder3Rec, Color.White);
                spriteBatch.Draw(superTowerTexture, superTowerRec, Color.White);

                //Draw the 2d Array of tiles
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 12; j++)
                    {
                        spriteBatch.Draw(tileArray[i, j].TileTexture, tileArray[i, j].TileRectangle, Color.White);
                    }
                }

                // Draw all enemies
                if (loadEnemiesOnce > 0)
                {
                    for (int i = 0; i < enemyArray.Length; i++)
                    {
                        if (enemyArray[i] != null)
                        {
                            if (enemyArray[i].Active != false )
                            {
                                
                                if (enemyArray[i].FirstNextMoveFound == true)
                                {

                                    enemyArray[i].GetDirection();
                                    int enHealth = enemyArray[i].GetHealth();
                                    int enOriginalHealth = enemyArray[i].GetOriginalHealth();

                                    // If enemy's health is less 25% of original health, change enemy's color to red
                                    if (enHealth < (enOriginalHealth / 4))
                                    {
                                        switch (enemyArray[i].EnemyDirection)
                                        {
                                            // right
                                            case 1:
                                                spriteBatch.Draw(enemyArray[i].EnemyTexture, enemyArray[i].EnemyRectangle, null, Color.Red, (float)Math.PI, rotationVector, SpriteEffects.None, 0.0f);
                                                break;

                                            // down
                                            case 2:
                                                spriteBatch.Draw(enemyArray[i].EnemyTexture, enemyArray[i].EnemyRectangle, null, Color.Red, (float)(Math.PI * (3.0 / 2.0)), rotationVector, SpriteEffects.None, 0.0f);
                                                break;

                                            // left
                                            case 3:
                                                spriteBatch.Draw(enemyArray[i].EnemyTexture, enemyArray[i].EnemyRectangle, null, Color.Red, 0.0f, rotationVector, SpriteEffects.None, 0.0f);
                                                break;

                                            // up
                                            case 4:
                                                spriteBatch.Draw(enemyArray[i].EnemyTexture, enemyArray[i].EnemyRectangle, null, Color.Red, (float)Math.PI / 2, rotationVector, SpriteEffects.None, 0.0f);
                                                break;
                                            default:
                                                spriteBatch.Draw(enemyArray[i].EnemyTexture, enemyArray[i].EnemyRectangle, null, Color.White, (float)Math.PI * (3 / 2), rotationVector, SpriteEffects.None, 0.0f);
                                                break;

                                        }
                                    } 
                                    else
                                    {
                                        switch (enemyArray[i].EnemyDirection)
                                        {
                                            // right
                                            case 1:
                                                spriteBatch.Draw(enemyArray[i].EnemyTexture, enemyArray[i].EnemyRectangle, null, Color.White, (float)Math.PI, rotationVector, SpriteEffects.None, 0.0f);
                                                break;

                                            // down
                                            case 2:
                                                spriteBatch.Draw(enemyArray[i].EnemyTexture, enemyArray[i].EnemyRectangle, null, Color.White, (float)(Math.PI * (3.0 / 2.0)), rotationVector, SpriteEffects.None, 0.0f);
                                                break;

                                            // left
                                            case 3:
                                                spriteBatch.Draw(enemyArray[i].EnemyTexture, enemyArray[i].EnemyRectangle, null, Color.White, 0.0f, rotationVector, SpriteEffects.None, 0.0f);
                                                break;

                                            // up
                                            case 4:
                                                spriteBatch.Draw(enemyArray[i].EnemyTexture, enemyArray[i].EnemyRectangle, null, Color.White, (float)Math.PI / 2, rotationVector, SpriteEffects.None, 0.0f);
                                                break;
                                            default:
                                                spriteBatch.Draw(enemyArray[i].EnemyTexture, enemyArray[i].EnemyRectangle, null, Color.White, (float)Math.PI * (3 / 2), rotationVector, SpriteEffects.None, 0.0f);
                                                break;

                                        }
                                    }                                   
                                    
                                }
                                else
                                {
                                    spriteBatch.Draw(enemyArray[i].EnemyTexture, enemyArray[i].EnemyRectangle, null, Color.White, (float)(Math.PI * (3.0 / 2.0)), rotationVector, SpriteEffects.None, 0.0f);
                                }
                                
                                
                            }
                        }
                    }
                }
                   
                // Draw all towers
                if (keepTowersOnScreen > 0)
                {
                    // Draw Basic towers
                    foreach (BasicTower btwr in basicTowerArray)
                    {
                        if (btwr != null)
                        {
                            if (btwr.IsAttacking == true)
                            {
                                // Draw the Basic Tower's pulse texture
                                if (btwr.AttackTracker1 < 240)
                                {
                                    btwr.AttackRectangle = new Rectangle((btwr.TowerX +20 ) - btwr.AttackTracker2, (btwr.TowerY + 20) - btwr.AttackTracker2, btwr.AttackTracker1, btwr.AttackTracker1);
                                    btwr.AttackTracker1 += 5;
                                    btwr.AttackTracker2 += 2;
                                }
                                if (btwr.AttackTracker1 >= 240)
                                {
                                    btwr.IsAttacking = false; ;
                                    btwr.AttackTracker1 = 0;
                                    btwr.AttackTracker2 = 0;
                                    btwr.AttackRectangle = new Rectangle(btwr.TowerX, btwr.TowerY, 0, 0);

                                }
                            }
                            spriteBatch.Draw(btwr.AttackTexture, btwr.AttackRectangle, Color.White);
                            spriteBatch.Draw(btwr.TowerTexture, btwr.TowerRec, Color.White);
                        }
                    }

                    // Draw Long Range Towers
                    foreach (LongRangeTower ltwr in longRangeTowerArray)
                    {
                        if (ltwr != null)
                        {
                            if (ltwr.IsAttacking == true)
                            {
                                // Draw Long Range Tower's bullet
                                if (Math.Abs(ltwr.AttackTracker1) < ltwr.AttackRadius && Math.Abs(ltwr.AttackTracker2) < ltwr.AttackRadius)
                                {                         
                                    ltwr.AttackRectangle = new Rectangle((ltwr.TowerX + 32) - ltwr.AttackTracker1, (ltwr.TowerY + 32) - ltwr.AttackTracker2, 16, 16);
                                    ltwr.AttackTracker1 += ltwr.AttackTrackerX;
                                    ltwr.AttackTracker2 += ltwr.AttackTrackerY;
                                }
                                else
                                {
                                    ltwr.IsAttacking = false;
                                    ltwr.AttackTracker1 = 0;
                                    ltwr.AttackTracker2 = 0;
                                    ltwr.AttackTrackerX = 1;
                                    ltwr.AttackTrackerY = 1;
                                    ltwr.AttackRectangle = new Rectangle(ltwr.TowerX+32, ltwr.TowerY+32, 0, 0);
                                    ltwr.IsTracking = false;
                                }
                                    
                            }
                            spriteBatch.Draw(ltwr.AttackTexture, ltwr.AttackRectangle, Color.White);
                            spriteBatch.Draw(ltwr.TowerTexture, ltwr.TowerRec, Color.White);
                        }
                    }

                    // Draw Missle Towers
                    foreach (MissileTower mtwr in missileTowerArray)
                    {
                        if (mtwr != null)
                        {
                            if (mtwr.IsAttacking == true)
                            {
                                // Draw Long Range Tower's bullet
                                if (Math.Abs(mtwr.AttackTracker1) < mtwr.AttackRadius && Math.Abs(mtwr.AttackTracker2) < mtwr.AttackRadius)
                                {
                                    mtwr.AttackRectangle = new Rectangle((mtwr.TowerX + 32) - mtwr.AttackTracker1, (mtwr.TowerY + 32) - mtwr.AttackTracker2, 24, 24);
                                    mtwr.AttackTracker1 += mtwr.AttackTrackerX;
                                    mtwr.AttackTracker2 += mtwr.AttackTrackerY;
                                }
                                else
                                {
                                    mtwr.IsAttacking = false;
                                    mtwr.AttackTracker1 = 0;
                                    mtwr.AttackTracker2 = 0;
                                    mtwr.AttackTrackerX = 1;
                                    mtwr.AttackTrackerY = 1;
                                    mtwr.AttackRectangle = new Rectangle(mtwr.TowerX + 32, mtwr.TowerY + 32, 0, 0);
                                    mtwr.IsTracking = false;
                                }

                            }
                            spriteBatch.Draw(mtwr.AttackTexture, mtwr.AttackRectangle, Color.White);
                            spriteBatch.Draw(mtwr.TowerTexture, mtwr.TowerRec, Color.White);
                        }
                    }

                    // Draw all Super Towers
                    foreach (SuperTower stwr in superTowerArray)
                    {
                        if (stwr != null)
                        {
                            if (stwr.IsAttacking == true)
                            {
                                // Draw Long Range Tower's bullet
                                if (Math.Abs(stwr.AttackTracker1) < stwr.AttackRadius && Math.Abs(stwr.AttackTracker2) < stwr.AttackRadius)
                                {
                                    stwr.AttackRectangle = new Rectangle((stwr.TowerX + 32) - stwr.AttackTracker1, (stwr.TowerY + 32) - stwr.AttackTracker2, 16, 16);
                                    stwr.AttackTracker1 += stwr.AttackTrackerX;
                                    stwr.AttackTracker2 += stwr.AttackTrackerY;
                                }
                                else
                                {
                                    stwr.IsAttacking = false;
                                    stwr.AttackTracker1 = 0;
                                    stwr.AttackTracker2 = 0;
                                    stwr.AttackTrackerX = 1;
                                    stwr.AttackTrackerY = 1;
                                    stwr.AttackRectangle = new Rectangle(stwr.TowerX + 32, stwr.TowerY + 32, 0, 0);
                                    stwr.IsTracking = false;
                                }

                            }
                            spriteBatch.Draw(stwr.AttackTexture, stwr.AttackRectangle, Color.White);
                            spriteBatch.Draw(stwr.TowerTexture, stwr.TowerRec, Color.White);
                        }
                    }

                }

                // Draw different Player and Tower information on the menu
                spriteBatch.DrawString(mySpriteFont, "" + player1.Name, new Vector2(1134, 59), Color.Black, 0, new Vector2(0, 0), .95f, 0, 0);
                spriteBatch.DrawString(mySpriteFont, "" + player1.CurrentWave, new Vector2(1134, 93), Color.Black);
                spriteBatch.DrawString(mySpriteFont, "" + player1.NumberOfLives, new Vector2(1170, 127), Color.Black);
                spriteBatch.DrawString(mySpriteFont, "" + player1.TotalScore, new Vector2(1125, 161), Color.Black);
                spriteBatch.DrawString(mySpriteFont, "" + player1.Money, new Vector2(1056, 195), Color.Black);
                spriteBatch.DrawString(mySpriteFont2, "50", new Vector2(1090, 247), Color.Black);
                spriteBatch.DrawString(mySpriteFont2, "1", new Vector2(1096, 278), Color.Black);
                spriteBatch.DrawString(mySpriteFont2, "1", new Vector2(1096, 308), Color.Black);
                spriteBatch.DrawString(mySpriteFont2, "100", new Vector2(1090, 329), Color.Black);
                spriteBatch.DrawString(mySpriteFont2, "1", new Vector2(1096, 359), Color.Black);
                spriteBatch.DrawString(mySpriteFont2, "2", new Vector2(1096, 389), Color.Black);
                spriteBatch.DrawString(mySpriteFont2, "150", new Vector2(1250, 247), Color.Black);
                spriteBatch.DrawString(mySpriteFont2, "2", new Vector2(1256, 278), Color.Black);
                spriteBatch.DrawString(mySpriteFont2, "2", new Vector2(1256, 308), Color.Black);
                spriteBatch.DrawString(mySpriteFont2, "500", new Vector2(1250, 329), Color.Black);
                spriteBatch.DrawString(mySpriteFont2, "2", new Vector2(1256, 359), Color.Black);
                spriteBatch.DrawString(mySpriteFont2, "3", new Vector2(1256, 389), Color.Black);

            }

            if (currentGameState == GameState.GameOver)
            {
                // Draw Player score information and Highscore information
                spriteBatch.Draw(gameOverTexture, screenRec, Color.White);
                spriteBatch.Draw(mainMenuButtonTexture, mainMenuRec, Color.White);
                spriteBatch.Draw(quitGameButtonTexture, quitGame2Rec, Color.White);
                spriteBatch.DrawString(mySpriteFont3, "" + player1.Name, new Vector2(412, 366), Color.Black, 0, new Vector2(0, 0), 0.80f, 0, 0);
                spriteBatch.DrawString(mySpriteFont3, "" + player1.CurrentWave, new Vector2(448, 434), Color.Black);
                spriteBatch.DrawString(mySpriteFont3, "" + player1.TotalScore, new Vector2(393, 508), Color.Black);
                spriteBatch.DrawString(mySpriteFont3, "" + hsName, new Vector2(1068, 366), Color.Black, 0, new Vector2(0, 0), 0.80f, 0, 0);
                spriteBatch.DrawString(mySpriteFont3, "" + hsWave, new Vector2(1110, 434), Color.Black);
                spriteBatch.DrawString(mySpriteFont3, "" + hsScore, new Vector2(1050, 508), Color.Black);
            }
            spriteBatch.End();

        }

        //ReadMap method
        public void ReadMap()
        {
            //This is where the map reading code should go. It should read the map file and then change the tileArray at those coordinates to be a PathTile object. the tileX and tileY should be ((40 + (the second coordinate of the tile in the array * 80)),(40 + (the first coordinate of the tile in the array * 80))
            //it should do this for each of the coordinates of the map file. 

            // we are changing the way we read the map file. x and y pixel coordinates are being read in and looping to create an array of path tile objects that each have the passed x and y values
            StreamReader reader = null;
            StreamReader reader2 = null;
            string line;
            string[] locationArray;
            int i = 0;
            int sizeTracker1 = 0;
            int sizeTracker2 = 0;

            try
            {
                reader = new StreamReader(currentMap);

                while ((line = reader.ReadLine()) != null)
                {
                    locationArray = line.Split(',');

                    int y = int.Parse(locationArray[0]);
                    int x = int.Parse(locationArray[1]);
                    pathTile = new PathTile((80 * x), (80 * y), pathBlockTexture);
                    tileArray[y, x] = pathTile;
                    i++;
                    sizeTracker1++;
                }

                reader2 = new StreamReader(currentMap);
                pathTileX = new int[sizeTracker1];
                pathTileY = new int[sizeTracker1];

                while ((line = reader2.ReadLine()) != null)
                {

                    locationArray = line.Split(',');

                    int y = int.Parse(locationArray[0]);
                    int x = int.Parse(locationArray[1]);
                    pathTileX[sizeTracker2] = x;
                    pathTileY[sizeTracker2] = y;
                    i++;
                    sizeTracker2++;
                }
            }
            catch
            {

            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }

        // Writes a new map into a text File if a map is created in the map menu
        protected void WriteMap()
        {
            StreamWriter writer = null;

            try
            {
                writer = new StreamWriter(currentMap);               
                foreach (PathTile pt in pathTileArray)
                {                                       
                    writer.WriteLine(pt.TileRectangle.Y / 80 + "," + pt.TileRectangle.X / 80);
                                      
                }

            }
            catch
            {
                
            }
            finally
            {
                
                if (writer != null)
                {
                    writer.Close();
                }
                
            }

        }

        // SingleKeyPress method
        protected Boolean SingleKeyPress(Keys k)
        {
            //kbState = Keyboard.GetState();
            if (kbState.IsKeyDown(k) && previousKbState.IsKeyUp(k))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Calculate the number of enemies that should spawn based on the round number and set the tower and enemy arrays
        protected int CalculateNumOfEnemies()
        {
            foreach (Tile tl in tileArray)
            {
                if (tl is EmptyTile)
                {
                    numOfTowerSpaces++;
                }
            }
            if (keepTowersOnScreen == 0)
            {
                basicTowerArray = new BasicTower[numOfTowerSpaces];
                longRangeTowerArray = new LongRangeTower[numOfTowerSpaces];
                missileTowerArray = new MissileTower[numOfTowerSpaces];
                superTowerArray = new SuperTower[numOfTowerSpaces];
            }
            numberOfEnemies = (waveNumber * 3) + 3;
            enemyArray = new Enemy[numberOfEnemies];
            return numberOfEnemies;
        }

        // Create new enemies every ___ based on a random number generator
        protected void CreatNewEnemy(int i, int ranNum)
        {   
            if (waveNumber < 10)
            {
                if (ranNum < 50)
                {
                    BasicEnemy newenemy1 = new BasicEnemy(pathTileX[0], pathTileY[0], subPlaceHolderTexture, subRectangle, waveNumber);
                    newenemy1.Active = true;
                    enemyArray[i] = newenemy1;
                    return;
                }
                if (ranNum >= 50 & ranNum < 75)
                {
                    FastEnemy newenemy2 = new FastEnemy(pathTileX[0], pathTileY[0], subPlaceHolder2Texture, subRectangle, waveNumber);
                    newenemy2.Active = true;
                    enemyArray[i] = newenemy2;
                    return;
                }
                if (ranNum >= 75 & ranNum < 95)
                {
                    HeavyEnemy newenemy3 = new HeavyEnemy(pathTileX[0], pathTileY[0], subPlaceHolder3Texture, subRectangle, waveNumber);
                    newenemy3.Active = true;
                    enemyArray[i] = newenemy3;
                    return;
                }
                if (ranNum >= 95)
                {
                    BigBoyEnemy newenemy4 = new BigBoyEnemy(pathTileX[0], pathTileY[0], subPlaceHolder4Texture, subRectangle, waveNumber);
                    newenemy4.Active = true;
                    enemyArray[i] = newenemy4;
                    return;
                }
            }
            else
            {
                if (ranNum < 30)
                {
                    BasicEnemy newenemy1 = new BasicEnemy(pathTileX[0], pathTileY[0], subPlaceHolderTexture, subRectangle, waveNumber);
                    newenemy1.Active = true;
                    enemyArray[i] = newenemy1;
                    return;
                }
                if (ranNum >= 30 & ranNum < 60)
                {
                    FastEnemy newenemy2 = new FastEnemy(pathTileX[0], pathTileY[0], subPlaceHolder2Texture, subRectangle, waveNumber);
                    newenemy2.Active = true;
                    enemyArray[i] = newenemy2;
                    return;
                }
                if (ranNum >= 60 & ranNum < 85)
                {
                    HeavyEnemy newenemy3 = new HeavyEnemy(pathTileX[0], pathTileY[0], subPlaceHolder3Texture, subRectangle, waveNumber);
                    newenemy3.Active = true;
                    enemyArray[i] = newenemy3;
                    return;
                }
                if (ranNum >= 85)
                {
                    BigBoyEnemy newenemy4 = new BigBoyEnemy(pathTileX[0], pathTileY[0], subPlaceHolder4Texture, subRectangle, waveNumber);
                    newenemy4.Active = true;
                    enemyArray[i] = newenemy4;
                    return;
                }
            }
        }

        // Find the enemies next move based on its property which contains the index of the PathTile value it needs
        protected void FindNextMove(int xMove, int yMove, Enemy en)
        {
            //////////
            if (xMove < pathTileX.Length && yMove < pathTileY.Length)
            {
                en.NextMove((pathTileX[xMove] * 80) + 40, (pathTileY[yMove] * 80)+40);
                en.FirstNextMoveFound = true;
            }
            else
            {
                en.ReachedEnd = true;
                en.Active = false;
            }
        }
        
        // Reset Game Method
        public void ResetGameEnemies()
        {
            enemyTimer = 0;
            enemyTimerT = 0;
            onlyIncrementOnce = 0;
            enemyArrayNumTracker = 0;
            loadEnemiesOnce = 0;

        }

        public void ResetGameTowers()
        {
            towerArrayNumTracker = 0;
            keepTowersOnScreen = 0;
            waveNumber = 1;
            foreach (Tile empTl in tileArray)
            {
                if (empTl is EmptyTile)
                {
                    empTl.Occupied = false;
                }
            }
        }

        // Next Level Method
        public void NextLevel()
        {
            ResetGameEnemies();
            player1.Money += 25;
            player1.TotalScore += waveNumber * 100;
            player1.CurrentWave++;
            waveNumber++;
            loadEnemiesOnce = 0;
            numOfEnemiesReachedEnd = 0;
            player1.LevelScore = 0;
            foreach (BasicTower bTwr in basicTowerArray)
            {
                if (bTwr != null && bTwr.Active == true)
                {
                    bTwr.CoolDown = 0;
                }
            }
            foreach (LongRangeTower ltwr in longRangeTowerArray)
            {
                if (ltwr != null && ltwr.Active == true)
                {
                    ltwr.CoolDown = 0;
                }
            }
            foreach (MissileTower mTwr in missileTowerArray)
            {
                if (mTwr != null && mTwr.Active == true)
                {
                    mTwr.CoolDown = 0;
                }
            }
            foreach (SuperTower sTwr in superTowerArray)
            {
                if (sTwr != null && sTwr.Active == true)
                {
                    sTwr.CoolDown = 0;
                }
            }
        }

        public void LoseLives()
        {
            foreach (Enemy en in enemyArray)
            {
                if (en != null && en.NextXTrackerNum > pathTileX.Length && en.NextYTrackerNum > pathTileY.Length && en.ReachedEnd == true)
                {
                    player1.NumberOfLives--;
                    en.ReachedEnd = false;
                    numOfEnemiesReachedEnd++;
                }
            }
        }

        // Method to have enemies attack
        public void Attack()
        {
            foreach (BasicTower bTwr in basicTowerArray)
            {
                if (bTwr != null && bTwr.Active == true)
                {
                    foreach (Enemy en in enemyArray)
                    {
                        if (en != null && en.Active == true && en.EnemyRectangle.Intersects(bTwr.RadiusRec) && bTwr.CoolDown >= bTwr.AttackFrequency)
                        {
                            bTwr.IsAttacking = true;
                            bTwr.CoolDown = 0;
                            bTwr.Active = true;
                        }
                    }
                }
            }
            foreach (LongRangeTower lTwr in longRangeTowerArray)
            {
                if (lTwr != null && lTwr.Active == true)
                {
                    foreach (Enemy en in enemyArray)
                    {

                        if (en != null && en.Active == true && en.EnemyRectangle.Intersects(lTwr.RadiusRec) && lTwr.CoolDown >= lTwr.AttackFrequency && lTwr.IsTracking == false)
                        {

                            lTwr.TargetLocation = GetEnemyLocation(en);

                            lTwr.AttackTracker1 = (lTwr.TowerX - lTwr.TargetLocation[0] + 40)/ lTwr.AttackSpeed;
                            lTwr.AttackTracker2 = (lTwr.TowerY - lTwr.TargetLocation[1] + 40)/ lTwr.AttackSpeed;
                            lTwr.AttackTrackerX = lTwr.AttackTracker1;
                            lTwr.AttackTrackerY = lTwr.AttackTracker2;

                            lTwr.IsTracking = true;
                                                       
                            lTwr.IsAttacking = true;
                            lTwr.CoolDown = 0;
                            lTwr.Active = true;
                            
                        }
                    }
                }
            }

            foreach (MissileTower mTwr in missileTowerArray)
            {
                if (mTwr != null && mTwr.Active == true)
                {
                    foreach (Enemy en in enemyArray)
                    {

                        if (en != null && en.Active == true && en.EnemyRectangle.Intersects(mTwr.RadiusRec) && mTwr.CoolDown >= mTwr.AttackFrequency && mTwr.IsTracking == false)
                        {

                            mTwr.TargetLocation = GetEnemyLocation(en);

                            mTwr.AttackTracker1 = (mTwr.TowerX - mTwr.TargetLocation[0] + 40) / mTwr.AttackSpeed;
                            mTwr.AttackTracker2 = (mTwr.TowerY - mTwr.TargetLocation[1] + 40) / mTwr.AttackSpeed;
                            mTwr.AttackTrackerX = mTwr.AttackTracker1;
                            mTwr.AttackTrackerY = mTwr.AttackTracker2;

                            mTwr.IsTracking = true;

                            mTwr.IsAttacking = true;
                            mTwr.CoolDown = 0;
                            mTwr.Active = true;

                        }
                    }
                }
            }
            foreach (SuperTower sTwr in superTowerArray)
            {
                if (sTwr != null && sTwr.Active == true)
                {
                    foreach (Enemy en in enemyArray)
                    {

                        if (en != null && en.Active == true && en.EnemyRectangle.Intersects(sTwr.RadiusRec) && sTwr.CoolDown >= sTwr.AttackFrequency && sTwr.IsTracking == false)
                        {

                            sTwr.TargetLocation = GetEnemyLocation(en);

                            sTwr.AttackTracker1 = (sTwr.TowerX - sTwr.TargetLocation[0] + 40) / sTwr.AttackSpeed;
                            sTwr.AttackTracker2 = (sTwr.TowerY - sTwr.TargetLocation[1] + 40) / sTwr.AttackSpeed;
                            sTwr.AttackTrackerX = sTwr.AttackTracker1;
                            sTwr.AttackTrackerY = sTwr.AttackTracker2;

                            sTwr.IsTracking = true;

                            sTwr.IsAttacking = true;
                            sTwr.CoolDown = 0;
                            sTwr.Active = true;

                        }
                    }
                }
            }


            foreach (BasicTower bTwr in basicTowerArray)
            {
                if (bTwr != null && bTwr.Active == true)
                {
                    foreach (Enemy en in enemyArray)
                    {
                        if (en != null && en.Active == true && en.EnemyRectangle.Intersects(bTwr.AttackRectangle))
                        {
                            bool result = en.TakeDamage(bTwr.AttackPower);
                            if (result == false)
                            {
                                player1.LevelScore++;
                                player1.TotalScore += 10;
                                player1.Money += 5;
                            }
                            bTwr.Active = false;
                            bTwr.CoolDown = 0;
                        }
                    }                   
                    bTwr.Active = true;
                }
            }
        
    
            foreach (LongRangeTower lTwr in longRangeTowerArray)
            {
                if (lTwr != null && lTwr.Active == true)
                {
                    foreach (Enemy en in enemyArray)
                    {
                        if (en != null && en.Active == true && en.EnemyRectangle.Intersects(lTwr.AttackRectangle) && lTwr.IsAttacking == true)
                        {
                            bool result = en.TakeDamage(lTwr.AttackPower);
                            if (result == false)
                            {
                                player1.LevelScore++;
                                player1.TotalScore += 10;
                                player1.Money += 5;
                            }
                            lTwr.Active = false;
                            lTwr.CoolDown = 0;
                        }
                    }
                    lTwr.Active = true;
                }
            }

            foreach (MissileTower mTwr in missileTowerArray)
            {
                if (mTwr != null && mTwr.Active == true)
                {
                    foreach (Enemy en in enemyArray)
                    {
                        if (en != null && en.Active == true && en.EnemyRectangle.Intersects(mTwr.AttackRectangle) && mTwr.IsAttacking == true)
                        {
                            bool result = en.TakeDamage(mTwr.AttackPower);
                            if (result == false)
                            {
                                player1.LevelScore++;
                                player1.TotalScore += 10;
                                player1.Money += 5;
                            }
                            mTwr.Active = false;
                            mTwr.CoolDown = 0;
                        }
                    }
                    mTwr.Active = true;
                }
            }

            foreach (SuperTower sTwr in superTowerArray)
            {
                if (sTwr != null && sTwr.Active == true)
                {
                    foreach (Enemy en in enemyArray)
                    {
                        if (en != null && en.Active == true && en.EnemyRectangle.Intersects(sTwr.AttackRectangle) && sTwr.IsAttacking == true)
                        {
                            bool result = en.TakeDamage(sTwr.AttackPower);
                            if (result == false)
                            {
                                player1.LevelScore++;
                                player1.TotalScore += 10;
                                player1.Money += 5;
                            }
                            sTwr.Active = false;
                            sTwr.CoolDown = 0;
                        }
                    }
                    sTwr.Active = true;
                }
            }


        }

        // Method to Get enemies location for the Tower's to track
        public int[] GetEnemyLocation(Enemy en)
        {
            int[] xyLoc = new int[2];
            xyLoc[0] = en.EnemyRectangle.X;
            xyLoc[1] = en.EnemyRectangle.Y;
            return xyLoc;
        }

        // Read HighScore
        public void ReadScore()
        {
            StreamReader reader = null;
            string line;
            hsName = "";
            hsWave = 1;
            hsScore = 1;
            int i = 0;

            try
            {
                reader = new StreamReader("HighScore.txt");

                while ((line = reader.ReadLine()) != null)
                {
                    if (i == 0)
                    {
                        hsName = line;
                    }
                    if (i == 1)
                    {
                        hsWave = int.Parse(line);
                    }
                    if (i == 2)
                    {
                        hsScore = int.Parse(line);
                    }
                    i++;
                }
            }
            catch
            {

            }
            finally
            {
                if (reader!=null)
                {
                    reader.Close();
                }
            }
        }

        // Write HighScore
        public void WriteScore(string plyrName, int wvReach, int ttlScore)
        {
            StreamWriter writer = null;
            
            try
            {
                writer = new StreamWriter("HighScore.txt");
                writer.WriteLine(plyrName);
                writer.WriteLine(wvReach);
                writer.WriteLine(ttlScore);
            }
            catch
            {

            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }



    }
}
