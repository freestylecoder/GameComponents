using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Xbox360IndieGameDesign {
	public class Star : DrawableGameComponent {
		static private Texture2D s_Sprite;
		static private SpriteBatch s_SpriteBatch;
		static private Random s_RandomNumberGenerator;
		static Star() {
			s_Sprite = null;
			s_SpriteBatch = null;
			s_RandomNumberGenerator = new Random();
		}

		private int m_Frame;
		private Rectangle m_SpriteFrame;

		private Color m_Shading;
		private Vector2 m_Position;
		private TimeSpan TimeToChange;

		public Star( Game game ) : base( game ) {
		}

		protected override void LoadContent() {
			if( s_SpriteBatch == null ) {
				s_SpriteBatch = new SpriteBatch( Game.GraphicsDevice );
			}

			if( s_Sprite == null ) {
				s_Sprite = Game.Content.Load<Texture2D>( "Star" );
			}

			m_Frame = s_RandomNumberGenerator.Next( 0, 8 );

			m_Position = new Vector2(
				s_RandomNumberGenerator.Next( 0, GraphicsDevice.Viewport.Width ),
				s_RandomNumberGenerator.Next( 0, GraphicsDevice.Viewport.Height ) );

			m_Shading = new Color(
				s_RandomNumberGenerator.Next( 0, 255 ),
				s_RandomNumberGenerator.Next( 0, 255 ),
				s_RandomNumberGenerator.Next( 0, 255 ),
				s_RandomNumberGenerator.Next( 0, 255 ) );

			TimeToChange = new TimeSpan( 0, 0, 0, 0, 100 * s_RandomNumberGenerator.Next( 1, 6 ) );

			base.LoadContent();
		}

		public override void Update( GameTime gameTime ) {
			// If push comes to shove, dropping a frame or two of the stars twinkling is not too bad
			if( gameTime.IsRunningSlowly )
				return;

			TimeToChange -= gameTime.ElapsedGameTime;
			if( TimeToChange < TimeSpan.Zero ) {
				m_SpriteFrame = new Rectangle( ++m_Frame * 5, 0, 5, 5 );

				if( m_SpriteFrame.X + m_SpriteFrame.Width == s_Sprite.Width ) {
					m_Frame = 0;
					m_SpriteFrame = new Rectangle( 0, 0, 5, 5 );

					m_Position = new Vector2(
						s_RandomNumberGenerator.Next( 0, GraphicsDevice.Viewport.Width ),
						s_RandomNumberGenerator.Next( 0, GraphicsDevice.Viewport.Height ) );

					m_Shading = new Color(
						s_RandomNumberGenerator.Next( 0, 255 ),
						s_RandomNumberGenerator.Next( 0, 255 ),
						s_RandomNumberGenerator.Next( 0, 255 ),
						s_RandomNumberGenerator.Next( 0, 255 ) );
				}

				TimeToChange = new TimeSpan( 0, 0, 0, 0, 100 * s_RandomNumberGenerator.Next( 1, 6 ) );
			}

			base.Update( gameTime );
		}

		public override void Draw( GameTime gameTime ) {
			// NOTE: You do not EVER want to do this
			// This is done to show the inefficenies of multiple calls to SpriteBatch.End
			// Massively increase the number of stars to see the game start to slow.
			// Get the current SpriteBatch from Game and use it while open
			s_SpriteBatch.Begin();
			s_SpriteBatch.Draw( s_Sprite, m_Position, m_SpriteFrame, m_Shading );
			s_SpriteBatch.End();

			base.Draw( gameTime );
		}
	}
}
