using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Xbox360IndieGameDesign {
	public class Star {
		static private Texture2D s_Sprite;
		static private Random s_RandomNumberGenerator;
		static Star() {
			s_Sprite = null;
			s_RandomNumberGenerator = new Random();
		}

		private int m_Frame;
		private Rectangle m_SpriteFrame;

		private Color m_Shading;
		private Vector2 m_Position;
		private TimeSpan TimeToChange;
		private GraphicsDevice m_Device;

		public Star( GraphicsDevice p_Device, ContentManager p_Manager ) {
			m_Device = p_Device;

			if( s_Sprite == null ) {
				s_Sprite = p_Manager.Load<Texture2D>( "Star" );
			}

			m_Frame = s_RandomNumberGenerator.Next( 0, 8 );

			m_Position = new Vector2(
				s_RandomNumberGenerator.Next( 0, m_Device.Viewport.Width ),
				s_RandomNumberGenerator.Next( 0, m_Device.Viewport.Height ) );

			m_Shading = new Color(
				s_RandomNumberGenerator.Next( 0, 255 ),
				s_RandomNumberGenerator.Next( 0, 255 ),
				s_RandomNumberGenerator.Next( 0, 255 ),
				s_RandomNumberGenerator.Next( 0, 255 ) );

			TimeToChange = new TimeSpan( 0, 0, 0, 0, 100 * s_RandomNumberGenerator.Next( 1, 6 ) );
		}

		public void Update( GameTimerEventArgs gameTime ) {
			TimeToChange -= gameTime.ElapsedTime;
			if( TimeToChange < TimeSpan.Zero ) {
				m_Position = new Vector2(
					s_RandomNumberGenerator.Next( 0, m_Device.Viewport.Width ),
					s_RandomNumberGenerator.Next( 0, m_Device.Viewport.Height ) );

				m_Shading = new Color(
					s_RandomNumberGenerator.Next( 0, 255 ),
					s_RandomNumberGenerator.Next( 0, 255 ),
					s_RandomNumberGenerator.Next( 0, 255 ),
					s_RandomNumberGenerator.Next( 0, 255 ) );

				m_SpriteFrame = new Rectangle( m_Frame * 5, 0, 5, 5 );

				if( m_SpriteFrame.X + m_SpriteFrame.Width == s_Sprite.Width ) {
					m_Frame = 0;

					m_Position = new Vector2(
						s_RandomNumberGenerator.Next( 0, m_Device.Viewport.Width ),
						s_RandomNumberGenerator.Next( 0, m_Device.Viewport.Height ) );

					m_Shading = new Color(
						s_RandomNumberGenerator.Next( 0, 255 ),
						s_RandomNumberGenerator.Next( 0, 255 ),
						s_RandomNumberGenerator.Next( 0, 255 ),
						s_RandomNumberGenerator.Next( 0, 255 ) );
				} else {
					++m_Frame;
				}

				TimeToChange = new TimeSpan( 0, 0, 0, 0, 100 * s_RandomNumberGenerator.Next( 1, 6 ) );
			}
		}

		public void Draw( SpriteBatch p_SpriteBatch ) {
			p_SpriteBatch.Draw( s_Sprite, m_Position, m_SpriteFrame, m_Shading );
		}
	}
}
