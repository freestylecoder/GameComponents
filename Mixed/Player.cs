using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

namespace Xbox360IndieGameDesign {
	public class Player {
		private static Texture2D s_Sprite;
		public Texture2D Sprite {
			get {
				return s_Sprite;
			}
		}

		private Vector2 m_StartingPosition;
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
				m_Position = m_StartingPosition;
			}
		}

		private IControlState m_Controls;
		private GraphicsDevice m_Device;
		private ContentManager m_Manager;
		public Player( IControlState p_controls, GraphicsDevice p_Device, ContentManager p_Manager ) {
			if( s_Sprite == null ) {
				s_Sprite = p_Manager.Load<Texture2D>( "Ship" );
			}

			Lives = 3;
			Score = 0;
			m_Device = p_Device;
			m_Manager = p_Manager;
			m_Controls = p_controls;
			m_NextLiveAt = c_PointsPerLife;
			m_StartingPosition = new Vector2( ( p_Device.Viewport.Width / 2 ) - ( Sprite.Width / 2 ), p_Device.Viewport.Height - ( Sprite.Height / 2 ) );
			m_Position = new Vector2( p_Device.Viewport.Width / 2, p_Device.Viewport.Height - Sprite.Height );
		}

		/// <summary>Allows the game component to update itself.</summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public void Update( List<Bullet> p_Bullets ) {
			#region Move Player
			m_Position += m_Controls.Movement;
			#endregion

			#region Keep Player in Viewport
			m_Position.X = MathHelper.Clamp( m_Position.X, 0, m_Device.Viewport.Width - Sprite.Width );
			m_Position.Y = MathHelper.Clamp( m_Position.Y, 0, m_Device.Viewport.Height - Sprite.Height );
			#endregion

			#region Fire Bullet
			if( m_Controls.Fire ) {
				p_Bullets.Add( new Bullet( this, m_Manager ) );
			}
			#endregion
		}
	}
}
