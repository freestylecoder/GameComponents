using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Xbox360IndieGameDesign {
	public class Bullet {
		private Player m_Owner;
		private Vector2 m_Position;
		private static Texture2D s_Sprite;

		public bool Visable {
			get;
			private set;
		}

		public Texture2D Sprite {
			get {
				return s_Sprite;
			}
		}

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

		public Bullet( Player p_player, ContentManager p_Manager ) {
			if( s_Sprite == null ) {
				s_Sprite = p_Manager.Load<Texture2D>( "Bullet" );
			}

			Visable = true;
			m_Owner = p_player;
			m_Position = new Vector2( m_Owner.Position.X + ( m_Owner.Position.Width / 2 ), m_Owner.Position.Y );
		}

		public void Update() {
			m_Position -= new Vector2( 0, 10 );

			if( Position.Y < ( Sprite.Height * -1 ) ) {
				Visable = false;
			}
		}
	}
}
