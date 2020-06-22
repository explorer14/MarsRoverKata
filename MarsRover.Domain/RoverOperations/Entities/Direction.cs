namespace MarsRover.Domain.RoverOperations.Entities
{
    // Assumptions:
    // a. rover never moves diagonally, only along one axis
    // b. therefore only 4 directions possible N, E, W, S
    // c. using upper case ASCII codes as numeric values to make turning
    // calculations easy. Could have picked up something even simpler like a custom
    // numbering scheme but its all encoded in the domain and risk of incorrect heading is low
    public enum Direction
    {
        North = 78,
        East = 69,
        West = 87,
        South = 83
    }
}