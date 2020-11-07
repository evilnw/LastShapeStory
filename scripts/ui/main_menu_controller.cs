using Godot;
using System;

public class main_menu_controller : CanvasLayer
{
	[Export]
	string color_play_button = "ffeb33";

	Polygon2D play_button_polygon_1;
	Polygon2D play_button_polygon_2;
	Polygon2D play_button_polygon_3;

	const int max_animation_count = 3;
	int count_animation = 1;

	public override void _Ready()
	{
		Color color = new Color(color_play_button);
		play_button_polygon_1 = GetNode<Polygon2D>("h_box_container/play_button/play_button_polygon_1");
		play_button_polygon_2 = GetNode<Polygon2D>("h_box_container/play_button/play_button_polygon_2");
		play_button_polygon_3 = GetNode<Polygon2D>("h_box_container/play_button/play_button_polygon_3");
		play_button_polygon_1.Color = color;
		play_button_polygon_2.Color = color;
		play_button_polygon_3.Color = color;
		play_button_polygon_2.Hide();
		play_button_polygon_3.Hide();
	}

	void timer_animation_timeout()
	{
		if (count_animation == 1) {
			play_button_polygon_1.Visible = true;
			play_button_polygon_2.Visible = false;
			play_button_polygon_3.Visible = false;
		}
		else if (count_animation == 2) {
			play_button_polygon_1.Visible = false;
			play_button_polygon_2.Visible = true;
			play_button_polygon_3.Visible = false;
		}
		else if (count_animation == 3) {
			play_button_polygon_1.Visible = false;
			play_button_polygon_2.Visible = false;
			play_button_polygon_3.Visible = true;
		}

		count_animation++;
		count_animation = (count_animation <= max_animation_count) ? count_animation : 1;
	}

	private void level_select_scene()
	{
		GetTree().ChangeScene("res://scenes/level_select_menu.tscn");
	}
	
	private void exit_button_pressed()
	{
		GetTree().Quit();
	}
}
