using Godot;
using System;

public class in_game_control : Control
{
	[Signal]
	public delegate void rotation_signal(float rotation);
	[Signal]
	public delegate void gamemenu_button_pressed();

	// Declare member variables here. 
	//Vector2 center_screen_point;

	Label score_label;                                      // show current score
	//Label test_label;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//center_screen_point = new Vector2(960, 540);
		score_label = GetNode<Label>("margin_container/h_box_container_2/score");
	}

	/*private void position_input_ui(object @event)
	{
		if (@event != null) {
			if (@event is InputEventMouseMotion) {
				InputEventMouseMotion mouse_event = (InputEventMouseMotion)@event;
				Vector2 vec = new Vector2(mouse_event.Position);
				float rotation = vec.AngleToPoint(center_screen_point);
				EmitSignal("rotation_signal", rotation);
			}
		}
	}*/

	private void open_gamemenu_button_pressed()
	{
		EmitSignal("gamemenu_button_pressed");
	}

	public void set_score_label(int score)
	{
		score_label.Text = score.ToString();
	}
	
	private void _on_touch_field_button_rotation_touch(float rotation)
	{
		EmitSignal("rotation_signal", rotation);
	}
}
