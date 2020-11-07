using Godot;
using System;

public class main_menu : Node
{
	[Export]
	string background_color_name = "#94b0c2";


	public override void _Ready()
	{
		VisualServer.SetDefaultClearColor(new Color(background_color_name));
	}
}
