using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public GameObject titulo;
    public Animator puerta;
    public Animator pantallaNegra;
    public GameObject fondo1;
    public GameObject fondo2;
    public GameObject botonTitulo; 
    public GameObject instrucciones;
    public GameObject botonInstrucciones;
    public GameObject conteoRegresivo;
    public TextMeshPro timer;
    public int maxTime;
    public TextMeshPro scoreText;
    public GameObject gameElements;
    //public GameObject caldero;
    public GameObject gameOverText;
    //public ParticleSystem confeti;
    public GameObject winningsPanel;
    public TextMeshPro punajeFinal;
    public GameObject panelRetry;

    RaycastHit2D hit;
    Camera cam;
    Vector3 pos;
    Vector3 mousePos;
    Transform focus;
    bool isDrag;

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
    [HideInInspector] public bool introDone = false;

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
        introDone = false;
        //----------------------------------------------------INICIO-------------------------------------------------------------------
        StartCoroutine(Intro());    //co runtina para esperar a que todos los elementos de la intro este en su lugar
        isDrag = false;
        cam = Camera.main;
    }

    void Update()
    {/*
        //----------------------------------------------------TUTORIAL----------------------------------------------------------------
        //Esconde titulo y MUESTRA el TUTORIAL 
        if (introInPos && (Input.GetKeyDown(KeyCode.Return)))
        {
            introInPos = false;
            puerta.SetTrigger("abrirPuerta");
            if(!introDone)
            {
                StartCoroutine(SecuenceToShop());
            }
        }
        if ((introDone)||(introDone && readyToRestart))
        {
            readyToRestart = false;
            introDone = false;
            Debug.Log("Inicia Tutorial");
            instrucciones.SetActive(true);
            botonInstrucciones.SetActive(true);
            StartCoroutine(TutorialInPosDelay(1.8f));               //espera 1.8s para permitir salir del tutorial
        }
        //--------------------------------------------------CONTEOREGRESIVO-------------------------------------------------------------
        //esconde tutorial y MUESTRA CONTEO REGRESIVO
        if (tutorialInPos && Input.GetKeyDown(KeyCode.Return))
        {
            titulo.SetActive(false);
            botonTitulo.SetActive(false);
            tutorialInPos = false;
            Debug.Log("Inicia ConteoRegresivo");
            instrucciones.GetComponent<TweenInAndOut>().HideGameObject();           //esconde el panel de tutorial
            botonInstrucciones.GetComponent<TweenInAndOut>().HideGameObject();
            conteoRegresivo.SetActive(true);   
            timer.gameObject.SetActive(true);
            scoreText.gameObject.SetActive(true);
            timer.text = maxTime.ToString();
            Score = 0;
            
            //AQUI SE AGREGARIAN ANIMACIONES POSIBLES DE OBJETOS EN LA ESCENA MIENTRAS CORRE EL CONTEOREGRESIVO PARA INICIAR JUEGO
        }

        //-------------------------------------------------------JUEGO------------------------------------------------------------------
        if (countdownDone)      //cuando el conteoRegresivo en ChangeTextInAnimations llega a cero, countdownDone es true
        {
            countdownDone = false;
            instrucciones.SetActive(false);
            botonInstrucciones.SetActive(false);
            Debug.Log("termina conteo");
            conteoRegresivo.SetActive(false);
            timerCoroutine = StartCoroutine(Timer(0, maxTime));
            gameBegan = true;
            etapa = 0;
            Debug.Log("etapa: " + etapa + 1);
        }
        */
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
            //StartCoroutine(WinningsInPosDelay(1.5f));                  //hace a la variable winningsInPos true despues de 1.5 segundos

            //Debug.Log("Game Over");
            winningsInPos = true;
            readyToShowWinnings = true;
        }
        //------------------------------------------------------PREMIOS-----------------------------------------------------------------
        if (winningsInPos && readyToShowWinnings)
        {
            winningsInPos = false;
            Debug.Log("mostrando puntaje final");
            //gameOverText.GetComponent<TweenInAndOut>().HideGameObject();
            gameOverText.transform.position = new Vector3(0f, 9.44f, 0f);
            gameOverText.SetActive(false);

            winningsPanel.SetActive(true);
            punajeFinal.text = "Puntaje final: " + Score.ToString();

            StartCoroutine(ReadyToRestart());
        }

        //--------------------------------------------------SALIDA DEL JUEGO--------------------------------------------------------
        if (readyToRestart)
        {
            if (Input.GetKey(KeyCode.Return))       //presionar SI o NO
            {
                GameReset();
            }
            else if(Input.GetKey(KeyCode.Escape))
                Application.Quit(); //se cierra el juego
        }

    }//fin de Update

    void DragDop2d(){
        if (Input.GetMouseButtonDown(0)){
            hit = Physics2D.GetRayIntersection(cam.ScreenPointToRay(Input.mousePosition));

            if (hit.collider != null)
            {
                focus = hit.transform;
                print("Clicked " + hit.collider.transform.name);
                isDrag = true;
            }

        }else if (Input.GetMouseButtonUp(0) && isDrag == true){
            isDrag = false;
        }else if (isDrag == true){
            mousePos = Input.mousePosition;
            mousePos.z = -cam.transform.position.z;
            pos = cam.ScreenToWorldPoint(mousePos);

            focus.position = new Vector3(pos.x, pos.y, focus.position.z);
        }
    }

    private void GameReset()
    {
        Score = 0;
        timer.text = "";
        introInPos = false;
        tutorialInPos = false;
        countdownDone = false;
        gameBegan = false;
        gameOver = false;
        winningsInPos = false;
        readyToShowWinnings = false;
        timer.gameObject.SetActive(false);
        scoreText.text = "0";
        panelRetry.transform.position = new Vector3(0f, 8.34f, 0f);
        panelRetry.SetActive(false);
        scoreText.gameObject.SetActive(false);
        conteoRegresivo.SetActive(false);
        introDone = true;
    }
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

        gameBegan = false;
        gameOver = true;
        Debug.Log("gameOver");
    }

    //----DELAYERS----
    IEnumerator Intro()
    {
        titulo.SetActive(true);
        botonTitulo.SetActive(true);

        //ApplyRotation();

        yield return new WaitForSeconds(1.5f);

        introInPos = true;
    }

    IEnumerator SecuenceToShop()
    {
        pantallaNegra.SetBool("toBlack", true);
        yield return new WaitForSeconds(1f);
        fondo1.SetActive(false);
        fondo2.SetActive(true);
        pantallaNegra.SetBool("toBlack", false);
        pantallaNegra.SetBool("toNormal", true);
        yield return new WaitForSeconds(1f);
        titulo.GetComponent<TweenInAndOut>().HideGameObject();
        botonTitulo.GetComponent<TweenInAndOut>().HideGameObject();
        introDone = true;
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
        //ParticleSystem tempConfeti;
        //tempConfeti = Instantiate(confeti, new Vector3(caldero.transform.position.x - 5, caldero.transform.position.y, caldero.transform.position.z), confeti.transform.rotation);
        //tempConfeti.Play();

        winningsInPos = true;
    }
    IEnumerator ReadyToRestart()
    {
        yield return new WaitForSeconds(2f);
        winningsPanel.GetComponent<TweenInAndOut>().HideGameObject();
        yield return new WaitForSeconds(0.8f);
        winningsPanel.SetActive(false);
        panelRetry.SetActive(true);

        readyToRestart = true;
    }
}
