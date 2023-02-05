using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public GameObject titulo;
    public GameObject botonTitulo; 
    public GameObject instrucciones;
    public GameObject conteoRegresivo;
    public TextMeshPro timer;
    public int maxTime;
    public TextMeshPro scoreText;
    public GameObject caldero;
    public GameObject gameOverText;
    public ParticleSystem confeti;
    public GameObject winningsPanel;

    public static int score;
    public int Score
    {
        set
        {
            score = value;
            scoreText.text = Score.ToString();
        }
        get
        {
            return score;
        }
    }

    [HideInInspector] public bool introInPos = false; 
    [HideInInspector] public bool tutorialInPos = false; 
    [HideInInspector] public bool countdownDone = false; 
    [HideInInspector] public bool gameBegan = false; 
    [HideInInspector] public bool gameOver = false;
    [HideInInspector] public int etapa; 
    [HideInInspector] public bool winningsInPos = false; 
    [HideInInspector] public bool readyToShowWinnings = false; 
    [HideInInspector] public bool readyToRestart = false;

    private Coroutine timerCoroutine;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);
    }

    void Start()
    {
        Score = 0;

        //----------------------------------------------------INICIO-------------------------------------------------------------------
        StartCoroutine(Intro());    //co runtina para esperar a que todos los elementos de la intro este en su lugar
    }
    void Update()
    {
        //----------------------------------------------------TUTORIAL----------------------------------------------------------------
        //Esconde titulo y MUESTRA el TUTORIAL 
        if (introInPos && (Input.GetKeyDown(KeyCode.Return)))
        {
            introInPos = false;
            Debug.Log("Inicia Tutorial");
            titulo.GetComponent<TweenInAndOut>().HideGameObject();
            botonTitulo.GetComponent<TweenInAndOut>().HideGameObject();
            instrucciones.SetActive(true);
            StartCoroutine(TutorialInPosDelay(1.8f));               //espera 1.8s para permitir salir del tutorial
        }
        //--------------------------------------------------CONTEOREGRESIVO-------------------------------------------------------------
        //esconde tutorial y MUESTRA CONTEO REGRESIVO
        if (tutorialInPos && (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.G)))
        {
            titulo.SetActive(false);
            botonTitulo.SetActive(false);
            tutorialInPos = false;
            Debug.Log("Inicia ConteoRegresivo");
            instrucciones.GetComponent<TweenInAndOut>().HideGameObject();           //esconde el panel de tutorial
            conteoRegresivo.SetActive(true);   
            timer.gameObject.SetActive(true);
            timer.text = maxTime.ToString();
            Score = 0;
            
            //AQUI SE AGREGARIAN ANIMACIONES POSIBLES DE OBJETOS EN LA ESCENA MIENTRAS CORRE EL CONTEOREGRESIVO PARA INICIAR JUEGO
        }

        //-------------------------------------------------------JUEGO------------------------------------------------------------------
        if (countdownDone)      //cuando el conteoRegresivo en ChangeTextInAnimations llega a cero, countdownDone es true
        {
            countdownDone = false;
            instrucciones.SetActive(false);
            Debug.Log("termina conteo");
            conteoRegresivo.SetActive(false);
            timerCoroutine = StartCoroutine(Timer(0, maxTime));
            gameBegan = true;
            etapa = 0;
            Debug.Log("etapa: " + etapa + 1);
        }

        if (gameBegan)
        {
            //SinglePlayerGame(); AGREGAR AQUI FASE DE JUEGO
        }
        /*else if (gameBegan && multiplayer) POR SI ACASO HAY OTRO MODO DE JUEGO
        {
            MultiPlayerGame();
        }*/
        //AL TERMINAR JUEGO, PONER VARIABLE gameOver = true

        //-----------------------------------------------------GAME OVER----------------------------------------------------------------
        if (gameOver && !winningsInPos && !readyToShowWinnings)
        {
            gameOverText.gameObject.SetActive(true);
            /*
            dagaCollectionsIcons.GetComponent<TweenInAndOut>().HideGameObject();        ESPACIO PARA SALIDA DE OBJETOS QUE SE REQUIERAN QUE SALGAN DE ESCENA CUANDO TERMINA JUEGO
            daga2CollectionsIcons.GetComponent<TweenInAndOut>().HideGameObject();
            daga3CollectionsIcons.GetComponent<TweenInAndOut>().HideGameObject();
            */
            StartCoroutine(WinningsInPosDelay(1.5f));                  //hace a la variable winningsInPos true despues de 1.5 segundos

            //Debug.Log("Game Over");
            readyToShowWinnings = true;
        }
        //------------------------------------------------------PREMIOS-----------------------------------------------------------------
        if (winningsInPos && readyToShowWinnings)
        {
            winningsInPos = false;
            Debug.Log("mostrando puntaje final");
            gameOverText.SetActive(false);
            foreach (GameObject panel in GameObject.FindGameObjectsWithTag("panel"))//ESTE LOOP SE PUEDE USAR PARA DESACTIVAR OBJETOS CON TAG panel
            {
                panel.SetActive(false);
            }
            
            winningsPanel.SetActive(true);

            StartCoroutine(ReadyToRestart());
        }

        //--------------------------------------------------SALIDA DEL JUEGO--------------------------------------------------------
        if (readyToRestart)
        {
            if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) || Input.GetKey(KeyCode.G))       //presionar SI o NO
            {
                if (true)//jugador elige reiniciar juego)
                {
                    //setear flags a false o true segun necesite el codigo para reiniciar
                }
                else
                    Application.Quit(); //se cierra el juego
            }
        }

    }//fin de Update


    IEnumerator Timer(float delay, int remainingTime)
    {
        yield return new WaitForSeconds(delay);     //una pausa antes de iniciar el conteo, segun variable delay
        while (remainingTime > 0 /*&& !etapaTerminada*/)
        {
            timer.text = remainingTime.ToString();//actualiza tiempo en el timer

            yield return new WaitForSeconds(1);//espera 1 segundo

            remainingTime--;//resta 1 segundo al timer
        }

        timer.text = remainingTime.ToString();//actualiza tiempo del timer cuando termina el tiempo de juego

        /*if (!etapaTerminada && !multiplayer)  AQUI SE PUEDE AGREGAR LOGICA QUE SE NECESITE CUANDO TERMINA EL TIEMPO
            etapaTerminada = true;
        else if (multiEtapa != 3 && multiplayer)
        {
            if (!blancoDestruido)
                etapaTerminada = true;
            if (!blanco2Destruido)
                etapaTerminada2 = true;
            if (!blanco3Destruido)
                etapaTerminada3 = true;
        }
        //etapa++;
        Debug.Log("etapa " + (etapa + 1) + " terminada.");
        */
    }

    //----DELAYERS----
    IEnumerator Intro()
    {
        titulo.SetActive(true);
        botonTitulo.SetActive(true);

        //ApplyRotation();

        yield return new WaitForSeconds(3f);

        introInPos = true;
    }
    IEnumerator TutorialInPosDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        tutorialInPos = true;
    }
    IEnumerator WinningsInPosDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameOverText.GetComponent<TweenInAndOut>().HideGameObject();
        ParticleSystem tempConfeti;
        tempConfeti = Instantiate(confeti, new Vector3(caldero.transform.position.x - 5, caldero.transform.position.y, caldero.transform.position.z), confeti.transform.rotation);
        tempConfeti.Play();

        winningsInPos = true;
    }
    IEnumerator ReadyToRestart()
    {
        yield return new WaitForSeconds(0.8f);

        readyToRestart = true;
    }
}
