using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Xbox360IndieGameDesign {
	public class ControllerControlState : GameComponent, IControlState {
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

		public static bool InvertYAxix = false;

		private PlayerIndex m_Controller;
		private GamePadState m_PreviousState;

		public ControllerControlState( Game p_game, PlayerIndex p_controller ) : base( p_game ) {
			m_Controller = p_controller;
			m_PreviousState = GamePad.GetState( p_controller );
		}

		public override void Update( GameTime gameTime ) {
			m_PreviousState = UpdateControlState( GamePad.GetState( m_Controller ), m_PreviousState );
			base.Update( gameTime );
		}

		public GamePadState UpdateControlState( GamePadState p_CurrentState, GamePadState p_PreviousState ) {
			Vector2 _Movement =
				new Vector2(
					p_CurrentState.ThumbSticks.Left.X * 5f,
					p_CurrentState.ThumbSticks.Left.Y * 5f * ( InvertYAxix ? 1f : -1f )
				);

			Movement = _Movement;

			if( p_CurrentState.Buttons.A == ButtonState.Pressed ) {
				Fire = ( p_PreviousState.Buttons.A == ButtonState.Released );
			} else {
				Fire = false;
			}

			if( p_CurrentState.Buttons.Start == ButtonState.Pressed ) {
				Pause = ( p_PreviousState.Buttons.Start == ButtonState.Released );
			} else {
				Pause = false;
			}

			if( p_CurrentState.Buttons.Back == ButtonState.Pressed ) {
				Exit = ( p_PreviousState.Buttons.Back == ButtonState.Released );
			} else {
				Exit = false;
			}

			return p_CurrentState;
		}
	}

	public class ControllerOneControlState : ControllerControlState {
		public ControllerOneControlState( Game p_game )
			: base( p_game, PlayerIndex.One ) {
		}
	}

	public class ControllerTwoControlState : ControllerControlState {
		public ControllerTwoControlState( Game p_game )
			: base( p_game, PlayerIndex.Two ) {
		}
	}

	public class ControllerThreeControlState : ControllerControlState {
		public ControllerThreeControlState( Game p_game )
			: base( p_game, PlayerIndex.Three ) {
		}
	}

	public class ControllerFourControlState : ControllerControlState {
		public ControllerFourControlState( Game p_game )
			: base( p_game, PlayerIndex.Four ) {
		}
	}
}