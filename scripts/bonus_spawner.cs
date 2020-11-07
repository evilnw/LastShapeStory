using Godot;
using System;

public class bonus_spawner : Node
{
	[Export]
	int game_field_width = 1920;
	[Export]
	int game_field_height = 1080;

	int difficulty_coef = 1;
	RandomNumberGenerator random_gen;
	PackedScene bonus_1;
	level lvl;

	public override void _Ready()
	{
		random_gen = new RandomNumberGenerator();
		bonus_1 = ResourceLoader.Load("res://objects/bonus/bonus_1.tscn") as PackedScene;
		lvl = (level)GetNode("/root/level");
	}

	private void timer_spawn_timeout()
	{
		difficulty_coef = lvl.get_difficulty();
		//ganerate random bonus object
		bonus bonus_object = (bonus)bonus_1.Instance();
		AddChild(bonus_object);

		// random pos
		random_gen.Randomize();
		float pos_x = random_gen.Randi() % game_field_width;
		pos_x = (pos_x >= 0) ? pos_x : pos_x * (-1);
		float pos_y = random_gen.Randi() % game_field_height;
		pos_y = (pos_y >= 0) ? pos_y : pos_y * (-1);
		bonus_object.Position = new Vector2(pos_x, pos_y);

		//random velocity
		float speed_x = random_gen.Randi() % (50 + difficulty_coef);
		float speed_y = random_gen.Randi() % (50 + difficulty_coef);
		int direction_x = (int)random_gen.Randi() % 2;
		int direction_y = (int)random_gen.Randi() % 2;
		Vector2 velocity = new Vector2(speed_x * direction_x, speed_y * direction_y);
		bonus_object.set_velocity_bonus(velocity);

		//random score
		int score = (int)random_gen.Randi() % 6;
		score = (score < 0) ? (score * -1) + 2 : score + 2;
		bonus_object.set_score_bonus(score);
	}
}
