using Godot;
using System;

public class block : StaticBody2D
{
	enum MoveDirectionX { Left, Right };
	enum MoveDirectionY { Up, Down};

	[Export]
	string color_name = "#ef7d57";              // format #ef7d57
	[Export]
	bool enable_difficulty = true;
	[Export]
	float width = 64;
	[Export]
	float height = 64;
	[Export]
	float distance_x = 0;
	[Export]
	float distance_y = 0;
	[Export]
	float move_speed = 0;
	[Export]
	MoveDirectionX move_direction_x = MoveDirectionX.Right;
	[Export]
	MoveDirectionY move_direction_y = MoveDirectionY.Down;

	[Export]
	float rotation_speed_direction = 0;                   // -1,-2,-3 - turn left, 1,2,3 - turn right, 0 - no rotation
	int difficulty_coef = 0;
	const int max_diff_coef = 100;


	float complete_distance_x = 0;
	float complete_distance_y = 0;

	CollisionShape2D collision_shape_2d;
	Polygon2D polygon;
	level lvl;


	public override void _Ready()
	{
		lvl = (level)GetNode("/root/level");
		if (lvl != null) {
			difficulty_coef = lvl.get_difficulty();
		}

		polygon = GetNode<Polygon2D>("polygon_2d");
		collision_shape_2d = GetNode<CollisionShape2D>("collision_shape_2d");
		polygon.Color = new Color(color_name);
		
		//draw square
		var vectors = polygon.Polygon;
		vectors[0].x = (-(width / 2));
		vectors[0].y = (-(height / 2));
		vectors[1].x = width / 2;
		vectors[1].y = (-(height / 2));
		vectors[2].x = width / 2;
		vectors[2].y = height / 2;
		vectors[3].x = (-(width / 2));
		vectors[3].y = height / 2;

		//vectors[0].Set(-(width / 2), -(height / 2));				// old api (before 3.2)
		//vectors[1].Set((width / 2), -(height / 2));
		//vectors[2].Set((width / 2), (height / 2));
		//vectors[3].Set(-(width / 2), (height / 2));
		polygon.Polygon = vectors;

		//set collision shape
		RectangleShape2D shape = new RectangleShape2D();
		shape.Extents = new Vector2(width / 2, height / 2);
		collision_shape_2d.Shape = shape;
	}


	public override void _Process(float delta)
	{
		if (enable_difficulty == true) {
			difficulty_coef = lvl.get_difficulty();
		}
		difficulty_coef = (difficulty_coef < max_diff_coef) ? difficulty_coef : max_diff_coef;

		if (rotation_speed_direction != 0) {
			float rotation = Rotation + rotation_speed_direction * delta;
			Rotation = rotation;
		}

		// change position
		if (move_speed > 0) {
			var current_pos = Position;

			if (distance_x > 0) {
				float direction_x = (move_direction_x == MoveDirectionX.Right)? 1 : -1;
				float step_x = (move_speed + difficulty_coef) * delta;
				complete_distance_x += step_x;
				current_pos.x = current_pos.x + step_x * direction_x;
			}

			if (distance_y > 0) {
				float direction_y = (move_direction_y == MoveDirectionY.Down) ? 1 : -1;
				float step_y = (move_speed + difficulty_coef) * delta;
				complete_distance_y += step_y;
				current_pos.y = current_pos.y + step_y * direction_y;
			}
			Position = current_pos;

			if (complete_distance_x > distance_x) {
				complete_distance_x = 0;
				move_direction_x = (move_direction_x == MoveDirectionX.Right) ? MoveDirectionX.Left : MoveDirectionX.Right;
			}

			if (complete_distance_y > distance_y)
			{
				complete_distance_y = 0;
				move_direction_y = (move_direction_y == MoveDirectionY.Down) ? MoveDirectionY.Up : MoveDirectionY.Down;
			}
		}
	}
}
