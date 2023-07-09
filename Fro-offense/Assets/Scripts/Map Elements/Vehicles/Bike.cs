using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bike : Element
{
    int carxDIR = 1;

    const float POSY_THRESHOLD = 1;

    private void Start()
    {
        base.Start();
        model.rotation = Quaternion.Euler(0, 90, 0);
    }

    private void Update()
    {
        base.Update();
    }

    protected override void MoveStateMachine()
    {
        base.MoveStateMachine();

        if (moveTimer <= 0)
        {
            moveTimer = TIMER_BASE;
            MapSystem.SquareValue nextSquareValue = GetNextSquare((int)moveSystem.destinationVector.x + carxDIR, (int)moveSystem.destinationVector.z);
            RotateTo(Quaternion.Euler(0, 90 * carxDIR, 0));
            Debug.Log(nextSquareValue.ToString());

            switch (nextSquareValue)
            {
                case MapSystem.SquareValue.EMPTY:
                    moveSystem.destinationVector = new Vector3(moveSystem.destinationVector.x + carxDIR, originalYPos, moveSystem.destinationVector.z);
                    break;
                case MapSystem.SquareValue.HOLE:
                    moveSystem.destinationVector = new Vector3(moveSystem.destinationVector.x + carxDIR, originalYPos + POSY_THRESHOLD, moveSystem.destinationVector.z);
                    break;
                case MapSystem.SquareValue.OBSTACLE:
                    SwitchCarDir();
                    break;
                case MapSystem.SquareValue.ANIMAL:
                    SwitchCarDir();
                    break;
                case MapSystem.SquareValue.OUTSIDE_MAP:
                    SwitchCarDir();
                    break;
            }
        }
    }

    void SwitchCarDir()
    {
        if (carxDIR == 1)
            carxDIR = -1;
        else if (carxDIR == -1)
            carxDIR = 1;
    }

    MapSystem.SquareValue GetNextSquare(int x, int y)
    {
        return map.GetSquareValue(x, y);
    }
}
