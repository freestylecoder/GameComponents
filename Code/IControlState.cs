using Microsoft.Xna.Framework;

namespace Xbox360IndieGameDesign {
	public interface IControlState : IGameComponent {
		Vector2 Movement { get; }

		bool Fire { get; }
		bool Pause { get; }
		bool Exit { get; }
	}
}