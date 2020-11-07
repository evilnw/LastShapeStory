using Godot;
using System;

public class player : KinematicBody2D
{
	// Declare member variables here.
	[Export]
	private float speed = 4;    
	[Export]
	float rotation_speed = 5f;
	
	float rotation_dir = 0;                     //  direction
	float final_rotation = 0;
	float correction_rotation = 0.1f;

	Polygon2D tail_1;
	Polygon2D tail_2;
	Polygon2D tail_3;
	Polygon2D body;
	Polygon2D window;
	Timer timer;
	const int tail_max_count_animation = 3;
	int count_animation = 1;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		timer = GetNode<Timer>("timer");
		body = GetNode<Polygon2D>("body_polygon");
		window = GetNode<Polygon2D>("window_polygon");
		tail_1 = GetNode<Polygon2D>("tail_polygon_1");
		tail_2 = GetNode<Polygon2D>("tail_polygon_2");
		tail_3 = GetNode<Polygon2D>("tail_polygon_3");
		//Settings settings = (Settings)GetNode("/root/Settings")
		Color body_color = new Color("3f48c2");
		Color tail_color = new Color("dc435b");
		Color window_color = new Color("ffeb33");

		body.Color = body_color;
		tail_1.Color = tail_color;
		tail_2.Color = tail_color;
		tail_3.Color = tail_color;
		window.Color = window_color;
	}

	private void timer_animation_timeout()
	{
		if(count_animation == 1) {
			tail_1.Visible = true;
			tail_2.Visible = false;
			tail_3.Visible = false;
		}
		else if (count_animation == 2) {
			tail_1.Visible = false;
			tail_2.Visible = true;
			tail_3.Visible = false;
		}
		else if (count_animation == 3) {
			tail_1.Visible = false;
			tail_2.Visible = false;
			tail_3.Visible = true;
		}
		
		count_animation++;
		count_animation = (count_animation <= tail_max_count_animation) ? count_animation : 1;
		//tail_1.Visible = (tail_1.Visible == true) ? false : true;
	}

	public override void _PhysicsProcess(float delta)
	{
		calcRotation();
		Rotation = Rotation + rotation_dir * rotation_speed * delta;
		Vector2 velocity = new Vector2(speed, 0).Rotated(Rotation);
		velocity = velocity.Normalized() * speed;
		var collision_info = MoveAndCollide(velocity);

		if (collision_info != null) {
			collision_handing(collision_info);
		}
	}

	private void collision_handing(KinematicCollision2D collision)
	{
		var collide_obj = collision.Collider;

		if (collide_obj is TileMap) {           // walls
			kill_player();                      //stop any collide, stop player, play death animation, game over

		}
		else if (collide_obj is PhysicsBody2D)       // enemy or bonus
		{
			var collide_body = (PhysicsBody2D)collide_obj;

			if (collide_body.GetCollisionLayerBit(2)) {  // bonus
					var bonus_object = (bonus)collide_body;
					level lvl = (level)GetNode("/root/level");
					lvl?.add_score_from_bonus_object(bonus_object.get_score_bonus());
					bonus_object.QueueFree();
				}
			else {
				kill_player();
			}
		}
	}

	public void kill_player()
	{
		this.Hide();
		timer.Stop();
		tail_1.Visible = false;
		tail_2.Visible = false;
		tail_3.Rotation = this.Rotation;
		window.Rotation = this.Rotation;

		this.SetCollisionLayerBit(0, false);
		this.SetCollisionLayerBit(1, false);
		this.rotation_speed = 0;
		this.speed = 0;

		level lvl = (level)GetNode("/root/level");
		lvl?.game_over();
	}

	private void calcRotation()
	{
		if (Rotation < final_rotation) {
			rotation_dir = 1;
			if (Rotation + 3 < final_rotation) {
				rotation_dir = -1;
			}
		}
		else if (Rotation > final_rotation) {
			rotation_dir = -1;
			if (Rotation - 3 > final_rotation) {
				rotation_dir = 1;
			}
		}
		// move forward
		if (Rotation - correction_rotation < final_rotation
			&& Rotation + correction_rotation > final_rotation) {
			rotation_dir = 0;
		}
	}

	private void rotation_inc_msg(float rotation)
	{
		final_rotation = rotation;
	}
}
