using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

using MonoGame.Extended;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;

using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.Screens;
using MonoGame.Extended.ViewportAdapters;

namespace TetraDungeon
{
    public class Game1 : Game
    {
        // Gestion Graphique
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // Gestionnaire de scènes
        private readonly ScreenManager _screenManager;

        // Tiled Map
        private TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;
        private TiledMapTileLayer mapLayer;

        // Caméra
        private OrthographicCamera _camera;

        // Steve
        private AnimatedSprite _steve;
        private string _animationSteve;
        string tempIdle;
        private SoundEffect _steveFootStep;

        private Player Steve;
        private AnimatedSprite _esquireTest;

        Esquire[] Chevaliers = new Esquire[3];



        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            
            // Screen Manager
            _screenManager = new ScreenManager();
            Components.Add(_screenManager);
        }




        protected override void Initialize()
        {
            // Fenêtre en 1280*720
            _graphics.PreferredBackBufferWidth = 1280;  
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();

            // Caméra
            var viewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, 800, 480);
            _camera = new OrthographicCamera(viewportAdapter);
            _camera.Zoom = 2f; // Zoom Caméra

            // Steve
            Steve.Position = new Vector2(80, 80);
            tempIdle = "idle";

            base.Initialize();
        }





        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _tiledMap = Content.Load<TiledMap>("map3");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            SpriteSheet steveSpriteSheet = Content.Load<SpriteSheet>("SteveSprite.sf", new JsonContentLoader());
            SpriteSheet esquireSpriteSheet = Content.Load<SpriteSheet>("EsquireSprite.sf", new JsonContentLoader());
            _steveFootStep = Content.Load<SoundEffect>("footstep");

            // Définition de Steve
            _steve = new AnimatedSprite(steveSpriteSheet);
            Steve = new Player(_steve);

            // Définition de l'écuyer
            _esquireTest = new AnimatedSprite(esquireSpriteSheet);
    
        }





        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;

            //Map
            mapLayer = _tiledMap.GetLayer<TiledMapTileLayer>("Collisions");

            // Caméra
            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            _camera.LookAt(_stevePosition);

            _tiledMapRenderer.Update(gameTime);

            // Steve
            Steve.MovementSteve(deltaSeconds, mapLayer, _tiledMap, _animationSteve, ref _stevePosition, ref tempIdle, _steveFootStep);
            _steve.Update(deltaSeconds);

            base.Update(gameTime);
        }





        protected override void Draw(GameTime gameTime)
        {
            var transformMatrix = _camera.GetViewMatrix();

            GraphicsDevice.Clear(Color.Black);

            _tiledMapRenderer.Draw(_camera.GetViewMatrix());

            _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp,null,null, transformMatrix: transformMatrix);
            _steve.Draw(_spriteBatch, _stevePosition, 0, new Vector2(1, 1));
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}