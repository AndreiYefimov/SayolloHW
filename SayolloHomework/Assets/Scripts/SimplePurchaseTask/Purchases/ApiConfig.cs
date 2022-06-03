using UnityEngine;

namespace AndriiYefimov.SayolloHW2
{
    [CreateAssetMenu(fileName = "ApiConfig", menuName = "ScriptableObjects/ApiConfig", order = 1)]
    public class ApiConfig : ScriptableObject
    {
        public string ApiRoot = "https://6u3td6zfza.execute-api.us-east-2.amazonaws.com";
        public string WorkingSpace = "prod";
        public string DetailsAPI = "v1/gcom/ad";
        public string PurchaseConfirmAPI = "v1/gcom/action"; 
    }
}