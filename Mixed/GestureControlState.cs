using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace Xbox360IndieGameDesign {
	public class GestureControlState : IControlState {
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

		public GestureControlState()
			: base() {
			TouchPanel.EnabledGestures = GestureType.Tap | GestureType.FreeDrag;
		}

		public void Update() {
			Fire = Pause = Exit = false;

			Vector2 _firstSample = Vector2.Zero, _lastSample = Vector2.Zero;
			while( TouchPanel.IsGestureAvailable ) {
				GestureSample _sample = TouchPanel.ReadGesture();
				switch( _sample.GestureType ) {
					case GestureType.Tap:
						Fire = true;
						break;
					case GestureType.FreeDrag:
						if( _firstSample == Vector2.Zero ) {
							_firstSample = _sample.Position;
						}

						_lastSample = _sample.Position;
						break;
					default:
						break;
				}
			}

			Movement = _lastSample - _firstSample;

			Exit = GamePad.GetState( PlayerIndex.One ).IsButtonDown( Buttons.Back );
		}
	}
}