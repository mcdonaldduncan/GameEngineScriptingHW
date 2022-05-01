using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlotStatus
{
    Empty,
    Occupied
}

public class PlantPlot : MonoBehaviour
{
    public PlotStatus plotStatus;
    protected SpriteRenderer spriteRenderer;
    protected Sprite emptyPlotSprite;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        this.plotStatus = PlotStatus.Empty;
        spriteRenderer = GetComponent<SpriteRenderer>();
        emptyPlotSprite = Resources.Load<Sprite>("Sprites/emptyplot");
    }
}
