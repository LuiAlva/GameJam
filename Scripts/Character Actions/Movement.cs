using Godot;
using System.ComponentModel;

namespace Character
{
	public class Movement
	{
		private KinematicBody Character;
		Spatial CameraPivot;
		Camera Camera;
		float acceleration = 5.0f;
		float speed = 10.0f;
		Vector3 velocity;
		Vector3 rotation;
		
		public Movement(KinematicBody character, Spatial cameraPivot){
			CameraPivot = cameraPivot;
			Camera = cameraPivot.GetChild<Camera>(0);
			cameraPivot.SetAsToplevel(true);
			Character = character;
			rotation = Vector3.Zero;
		}
		
		public void Update(Vector3 direction, float delta){
			if (direction != Vector3.Zero)
			{
				velocity = Vector3.Zero;
				velocity = new Vector3(direction.x * speed, 0, direction.z * speed);
				rotation.y = Mathf.Atan2(-velocity.x, -velocity.z);
				Character.Rotation = rotation;
				velocity = direction.Normalized() * speed;
				velocity.LinearInterpolate(velocity, acceleration * delta);

				Character.MoveAndSlide(velocity, Vector3.Up);
				if (Camera != null)
				{
					CameraPivot.Translate(new Vector3(Character.Translation.x - CameraPivot.Translation.x, 0, Character.Translation.z - CameraPivot.Translation.z));
				}
			}
		}
	}
}
