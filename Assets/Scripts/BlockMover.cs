using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMover : MonoBehaviour
{
    // Fields
    [SerializeField]
    public float GridMoveTick = 3.0f;

    // Members
    private Tuple<int, int> m_GridPosition;
    private float m_LastTickTime;
    private bool m_HasLanded;

    private void Awake()
    {
        OnEnable();
    }

    private void OnEnable()
    {
        m_GridPosition = new Tuple<int, int>((BlockGrid.GetXSize() / 2) + 1, BlockGrid.GetYSize());
        m_LastTickTime = Time.time;
        m_HasLanded = false;

        transform.position = BlockGrid.GridCoordToWorldPosition(m_GridPosition);
    }

    private void Update()
    {
        if (!m_HasLanded && Time.time - m_LastTickTime >= GridMoveTick)
        {
            m_LastTickTime = Time.time;

            // Attempt to move downwards
            if (!BlockGrid.IsFilled(m_GridPosition.Item1, m_GridPosition.Item2 - 1))
            {
                // Move out of previous space
                BlockGrid.ChangeBlockStatus(false, m_GridPosition);

                // Move into new space
                m_GridPosition = new Tuple<int, int>(m_GridPosition.Item1, m_GridPosition.Item2 - 1);
                BlockGrid.ChangeBlockStatus(true, m_GridPosition);

                // Finally, reset world position
                transform.position = BlockGrid.GridCoordToWorldPosition(m_GridPosition);
            }
            else
            {
                // Block has landed
                m_HasLanded = true;
            }
        }
    }

    public void ShiftBlock(bool left)
    {
        if (m_HasLanded) return;

        if (left && !BlockGrid.IsFilled(m_GridPosition.Item1 - 1, m_GridPosition.Item2))
        {
            // Move out of previous space
            BlockGrid.ChangeBlockStatus(false, m_GridPosition);

            // Move into new space
            m_GridPosition = new Tuple<int, int>(m_GridPosition.Item1 - 1, m_GridPosition.Item2);
            BlockGrid.ChangeBlockStatus(true, m_GridPosition);

            // Finally, reset world position
            transform.position = BlockGrid.GridCoordToWorldPosition(m_GridPosition);
        }
        else if (!left && !BlockGrid.IsFilled(m_GridPosition.Item1 + 1, m_GridPosition.Item2))
        {
            // Move out of previous space
            BlockGrid.ChangeBlockStatus(false, m_GridPosition);

            // Move into new space
            m_GridPosition = new Tuple<int, int>(m_GridPosition.Item1 + 1, m_GridPosition.Item2);
            BlockGrid.ChangeBlockStatus(true, m_GridPosition);

            // Finally, reset world position
            transform.position = BlockGrid.GridCoordToWorldPosition(m_GridPosition);
        }
    }
}
