using Godot;

namespace Character
{
	public class MovementAndAim
	{

		private KinematicBody Character;
		Spatial CameraPivot;
		Camera Camera;
		Spatial MagicOrbPivot;
		float acceleration = 5.0f;
		const float speed = 10.0f;
		Vector3 velocity;

		Vector2 MousePosition;
		public Vector3 MouseWorldIntersectPosition;
		public float AngleToMouse;

		const float SecondsUntilMouseHide = 1.5f;
		float MouseTimer = 0.0f;


		public MovementAndAim(KinematicBody character, Spatial cameraPivot){
			CameraPivot = cameraPivot;
			Camera = cameraPivot.GetChild<Camera>(0);
			cameraPivot.SetAsToplevel(true);
			MagicOrbPivot = character.GetChild<Spatial>(4);
			Character = character;
		}
		
		public void Update(Vector3 moveDirection, Vector3 faceDirection, float delta){
			Move(moveDirection, delta);
			Aim(moveDirection, faceDirection, delta);
		}

		void Move(Vector3 moveDirection, float delta)
		{
			if (moveDirection != Vector3.Zero)
			{
				velocity = new Vector3(moveDirection.x * speed, 0, moveDirection.z * speed);
				velocity = moveDirection.Normalized() * speed;
				Character.MoveAndSlide(velocity, Vector3.Up);
				
			}
			if (Camera != null)
			{
				CameraPivot.Translation = CameraPivot.Translation.LinearInterpolate(Character.Translation, delta * speed * 0.36f);
				//CameraPivot.Translate(new Vector3(Character.Translation.x - CameraPivot.Translation.x, 0, Character.Translation.z - CameraPivot.Translation.z));
			}
		}

		void Aim(Vector3 moveDirection, Vector3 faceDirection, float delta)
		{
			CheckMouseMovement(delta);
			if (Input.MouseMode == Input.MouseModeEnum.Visible) { AimWithMouse(); }
			else if (faceDirection != Vector3.Zero)
			{
				MagicOrbPivot.Rotation = new Vector3(0, Mathf.Atan2(-faceDirection.x, -faceDirection.z), 0);
			}
			else if (Input.MouseMode == Input.MouseModeEnum.Hidden) { MagicOrbPivot.Rotation = new Vector3(0, Mathf.Atan2(-moveDirection.x, -moveDirection.z), 0); }
		}

		void AimWithMouse()
		{
			PhysicsDirectSpaceState directSpaceState = Character.GetWorld().DirectSpaceState;
			MousePosition = Character.GetViewport().GetMousePosition();
			Vector3 rayOrigin = Camera.ProjectRayOrigin(MousePosition);
			Vector3 rayTarget = rayOrigin + Camera.ProjectRayNormal(MousePosition) * 2000;

			var intersection = directSpaceState.IntersectRay(rayOrigin, rayTarget);
			if (intersection.Keys.Count != 0)
			{
				MouseWorldIntersectPosition = (Vector3)intersection["position"];
				Vector3 characterPosition = Character.GlobalTranslation;
				AngleToMouse = Mathf.Atan2(-(MouseWorldIntersectPosition.x - characterPosition.x), -(MouseWorldIntersectPosition.z - characterPosition.z));
				MagicOrbPivot.Rotation = new Vector3(0, AngleToMouse, 0);
			}
		}

		void CheckMouseMovement(float delta)
		{
			Vector2 mousePosition = Camera.GetViewport().GetMousePosition();
			if (MousePosition == mousePosition)
			{
				MousePosition = mousePosition;
				if (MouseTimer > 0)
				{
					MouseTimer -= delta;
				}
				else if (Input.MouseMode == Input.MouseModeEnum.Visible) { Input.MouseMode = Input.MouseModeEnum.Hidden; }
			}
			else {
				if (Input.MouseMode == Input.MouseModeEnum.Hidden) { Input.MouseMode = Input.MouseModeEnum.Visible; }
				MouseTimer = SecondsUntilMouseHide;
			}
		}

	}
}
