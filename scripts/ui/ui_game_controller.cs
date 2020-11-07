using Godot;
using System;

public class ui_game_controller : CanvasLayer
{
	enum Menu_Status { None, Game_Control, In_Game_Menu, GameOver_Menu }

	[Signal]
	public delegate void rotation_signal(float rotation);

	Menu_Status menu_status = Menu_Status.Game_Control;
	in_game_control in_game_control_ui;
	in_game_menu in_game_menu_ui;
	game_over_menu game_over_menu_ui;
	Settings settings;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		settings = (Settings)GetNode("/root/Settings");
		in_game_control_ui = GetNode<in_game_control>("in_game_control");
		in_game_menu_ui = GetNode<in_game_menu>("in_game_menu");
		game_over_menu_ui = GetNode<game_over_menu>("game_over_menu");

		enable_control_node(in_game_control_ui);
		disable_control_node(in_game_menu_ui);
		disable_control_node(game_over_menu_ui);
	}
	private void rotation_input_ui(float rotation)
	{
		EmitSignal("rotation_signal", rotation);
	}

	public void open_game_over_menu(int score)
	{
		menu_status = Menu_Status.GameOver_Menu;
		game_over_menu_ui.set_score_text(score);

		disable_control_node(in_game_menu_ui);
		disable_control_node(in_game_control_ui);
		enable_control_node(game_over_menu_ui);
	}
	private void open_in_game_menu()
	{
		if (menu_status != Menu_Status.Game_Control) {
			return;
		}
		menu_status = Menu_Status.In_Game_Menu;

		disable_control_node(in_game_control_ui);
		enable_control_node(in_game_menu_ui);
	}
	
	private void back_to_game()
	{
		menu_status = Menu_Status.Game_Control;
		disable_control_node(in_game_menu_ui);
		enable_control_node(in_game_control_ui);
	}
	private void restart_level()
	{
		level lvl = (level)GetNode("/root/level");
		int current_level = lvl.get_level_number();
		GetTree().ChangeScene("res://scenes/levels/lvl_" + current_level.ToString() + ".tscn");
	}
	private void back_to_mainmenu()
	{
		GetTree().ChangeScene("res://scenes/main_menu.tscn");
	}
	
	public void show_score_in_game_ui(int score)
	{
		in_game_control_ui.set_score_label(score);
	}

	private void enable_control_node(Control control_menu_node)
	{
		control_menu_node.FocusMode = Control.FocusModeEnum.All;
		control_menu_node?.Show();
	}

	private void disable_control_node(Control control_menu_node)
	{
		control_menu_node.FocusMode = Control.FocusModeEnum.None;
		control_menu_node?.Hide();
	}
}
