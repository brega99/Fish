using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockManager myManager;//Coletando o script referente a váriavel
    public float speed;// Velocidade 
    bool turning = false; // váriavel referente o retorno ou não do objeto

    void Start()
    {
        speed = Random.Range(myManager.minSpeed, myManager.maxSpeed);//Serve para coletar a velocidade dentro do script FlockManager, gerando randomicamente entra minima e maxima
    }

    void Update()
    {
        Bounds b = new Bounds(myManager.transform.position, myManager.swinLimits * 2);// Coletar a distancia entre os objetos e determinar o limite
        if (!b.Contains(transform.position))
        {
            turning = true;
        }
        else
        {
            turning = false;
        }
        if (turning)//Serve para voltar mas ao mesmo tempo determina a rotação do objeto
        {
            Vector3 direction = myManager.transform.position - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, 
                Quaternion.LookRotation(direction), myManager.rotationSpeed * Time.deltaTime);
        }
        else
        {
            if (Random.Range(0, 100) < 10)
                speed = Random.Range(myManager.minSpeed, myManager.maxSpeed);
            if (Random.Range(0, 100) < 20)
                ApplyRules();
        }
        ApplyRules();
        transform.Translate(0, 0, Time.deltaTime * speed);
        
    }

    void ApplyRules()//Aplicar regras
    {
        GameObject[] gos;// Formar um array
        gos = myManager.allFish;// Determinano que o array serão os objetos
        

        Vector3 vcenter = Vector3.zero;// Pegando o centro dos objetos
        Vector3 vavoid = Vector3.zero;// Pegando o centro dos objetos
        float gSpeed = 0.01f;
        float nDistance;
        int groupsize = 0;//Tamanho do grupo

        foreach (GameObject go in gos)//Serve para fazer a checagem de cadda objeto dentro da mesma lista denominada "gos"
        {
            if (go != this.gameObject)//Se go for diferente desse objeto faça...
            {
                nDistance = Vector3.Distance(go.transform.position, this.transform.position);
                if (nDistance <= myManager.neighbourDistance)
                {
                    vcenter += go.transform.position;
                    groupsize++;

                    if(nDistance< 1.0f)
                    {
                        vavoid = vavoid + (this.transform.position - go.transform.position);
                    }

                    Flock anotherFlock = go.GetComponent<Flock>();
                    gSpeed = gSpeed + anotherFlock.speed;
                }
            }
        }
        if (groupsize > 0)//Se o tamanho do grupo for maior que 0 faça com que ele se mova de acordo com o restante do grupo e quando for necessária a rotação, faça!
        {
            vcenter = vcenter / groupsize + (myManager.goalPos - this.transform.position);
            speed = gSpeed / groupsize;
            Vector3 direction = (vcenter + vavoid) - transform.position;
            if (direction != Vector3.zero)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction),
                    myManager.rotationSpeed * Time.deltaTime);

        }
    }
}
