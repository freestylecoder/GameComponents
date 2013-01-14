using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Xbox360IndieGameDesign {
	public class KeyboardControlState : GameComponent, IControlState {
		#region IControlState Members
		public Vector2 Movement {
			get;
			private set;
		}

		public bool Fire {
			get;
			private set;
		}

		public bool Pause {
			get;
			private set;
		}

		public bool Exit {
			get;
			private set;
		}
		#endregion

		private KeyboardState m_PreviousState;

		public KeyboardControlState( Game p_game ) : base( p_game ) {
			m_PreviousState = Keyboard.GetState();
		}

		public override void Update( GameTime gameTime ) {
			KeyboardState _CurrentState = Keyboard.GetState();

			Vector2 _PositiveMovement =
				new Vector2(
					_CurrentState.IsKeyDown( Keys.Right ) ? 5f : 0f,
					_CurrentState.IsKeyDown( Keys.Down ) ? 5f : 0f
				);

			Vector2 _NegitiveMovement =
				new Vector2(
				_CurrentState.IsKeyDown( Keys.Left ) ? 5f : 0f,
				_CurrentState.IsKeyDown( Keys.Up ) ? 5f : 0f
			);

			Movement = _PositiveMovement - _NegitiveMovement;
			Fire = _CurrentState.IsKeyDown( Keys.Space ) && m_PreviousState.IsKeyUp( Keys.Space );
			Pause = _CurrentState.IsKeyDown( Keys.F12 ) && m_PreviousState.IsKeyUp( Keys.F12 );
			Exit = _CurrentState.IsKeyDown( Keys.Escape ) && m_PreviousState.IsKeyUp( Keys.Escape );

			m_PreviousState = _CurrentState;

			base.Update( gameTime );
		}
	}
}