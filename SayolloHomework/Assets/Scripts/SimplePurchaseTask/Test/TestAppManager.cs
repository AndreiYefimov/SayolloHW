using System;
using System.Collections.Generic;
using AndriiYefimov.SayolloHW.Senders;
using AndriiYefimov.SayolloHW2.Models;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Delegate = AndriiYefimov.SayolloHW2.Delegates.Delegate;

namespace AndriiYefimov.SayolloHW2.Test
{
    public class TestAppManager : MonoBehaviour
    {
        [SerializeField] private string detailsAPI;
        [SerializeField] private string purchaseConfirmAPI; 
        
        [SerializeField] private ConfirmPurchasePanel confirmPurchasePanel;
        [SerializeField] private TestPurchaseManager testPurchaseManager;
        
        private PostRequestSender _postRequestSender;
        private GetRequestSender _getRequestSender;

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
            StartCoroutine(_postRequestSender.SendPostRequest(detailsAPI, resultCallback));
        }
        
        private void OnImageRequest(string imagePath, Action<Sprite> resultCallback)
        {
            StartCoroutine(_getRequestSender.SendImageGetRequest(imagePath, resultCallback));
        }
        
        private void OnItemPurchase(string itemId)
        {
            confirmPurchasePanel.SetVisibleState(true);
        }

        private void OnPurchaseConfirmRequest(PurchaseConfirmModel purchaseConfirmModel)
        {
            var postData = JsonUtility.ToJson(purchaseConfirmModel);
            StartCoroutine(_postRequestSender
                .SendSimplePostRequest(purchaseConfirmAPI, postData, OnPurchaseConfirmed));
        }

        private void OnPurchaseConfirmed()
        {
            DisableConfirmPanel();
        }
    }

    [Serializable]
    public class PurchaseConfirmModel
    {
        public string Email;
        public string CardNumber;
        public string ExpirationDate;
    }

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

        private void AddListeners()
        {
            confirmButton.onClick.AddListener(OnConfirmClicked);
        }

        private void OnConfirmClicked()
        {
            ConfirmRequested?.Invoke(new PurchaseConfirmModel()
            {
                // TODO: add input check
                Email = email.text,
                CardNumber = cardNumber.text,
                ExpirationDate = $"{cardExpirationMonth.text}/{cardExpirationYear.text}"
            });
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

    public class TestPurchaseManager : MonoBehaviour
    {
        [SerializeField] private List<PurchaseItem> purchaseItems;

        private string _testItemId = "";
        
        public void Init(Delegate.GetItemDetails getDetailsCallback,
            Delegate.GetItemProfileIcon getIconCallback, Delegate.PurchaseItem purchaseItemCallback)
        {
            InitItemsIds();
            AddItemsListeners(getDetailsCallback, getIconCallback, purchaseItemCallback);
        }

        private void AddItemsListeners(Delegate.GetItemDetails getDetailsCallback,
            Delegate.GetItemProfileIcon getIconCallback, Delegate.PurchaseItem purchaseItemCallback)
        {
            // set all the callbacks for purchase item events
            foreach (var item in purchaseItems)
            {
                AddItemListeners(getDetailsCallback, getIconCallback, purchaseItemCallback, item);
            }
        }

        private static void AddItemListeners(Delegate.GetItemDetails getDetailsCallback,
            Delegate.GetItemProfileIcon getIconCallback, Delegate.PurchaseItem purchaseItemCallback, PurchaseItem item)
        {
            item.ItemDetailsRequest += getDetailsCallback;
            item.ProfileIconRequest += getIconCallback;
            item.ItemPurchaseRequested += purchaseItemCallback;
        }

        private void InitItemsIds()
        {
            foreach (var item in purchaseItems)
                item.Init(_testItemId);
        }
    }

    public class PurchaseItem : MonoBehaviour
    {
        [SerializeField] private PurchaseItemView purchaseItemView;
        [SerializeField] private PurchaseItemDetailsView purchaseItemDetailsView;

        private string _itemId;
        
        public event Delegate.GetItemDetails ItemDetailsRequest;
        public event Delegate.GetItemProfileIcon ProfileIconRequest;
        public event Delegate.PurchaseItem ItemPurchaseRequested;

        private void Awake()
        {
            AddListeners();
        }

        public void Init(string itemId)
        {
            _itemId = itemId;
        }

        private void AddListeners()
        {
            purchaseItemView.AddPurchaseListener(OnPreviewPurchaseClicked);
            purchaseItemDetailsView.AddPurchaseListener(OnPurchaseClicked);
        }

        private void OnPurchaseClicked()
        {
            ItemPurchaseRequested?.Invoke(_itemId);
        }

        private void OnPreviewPurchaseClicked()
        {
            ItemDetailsRequest?.Invoke(_itemId, OnDetailsReceived);
        }

        private void OnDetailsReceived(PurchaseItemModel itemDetails)
        {
            purchaseItemDetailsView.SetDetails(itemDetails);
            ProfileIconRequest?.Invoke(itemDetails.item_image, OnProfileIconReceived);
        }

        private void OnProfileIconReceived(Sprite icon)
        {
            purchaseItemDetailsView.SetProfileIcon(icon);
        }
    }
    
    public class PurchaseItemView : PurchaseEntityView, IPurchaseItemView
    {
        private IPurchaseItemDetailsView _itemDetailsView;
        
        private void OnDestroy()
        {
            RemoveListeners();
        }

        public void Init(IPurchaseItemDetailsView itemDetailsView)
        {
            _itemDetailsView = itemDetailsView;
            AddListeners();
        }

        private void AddListeners()
        {
            AddPurchaseListener(OnPurchaseClicked);
        }

        private void RemoveListeners()
        {
            RemovePurchaseListener(OnPurchaseClicked);
        }

        private void OnPurchaseClicked()
        {
            _itemDetailsView?.SetVisibleState(true);
        }
    }

    public interface IPurchaseItemView
    {
        void Init(IPurchaseItemDetailsView itemDetailsView);
    }

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
            itemImage.enabled = enableState;
        }

        private void SetDefaults()
        {
            UpdateImage(null, false);
            SetText(price, "");
        }

    }

    public interface IPurchaseItemDetailsView : IVisible
    {
        void SetDetails(PurchaseItemModel itemDetails);
        void SetProfileIcon(Sprite sprite);
        void AddCloseListener(UnityAction purchaseCallback);
        void RemoveCloseListener(UnityAction purchaseCallback);
    }

    public interface IVisible
    {
        void SetVisibleState(bool state);
    }

    public class PurchaseEntityView : MonoBehaviour, IPurchasable
    {
        [SerializeField] protected Button purchaseButton;
        
        public void AddPurchaseListener(UnityAction purchaseCallback)
        {
            purchaseButton.onClick.AddListener(purchaseCallback);
        }

        public void RemovePurchaseListener(UnityAction purchaseCallback)
        {
            purchaseButton.onClick.RemoveListener(purchaseCallback);
        }
    }

    public interface IPurchasable
    {
        void AddPurchaseListener(UnityAction purchaseCallback);
        void RemovePurchaseListener(UnityAction purchaseCallback);
    }
}