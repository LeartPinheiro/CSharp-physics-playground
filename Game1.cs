using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;

namespace mg_playground;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private World world;

    private double Speed = 100;
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
       world = new World(1);
        var rnd = new Random();
        for(int i = 0; i < 10; i++)
        {
            int x = rnd.Next(0, 800);
            int y = rnd.Next(0, 600);
            double mass = rnd.Next(1, 3);
            world.add_entity(new Entitily(x, y, mass));
        }
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        world.update(gameTime.ElapsedGameTime.TotalSeconds * Speed);
        // TODO: Add your update logic here
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        foreach (Entitily entity in world.entities)
        {
            _spriteBatch.DrawCircle(new Vector2((float)entity.x, (float)entity.y), (float)entity.radius, 100, Color.White);
        }
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}
