using InControl;

public class CharacterAction : PlayerActionSet
{
    public PlayerAction Left;
    public PlayerAction Right;
    public PlayerAction Down;
    public PlayerAction Up;
    public PlayerTwoAxisAction Move;

    public PlayerAction Special;

    public CharacterAction()
    {
        Left = CreatePlayerAction("Move Left");
        Right = CreatePlayerAction("Move Right");
        Down = CreatePlayerAction("Move Down");
        Up = CreatePlayerAction("Move Up");
        Move = CreateTwoAxisPlayerAction(Left, Right, Down, Up);

        Special = CreatePlayerAction("Special");
    }
}