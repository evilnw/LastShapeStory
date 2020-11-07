using Godot;
using System;

public class circle : KinematicBody2D
{
	[Export]
	string color_name = "#ef7d57";              // format #ef7d57
	[Export]
	float radius = 50;
	[Export]
	float velocity_x = 100;
	[Export]
	float velocity_y = 100;
	[Export]
	bool enable_difficulty = true;
	int difficulty_coef = 1;
	const int max_diff_coef = 100;

	Vector2 velocity;
	CollisionShape2D collision_shape_2d;
	level lvl;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		lvl = (level)GetNode("/root/level");
		velocity = new Vector2(velocity_x, velocity_y);
		collision_shape_2d = GetNode<CollisionShape2D>("collision_shape_2d");

		CircleShape2D shape = new CircleShape2D();
		shape.Radius = radius;
		collision_shape_2d.Shape = shape;
	}

	public override void _Draw()
	{
		this.DrawCircle(new Vector2(0, 0), radius, new Color(color_name));
	}

	public override void _PhysicsProcess(float delta)
	{
		if (enable_difficulty == true) {
			difficulty_coef = lvl.get_difficulty();
			difficulty_coef = (difficulty_coef < max_diff_coef) ? difficulty_coef : max_diff_coef;
			difficulty_coef = 1 + difficulty_coef / max_diff_coef;
		}

		var collision_info = MoveAndCollide(velocity * delta * difficulty_coef);
		if (collision_info != null) {
			var reflect = collision_info.Remainder.Bounce(collision_info.Normal);
			velocity = velocity.Bounce(collision_info.Normal);
			this.MoveAndCollide(reflect);

			var collide_obj = collision_info.Collider;
			if (collide_obj is PhysicsBody2D) {
				var collide_body = (PhysicsBody2D)collide_obj;
				if (collide_body.GetCollisionLayerBit(0)) {             // player
					var player_object = (player)collide_body;
					player_object.kill_player();
				}
			}
		}
	}
}
