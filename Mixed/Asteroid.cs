using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Xbox360IndieGameDesign {
	public class Asteroid {
		#region Static Area
		static private Random s_rng;
		static private List<Texture2D> s_AstroidSprites;

		static Asteroid() {
			s_rng = new Random();
			s_AstroidSprites = null;
		}
		#endregion

		private int m_Index;
		private Vector2 m_Position;
		private Vector2 m_Trajectory;

		private GraphicsDevice m_Device;
		private ContentManager m_Content;

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

		public Texture2D Sprite {
			get {
				return s_AstroidSprites[m_Index];
			}
		}

		public bool Visable;

		public Asteroid( GraphicsDevice p_Device, ContentManager p_Content ) {
			m_Device = p_Device;
			m_Content = p_Content;

			if( s_AstroidSprites == null ) {
				s_AstroidSprites = new List<Texture2D>();
				s_AstroidSprites.Add( p_Content.Load<Texture2D>( "Asteroids\\blue-one" ) );
				s_AstroidSprites.Add( p_Content.Load<Texture2D>( "Asteroids\\cool-rock" ) );
				s_AstroidSprites.Add( p_Content.Load<Texture2D>( "Asteroids\\exploder" ) );
				s_AstroidSprites.Add( p_Content.Load<Texture2D>( "Asteroids\\frozen" ) );
				s_AstroidSprites.Add( p_Content.Load<Texture2D>( "Asteroids\\green-chunk" ) );
				s_AstroidSprites.Add( p_Content.Load<Texture2D>( "Asteroids\\hot-rock" ) );
				s_AstroidSprites.Add( p_Content.Load<Texture2D>( "Asteroids\\hunk-o-rock" ) );
				s_AstroidSprites.Add( p_Content.Load<Texture2D>( "Asteroids\\magma" ) );
			}

			Visable = true;
			m_Trajectory = new Vector2( s_rng.Next( -5, 5 ), s_rng.Next( 1, 10 ) );
			m_Index = s_rng.Next( 0, s_AstroidSprites.Count );
			m_Position = new Vector2( s_rng.Next( 0, p_Device.Viewport.Width ), Sprite.Height * -1 );
		}

		public void Update() {
			m_Position += m_Trajectory;

			if( ( Position.Y > m_Device.Viewport.Height ) ||	// Off the bottom of the screen
				( Position.X > m_Device.Viewport.Width ) ||	// Off the right side of the screen
				( Position.X < ( Sprite.Width * -1 ) ) ) {				// Off the left side of the screen
					// NOTE: You'd probably want to pool "dead" Asteroids in a real game
					// This would reduce the number you had to create from scratch every time.
					Visable = false;
			}
		}
	}
}
