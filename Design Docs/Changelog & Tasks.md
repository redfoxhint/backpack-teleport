# Header

[August 27th 2019]
	- [Addition] Implemented new event system in form of a tutorial level.
	- [Addition] Added new tutorial level featuring a walkthrough on how to use the backpack to teleport around the map.
	- [Addition] Added new dialogue system.
	- [Change] UI elements representing the player stats will now be updated using events.
	- [Change] Player Stats are now a scriptable object. This makes it easier to add any stats that we want the player or other entity to have.

[Tasks]
	- [Feature] Start working on player combat. (Melee attack, on teleport AOE, backpack damage)
	- [Fix] Rework tutorial level to store a list of tutorial segments. Use reordered list library.

[Tutorial System]

Classes needed:

	- TutorialSegement : ScriptableObject
	{
		Each segment will be composed of 1 dialog object which will hold the sentences that belong to the tutorial.
		It will also contain variables for the tutorial name, and the event which it must listen to.
	}

	- TutorialManager : Monobehavior
	{
		The tutorial manager will be responsible for displaying the tutorial.
		It will also be responsible for catching the event that was raised.
	}


[August 28th 2019] HAPPY BIRTHDAY!

[August 30th 2019]
		
	- [Addition] Added new spider enemy animations.

[August 31st 2019]
	- [Addition] Any BaseCharacter entity will now recieve a knockback effect when recieving damage.
	- [Change] Enemy types will now reference the BaseEnemy class instead of the BaseCharacter Class.
			   This will allow for custom code to be applied to enemy types that require an extra level of depth.
	- [Addition] Added new dash ability.
	- [Addition] Added dash ability ghosting effect.
	- [Change] Player now derives from BaseCharacter.
	- [Change] Removed Force Mechanic.
	- [Change] Player can now choose what type of dash aim type they want to use. Move Direction will
			   mean the player will dash in the direction it's moving. Mouse Direction will mean the player 
			   will dash in the direction of where the mouse is on the screen.

[Septemeber 3rd 2019]

	- [Addition] Started work on editor tool for creating new enemy types.

	New Character Wizard
	{
		Function: Automatic creation and setup of new character types.

		Variables needed: (Initial Values. Values can be tweaked in the inspector individually.)
			Movement Setup:
			- Move speed
			- Attack cooldown time

			Knockback Setup:
			- Knocback amount
			- Knockback time

			Poly nav default settings:
			- Mass = 20
			- Max speed = base move speed
			- Avoidance radius = 0.39
			- Stuck time = 3
			- Reached time = 1

			Rigidbody2D default settings:
			- Mass  = 1
			- Linear Drag = 10
			- Collision Dectection = Continuous
			- Freeze rotation on the z axis

		Objects to create:
			- New C# class which derives from BaseEnemy
			- New base character data object
			- Animator component

		Components to add automatically:
			- Animator
			- Sprite Renderer
			- Newly created c# class
			- Poly Nav Agent
			- RigidBody2D
			- Knockback
			- Circle Collider
	}