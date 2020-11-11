using UnityEngine;

public class ProceduralDungeon_Manager : MonoBehaviour
{

    public Dungeon[] pieces;        //Differentes piece

    public Dungeon entree;          //Une piece qui servira d'entree/debut pour le joueur
    public Dungeon sortie;          //une piece qui servira de sortie/objectif pour le joueur

    public int dungeonLength;       //la taille du dungeon souhaite
    public Vector3 dungeonOffSet;   //la position(x, y) de la piece d'entree

    Vector3 piecePosition;          //la position(x, y) de la piece cree
    Vector3[] existingPiecePosition;//la memoire de toute les positions de piece precedement cree
    bool error = false;             //en cas de probleme lors de la generation du dungeon


    void Start()
    {
        Reset();                    //je m'assure que toute les variables soit bien reset
    }

    void Generate()                 //le code utiliser pour instancier les differents pieces
    {
        existingPiecePosition = new Vector3[dungeonLength];

        Instantiate(entree, dungeonOffSet, Quaternion.identity);

        existingPiecePosition[0] = dungeonOffSet;

        for(int i = 1; i < dungeonLength - 1; i++){

            RandomPiecePosition();

            existingPiecePosition[i] = piecePosition;

            Instantiate(pieces[(int)Random.Range(0, pieces.Length)], dungeonOffSet + piecePosition, Quaternion.identity);

        }

        RandomPiecePosition();

        Instantiate(sortie, dungeonOffSet, Quaternion.identity);

        if (error)
        {
            Reset();
        }

    }

    void Reset()        
    {

        foreach(GameObject _piece in GameObject.FindGameObjectsWithTag("piece")){

            Destroy(_piece);

        }

        existingPiecePosition = new Vector3[dungeonLength];

        error = false;

        Generate();

    }

    void RandomPiecePosition()          //on verifie la position de la nouvelle piece
    {

        if (Random.value < 0.25f && AvailablePlacement(new Vector3(1, 0)))
        {
            piecePosition += new Vector3(1, 0);
        }
        else if(0.5f > Random.value && Random.value > 0.25f && AvailablePlacement(new Vector3(-1, 0)))
        {
            piecePosition += new Vector3(-1, 0);
        }
        else if (0.5f < Random.value && Random.value < 0.75f && AvailablePlacement(new Vector3(0, 1)))
        {
            piecePosition += new Vector3(0, 1);
        }
        else if(AvailablePlacement(new Vector3(0, -1)))
        {
            piecePosition += new Vector3(0, -1);
        }
        else
        {
            error = true;                    //si on a cree un cul-de-sac on reset la generation jusqu'a en cree un sans
        }
    }

    bool AvailablePlacement(Vector3 placement) //on verifie dans existingPiecePosition qu'une piece n'a pas deje ete cree a cette position
    {

        for (int i = 0; i < existingPiecePosition.Length; i++)
        {

            if (existingPiecePosition[i] == placement)
            {
                return false;
            }

        }

        return true;

    }


}
