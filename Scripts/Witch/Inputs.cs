using Character;
using Godot;

namespace Player
{
	public class Inputs
	{
		MovementAndAim MoveAndAim;
		Vector3 moveDirection;
		Vector3 aimDirection;
		public Inputs(KinematicBody character, Spatial cameraPivot)
		{
			MoveAndAim = new MovementAndAim(character, cameraPivot);
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public void Update(float delta)
		{
			MoveUpdate(delta);
		}

		void MoveUpdate(float delta)
		{
			moveDirection = Vector3.Zero;
			moveDirection.x = Input.GetActionStrength("Move_Right") - Input.GetActionStrength("Move_Left");
			moveDirection.z = Input.GetActionStrength("Move_Down") - Input.GetActionStrength("Move_Up");
			aimDirection = Vector3.Zero;
			aimDirection.x = Input.GetActionStrength("Aim_Right") - Input.GetActionStrength("Aim_Left");
			aimDirection.z = Input.GetActionStrength("Aim_Down") - Input.GetActionStrength("Aim_Up");
			MoveAndAim.Update(moveDirection, aimDirection, delta);
		}
	}
}
