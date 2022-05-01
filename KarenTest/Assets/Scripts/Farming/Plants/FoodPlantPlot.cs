using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPlantPlot : PlantPlot
{
    public FoodPlant plantInPlot;
    bool triggerActive = false;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        //SetFoodPlant(FoodPlantFactory.Instance.GetFoodPlant("HP Berry"));

        if (plotStatus == PlotStatus.Occupied)
        {
            UpdateSprite(plantInPlot);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && triggerActive)
        {
            if (plotStatus == PlotStatus.Occupied)
            {
                plantInPlot.plantEvo.Evolve();
                UpdateSprite(plantInPlot);
                CheckIfGrown();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Player in trigger");
        triggerActive = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        triggerActive = false;
    }

    public void SetFoodPlant(FoodPlant fp)
    {
        this.plantInPlot = fp;
        this.plotStatus = PlotStatus.Occupied;
        UpdateSprite(fp);
    }

    void UpdateSprite(FoodPlant fp)
    {
        this.spriteRenderer.sprite = fp.plantEvo.SetCurrentSprite();
    }

    void CheckIfGrown()
    {
        if (plantInPlot.plantEvo.hasGrown)
        {
            Player.Instance.playerInventory.AddFood(plantInPlot.food);
            this.spriteRenderer.sprite = emptyPlotSprite;
            this.plotStatus = PlotStatus.Empty;
        }
    }
}
