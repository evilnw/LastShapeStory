using Godot;
using System;

public class game_over_menu : Control
{
	[Signal]
	public delegate void restart_level_signal();
	[Signal]
	public delegate void back_to_mainmenu_signal();

	Label score_label;

	public override void _Ready()
	{
		score_label = GetNode<Label>("h_box_container/v_box_container/score_label");
	}

	public void set_score_text(int score)
	{
		score_label.Text = score.ToString();
	}

	private void mainmenu_button_pressed()
	{
		EmitSignal("back_to_mainmenu_signal");
	}
	private void restart_level_button_pressed()
	{
		EmitSignal("restart_level_signal");
	}
}
