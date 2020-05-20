using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Text;
using BackpackTeleport.Character;
using PolyNav;
using UnityEngine.Animations;
using UnityEditor.Animations;

public class CreateNewEnemyTypeWizard : EditorWindow
{
	private static string assetFilePath = "Assets/_Project/Game Data/Enemy Types";

	private string characterName;
	private float characterMoveSpeed;

	private float knockbackAmount;
	private float knockbackTime;
	private Color damageColor;

	private float attackCooldown;

	private float mass = 20f;
	private float maxSpeed = 3f;
	private float avoidanceRadius = 0.39f;
	private float stuckTime = 3f;
	private float reachedTime = 1f;

	private float rBodyMass = 1f;
	private float linearDrag = 10f;
	private CollisionDetectionMode2D collisionDetectionMode;
	private bool freezeZRotation;

	string assetName; // The name of the asset object.
	string folderName; // The name of the folder that the data objects will live in.
	string newPath; // The path that gets created when we input a character name.

	bool folderCreated = false;

	GameObject newEnemyObject;

	BaseCharacterData baseCharacterData;


	[MenuItem("Adrian's Tools/Create New Enemy Type")]
	private static void Init()
	{
		//ScriptableWizard.DisplayWizard<CreateNewEnemyTypeWizard>("Create Enemy", "Create New Enemy", "Update Selected Enemy"); // Might change update to cancel button
		CreateNewEnemyTypeWizard window = (CreateNewEnemyTypeWizard)EditorWindow.GetWindow(typeof(CreateNewEnemyTypeWizard));
		window.minSize = new Vector2(300, 200);
		window.Show();
	}

	private void InitializeFolder()
	{
		folderName = characterName + " Character Data";
		newPath = assetFilePath + "/" + folderName + "/";
		AssetDatabase.CreateFolder(assetFilePath, folderName); // Create the folder to store that enemy and its data in.

		//CreateBaseClass();

		EditorUtility.FocusProjectWindow();
		AssetDatabase.SaveAssets();

		Debug.Log("Enemy Type Created!");
	}

	private void OnCreate()
	{
		CreateDataObjects();
		SetCharacterStats();
		CreateGameObject();
		AssetDatabase.SaveAssets();
	}

	private void OnGUI()
	{
		var window = (CreateNewEnemyTypeWizard)EditorWindow.GetWindow(typeof(CreateNewEnemyTypeWizard)); // Reference the current window.
		GUI.backgroundColor = Color.grey;


		// Base Character Settings
		GUILayout.Space(5);
		GUILayout.Label("Base Character Settings", EditorStyles.boldLabel);
		GUILayout.Space(5);

		characterName = EditorGUILayout.TextField("Character Name", characterName);
		characterMoveSpeed = EditorGUILayout.FloatField("Character Move Speed", characterMoveSpeed);

		// Knockback Settings
		GUILayout.Space(5);
		GUILayout.Label("Knockback Settings", EditorStyles.boldLabel);
		GUILayout.Space(5);

		knockbackAmount = EditorGUILayout.FloatField("Knockback Amount", knockbackAmount);
		knockbackTime = EditorGUILayout.FloatField("Knockback Time", knockbackTime);
		damageColor = EditorGUILayout.ColorField("Damage Color", damageColor);

		// Attack Settings
		GUILayout.Space(5);
		GUILayout.Label("Attack Settings", EditorStyles.boldLabel);
		GUILayout.Space(5);

		attackCooldown = EditorGUILayout.FloatField("Attack Cooldown", attackCooldown);

		// Poly Nav Settings
		GUILayout.Space(5);
		GUILayout.Label("Poly Nav Settings", EditorStyles.boldLabel);
		GUILayout.Space(5);

		mass = EditorGUILayout.FloatField("Mass", mass);
		maxSpeed = EditorGUILayout.FloatField("Max Speed", maxSpeed);
		avoidanceRadius = EditorGUILayout.FloatField("Avoidance Radius", avoidanceRadius);
		stuckTime = EditorGUILayout.FloatField("Stuck Time", stuckTime);
		reachedTime = EditorGUILayout.FloatField("Reached Time", reachedTime);

		// Rigidbody2D Settings
		GUILayout.Space(5);
		GUILayout.Label("Rigidbody Settings", EditorStyles.boldLabel);
		GUILayout.Space(5);

		rBodyMass = EditorGUILayout.FloatField("RBody Mass", rBodyMass);
		linearDrag = EditorGUILayout.FloatField("Linear Drag", linearDrag);
		stuckTime = EditorGUILayout.FloatField("Stuck Time", stuckTime);
		reachedTime = EditorGUILayout.FloatField("Reached Time", reachedTime);

		if (GUI.Button(new Rect(95, 500, 100, 25), "Exit"))
		{
			window.Close();
		}

		if (GUI.Button(new Rect(10, 440, 100, 25), "Create Class"))
		{
			if (characterName == null)
			{
				EditorUtility.DisplayDialog("Error!", "Error: No type name found. Please input a character name.", "Ok");
			}
			else
			{
				InitializeFolder();
				CreateBaseClass();
				folderCreated = true;
			}
		}

		GUI.enabled = folderCreated;

		if (GUI.Button(new Rect(115, 440, 125, 25), "Create Enemy Type"))
		{
			OnCreate();
			window.Close();
		}

		/* groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
		myBool = EditorGUILayout.Toggle("Toggle", myBool);
		myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
		EditorGUILayout.EndToggleGroup(); */
	}

