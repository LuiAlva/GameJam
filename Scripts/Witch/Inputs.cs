using Character;
using Godot;

namespace Player
{
	public class Inputs
	{
		Movement Move;
		Vector3 direction;
		public Inputs(KinematicBody character, Spatial cameraPivot)
		{
			Move = new Movement(character, cameraPivot);
			Input.MouseMode = Input.MouseModeEnum.Captured;
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public void Update(float delta)
		{
			direction = Vector3.Zero;
			direction.x = Input.GetActionStrength("Move_Right") - Input.GetActionStrength("Move_Left");
			direction.z = Input.GetActionStrength("Move_Down") - Input.GetActionStrength("Move_Up");
			Move.Update(direction, delta);
		}
	}
}
