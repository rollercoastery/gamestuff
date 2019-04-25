using UnityEngine;
using UnityEngine.UI;

/*
    For storing critical information such as gameplay information.
*/
[CreateAssetMenu(fileName = "GameplayData", menuName = "Assets/Global/GameplayData", order = 1)]
public class GameplayData : ScriptableObject {

    [Header("Global")]
    public float gameSpeed;

    [Header("Player Stats")]
    public float currentHealth;
    public float maxHealth;
    public uint foodCollected;
    public uint bodyCount;
    public float moveSpeed;
    public float turnSpeed;
    
    //Slider healthBar;

    [Header("Build Stats")]
    public bool isBuildMode;
    public float slowmoFuel;

    private void OnEnable()
    {
        gameSpeed = 1f;
        bodyCount = foodCollected = 0;
        currentHealth = maxHealth;
        //healthBar = GameObject.FindWithTag("HealthBar").GetComponent<Slider>();
    }

    /*
    void Awake ()
    {
        gameSpeed = 1f;
        currentHealth = maxHealth;
	}

    public void UpdateHealthBar(ref float prevHealth)
    {
        if (prevHealth != currentHealth)
        {
            float result = currentHealth / maxHealth;
            healthBar.value = result;
        }
    }*/
}
