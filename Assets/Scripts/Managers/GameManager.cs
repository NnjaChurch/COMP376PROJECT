using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	[SerializeField] SaveManager manager_save;
	[SerializeField] StageManager manager_stage;
	[SerializeField] PlayerManager manager_player;
	[SerializeField] EquipmentManager manager_equipment;
	[SerializeField] InventoryManager manager_inventory;
	[SerializeField] InputManager manager_input;
	[SerializeField] LootManager manager_loot;
	[SerializeField] SpawnManager manager_spawn;
	[SerializeField] LootSpawnManager manager_lootspawn;
	[SerializeField] AudioManager manager_audio;
	[SerializeField] UIManager manager_ui;

	void Start() {
		// Initialize Game
		Debug.Log("Initializing Game...");
		// Initialize Managers (Returning booleans so it waits until the Initialize is complete before proceeding, also for potential error detection)
		_ = manager_save.Initialize();
		_ = manager_stage.Initialize();
		_ = manager_player.Initialize();
		_ = manager_loot.Initialize();
		_ = manager_equipment.Initialize();
		_ = manager_inventory.Initialize();
		_ = manager_input.Initialize();
		_ = manager_spawn.Initialize();
		_ = manager_lootspawn.Initialize();
		_ = manager_audio.Initialize();
		_ = manager_ui.Initialize();
		Debug.Log("Game Initialized!");
	}

	// Update is called once per frame
	void Update() {

	}
}
