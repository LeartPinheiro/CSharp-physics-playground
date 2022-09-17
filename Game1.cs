using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;

namespace mg_playground;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    // font with MonoGame.Extended
    private SpriteFont font;
    private World world;
    private Camera camera;
    private double Speed = 500;
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }
    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        world = new World(0.05);
        int width = GraphicsDevice.Viewport.Width;
        int height = GraphicsDevice.Viewport.Height;
        camera = new Camera(0, 0, width, height, 1, 0);
        var rnd = new Random();
        for(int i = 0; i < 10; i++)
        {
            int x = rnd.Next(-width/2, width/2);
            int y = rnd.Next(-height/2, height/2);
            double mass = rnd.NextDouble() * 4 + 1;
            world.add_entity(new Entitily(x, y, mass));
        }
        base.Initialize();
    }
    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        // TODO: use this.Content to load your game content here
        font = Content.Load<SpriteFont>("File");
    }

    protected void CheckInput()
    {

        if (Keyboard.GetState().IsKeyDown(Keys.W))
        {
            camera.y -= (int)camera.speed;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.S))
        {
            camera.y += (int)camera.speed;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.A))
        {
            camera.x -= (int)camera.speed;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.D))
        {
            camera.x += (int)camera.speed;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.OemPlus))
        {
            camera.zoom *= 1.1;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.OemMinus))
        {
            camera.zoom /= 1.1;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.Q))
        {
            camera.rotation -= 0.1;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.E))
        {
            camera.rotation += 0.1;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.Space))
        {
            camera.rotation = 0;
            camera.zoom = 1;
        }
    }
    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        CheckInput();
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
            if (camera.IsOnScreen(entity.x, entity.y, entity.radius))
            {
                DrawEntity(entity);
            }
        }
        _spriteBatch.End();
        base.Draw(gameTime);
    }
    protected void DrawEntity(Entitily entity)
    {
        int[] pos = camera.GetScreenPos(entity.x, entity.y);
        float size = (float)(entity.radius * camera.zoom);
        _spriteBatch.DrawCircle(new Vector2(pos[0], pos[1]), size, 100, Color.White, size);
        String text = entity.velocity.ToString("0.##");
        _spriteBatch.DrawString(font, text, new Vector2(pos[0], pos[1]), Color.Black);
    }

}
