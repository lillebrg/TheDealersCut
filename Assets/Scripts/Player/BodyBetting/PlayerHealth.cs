using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    private float health;
    private float lerpTimer;
    public float maxHealth = 100f;
    public float chipSpeed = 2f;

    public TextMeshProUGUI healthText;

    public Image frontHealthBar;
    public Image backHealthBar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthUI();
    }

    public void UpdateHealthUI()
    {
        float hFraction = health / maxHealth;

        if (frontHealthBar.fillAmount > hFraction) // Taking damage
        {
            frontHealthBar.fillAmount = hFraction;
            // Optional: smooth lerp for back bar
            lerpTimer += Time.deltaTime;
            frontHealthBar.fillAmount = Mathf.Lerp(frontHealthBar.fillAmount, hFraction, lerpTimer / chipSpeed);
        } 
    }

    public void HealthLost()
    {        float hFraction = health / maxHealth;

        if (backHealthBar.fillAmount > hFraction) // Taking damage
        {
            backHealthBar.fillAmount = hFraction;
            // Optional: smooth lerp for back bar
            lerpTimer += Time.deltaTime;
            backHealthBar.fillAmount = Mathf.Lerp(backHealthBar.fillAmount, hFraction, lerpTimer / chipSpeed);
            healthText.text = $"{Mathf.RoundToInt(health)}";
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        lerpTimer = 0f;
    }

    public void Heal(float heal)
    {
        health += heal;
        lerpTimer = 0f;
    }
}
