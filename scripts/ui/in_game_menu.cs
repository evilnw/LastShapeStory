using Godot;
using System;

public class in_game_menu : Control
{
	[Signal]
	public delegate void back_to_game_signal();
	[Signal]
	public delegate void restart_level_signal();
	[Signal]
	public delegate void back_to_mainmenu_signal();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	private void restart_level_button_pressed()
	{
		EmitSignal("restart_level_signal");
	}

	private void mainmenu_button_pressed()
	{
		EmitSignal("back_to_mainmenu_signal");
	}

	private void back_to_game_button_pressed()
	{
		EmitSignal("back_to_game_signal");
	}
}
