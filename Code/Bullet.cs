using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Xbox360IndieGameDesign {
	public class Bullet : GameComponent {
		private Player m_Owner;
		private Vector2 m_Position;
		private static Texture2D s_Sprite;

		public Texture2D Sprite {
			get {
				if( s_Sprite == null ) {
					s_Sprite = Game.Content.Load<Texture2D>( "Bullet" );
				}

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

		public Bullet( Game p_game, Player p_player ) : base( p_game ) {
			m_Owner = p_player;
			m_Position = new Vector2( m_Owner.Position.X + ( m_Owner.Position.Width / 2 ), m_Owner.Position.Y );
		}

		public override void Update( GameTime gameTime ) {
			m_Position -= new Vector2( 0, 10 );

			if( Position.Y < ( Sprite.Height * -1 ) ) {
				if( Game.Components.Remove( this ) ) {
					Dispose();
				}
			}

			base.Update( gameTime );
		}
	}
}
