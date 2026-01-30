---
package-name: com.tyrads.unity-sdk
versioning: SemVer 2.0
---

# Table of Contents

- [TyrAds Unity SDK](#tyrads-unity-sdk)
    - [Benefits](#benefits)
- [Requirements](#requirements)
- [Installation](#installation)
- [Getting Started](#getting-started)
  - [Scene setup](#scene-setup)
  - [Credentials setup](#credentials-setup)
  - [User Login](#user-login)
    - [Advanced Practices for personalized rewards](#advanced-practices-for-personalized-rewards)
  - [Open Offerswall](#open-offerswall)
    - [Deeplinking Routes](#deeplinking-routes) 
  - [Configure SDK Initialization Wizard](#configure-sdk-initialization-wizard)
  - [Change Language](#change-language)
  - [Setup Required Android Permissions](#setup-required-android-permissions)
  - [Obtaining Advertising ID's](#obtaining-advertising-ids)
- [Documentation](#documentation)
- [Support](#support)
- [License](#license)

# TyrAds Unity SDK

The **TyrAds Unity SDK** provides an easy way to integrate TyrAds ads and monetization features into your Unity project.  
Supports **Android** and **iOS** platforms.

## Benefits

* The All In One Offer Wall Solution;
* Simple Integration Flow;
* WebView Rendering;
* Locale Management;
* Playtime Rewards;
* Achievements;
* Daily Rewards;

# Requirements
    •   Unity 2021.3 LTS or newer
    •   Android API Level 24+
    •   iOS 12.0+

# Installation

The **TyrAds Unity SDK** available from a Git URL. To install:
1. Open the **_Package Manager_** window in Unity, if it’s not already open.
2. Open the **_Add (+)_** menu in the Package Manager’s toolbar.
3. Select **_Install package from git URL_** from the install menu.
4. Enter a Git URL in the text box:
   ```
   https://github.com/tyrads-com/tyrads-unity-sdk-package.git
   ```
5. Select `Install`.
   
If you want to check for updates and update **TyrAds Unity SDK** dependency to the latest version from the repository, 
click **_Update_** in the **_Package Manager_** window.

If you want to install a certain version of the **TyrAds Unity SDK** you can specified a version in the Git URL:
```
https://github.com/tyrads-com/tyrads-unity-sdk-package.git#v4.0.0-pre.4
```
To explore how to use the TyrAds Unity SDK, import the Demo example from the package’s Samples section in the Unity Package Manager.
To run a Demo scene, please add a valid credentials from our CRM.

# Getting Started

## Scene setup

Add the `TyrSDKPlugin` prefab to your initial scene:
```
Packages/com.tyrads.unity-sdk/Runtime/Resources/TyrAds/Prefabs/TyrSDKPlugin.prefab
```
This enables API access via `SDKPlugin.Instance` in code.

## Credentials setup

To initializes the **TyrAds Unity SDK** you must provide the necessary credentials obtained from the TyrAds platform.
These credentials allow your application to establish secure communication with the TyrAds' backend services.
There are two supported approaches for setting up credentials: a Unity Editor–based approach and a code-based approach.

> SDK Initialization best practices:
> * **Initiate early**: It's advisable to initialize the SDK promptly after your app launches to ensure that all Tyr SDK functionalities are accessible when needed.
> * **Initiate authentication**: Login to the SDK with current user details immediately after your user signs up or signs in to the app to set the `userId`.

1. **Unity Editor–based approach**

   Once TyrSDK is imported, follow these steps to configure it in your project:
   * **Open the Configuration Window**
     
     Navigate to _**TyrSDK > TyrSDK Settings**_ to access the _**TyrSDK Settings**_ panel.
   * **Manage Credentials**

      * API Key: A 32-character hexadecimal string (Mandatory. Grabbed from TyrAds Dashboard).
      * API Secret: A 92-character hexadecimal string (Mandatory. Grabbed from TyrAds Dashboard).
      * Encryption Key: A 32-character hexadecimal string (Optional. Grabbed from TyrAds Dashboard).
![Screenshot of the Configuration window](https://images.gitbook.com/__img/dpr=2,width=760,onerror=redirect,format=auto,signature=-628758196/https%3A%2F%2Ffiles.gitbook.com%2Fv0%2Fb%2Fgitbook-x-prod.appspot.com%2Fo%2Fspaces%252FqhsVeeAwxdFStqbek1mZ%252Fuploads%252Fodw36dOThd903hDiofUD%252FScreenshot%25202026-01-15%2520at%252017.36.19.png%3Falt%3Dmedia%26token%3Df27a4fd6-af74-44b1-86b4-7e12a934adf4)

2. **Code-based approach**

   You can also initialize credentials programmatically. Credentials could be initialized by a call:
   ```
   SessionConfig sessionConfig = new(
                    apiKey: "TestApiKey",
                    apiSecret: "TestApiSecret",
                    encryptionKey: "testEncryptionKey");
   InitConfig initConfig = new (sessionConfig);
   TyrSDKPlugin.Instance.Init(initConfig);
   ```

   `Init` method had to be called before calling `LoginUserAsunc` to ensure that your credentials are properly set before the login process begins.
   
   **Notes**
   
   If credentials are configured both in the Editor and via code, the programmatic initialization will override the Editor settings at runtime.


   **Using Different Credentials for Android and iOS**

   In some projects, you may need to initialize the SDK with different credentials per platform (e.g., separate API keys for iOS and Android).
   
Editor-based configuration does not support platform-specific credentials, this type of configuration cannot be handled through the Settings Editor window.
   
To support platform-specific credentials, you must use the code-based initialization approach.
   
On the developer side, the application should detect the active platform at runtime and provide the correct credentials accordingly.
   
Below is a simple example demonstrating how to configure this:
```
string apiKey = "DEFAULT_API_KEY";
string apiSecretv = "DEFAULT_API_SECRET";
string encryptionKey = "DEFAULT_ENCRYPTION_KEY";

#if UNITY_ANDROID
  apiKey = "ANDROID_API_KEY";
  apiSecretv = "ANDROID_API_SECRET";
  encryptionKey = "ANDROID_ENCRYPTION_KEY";
#elif UNITY_IOS
  apiKey = "IOS_API_KEY";
  apiSecretv = "IOS_API_SECRET";
  encryptionKey = "IOS_ENCRYPTION_KEY";
#endif

SessionConfig sessionConfig = new(
                    apiKey: "TestApiKey",
                    apiSecret: "TestApiSecret",
                    encryptionKey: "testEncryptionKey");
InitConfig initConfig = new (sessionConfig);
TyrSDKPlugin.Instance.Init(initConfig);
```

## User Login

Upon initializing the SDK, the mandatory step is to log in the user. 
However, passing a user ID is optional and is only necessary when the publisher operates its own user system. 
This login process ensures that user interactions with the offerwall are accurately tracked and attributed within the application.
```
LoginData loginData = new LoginData(userId: "userId"); //userID is optional
_ = await TyrSDKPlugin.Instance.LoginUserAsync(loginData);
```

> * If you do not provide a user ID, it will be generated automatically and stored in app storage. In that case, uninstalling the app will erase the ID and the user’s progress.
> * **Preferred: supply a backend-controlled, stable user ID (or equivalent) so progress persists across reinstalls and device changes.**

To determine when initialization has completed, you can await the login operation and read the relevant data from the returned `LoginResult`:
```
LoginData loginData = new LoginData(userId: "userId");
LoginResult result = await TyrSDKPlugin.Instance.LoginUserAsync(loginData);

if (result.IsSuccessful)
{
   //do anything what you want after successful init...
}
```
If you don’t set a user ID in LoginUser, you can retrieve the generated user ID after successful initialization by calling the following method:
```
var userId = TyrSDKPlugin.Instance.GetUserId();
```
### Advanced Practices for personalized rewards
>To maximize the value to the user sending us more data about the user and where they came from allow us to customize the reward experience.
>This can be used to provide feedback of quality of users aswell as customize the earnings journey of different segments of users.

To maximize the value of our Tyr SDK please follow the advanced options for user login. 
This will allow us to personalize the rewards for the user event further and maximize the earnings for you as publisher.
```
var userInfo = new TyradsUserInfo(
    userPhoneNumber: "+1234567890",
    userEmail: "demo@example.com",
    userGroup: "premium_users"
);

var mediaSourceInfo = new TyradsMediaSourceInfo(
    mediaSourceName: "Facebook", //mandatory
    mediaCampaignName: "Summer Sale Campaign",
    mediaSourceId: "fb_123",
    mediaSubSourceId: "fb_sub_456",
    incentivized: true,
    mediaAdsetName: "Summer Sale Adset",
    mediaAdsetId: "adset_789",
    mediaCreativeName: "Summer Sale Creative",
    mediaCreativeId: "creative_101",
    sub1: "campaign_source",
    sub2: "ad_group",
    sub3: "creative_type",
    sub4: "placement",
    sub5: "custom_param"
);

var engagementInfo = new TyradsEngagementInfo(
    engagementId: 12345  // Unique identifier for tracking user engagement
);

LoginData loginData = new LoginData("userID", userInfo, mediaSourceInfo, engagementInfo);
LoginResult result = await TyrSDKPlugin.Instance.LoginUserAsync(loginData);
```
[Sending Media Source Data](https://sdk-doc.tyrads.com/getting-started/advanced-options/sending-media-source-data)

[Sending User Segments / User Info](https://sdk-doc.tyrads.com/getting-started/advanced-options/sending-user-segments-user-info)

## Open Offerswall

Once the SDK is initialized, you can present the offerwall to the user by calling the appropriate method from the TyrAds SDK. 
This is done by creating an OffersRoutingData instance with a specific placementId and passing it to the ShowOffers method.

The placementId (for example, 123) defines the placement configured on the backend and determines which offerwall content will be shown.
```
OffersRoutingData offersRoutingData = new OffersRoutingData(placementId: 123); 
TyrSDKPlugin.Instance.ShowOffers(offersRoutingData);
```
This call opens the offerwall, where users can interact with available offers, advertisements, or promotions and earn rewards or incentives based on their engagement.

### Deeplinking Routes
The Tyrads SDK supports deeplinking to specific sections of the offerwall. 
When initializing or interacting with the SDK, you can specify a route to open a particular page. 
For campaign-specific routes, you'll need to provide the campaignID as well. 
Available routes and their usage:
* `TyradsDeepRoutes.Offers` - opens the Campaigns Page
* `TyradsDeepRoutes.ActiveOffers` - opens the Activated Campaigns Page
* `TyradsDeepRoutes.Offer` - opens the Campaign Details Page (requires campaignID)
* `TyradsDeepRoutes.Support` - opens the Campaign Tickets Page (requires campaignID)
```
//Use TyradsDeepRoutes class to avoid typos
OffersRoutingData offersRoutingData = new OffersRoutingData(placementId: 345, TyradsDeepRoutes.Offers); 
TyrSDKPlugin.Instance.ShowOffers(offersRoutingData);

//Specify a route and campaignID
OffersRoutingData offersRoutingData = new OffersRoutingData(placementId: 231, TyradsDeepRoutes.Offer, campaignId: 111); 
TyrSDKPlugin.Instance.ShowOffers(offersRoutingData);
```

## Configure SDK Initialization Wizard
By default TyrAds SDK before open any offers page show initialization wizard, where user could read and accept TyrAds Privacy Policy, 
give acess to usege stats permit (only on Android) and provide information about age and gender.
You able to disable presentation of the privacy policy and usege stats permit pages.

To do that follow next steps:
1. Navigate to _**TyrSDK > TyrSDK Settings**_ to open the _**TyrSDK Settings**_ panel.
2. Select the **Settings** tab.
3. Switch toggles for 'Show Privacy Policy Page' and 'Show Usage Stats Permit Page'.

![Screenshot of the Settings tab](https://images.gitbook.com/__img/dpr=2,width=760,onerror=redirect,format=auto,signature=1814847610/https%3A%2F%2Ffiles.gitbook.com%2Fv0%2Fb%2Fgitbook-x-prod.appspot.com%2Fo%2Fspaces%252FqhsVeeAwxdFStqbek1mZ%252Fuploads%252FtNMtm7NrFqFF7atcDY7t%252FScreenshot%25202026-01-16%2520at%252010.32.15.png%3Falt%3Dmedia%26token%3D902755e9-5f59-49ac-a8e1-8944f071bc16)

## Change Language

Use the following to change the **TyrAds Unity SDK** language and update its internal locale settings:
```
TyrSDKPlugin.Instance.SetLanguage(LanguageCode.English);
```
**Parameters:**
* `languageCode` (string): A string representing the desired language code (e.g., "`en`" for English, "`es`" for Spanish). 
  This should be a valid ISO 639-1 language code.

  Supported languages: English (`en`), Spanish (`es`), Indonesian (`id`), Japanese (ja), Korean (`ko`), Chinese Simplified (`zh-Hans-CN`).

**Notes:**
* By default, the **TyrAds Unity SDK** uses the device’s system language.
* This method saves the selected language in shared preferences so it persists across sessions.
* Ensure your app and the **TyrAds Unity SDK** support the provided language code; otherwise, **English** will be used.

## Setup Required Android Permissions
To ensure proper SDK functionality, configure your main `AndroidManifest.xml` at:
```
Assets/Plugins/Android/AndroidManifest.xml
```
You can either:
* Copy the reference manifest as your main file from `Assets/Plugins/TyrAdsSDK/AndroidManifestReference/AndroidManifest.xml`, or
* Add the following permissions manually to your existing manifest:
```
<uses-permission android:name="android.permission.PACKAGE_USAGE_STATS" tools:ignore="ProtectedPermissions"/>
<uses-permission android:name="android.permission.READ_EXTERNAL_STORAGE"/>
<uses-permission android:name="android.permission.WRITE_EXTERNAL_STORAGE"/>
<uses-permission android:name="android.permission.INTERNET" />
<uses-permission android:name="android.permission.ACCESS_NETWORK_STATE" />
<uses-permission android:name="com.google.android.gms.permission.AD_ID"/>
<uses-permission android:name="com.google.android.finsky.permission.BIND_GET_INSTALL_REFERRER_SERVICE" />
<service android:name=".BackgroundService" android:enabled="true" android:exported="false" />
    <receiver android:name="com.adjust.sdk.AdjustReferrerReceiver" android:permission="android.permission.INSTALL_PACKAGES" android:exported="true">
      <intent-filter>
        <action android:name="com.android.vending.INSTALL_REFERRER" />
      </intent-filter>
    </receiver>
```
Important Notes:
* **Placement**: Add the permissions inside the `<manifest>` tag but outside the `<application>` tag.
* **Manual Integration**: These permissions are not **added automatically** due to a conflict with main `AndroidManifest`. Make sure to insert them manually.

Minimum SDK Requirements:
* `PACKAGE_USAGE_STATS` may require your app to target **API level 30+**.

## Obtaining Advertising ID's
**Andrid 12+**

Apps updating their target API level to 31 (Android 12) or higher will need to declare a Google Play services normal permission in the `AndroidManifest.xml` file.

Navigate to the `Assets/Plugins/Android/AndroidManifest.xml` inside your project, locate the `AndroidManifest.xml` file and add the following line just before the `<application>`.
```
<uses-permission android:name="com.google.android.gms.permission.AD_ID" />
```
You can read more about Google Advertising ID changes [here](https://support.google.com/googleplay/android-developer/answer/6048248).

**iOS 14+**

`NSUserTrackingUsageDescription` should be added to `Info.plist` file like
```
<key>NSUserTrackingUsageDescription</key><string>
```
The SDK includes a Unity build post-process that adds this to the build’s `Info.plist`.

**Request IDFA Permission**

Tyrads SDK can work with or without the IDFA permission on iOS 14+. 
If no permission is granted in the ATT popup, the SDK will serve non personalized offers to the user. 
In that scenario the conversion is expected to be lower. 
Offerwall integrations perform better compared to when no IDFA permission is given. 
Our recommendation is that you should ask for IDFA usage permission prior to TyrAds SDK initialization.

# Documentation

Full documentation is available at:
👉 https://sdk-doc.tyrads.com

# Support

If you encounter any issues or have feature requests, please contact us:
    •   📧 Email: tech@tyrads.com

# License

© 2026 Tyrads PTE. LTD. All rights reserved.

