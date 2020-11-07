using Godot;
using System;

public class level_select_menu : Control
{
	Label score_lvl_1_label;
	Label score_lvl_2_label;
	Label score_lvl_3_label;
	Settings settings;

	public override void _Ready()
	{
		Settings settings = (Settings)GetNode("/root/Settings");
		score_lvl_1_label = GetNode<Label>("margin_container/v_box_container/h_level_select_container/v_level_1_container/score_lvl_1_label");
		score_lvl_2_label = GetNode<Label>("margin_container/v_box_container/h_level_select_container/v_level_2_container/score_lvl_2_label");
		score_lvl_3_label = GetNode<Label>("margin_container/v_box_container/h_level_select_container/v_level_3_container/score_lvl_3_label");

		score_lvl_1_label.Text = settings.get_level_score(1).ToString();
		score_lvl_2_label.Text = settings.get_level_score(2).ToString();
		score_lvl_3_label.Text = settings.get_level_score(3).ToString();

	}

	void select_lvl_button_pressed(int lvl)
	{
		GetTree().ChangeScene("res://scenes/levels/lvl_" + lvl.ToString() + ".tscn");
	}
}
