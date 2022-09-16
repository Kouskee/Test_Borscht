using Player;
using UnityEngine;

public class UIControllerInput : MonoBehaviour
{
    private Movement _player;

    public void Init(Movement player)
    {
        _player = player;
    }

    public void VirtualMoveInput(Vector2 virtualMoveDirection)
    {
        _player.MoveInput(virtualMoveDirection);
    }
}