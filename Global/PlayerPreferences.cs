using UnityEngine;

/*
    For storing non-critical information such as player preferences in the game options menu.
*/
[CreateAssetMenu(fileName = "PlayerPreferences", menuName = "Assets/Global/PlayerPreferences", order = 2)]
public class PlayerPreferences : ScriptableObject {

    [Header("Audio")]
    public float masterVolume;
    public float musicVolume;
    public float sfxVolume;

    [Header("UI")]
    public float fontSize;
}
