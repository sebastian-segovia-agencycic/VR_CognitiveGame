
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Zinnia.Utility;

public class GeneratorGame : MonoBehaviour
{
    public static GeneratorGame Instance;
    public InteractableForm winInteractableForm;
    public int ramdonColor, ramdonForm, countRamdon;

    [SerializeField] private TMP_Text readyText, regresiveCounterText;
    [SerializeField] private Image imageForm;
    [SerializeField] private Color colorForm;
    [SerializeField] private List<Sprite> sprites = new List<Sprite>();
    [SerializeField] private List<Basket> basketList = new List<Basket>(); 

    private AudioSource audioSource;
    private float countdownTimer, countdownDuration;
    private bool paused = true;

    private void Awake()
    {
        Instance = this;
        imageForm.gameObject.SetActive(false);
        ResetUI();
    }

    private void ResetUI()
    {
        countdownDuration = 60 * 5;
        countdownTimer = countdownDuration;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(StartCountdown());
    }

    private void Update()
    {

    }

    public void InitGaneratedGame()
    {
        ramdonColor = Random.Range((int)ColorType.Yellow, (int)ColorType.Red);
        ramdonForm = Random.Range((int)FormType.Cube, (int)FormType.Sphere);
        countRamdon = Random.Range(1, 9);
        imageForm.sprite = SetForm(ramdonForm);
        colorForm = SetColor(ramdonColor);
        imageForm.color = colorForm;
       
        StartReadyRegresiveCount();
    }

    IEnumerator StartCountdown()
    {
        float timer = countdownDuration;

        while (true) // Mantenemos la corrutina ejecutándose para siempre
        {
            if (!paused)
            {
                timer -= Time.deltaTime;
                UpdateCountdownText(timer);
            }
            yield return null;
        }
    }

    void UpdateCountdownText(float timeRemaining)
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        int milliseconds = Mathf.FloorToInt((timeRemaining * 1000) % 1000); // Calcula los milisegundos

        // Actualizar el texto TMP con el tiempo restante formateado
        regresiveCounterText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }

    private Sprite SetForm(int ramdonForm)
    {
        if (ramdonForm == (int)FormType.Cube)
            return sprites[0];
        else if (ramdonForm == (int)FormType.Prism)
            return sprites[1];
        else
            return sprites[2];
    }

    private Color SetColor(int ramdonColor)
    {
        if (ramdonColor == (int)ColorType.Yellow)
            return Color.yellow;
        else if (ramdonColor == (int)ColorType.Blue)
            return Color.blue;
        else
            return Color.red;
    }

    private IEnumerator StartReadyRegresiveCount()
    {
        readyText.text = "¿Estas Listo?";
        yield return new WaitForSeconds(3f);
        readyText.text = "3";
        yield return new WaitForSeconds(1f);
        readyText.text = "2";
        yield return new WaitForSeconds(1f);
        readyText.text = "1";
        yield return new WaitForSeconds(1f);
        readyText.text = "¡Adelante!";
        yield return new WaitForSeconds(1f);

        paused = true;
        imageForm.gameObject.SetActive(true);
        readyText.text = countRamdon.ToString();
    }

}