using Godot;
using Player;

public class Witch : KinematicBody
{
	public int Health = 3;
	Inputs inputs;
	Spatial cameraPivot;
	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		cameraPivot = GetChild<Spatial>(0);
		inputs = new Inputs(this, cameraPivot);
	}

  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
		inputs.Update(delta);
  }
}
