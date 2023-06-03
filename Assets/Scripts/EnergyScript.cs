using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyScript : MonoBehaviour
{
    public Slider energySlider;
    public float initialEnergy = 100;
    float newIncreasedEnergy;
    public float currentEnergy;
    
    public float energyDuration = 15;
    private float elapsedTime;
    float percentOfTime;

    private static EnergyScript instancia;

    // Accede a la instancia del Singleton
    public static EnergyScript Instancia
    {
        get { return instancia; }
    }

    private void Awake()
    {
        if (instancia != null && instancia != this)
        {
            // Si ya hay una instancia creada, destruye este objeto
            Destroy(this.gameObject);
        }
        else
        {
            // Establece esta instancia como la única instancia
            instancia = this;

            // Opcional: asegurarse de que el objeto no se destruya al cambiar de escena
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentEnergy = initialEnergy;
        newIncreasedEnergy = initialEnergy;
    }

    // Update is called once per frame
    void Update()
    {
        ReduceEnergyOverTime();
        elapsedTime += Time.deltaTime;
        percentOfTime = elapsedTime / energyDuration;
        energySlider.value = currentEnergy;
    }

    void ReduceEnergyOverTime()
    {

        if (currentEnergy > 0)
        {
            currentEnergy = Mathf.Lerp(newIncreasedEnergy, 0, percentOfTime);
        }
    }

    public void IncreaseEnergy(float percent)
    {

        float percentOfTotalEnergy = initialEnergy * percent;

        float newEnergy = percentOfTotalEnergy + currentEnergy > initialEnergy ? initialEnergy : percentOfTotalEnergy + currentEnergy;

        currentEnergy = newEnergy;
        newIncreasedEnergy = newEnergy;
        elapsedTime = 0;
        energyDuration = newEnergy / newIncreasedEnergy * energyDuration;
    }

}
