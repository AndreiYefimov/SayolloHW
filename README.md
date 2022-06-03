# SayolloHW

## 1. Video Ads

**Video Ads** represents a set of tools for displaying some video add directly from the app.

From the given pre-installed VAST XML model we receive the video link, that is cached into the user persistent data folder and then re-used instead of downloading or spending user traffic again and again.

#### Installation

There are several prefabs, united into a single demo on, that is called **TestAdsManager**, that can be found in ```Assets -> Prefabs -> Ads``` folder.

![image](https://user-images.githubusercontent.com/29107526/171829786-dadcfd13-959b-4b5e-9e83-36ec81e0fbe4.png)

**TestAdsManager** has a components with same name, which has the following fields:
  - Play Video Button - play button, afther clicking on which the video payback is requested
  - Canvas - scene canvas, that is turned off durring the video payback
  - Video Controller - controlls the video payback flow
  - API Url - API, where the VAST XML model data can be taken via the web request
  - Video Default Name - the dafult name of the video file that system will use while caching it
  - Cached Video Path - directoty in users pesistent data folder, where all the cached videos are stored

![image](https://user-images.githubusercontent.com/29107526/171831380-7a6e61de-745f-446d-a1b4-8c55bff6e057.png)

**TestAdsManager** represents some test system with a test UI, including sending requests, handling their responces, and sending the required conponents throw the test pipeline, that is recommended to implement in some advanced way.

Key prefabs, such as **VideoController** and **VideoPlayer** are also placed as separate ones, can be found at the same prefab folder (```Assets -> Prefabs -> Ads```).

![image](https://user-images.githubusercontent.com/29107526/171832843-e05e2fef-de7c-4e75-9f10-c9fecf65ec98.png)

#### Demo Scene

Demo scene '**VideoAdsDemo**' is added to assets as well and can be found in ```Assets -> Scenes``` folder.

![image](https://user-images.githubusercontent.com/29107526/171833072-e57927a1-b57e-4968-9fab-d93a92da9486.png)
![image](https://user-images.githubusercontent.com/29107526/171834861-77397c0a-ace7-4a79-85fe-1979025b8270.png)



## 2. Purchase View

**Purchase View** represent showing a UI of a simple purchase - prefab with a
button that opens the purchase view where the user can buy a product.

## Installation

Basically we havea **PuracheItem** prefab, that contains its pre-view and detailed view panels with such info as:
  * Title
  * Item_image
  * Currency
  * Currency_sign

Prefab review:
![image](https://user-images.githubusercontent.com/29107526/171835975-1c4ea983-5cce-4d48-9216-39338ebac7df.png)

Item Pre-view:
![image](https://user-images.githubusercontent.com/29107526/171836172-05c0ccc5-2aeb-48a5-afb4-be60f61ce22e.png)

Item detailed view:
![image](https://user-images.githubusercontent.com/29107526/171836253-81cb7151-ffe8-430d-9287-3c8a573fa62d.png)

To use it in your scen - just drag and drop it into the scene canvas, prefab **PuracheItem** can be found in ```Assets -> Prefabs -> Purchases``` folder,
however all the purchase items should attached to **PurchaseManager**, which should be placed on scene as well, and currently is represented as a **TestPurchaseManager** prefab in the same folder (```Assets -> Prefabs -> Purchases```).

TestPurchaseManager prefab:
![image](https://user-images.githubusercontent.com/29107526/171837038-afd73fa1-f796-43af-8faf-557af08e4979.png)


![image](https://user-images.githubusercontent.com/29107526/171836994-b23a036b-2c31-4060-a39e-0de6ea074eea.png)
![image](https://user-images.githubusercontent.com/29107526/171837008-519b5c56-b02e-478e-98cd-492892ffc13f.png)

#### Demo scene

There is **TestAppManager** prefab, that represents whole the flow of purchasing some item (prefab can be found in ```Assets -> Prefabs -> Purchases``` folder).

![image](https://user-images.githubusercontent.com/29107526/171837387-00da5f34-f4ce-43ec-8ef2-ed717abd48b8.png)

The flow is the following:
  - We get all the purchasable items from the start (x1 items)

![image](https://user-images.githubusercontent.com/29107526/171837789-f70251d6-2d49-4db9-84bf-2db23c390c7c.png)

  - When we press 'Purchase' on eny of them - system sends a request for getting a detailed info
  - When detailed info request response is received - sustem opens the detailed view of the selected item and shows the following info:
      * Title
      * Item_image
      * Currency
      * Currency_sign
      
![image](https://user-images.githubusercontent.com/29107526/171837871-46b376b6-0f47-4e3b-8bd6-f6c8aa7c42be.png)

  - Then, if want to purchase it for 100% sure - we just click 'Purchase' in item detailed view, and receive a confirmation panel (that is represented by the **ConfirmPurchasePanel** prefab in ```Assets -> Prefabs -> Purchases``` folder). Otherise - press the (X) close button in the top-left corner of a confirmation panel to get back to the item detailed view.

![image](https://user-images.githubusercontent.com/29107526/171838069-edc3b0f4-ea1a-40b5-a0cd-89228a68c393.png)

  - On a confirmation panel we can see the foolowing fields to be filled:
    ➢ Email
    ➢ Credit card number
    ➢ Expiration date
  - To confirm our purchase all the fields should be filled and then 'Confirm' button should be pressed.
  - After passing the previous step, system checks all the given data end send a purchase request.
  - When the request is sent - we get back to the item detailed view, where we can get back to items list via pressing (X) close button in the top-left corner.

For checking the testing system, you just need to drug and drop a **TestPurchaseManager** prefab from the ```Assets -> Prefabs -> Purchases``` folder to your scene.

![image](https://user-images.githubusercontent.com/29107526/171839278-b4d2f7cc-2de2-4e47-8504-b6947aca42f4.png)

Or open a pre-installed demo scene **PurchaseViewDemo**, that can be found in ```Assets -> Scenes``` folder.

![image](https://user-images.githubusercontent.com/29107526/171839357-29206b70-a1e6-4169-9c20-3c664fcc3add.png)

