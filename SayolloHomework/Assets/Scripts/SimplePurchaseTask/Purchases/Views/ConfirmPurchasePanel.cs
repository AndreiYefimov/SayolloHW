using System;
using System.Collections.Generic;
using System.Linq;
using AndriiYefimov.SayolloHW2.Interfaces;
using AndriiYefimov.SayolloHW2.Purchases.Models;
using AndriiYefimov.SayolloHW2.Validators;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace AndriiYefimov.SayolloHW2.Purchases.Views
{
    public class ConfirmPurchasePanel : MonoBehaviour, IVisible
    {
        [SerializeField] private Button confirmButton;
        [SerializeField] private Button backButton;
        [Space] 
        [SerializeField] private TMP_InputField email;
        [SerializeField] private TMP_InputField cardExpirationMonth;
        [SerializeField] private TMP_InputField cardExpirationYear;
        [SerializeField] private TMP_InputField cardNumber;
        
        public event Action<PurchaseConfirmModel> ConfirmRequested;

        private void Awake()
        {
            AddListeners();
        }

        private void OnDestroy()
        {
            RemoveListeners();
        }

        private void OnDisable()
        {
            SetDefaults();
        }

        private void SetDefaults()
        {
            SetEmptyText(email);
            SetEmptyText(cardExpirationMonth);
            SetEmptyText(cardExpirationYear);
            SetEmptyText(cardNumber);
        }

        private void SetEmptyText(TMP_InputField inputField)
        {
            SetInputText(inputField, string.Empty);
        }

        private static void SetInputText(TMP_InputField inputField, string text)
        {
            inputField.text = text;
        }

        private void AddListeners()
        {
            confirmButton.onClick.AddListener(OnConfirmClicked);
        }
        
        private void RemoveListeners()
        {
            confirmButton.onClick.RemoveListener(OnConfirmClicked);
        }

        private void OnConfirmClicked()
        {
            if (!IsInputValid()) return;

            ConfirmRequested?.Invoke(new PurchaseConfirmModel()
            {
                Email = email.text,
                CardNumber = cardNumber.text,
                ExpirationDate = $"{cardExpirationMonth.text}/{cardExpirationYear.text}"
            });
        }

        private bool IsInputValid()
        {
            return StringValidator.IsNotEmpty(email.text,
                cardNumber.text, cardExpirationMonth.text, cardExpirationYear.text);
        }

        public void SetVisibleState(bool state)
        {
            gameObject.SetActive(state);
        }

        public void AddBackListener(UnityAction callback)
        {
            backButton.onClick.AddListener(callback);
        }
        
        public void RemoveBackListener(UnityAction callback)
        {
            backButton.onClick.RemoveListener(callback);
        }
    }
}