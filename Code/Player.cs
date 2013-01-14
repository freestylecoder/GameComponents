using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Xbox360IndieGameDesign {
	public class Player : GameComponent {
		private static Texture2D s_Sprite;
		public Texture2D Sprite {
			get {
				if( s_Sprite == null ) {
					s_Sprite = Game.Content.Load<Texture2D>( "Ship" );
				}

				return s_Sprite;
			}
		}

		private Vector2 m_Position;
		public Rectangle Position {
			get {
				return
					new Rectangle(
						Convert.ToInt32( m_Position.X ),
						Convert.ToInt32( m_Position.Y ),
						Sprite.Width,
						Sprite.Height );
			}
		}

		private const long c_PointsPerLife = 30000;
		private long m_NextLiveAt = c_PointsPerLife;
		private long m_Score;
		public long Score {
			get {
				return m_Score;
			}
			set {
				m_Score = value;
				if( m_Score >= m_NextLiveAt ) {
					++Lives;
					m_NextLiveAt += c_PointsPerLife;
				}
			}
		}

		public int Lives {
			get;
			private set;
		}

		public void LoseLife() {
			--Lives;
			if( Lives == 0 ) {
				throw new GameOverException();
			} else {
				m_Position =
					new Vector2(
						( Game.GraphicsDevice.Viewport.Width / 2 ) - ( Sprite.Width / 2 ),
						Game.GraphicsDevice.Viewport.Height - ( Sprite.Height / 2 )
					);
			}
		}

		private IControlState m_Controls;
		public Player( Game game, IControlState p_controls )
			: base( game ) {
			Lives = 3;
			Score = 0;
			m_Controls = p_controls;
			m_NextLiveAt = c_PointsPerLife;
			m_Position = new Vector2( Game.GraphicsDevice.Viewport.Width / 2, Game.GraphicsDevice.Viewport.Height - Sprite.Height );
		}

		/// <summary>Allows the game component to update itself.</summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public override void Update( GameTime gameTime ) {
			#region Move Player
			m_Position += m_Controls.Movement;
			#endregion

			#region Keep Player in Viewport
			m_Position.X = MathHelper.Clamp( m_Position.X, 0, Game.GraphicsDevice.Viewport.Width - Sprite.Width );
			m_Position.Y = MathHelper.Clamp( m_Position.Y, 0, Game.GraphicsDevice.Viewport.Height - Sprite.Height );
			#endregion

			#region Fire Bullet
			if( m_Controls.Fire ) {
				Game.Components.Add( new Bullet( Game, this ) );
			}
			#endregion

			base.Update( gameTime );
		}
	}
}
