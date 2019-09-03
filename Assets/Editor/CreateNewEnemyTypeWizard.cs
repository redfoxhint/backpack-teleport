using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Text;
using BackpackTeleport.Character;
using PolyNav;

public class CreateNewEnemyTypeWizard : ScriptableWizard
{
	private static string assetFilePath = "Assets/_Project/Game Data/Enemy Types";

	[Header("Base Character Settings")]
	public string characterName;

	string assetName; // The name of the asset object.
	string folderName; // The name of the folder that the data objects will live in.
	string newPath; // The path that gets created when we input a character name.

	BaseCharacterData baseCharacterData;

	[MenuItem("Adrian's Tools/Create New Enemy Type")]
	private static void MenuEntryCall()
	{
		ScriptableWizard.DisplayWizard<CreateNewEnemyTypeWizard>("Create Enemy", "Create New Enemy", "Update Selected Enemy"); // Might change update to cancel button
	}

	private void OnWizardCreate()
	{
		folderName = characterName + " Character Data";
		newPath = assetFilePath + "/" + folderName + "/";
		AssetDatabase.CreateFolder(assetFilePath, folderName); // Create the folder to store that enemy and its data in.

		CreateDataObjects();
		SetCharacterStats();
		CreateCharacterClass();
		CreateGameObject();

		EditorUtility.FocusProjectWindow();
		AssetDatabase.SaveAssets();

		Debug.Log("Enemy Type Created!");
	}

	private void CreateCharacterClass()
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
		GameObject newEnemyObject = new GameObject(characterName);


		newEnemyObject.AddComponent<SpriteRenderer>();
		newEnemyObject.AddComponent<Rigidbody2D>();
		newEnemyObject.AddComponent<PolyNavAgent>();
		newEnemyObject.AddComponent<Animator>();
		newEnemyObject.AddComponent<Knockback>();
	}
}
