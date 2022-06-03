using AndriiYefimov.SayolloHW2.Purchases.Interfaces;
using AndriiYefimov.SayolloHW2.Purchases.Models;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace AndriiYefimov.SayolloHW2.Purchases.Views
{
    public class PurchaseItemDetailsView : PurchaseEntityView, IPurchaseItemDetailsView
    {
        [SerializeField] private Button closeButton;
        [Space] 
        [SerializeField] private Image itemImage;
        [SerializeField] private TMP_Text price;

        private void Awake()
        {
            SetDefaults();
            AddCloseListener(DisableSelfOnClose);
        }

        private void OnDestroy()
        {
            RemoveCloseListener(DisableSelfOnClose);
        }

        private void OnDisable()
        {
            SetDefaults();
        }

        public void SetDetails(PurchaseItemModel itemDetails)
        {
            UpdateText(itemDetails);
        }

        public void SetProfileIcon(Sprite sprite)
        {
            UpdateImage(sprite, true);
        }

        public void AddCloseListener(UnityAction purchaseCallback)
        {
            closeButton.onClick.AddListener(purchaseCallback);
        }

        public void RemoveCloseListener(UnityAction purchaseCallback)
        {
            closeButton.onClick.RemoveListener(purchaseCallback);
        }

        public void SetVisibleState(bool state)
        {
            gameObject.SetActive(state);
        }

        private void DisableSelfOnClose()
        {
            SetVisibleState(false);
        }
        
        private void UpdateText(PurchaseItemModel itemDetails)
        {
            var priceText = $"{itemDetails.currency_sign} {itemDetails.price} {itemDetails.currency}";
            SetText(price, priceText);
        }
        
        private void SetText<T>(TMP_Text textComponent, T text)
        {
            textComponent.text = text.ToString();
        }

        private void UpdateImage(Sprite profileImage, bool enableState)
        {
            itemImage.sprite = profileImage;
            UpdatePreserveAspect(profileImage);
            itemImage.enabled = enableState;
        }

        private void UpdatePreserveAspect(Object profileImage)
        {
            itemImage.preserveAspect = profileImage != null;
        }

        private void SetDefaults()
        {
            UpdateImage(null, false);
            SetText(price, "");
        }

    }
}