namespace ForestFireSimulator.Domain.Entities;

/// <summary>
/// classe publique pour representer une foret
/// </summary>
public class Forest
{
    /// <summary>
    /// variable pour la taille de la foret
    /// </summary>
    public int Size { get; private set; }
    public TreeState[][] Cells { get; set; }

    /// <summary>
    /// constructeur par defaut
    /// </summary>
    /// <param name="size"></param>
    public Forest(int size)
    {
        Size = size;
        Cells = new TreeState[Size][];

        for (int i = 0; i < size; i++)
        {
            Cells[i] = new TreeState[size]; // alloue chaque ligne de taille 'size'
        }
    }

    /// <summary>
    /// methode d'initialisation du feu de foret
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void Ignite()
    {
        var rand = new Random();
        var rand_x = rand.Next(Size);
        var rand_y = rand.Next(Size);
        Cells[rand_x][rand_y] = TreeState.Fire;
        Cells[rand_y][rand_x] = TreeState.Fire;
        // Cells[1][1] = TreeState.Fire;
    }

    /// <summary>
    /// methode d'initialisation de la foret
    /// chaque case a une case d'etre soit vide soit arbre
    /// </summary>
    /// <param name="treeProbability"></param>
    public void InitializeRandom(double treeProbability = 0.8)
    {
        var rand = new Random();
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                Cells[i][j] = rand.NextDouble() < treeProbability ? TreeState.Tree : TreeState.Empty;
            }
        }
    }

    // public Forest Clone()
    // {
    //     var clone = new Forest(Size);
    //     Array.Copy(Cells, clone.Cells, Cells.Length);
    //     return clone;
    // }
    public Forest Clone()
    {
        var clone = new Forest(Size);
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                clone.Cells[i][j] = this.Cells[i][j];
            }
        }
        return clone;
    }

}