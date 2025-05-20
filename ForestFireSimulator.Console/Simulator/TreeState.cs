namespace ForestFireSimulator.Console.Simulator;

/// <summary>
/// enum pour representer l'etat d'une case vide ou d'un arbre
/// </summary>
public enum TreeState
{
    /// <summary>
    /// case vide
    /// </summary>
    Empty = 0,
    
    /// <summary>
    /// case avec un arbre
    /// </summary>
    Tree = 1,

    /// <summary>
    /// un arbre est en feu
    /// </summary>
    Fire = 2,

    /// <summary>
    /// un arbre a brule
    /// </summary>
    Ash = 3
}