﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tetris;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace MonoGame
{
    class ShapeSprite : DrawableGameComponent
    {
        private IShape shape;

        private Game game;
        private SpriteBatch spriteBatch;

        //for movedown frequency
        private Score score;
        private int counterMoveDown;

        //for keyboard input
        private KeyboardState oldstate;
        private int counterInput;
        private int threshold;

        //To Render
        private Texture2D filledBlock;

        public ShapeSprite(Game game, IBoard board, Score score)
            : base(game)
        {
            this.game = game;
            this.score = score;
        }
        public Shape Shape
        {
            get { return (Shape)shape; }
        }

        public override void Initialize()
        {
            oldstate = Keyboard.GetState();
            threshold = 6;
            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            filledBlock = game.Content.Load<Texture2D>("FilledBlock");
            shape = new Shape(filledBlock.Width, filledBlock.Height, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, 5);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            checkInput();
            base.Update(gameTime);
        }

        private void checkInput()
        {
            KeyboardState newState = Keyboard.GetState();
            if (newState.IsKeyDown(Keys.Right))
            {
                // If not down last update, key has just been pressed.
                if (!oldState.IsKeyDown(Keys.Right))
                {
                    shape.MoveRight();
                    counterMoveDown = 0; //reset counter with every new keystroke
                }
                else
                {
                    counterMoveDown++;
                    if (counterMoveDown > threshold)
                        shape.MoveRight();
                }
            }
            else if (newState.IsKeyDown(Keys.Left))
            {
                if (!oldState.IsKeyDown(Keys.Left))
                {
                    shape.MoveLeft();
                    counterMoveDown = 0;
                }
                else
                {
                    counterMoveDown++;
                    if (counterMoveDown > threshold)
                        shape.MoveLeft();
                }
            }
            // Improve/change the code above to also check forKeys.Left
            // Once finished checking all keys, update old state.
            oldState = newState;
        }
    }
}
