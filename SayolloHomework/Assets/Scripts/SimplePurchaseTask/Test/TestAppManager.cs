using System;
using AndriiYefimov.SayolloHW.Senders;
using AndriiYefimov.SayolloHW2.Purchases.Models;
using AndriiYefimov.SayolloHW2.Purchases.Views;
using Newtonsoft.Json;
using UnityEngine;

namespace AndriiYefimov.SayolloHW2.Test
{
    public class TestAppManager : MonoBehaviour
    {
        [SerializeField] private ApiConfig apiConfig;
        [Space]
        [SerializeField] private ConfirmPurchasePanel confirmPurchasePanel;
        [SerializeField] private TestPurchaseManager testPurchaseManager;
        
        private PostRequestSender _postRequestSender;
        private GetRequestSender _getRequestSender;

        private string DetailsAPI => $"{apiConfig.ApiRoot}/{apiConfig.WorkingSpace}/{apiConfig.DetailsAPI}";
        private string PurchaseConfirmAPI => $"{apiConfig.ApiRoot}/{apiConfig.WorkingSpace}/{apiConfig.PurchaseConfirmAPI}";

        private void Awake()
        {
            InitComponents();
            AddListeners();
        }

        private void OnDestroy()
        {
            RemoveListeners();
        }
        
        private void Start()
        {
            testPurchaseManager.Init(OnItemDetailsRequest, OnImageRequest, OnItemPurchase);
        }

        private void InitComponents()
        {
            _postRequestSender = new PostRequestSender();
            _getRequestSender = new GetRequestSender();
        }
        
        private void AddListeners()
        {
            confirmPurchasePanel.ConfirmRequested += OnPurchaseConfirmRequest;
            confirmPurchasePanel.AddBackListener(DisableConfirmPanel);
        }

        private void DisableConfirmPanel()
        {
            confirmPurchasePanel.SetVisibleState(false);
        }

        private void RemoveListeners()
        {
            confirmPurchasePanel.ConfirmRequested -= OnPurchaseConfirmRequest;
        }

        private void OnItemDetailsRequest(string itemId, Action<PurchaseItemModel> resultCallback)
        {
            Debug.Log($"[TestAppManager::OnItemDetailsRequest]");
            StartCoroutine(_postRequestSender.SendPostRequest(DetailsAPI, resultCallback));
        }
        
        private void OnImageRequest(string imagePath, Action<Sprite> resultCallback)
        {
            Debug.Log($"[TestAppManager::OnImageRequest]");
            StartCoroutine(_getRequestSender.SendImageGetRequest(imagePath, resultCallback));
        }

        private void OnPurchaseConfirmRequest(PurchaseConfirmModel purchaseConfirmModel)
        {
            Debug.Log($"[TestAppManager::OnPurchaseConfirmRequest]");
            var postData = JsonConvert.SerializeObject(purchaseConfirmModel);
            StartCoroutine(_postRequestSender
                .SendSimplePostRequest(PurchaseConfirmAPI, postData, OnPurchaseConfirmed));
        }
        
        private void OnItemPurchase(string itemId)
        {
            confirmPurchasePanel.SetVisibleState(true);
        }

        private void OnPurchaseConfirmed()
        {
            DisableConfirmPanel();
        }
    }
}