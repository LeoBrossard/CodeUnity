using UnityEngine;

public class Player_Mouvement : MonoBehaviour
{

    public float speed;             //La vitesse de deplacement
    public float raycastDistance;   //La portee de detection des murs

    RaycastHit2D hitRight;          //Le rayon pour detecter les murs
    float inputX;                   //Input sur les x Joueur
    float inputY;                   //Input sur les y du Joueur

    void Update()
    {

        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");


        if(!Physics2D.Raycast(transform.position, Vector3.right, raycastDistance, LayerMask.NameToLayer("Wall")) && inputX > 0)
        {

            Move(Vector3.right);

        }
        if (!Physics2D.Raycast(transform.position, Vector3.left, raycastDistance, LayerMask.NameToLayer("Wall")) && inputX < 0)
        {

            Move(Vector3.left);

        }
        if (!Physics2D.Raycast(transform.position, Vector3.up, raycastDistance, LayerMask.NameToLayer("Wall")) && inputY > 0)
        {

            Move(Vector3.up);

        }
        if (!Physics2D.Raycast(transform.position, Vector3.down, raycastDistance, LayerMask.NameToLayer("Wall")) && inputY < 0)
        {

            Move(Vector3.down);

        }


    }

    private void Move(Vector3 targetLocation)
    {

        transform.position = Vector3.MoveTowards(transform.position, transform.position + targetLocation, speed * Time.deltaTime);

    }

}
