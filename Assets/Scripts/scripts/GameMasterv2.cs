using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

    //public static GameMaster instance = null;    
    public float puntaje;
    public bool canPlay;
    public int mixValueRecipe;
    public int[] gameRecipe;
    public List<GameObject> itemList = new List<GameObject>();
    public SpriteRenderer spriteShowRecipeElement;
    public List<int> playerList = new List<int>();

    /*void Awake(){
        if (instance != null) {
            instance = this;
        } else if(instance == null) {
            Destroy(gameObject);
        }
    }*/

    void Start() {
        Debug.Log("juego creado por Hector Perez, Brayam Barrios y Alexis Espinosa");
        puntaje = 0;
        gameRecipe = new int[mixValueRecipe];
        generateRecipe();
        StartCoroutine(show3SecondsTheRecipe());
    }

    void Update(){
        
    }

    void generateRecipe() {
        int randomValue;
        for (int i = 0; i < mixValueRecipe; i++) {
            randomValue = Random.Range(0, 27);
            if (i == 0) {
                gameRecipe[i] = randomValue;
            } else {
                foreach (int n in gameRecipe) {
                    while (randomValue == n) {
                        randomValue = Random.Range(0, 27);
                    }
                }
                gameRecipe[i] = randomValue;
            }   
            Debug.Log(gameRecipe[i]);
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        int tempNumberValue = 0;
        Debug.Log("el objeto " + col.name + " esta chocando con el caldero");
        foreach (GameObject x in itemList)
        {
            if (x.name.Equals(col.name))
            {
                tempNumberValue = itemList.IndexOf(x);
            }
        }
        playerList.Add(tempNumberValue);
        col.gameObject.SetActive(false);
    }

    void showGeneratedRecipe() {
        //for (int i = 0; i < mixValueRecipe; i++){
            //spriteShowRecipeElement.sprite = itemList[gameRecipe[i]].gameObject.GetComponent<SpriteRenderer>().sprite;
            // esto solo es si los sprites son pequeños 
            StartCoroutine(showElementby3seconds());
        //}
    }

    IEnumerator showElementby3seconds() {
        for (int i = 0; i < mixValueRecipe; i++) {
            yield return new WaitForSeconds(3);
            Debug.Log("valor de i " + i);
            Debug.Log("nombre del elemento " + itemList[gameRecipe[i]].gameObject.name);
            spriteShowRecipeElement.sprite = itemList[gameRecipe[i]].gameObject.GetComponent<SpriteRenderer>().sprite;
            spriteShowRecipeElement.transform.localScale = new Vector3(10, 10, 1);
        }
        yield return new WaitForSeconds(2);
        spriteShowRecipeElement.sprite = null;
    }

    IEnumerator show3SecondsTheRecipe() {
        yield return new WaitForSeconds(3);
        showGeneratedRecipe();
    }
    
}