	private void CreateBaseClass()
	{
		string name = characterName;
		name = name.Replace("-", "_");
		string copyPath = newPath + name + ".cs";

		if (File.Exists(copyPath) == false)
		{
			using (StreamWriter outfile =
				 new StreamWriter(copyPath))
			{
				outfile.WriteLine("using UnityEngine;");
				outfile.WriteLine("using System.Collections;");
				outfile.WriteLine("using BackpackTeleport.Character.Enemy;");
				outfile.WriteLine("");
				outfile.WriteLine("public class " + name + " : BaseEnemy {");
				outfile.WriteLine(" ");
				outfile.WriteLine(" ");
				outfile.WriteLine(" public override void Awake () {");
				outfile.WriteLine("base.Awake(); ");
				outfile.WriteLine(" ");
				outfile.WriteLine(" }");
				outfile.WriteLine(" public override void Start () {");
				outfile.WriteLine("base.Start(); ");
				outfile.WriteLine(" ");
				outfile.WriteLine(" }");
				outfile.WriteLine(" ");
				outfile.WriteLine(" ");
				outfile.WriteLine(" public override void Update () {");
				outfile.WriteLine(" base.Update();");
				outfile.WriteLine(" ");
				outfile.WriteLine(" }");
				outfile.WriteLine("}");
			}
		}
		AssetDatabase.Refresh();
	}

	private void CreateDataObjects()
	{
		baseCharacterData = ScriptableObject.CreateInstance<BaseCharacterData>();

		AssetDatabase.CreateAsset(baseCharacterData, newPath + characterName + " Data.asset");
	}

	private void SetCharacterStats()
	{
		baseCharacterData.characterName = characterName;
	}

	private void CreateGameObject()
	{
		newEnemyObject = new GameObject(characterName);

		// Add Rigidbody2D
		AddAndConfigureRigidBody2D();

		// Add PolyNavAgent
		AddAndConfigurePolyNavAgent();

		// Add knockback
		AddAndConfigureKnockbackComponent();

		// Add animator
		CreateAnimatorControllerAndAddAnimator();

		newEnemyObject.AddComponent<SpriteRenderer>();

		AddAndConfigureBaseComponent();
	}

	private void AddAndConfigureKnockbackComponent()
	{
		Knockback knockback = newEnemyObject.AddComponent<Knockback>();
		knockback.KnockbackAmount = knockbackAmount;
		knockback.KnockbackTime = knockbackTime;
	}

	private void AddAndConfigureRigidBody2D()
	{
		Rigidbody2D newRBody2D = newEnemyObject.AddComponent<Rigidbody2D>();
		newRBody2D.mass = rBodyMass;
		newRBody2D.drag = linearDrag;
		newRBody2D.collisionDetectionMode = collisionDetectionMode;
		newRBody2D.freezeRotation = freezeZRotation;
	}

	private void AddAndConfigurePolyNavAgent()
	{
		PolyNavAgent newPolyNavAgent = newEnemyObject.AddComponent<PolyNavAgent>();
		newPolyNavAgent.mass = mass;
		newPolyNavAgent.maxSpeed = characterMoveSpeed;
		newPolyNavAgent.avoidRadius = avoidanceRadius;
		newPolyNavAgent.avoidanceConsiderStuckedTime = stuckTime;
		newPolyNavAgent.avoidanceConsiderReachedDistance = reachedTime;
	}

	private void AddAndConfigureBaseComponent()
	{
		Type newType = GetTypeWrapper.GetType(characterName);
		Debug.Log(newType.ToString());
		var newComponent = newEnemyObject.AddComponent(newType);
		//newComponent.GetComponent<CharacterMovement>().MoveSpeed = characterMoveSpeed;
		//newComponent.GetComponent<BaseDamageable>().BaseCharacterData = baseCharacterData;
		//newComponent.GetComponent<BaseDamageable>().DamageColor = damageColor;
	}

	private void CreateAnimatorControllerAndAddAnimator()
	{
		var animatorController = UnityEditor.Animations.AnimatorController.CreateAnimatorControllerAtPath(newPath + characterName + ".controller");
		//AssetDatabase.CreateAsset(animatorController, animationsPath + folderName + " Controller.controller");
		AssetDatabase.Refresh();

		Animator animator = newEnemyObject.AddComponent<Animator>();
		animator.runtimeAnimatorController = animatorController;

		AssetDatabase.SaveAssets();
	}
}
