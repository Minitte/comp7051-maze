using UnityEngine;
/// <summary>
/// This is a class for maze tiles.
/// </summary>
public class MazeTile : MonoBehaviour {
	/// <summary>
	/// list of walls of the tile
	/// </summary>
	public GameObject[] walls;

	/// <summary>
	/// The floor of the tile
	/// </summary>
	public GameObject floor;

	/// <summary>
	/// The coordinate of the tile.
	/// </summary>
	public Coordinate coord;

	/// <summary>
	/// Toggle for day light
	/// </summary>
	private bool _dayToggle;

	/// <summary>
	/// key cooldown
	/// </summary>
	private bool _cooldown;

    private void Start() {
        _dayToggle = true; // Game starts on daytime
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update() {
		float dni = Input.GetAxis("DayNightInput");
		
		if (_cooldown) {
            if (dni > 0.1f) {
                return;
            } else {
                _cooldown = false;
            }
        }

		if (dni > 0.1f) {
			_dayToggle = !_dayToggle;
			_cooldown = true;

			float light = _dayToggle ? 1f : 0.1f;
            SoundManager.instance.Day = _dayToggle;

			for (int i = 0; i < walls.Length; i++) {
				if (walls[i] != null) {
					walls[i].GetComponent<Renderer>().material.SetFloat("_AmbientLighIntensity", light);
				}
			}

			floor.GetComponent<Renderer>().material.SetFloat("_AmbientLighIntensity", light);
		}
	}

	/// <summary>
	/// Breaks a wall in the input direction.
	/// </summary>
	/// <param name="index">The direction of the wall to break</param>
	public void BreakWall(int index) {
		if (walls[index] != null) {
			Destroy(walls[index]);
		} else {
			Debug.Log("Tried to destroy a nonexistent wall");
		}
	}
}
