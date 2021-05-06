using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour
{
    public GameObject fishPrefab;//Instanciando variavel de um objeto publico
    public int numFish = 20;//Instanciando variavel publica de numero de objetos criados em cena
    public GameObject[] allFish;// Lista de objetos
    public Vector3 swinLimits = new Vector3(5, 5, 5);//Limite para aonde os objetos podem se locomover em cena
    public Vector3 goalPos;//posição

    [Header("Configurações do Cardume")]
    [Range(0.0f, 5.0f)]
    public float minSpeed;// Velocidade minima
    [Range(0.0f, 5.0f)]
    public float maxSpeed;// Velocidadde maxima
    [Range(1.0f, 10.0f)]
    public float neighbourDistance;//Distancia entre os objetos
    [Range(0.0f, 5.0f)]
    public float rotationSpeed;// Velocidade de rotação dos objetos

     void Start()
    {
        allFish = new GameObject[numFish];//Determinando a quantidade de objetos que irão entrar na lista
        for(int i=0; i < numFish; i++)//Criando posições distintas em que os objetos serão spawnados randomicamente
        {
            Vector3 pos = this.transform.position + new Vector3(Random.Range(-swinLimits.x, swinLimits.x),
                                                                Random.Range(-swinLimits.y, swinLimits.y),
                                                                Random.Range(-swinLimits.z, swinLimits.z));
            allFish[i] = (GameObject)Instantiate(fishPrefab, pos, Quaternion.identity);
            allFish[i].GetComponent<Flock>().myManager = this;
        }
        goalPos = this.transform.position;
    }
     void Update()
    {
        goalPos = this.transform.position + new Vector3(Random.Range(-swinLimits.x, swinLimits.x),
                                                                Random.Range(-swinLimits.y, swinLimits.y),
                                                                Random.Range(-swinLimits.z, swinLimits.z));
    }
}
