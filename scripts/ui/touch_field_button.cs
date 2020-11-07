using Godot;
using System;

public class touch_field_button : TextureRect
{
	[Signal]
	public delegate void rotation_touch(float rotation);

	Vector2 centerlJousticPoint;

	public override void _Ready()
	{
		centerlJousticPoint = new Vector2(this.RectSize.x / 2, this.RectSize.y / 2);
	}

	public override void _Input(InputEvent @event)
	{
		var imput_local = this.MakeInputLocal(@event);

		if (imput_local is InputEventScreenDrag touch_drag) {
			float rotation = touch_drag.Position.AngleToPoint(centerlJousticPoint);
			EmitSignal("rotation_touch", rotation);
		}

		if (imput_local is InputEventScreenTouch touch && @event.IsPressed()) {
			float rotation = touch.Position.AngleToPoint(centerlJousticPoint);
			EmitSignal("rotation_touch", rotation);
		}
	}

}
