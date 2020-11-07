using Godot;
using System;

public class bonus : KinematicBody2D
{
	Polygon2D tail_1;
	Polygon2D tail_2;
	Polygon2D tail_3;
	Polygon2D body;
	Polygon2D window;
	Label score_label;
	
	const int tail_max_count_animation = 3;
	int count_animation = 1;
	private Vector2 velocity;
	private float rotation_x = 0;
	private float rotation_y = 0;

	float life_time = 10;               // seconds
	float time_step = 0.5f;
	int score = 5;
	Timer timer;

	public override void _Ready()
	{ 
		body = GetNode<Polygon2D>("body_polygon");
		tail_1 = GetNode<Polygon2D>("tail_polygon_1");
		tail_2 = GetNode<Polygon2D>("tail_polygon_2");
		tail_3 = GetNode<Polygon2D>("tail_polygon_3");
		score_label = GetNode<Label>("score_label");
		score_label.Text = score.ToString();

		velocity = new Vector2(0, 0);
		Rotation = velocity.Angle();

		timer = GetNode<Timer>("timer");
		timer.Start(time_step);
	}
	
	public void set_rotation_bonus(float rt_x, float rt_y)
	{
		rotation_x = rt_x;
		rotation_y = rt_y;
	}
	
	public int get_score_bonus()
	{
		return score;
	}

	public void set_lifetime_bonus(float lifetime_sec)
	{
		life_time = lifetime_sec;
	}

	public void set_velocity_bonus(Vector2 velocity_vec)
	{
		velocity = velocity_vec;
		Rotation = velocity.Angle();
	}

	public void set_score_bonus(int bonus_score)
	{
		score = bonus_score;
		score_label.Text = bonus_score.ToString();
	}

	private void timer_timeout()
	{
		life_time -= time_step;
		if (life_time < 5)
		{
			if (Visible == true)
			{
				this.Hide();
			}
			else
			{
				this.Show();
			}
		}
		if (life_time <= 0) // destroy object
		{    
			this.QueueFree();
		}

		// animation
		if (count_animation == 1) {
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

	}

	public override void _PhysicsProcess(float delta)
	{
		velocity.x += rotation_x;
		velocity.y -= rotation_y;
		Rotation = velocity.Angle();

		var collision_info = MoveAndCollide(velocity * delta);
		if (collision_info != null)
		{
			collision_handing(collision_info);
		}
	}

	private void collision_handing(KinematicCollision2D collision)
	{
		var collide_obj = collision.Collider;
		if (collide_obj is TileMap)               // walls
		{
			this.QueueFree();                     //delete bonus
		}
	}
}
