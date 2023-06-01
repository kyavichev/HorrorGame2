using UnityEngine;

public class PlayerPrefExample : MonoBehaviour
{
	// Key for the Player Prefs. Should be unique.
	public const string ExampleKey = "example_key";
	
	
	// This function gets called whenever a collider enters the trigger zone
	private void OnTriggerEnter(Collider other)
	{
		// Current example value
		int currentValue = 0;

		// If it has been saved, read it
		if (PlayerPrefs.HasKey(ExampleKey))
		{
			currentValue = PlayerPrefs.GetInt(ExampleKey);
		}
	
		// Create a new value that is old value + 1
		int newValue = currentValue + 1;
		
		// Save the new value in Player Prefs
		PlayerPrefs.SetInt(ExampleKey, newValue);
		PlayerPrefs.Save();
		
		// Print to console for fun
		Debug.Log($"Updating Example Player Pref value {currentValue} -> {newValue}");
	}
}
