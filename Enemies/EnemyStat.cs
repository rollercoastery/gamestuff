using UnityEngine;

/*
    For storing non-critical information such as player preferences in the game options menu.
*/
[CreateAssetMenu(fileName = "EnemyStat", menuName = "Assets/Enemies/EnemyStat", order = 2)]
public class EnemyStat : ScriptableObject {

    [Header("Basic")]
    public uint health;

    [Header("Movement")]
    [Tooltip("Movement type that will eventually be set after first spawn period")]
    public MovementType movementType;       // Movement type that will eventually be set after spawning period
    public float moveSpeed;
    public float turnSpeed;

    public enum MovementType
    {
        Straight,       // Moves in a straight line when just spawned
        Seeking,        // Seeks target in a straight line
        Random,         // Moves randomly
        Distance,       // Keeps a small randomized range of distance from the target
        Teleport        // Teleports towards the target in steps
    }

}
