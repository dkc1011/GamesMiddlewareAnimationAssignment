using Photon.Pun;
using Photon.Realtime;
using System.IO;
using UnityEngine;

public class GameSetupController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    CharacterSelect charSelect;
    [SerializeField]
    Vector3 KnightStartPos;
    [SerializeField]
    Vector3 PaladinStartPos;

    private static Player Player { get; }


    // Start is called before the first frame update
    void Start()
    {
        CreateCharacter();
    }

    void CreateCharacter()
    {
        Debug.Log("Creating Player");
        if(charSelect.getKnightSelect())
        {
            PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Characters", "KnightPrefab"), KnightStartPos, Quaternion.Euler(0,-90,0), 0);
        }
        else if(charSelect.getPaladinSelect())
        {
            PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Characters", "PaladinPrefab"), PaladinStartPos, Quaternion.Euler(0,90,0), 0);
        }


    }


}
