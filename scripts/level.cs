using Godot;
using System;

public class level : Node
{
	enum Level_Status { Process, Gameover };
	
	[Export]
	string color_background = "#94b0c2";                       // #ffcd75 , etc
	[Export]
	int level_number = 1;
	
	int difficulty = 1;
	int current_score = 0;
	Level_Status level_status = Level_Status.Process;

	ui_game_controller ui_controller;

	public override void _Ready()
	{
		VisualServer.SetDefaultClearColor(new Color(color_background));
		ui_controller = GetNode<ui_game_controller>("ui_game_controller");
	}
	public int get_level_number()
	{
		return level_number;
	}

	public int get_difficulty()
	{
		return difficulty;
	}

	private void add_score_from_timer()
	{
		if (level_status == Level_Status.Process) {
			current_score++;
			ui_controller?.show_score_in_game_ui(current_score);
		}
		difficulty++;
	}

	public void add_score_from_bonus_object(int bonus_score)
	{
		current_score += bonus_score;
		ui_controller?.show_score_in_game_ui(current_score);
	}

	public void game_over()
	{
		level_status = Level_Status.Gameover;
		Settings settings = (Settings)GetNode("/root/Settings");
		settings?.update_lvl_info(level_number, current_score);
		ui_controller?.open_game_over_menu(current_score);
	}
}
