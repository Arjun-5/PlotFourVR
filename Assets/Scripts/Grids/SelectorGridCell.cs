using UnityEngine.EventSystems;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class SelectorGridCell : XRBaseInteractable, IPointerDownHandler
{
    private int columnIndex;

    private PlotFourGameBoard board;

    public void Initialize(PlotFourGameBoard gameBoard, int column)
    {
        board = gameBoard;
        columnIndex = column;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        board.TryDropDisc(columnIndex);
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        board.TryDropDisc(columnIndex);
    }
}