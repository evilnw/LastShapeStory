using Godot;
using System;

public class Settings : Node
{
	ConfigFile settings_file;
	const string settings_file_path = "user://settings.cfg";
	public const int MAX_LEVELS = 6;
	public const int MAX_PLAYER_SKINS = 2;
	private int current_player_skin = 1;

	public override void _Ready()
	{
		settings_file = new ConfigFile();
		var error = settings_file.Load(settings_file_path);
		if (error == Error.Ok) {	// no errors
			
		}
		settings_file.SetValue("player_skin_status", 1.ToString(), true);       // player skin_1 all time unlocked
		current_player_skin = get_current_player_skin_value();
	}

	public void update_lvl_info(int level, int score)
	{
		// load previous record for level. 0 - if no record.
		int previous_max_lvl_score = (int)settings_file.GetValue("level_score", level.ToString(), 0);
		// update record
		if (score > previous_max_lvl_score) {
			settings_file.SetValue("level_score", level.ToString(), score);
		}
		settings_file.Save(settings_file_path);
	}

	/*public string get_next_level_path(int current_level)
	{
		string level_path;
		if (current_level < MAX_LEVELS) {
			int next_level = current_level + 1;
			level_path = "res://scenes/levels/lvl_" + next_level.ToString() + ".tscn";
		}
		else {
			level_path = "res://scenes/main_menu.tscn";
		}
		return level_path;
	}*/
	public int get_level_score(int level)
	{
		return (int)settings_file.GetValue("level_score", level.ToString(), 0);       //0 - if no record.
	}

	public string get_current_player_skin_path()
	{
		string path = "res://objects/player_skin/" + current_player_skin.ToString() + "/animated_sprite.tscn";
		return path;
	}

	public void unlock_player_skin(int skin_value)
	{
		settings_file.SetValue("player_skin_status", skin_value.ToString(), true);
		settings_file.Save(settings_file_path);
	}

	public bool player_skin_is_unlocked(int skin_value)
	{
		return (bool)settings_file.GetValue("player_skin_status", skin_value.ToString(), false);     // default status false 
	}

	public void set_current_player_skin_value(int skin_value)
	{
		current_player_skin = skin_value;
	}

	private int get_current_player_skin_value()
	{
		return (int)settings_file.GetValue("player", "current_player_skin", 1);             // 1 - default value if current is broken
	}
}
