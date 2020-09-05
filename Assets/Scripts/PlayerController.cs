using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Fields
    [SerializeField]
    public float BlockMovementSpeed = 3f;

    // Const input names
    private const string SPACE = "Space";
    private const string LEFT = "Left";
    private const string RIGHT = "Right";
    private const string DOWN = "Down";

    // Members
    private BlockMover m_CurrentlyControlledBlock = null;

    private void Update()
    {
        SpawnBlock(Input.GetAxisRaw(SPACE) > 0f);

        // Apply positional changes to currently-controlled block
        if (m_CurrentlyControlledBlock)
        {
            MoveLeft(Input.GetAxisRaw(LEFT) > 0f);
            MoveRight(Input.GetAxisRaw(RIGHT) > 0f);
            //if (Input.GetAxisRaw(DOWN) > 0f) PushDown();
        }
    }

    private bool m_SpawnBlockPressed;
    private void SpawnBlock(bool isHeld)
    {
        if (isHeld && !m_SpawnBlockPressed)
        {
            m_SpawnBlockPressed = true;

            m_CurrentlyControlledBlock = BlockSpawner.SpawnBlock().gameObject.GetComponent<BlockMover>();
        }
        else if (!isHeld)
        {
            m_SpawnBlockPressed = false;
        }
    }

    private bool m_MoveLeftPressed;
    private void MoveLeft(bool isHeld)
    {
        if (isHeld && !m_MoveLeftPressed)
        {
            m_MoveLeftPressed = true;

            m_CurrentlyControlledBlock.ShiftBlock(true);
        }
        else if (!isHeld)
        {
            m_MoveLeftPressed = false;
        }
    }

    private bool m_MoveRightPressed;
    private void MoveRight(bool isHeld)
    {
        if (isHeld && !m_MoveRightPressed)
        {
            m_MoveRightPressed = true;

            m_CurrentlyControlledBlock.ShiftBlock(false);
        }
        else if (!isHeld)
        {
            m_MoveRightPressed = false;
        }
    }

    private void PushDown()
    {
        // TODO: Implement
    }
}
