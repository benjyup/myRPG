using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	// Load the scene
	public void StartGame(){
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
	}
}
