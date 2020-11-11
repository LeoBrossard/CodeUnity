using UnityEngine;

public class ProceduralDungeon_Manager : MonoBehaviour
{

    public Dungeon[] pieces;

    public Dungeon entree;
    public Dungeon sortie;

    public int dungeonLength;
    public Vector3 dungeonOffSet;

    Vector3 piecePosition;
    Vector3[] existingPiecePosition;
    bool error = false;


    void Start()
    {
        Reset();
    }

    void Generate()
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

    void RandomPiecePosition()
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
            error = true;
        }
    }

    bool AvailablePlacement(Vector3 placement)
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
