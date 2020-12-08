using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class SaveManager : MonoBehaviour {

	private PlayerData player_data;

	// Manager References
	[SerializeField] PlayerManager manager_player;
	[SerializeField] EquipmentManager manager_equipment;
	[SerializeField] InventoryManager manager_inventory;

	public bool Initialize() {
		Debug.Log("Initializing SaveManager...");
		if(CheckSave()) {
			Debug.Log("Save File Detected. Loading from Save File");
			LoadGame();
			return true;
		}
		else {
			Debug.Log("No Save File Detected. Perfoming First Time Setup");
			return false;
		}
	}
	[System.Serializable]
	private class PlayerData {
		List<int> player_stats;
		List<int> player_skills;
		List<int> equipment;
		List<int> inventory;

		public PlayerData() {
			player_stats = new List<int>();
			player_skills = new List<int>();
			equipment = new List<int>();
			inventory = new List<int>();
		}

		public void SavePlayerStats(List<int> stats_save) {
			player_stats = stats_save;
		}

		public void SavePlayerSkills(List<int> skills_save) {
			player_skills = skills_save;
		}

		public void SaveEquipment(List<int> equipment_save) {
			equipment = equipment_save;
		}

		public void SaveInventory(List<int> inventory_save) {
			inventory = inventory_save;
		}

		public List<int> LoadPlayerStats() {
			return player_stats;
		}

		public List<int> LoadPlayerSkills() {
			return player_skills;
		}

		public List<int> LoadEquipment() {
			return equipment;
		}

		public List<int> LoadInventory() {
			return inventory;
		}

	}

	public void LoadGame() {
		if (File.Exists(System.IO.Directory.GetCurrentDirectory() + "/save_file.dat")) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.OpenRead(System.IO.Directory.GetCurrentDirectory() + "/save_file.dat");
			player_data = (PlayerData)bf.Deserialize(file);

			file.Close();
		}
		else {
			Debug.Log("SAVE_MANAGER::LOAD_GAME::Cannot find file at specified location: " + System.IO.Directory.GetCurrentDirectory() + "/save_file.dat");
		}
	}
	public void SaveGame() {
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Open(System.IO.Directory.GetCurrentDirectory() + "/save_file.dat", FileMode.OpenOrCreate);
		Debug.Log("File Saved to: " + System.IO.Directory.GetCurrentDirectory() + "/save_file.dat");
		player_data = new PlayerData();

		player_data.SavePlayerStats(manager_player.SavePlayerStats());
		player_data.SavePlayerSkills(manager_player.SavePlayerSkills());
		player_data.SaveEquipment(manager_equipment.SaveEquipment());
		player_data.SaveInventory(manager_inventory.SaveInventory());

		bf.Serialize(file, player_data);

		file.Close();
	}

	public bool CheckSave() {
		if(File.Exists(System.IO.Directory.GetCurrentDirectory() + "/save_file.dat")) {
			return true;
		}
		return false;
	}

	public List<int> LoadPlayerStats() {
		if(player_data == null) {
			LoadGame();
		}
		return player_data.LoadPlayerStats();
	}

	public List<int> LoadPlayerSkills() {
		if (player_data == null) {
			LoadGame();
		}
		return player_data.LoadPlayerSkills();
	}

	public List<int> LoadEquipment() {
		if (player_data == null) {
			LoadGame();
		}
		return player_data.LoadEquipment();
	}

	public List<int> LoadInventory() {
		if (player_data == null) {
			LoadGame();
		}
		return player_data.LoadInventory();
	}
}
