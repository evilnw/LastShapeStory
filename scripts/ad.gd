extends Node2D


# Declare member variables here. Examples:
# var a = 2
# var b = "text"

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass

func _show_banner():
	Admanager.showBanner()

func _hide_banner():
	Admanager.hideBanner()
