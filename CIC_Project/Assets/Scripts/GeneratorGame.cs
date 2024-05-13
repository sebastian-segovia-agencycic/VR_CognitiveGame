
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Zinnia.Utility;

public class GeneratorGame : MonoBehaviour
{
    public static GeneratorGame Instance;
    public int randomColor, randomForm, randomCount;
    public int counterAttempts = 3;
    [SerializeField] private GameObject timerPanel;
    [SerializeField] private TMP_Text readyText, regresiveCounterText, counterAtempsText;
    [SerializeField] private Image imageForm;
    [SerializeField] private Color colorForm;
    [SerializeField] private List<Sprite> sprites = new List<Sprite>();
    [SerializeField] private List<Basket> basketList = new List<Basket>();
    private AudioSource audioSource;
    private float countdownTimer, countdownDuration;
    private bool paused = true, canWin = true;

    private void Awake()
    {
        Instance = this;
        ResetUI();
    }

    private void ResetUI()
    {
        imageForm.gameObject.SetActive(false);
        timerPanel.SetActive(false);
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
        if (counterAttempts <= 0)
        {

        }
    }

    public void InitGaneratedGame()
    {
        randomColor = Random.Range((int)ColorType.Yellow, (int)ColorType.Red);
        randomForm = Random.Range((int)FormType.Cube, (int)FormType.Sphere);
        randomCount = Random.Range(3, 10);
        imageForm.sprite = SetForm(randomForm);
        colorForm = SetColor(randomColor);
        imageForm.color = colorForm;
        
        StartCoroutine(StartReadyRegresiveCount());
    }

    IEnumerator StartCountdown()
    {
        while (true) // Mantenemos la corrutina ejecutándose para siempre
        {
            if (!paused)
            {
                countdownTimer -= Time.deltaTime;
                UpdateCountdownText(countdownTimer);
                Attempts();
            }
            yield return null;
        }
    }

    private void Attempts()
    {
        
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
        {
            basketList[0].basketSelected = true;
            basketList[1].basketSelected = false;
            basketList[2].basketSelected = false;

            basketList[0].formColor = (ColorType)this.randomColor;
            basketList[0].formType = (FormType)randomForm;
            basketList[0].countWinForms = randomCount;

            return Color.yellow;
        }
        else if (ramdonColor == (int)ColorType.Blue)
        {
            basketList[0].basketSelected = false;
            basketList[1].basketSelected = true;
            basketList[2].basketSelected = false;

            basketList[1].formColor = (ColorType)this.randomColor;
            basketList[1].formType = (FormType)randomForm;
            basketList[1].countWinForms = randomCount;
            return Color.blue;
        }  
        else
        {
            basketList[0].basketSelected = false;
            basketList[1].basketSelected = false;
            basketList[2].basketSelected = true;

            basketList[2].formColor = (ColorType)this.randomColor;
            basketList[2].formType = (FormType)randomForm;
            basketList[2].countWinForms = randomCount;

            return Color.red;
        }
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

        paused = false;
        timerPanel.SetActive(true);
        imageForm.gameObject.SetActive(true);
        readyText.text = randomCount.ToString();
    }

}