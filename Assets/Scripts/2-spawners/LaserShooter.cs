using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; 


/**
 * This component spawns the given laser-prefab whenever the player clicks a given key.
 * It also:
 *  - Updates the score when a laser hits an enemy.
 *  - Enforces a cooldown between shots.
 *  - Limits the amount of ammo per level and moves to the next level when ammo is empty.
 *  - Updates a HUD NumberField showing remaining ammo.
 */
public class LaserShooter : ClickSpawner
{
    [Header("Score")]
    [SerializeField]
    [Tooltip("How many points to add to the shooter, if the laser hits its target")]
    private int pointsToAdd = 1;

    [SerializeField]
    [Tooltip("Reference to the NumberField that shows the player's score (HUD in the top-right).")]
    private NumberField scoreField;

    [Header("Ammo")]
    [SerializeField]
    [Tooltip("How many shots the player has at the beginning of the level.")]
    private int maxAmmo = 20;

    [SerializeField]
    [Tooltip("When ammo is at or below this value, color changes to 'low ammo' color.")]
    private int lowAmmoThreshold = 3;

    [SerializeField]
    private Color normalAmmoColor = Color.white;

    [SerializeField]
    private Color lowAmmoColor = Color.yellow;

    [SerializeField]
    private Color emptyAmmoColor = Color.red;

    private TMP_Text ammoText;   // cached reference to the text component


    [SerializeField]
    [Tooltip("NumberField that shows remaining ammo (HUD in the top-left).")]
    private NumberField ammoField;

    [Header("Shooting limits")]
    [SerializeField]
    [Tooltip("Minimum time (in seconds) between two shots.")]
    private float fireCooldown = 0.5f;

    [Header("Level flow")]
    [SerializeField]
    [Tooltip("Name of the scene to load when the player runs out of ammo.")]
    private string nextSceneName;

    [Header("Out of Ammo UI")]
    [SerializeField]
    private GameObject outOfAmmoTextObject;

    [SerializeField]
    private float outOfAmmoDelay = 2f; // seconds to wait before next scene


    private float nextAllowedShotTime = 0f;
    private int currentAmmo;

    private void Start()
    {
        // Refill ammo at the start of each level
        currentAmmo = maxAmmo;
        UpdateAmmoUI();
        if (ammoField != null)
        {
            ammoText = ammoField.GetComponent<TMP_Text>();
        }


        // Try to auto-find a NumberField for the score if not assigned in the Inspector
        if (!scoreField)
        {
#if UNITY_2022_1_OR_NEWER
            scoreField = FindAnyObjectByType<NumberField>();
#else
            scoreField = FindObjectOfType<NumberField>();
#endif
        }

        if (!scoreField)
        {
            UnityEngine.Debug.LogError(
                $"LaserShooter on {gameObject.name} could not find a NumberField for the score. " +
                $"Please assign it in the Inspector.");
        }
    }

    private System.Collections.IEnumerator HandleOutOfAmmo()
    {
        // Show the "Out of ammo!" text if assigned
        if (outOfAmmoTextObject != null)
        {
            outOfAmmoTextObject.SetActive(true);
        }

        // Wait a bit so the player can read it
        yield return new WaitForSeconds(outOfAmmoDelay);

        // Then go to the next level
        GoToNextLevel();
    }


    private void Update()
    {
        // If the input action wasn't pressed this frame, nothing to do
        if (!spawnAction.WasPressedThisFrame())
            return;

        // Cooldown: too soon since last shot
        if (Time.time < nextAllowedShotTime)
            return;

        // No ammo left: can't shoot, go to next level
        if (currentAmmo <= 0)
        {
            return;
        }

        ShootOnce();
    }

    private void ShootOnce()
    {
        // Consume ammo and set next allowed shot time
        currentAmmo--;
        nextAllowedShotTime = Time.time + fireCooldown;
        UpdateAmmoUI();

        // Actually spawn the laser
        GameObject newObject = spawnObject();

        // If that was the last bullet, immediately go to next level
        if (currentAmmo <= 0)
        {
            StartCoroutine(HandleOutOfAmmo());
        }

    }

    private void UpdateAmmoUI()
    {
        if (ammoField != null)
        {
            ammoField.SetNumber(currentAmmo);
        }

        if (ammoText != null)
        {
            if (currentAmmo <= 0)
            {
                ammoText.color = emptyAmmoColor;
            }
            else if (currentAmmo <= lowAmmoThreshold)
            {
                ammoText.color = lowAmmoColor;
            }
            else
            {
                ammoText.color = normalAmmoColor;
            }
        }
    }


    private void GoToNextLevel()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            UnityEngine.Debug.LogWarning(
                "LaserShooter: out of ammo but 'nextSceneName' is not set on the component.");
        }
    }

    private void AddScore()
    {
        if (scoreField != null)
        {
            scoreField.AddNumber(pointsToAdd);
        }
    }

    // Attach our score callback to the laser's DestroyOnTrigger2D
    protected override GameObject spawnObject()
    {
        GameObject newObject = base.spawnObject();  // base = ClickSpawner

        if (newObject == null)
            return null;

        DestroyOnTrigger2D newObjectDestroyer = newObject.GetComponent<DestroyOnTrigger2D>();
        if (newObjectDestroyer != null)
        {
            newObjectDestroyer.onHit += AddScore;
        }

        return newObject;
    }

    // Public helper if you ever want to refill ammo from another script
    public void RefillAmmo()
    {
        currentAmmo = maxAmmo;
        UpdateAmmoUI();
    }
}
