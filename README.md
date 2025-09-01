---
package-name: com.tyrads.unity-sdk
versioning: SemVer 2.0
---

# Table of Contents

- [TyrAds Unity SDK](#tyrads-unity-sdk)
    - [Benefits](#benefits)
- [Installation](#installation)
- [Getting Started](#getting-started)
  - [Scene setup](#scene-setup)
  - [Initialization](#initialization)
  - [User Login](#user-login)
  - [Show Offerwall](#show-offerwall)
- [Premium Offers](#premium-offers)
  - [Setting Up the Premium Widget](#setting-up-the-premium-widget)
- [Change Language](#change-language)
- [Requirements](#requirements)
- [Required Android Permissions](#required-android-permissions)
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
https://github.com/tyrads-com/tyrads-unity-sdk-package.git#3.0.0
```

# Getting Started

## Scene setup

Add the `TyrSDKPlugin` prefab to your initial scene:
```
Packages/com.tyrads.unity-sdk/Runtime/Prefabs/TyrSDKPlugin.prefab
```
This enables API access via SDKPlugin.Instance in code.

## Initialization

To initializes the **TyrAds Unity SDK** within your application you need to provide the API key and API secret obtained from the TyrAds platform. 
This allows your app to communicate securely with TyrAds' servers.
> SDK Initialization best practices:
> * **Initiate early**: It's advisable to initialize the SDK promptly after your app launches to ensure that all Tyr SDK functionalities are accessible when needed.
> * **Initiate authentication**: Login to the SDK with current user details immediately after your user signs up or signs in to the app to set the `userId`.

1. **Adding Credentials via the Editor**
   
   Once TyrSDK is imported, follow these steps to configure it for your project:
   * To open the Configuration Window, navigate to _**TyrSDK > TyrSDK**_ Settings to access the _**TyrSDK Settings**_ panel.
   * Enter Your Credentials
      * API Key: A 32-character hexadecimal string.
      * API Secret: A 92-character hexadecimal string.
      * Encryption Key: A 32-character hexadecimal string.
![Screenshot of the Configuration window](https://sdk-doc.tyrads.com/~gitbook/image?url=https%3A%2F%2F347922413-files.gitbook.io%2F%7E%2Ffiles%2Fv0%2Fb%2Fgitbook-x-prod.appspot.com%2Fo%2Fspaces%252FbqqMYRbr0w4Hv3JxQUF7%252Fuploads%252Fy9vm2HSd0uh5dAyfLcek%252FScreenshot%25202025-08-19%2520at%252018.16.13.png%3Falt%3Dmedia%26token%3D6b0d470f-ef16-4c60-bf2c-b4e56bee43ff&width=768&dpr=2&quality=100&sign=802efbe0&sv=2)

2. **Adding Credentials via the Code**

    Make a call:
    ```
    TyrSDKPlugin.Instance.Init("API_KEY", "API_SECRET", "ENCRYPTION_KEY");
    ```
    before making a LoginUser call. This ensures that your credentials are properly set before the initialization process begins.

## User Login

Upon initializing the SDK, the mandatory step is to log in the user. However, passing a user ID is optional and is only necessary when the publisher operates its own user system. This login process ensures that user interactions with the offerwall are accurately tracked and attributed within the application.
```
TyrSDKPlugin.Instance.LoginUser(userId); //userID is optional
```
> * If you do not provide a user ID, it will be generated automatically and stored in app storage. In that case, uninstalling the app will erase the ID and the user’s progress.
> * **Preferred: supply a backend-controlled, stable user ID (or equivalent) so progress persists across reinstalls and device changes.**

To receive the initialization completion result, subscribe to the event before calling LoginUser:
```
TyrSDKPlugin.Instance.InitializationCompleted += OnSdkInitializationCompleted;
...
private void OnSdkInitializationCompleted(bool isSuccess)
{
    // your code here
}
```
If you don’t set a user ID in `LoginUser`, you can retrieve the generated user ID after successful initialization by calling the following method:
```
var userId = TyrSDKPlugin.Instance.GetUserId();
```
## Show Offerwall

Once the SDK is initialized and the user is logged in (if applicable), you can display the offerwall to the user. 
This typically involves calling a function provided by the **TyrAds Unity SDK**, such as `ShowOffers`, passing in the context of your application. 
The offerwall is where users can engage with various offers, advertisements, or promotions provided by TyrAds, potentially earning rewards or incentives in the process.
```
TyrSDKPlugin.Instance.ShowOffers();
```
# Premium Offers

The **Premium Widget** is a UI element that becomes available after SDK initialization. 
It is independent of the SDK itself and can be dragged into any **Canvas** within your application.

The **Premium Widget** provides seamless access to various offerwall features:
* More Offers – Opens the full offerwall.
* Active Offers – Displays active offers.
* Campaign record - Open the page with campaign details.  
* Play – Redirects users to the store.

This widget enhances user engagement by integrating smoothly within your game's UI.

![Screenshot of the PremiumWidget](https://sdk-doc.tyrads.com/~gitbook/image?url=https%3A%2F%2F347922413-files.gitbook.io%2F%7E%2Ffiles%2Fv0%2Fb%2Fgitbook-x-prod.appspot.com%2Fo%2Fspaces%252FbqqMYRbr0w4Hv3JxQUF7%252Fuploads%252FcL2uTFxnyJ6JLVrB0gZm%252FFrame%25201171276648.png%3Falt%3Dmedia%26token%3D47522ee9-cb4e-488f-97bb-3c647b9ccdf6&width=300&dpr=2&quality=100&sign=72bb3045&sv=2)

## Setting Up the Premium Widget

To integrate the **Premium Widget** into your application, navigate to `PremiumWidget` prefab:
```
Packages/com.tyrads.unity-sdk/Runtime/Prefabs/PremiumWidget/PremiumWidget.prefab
```
Drag the `PremiumWidget` prefab into Canvas on your scene.

Example with PremiumWidget:

![Screenshot of the PremiumWidget](https://sdk-doc.tyrads.com/~gitbook/image?url=https%3A%2F%2F347922413-files.gitbook.io%2F%7E%2Ffiles%2Fv0%2Fb%2Fgitbook-x-prod.appspot.com%2Fo%2Fspaces%252FbqqMYRbr0w4Hv3JxQUF7%252Fuploads%252Fgx4NUoBPMPBFep7XoTm3%252FScreenshot%25202025-08-19%2520at%252021.35.12.png%3Falt%3Dmedia%26token%3Dc977076a-6255-4822-80f9-695b3d2a9c17&width=768&dpr=4&quality=100&sign=fe975c33&sv=2)

# Change Language

Use the following to change the **TyrAds Unity SDK** language and update its internal locale settings:
```
TyrSDKPlugin.Instance.SetLanguage("en");
```
**Parameters:**
* languageCode (string): A string representing the desired language code (e.g., "`en`" for English, "`es`" for Spanish). 
  This should be a valid ISO 639-1 language code.

  Supported languages: English (`en`), Spanish (`es`), Indonesian (`id`), Japanese (ja), Korean (`ko`), Chinese Simplified (`zh-Hans-CN`).

**Notes:**
* By default, the **TyrAds Unity SDK** uses the device’s system language.
* This method saves the selected language in shared preferences so it persists across sessions.
* Ensure your app and the **TyrAds Unity SDK** support the provided language code; otherwise, **English** will be used.

# Requirements
    •   Unity 2021.3 LTS or newer
    •   Android API Level 24+
    •   iOS 11.0+

# Required Android Permissions
To ensure proper SDK functionality, configure your main AndroidManifest.xml at:
```
Assets/Plugins/Android/AndroidManifest.xml
```
You can either:
* Copy the reference manifest as your main file from 
```
Assets/Plugins/TyrAdsSDK/AndroidManifestReference/AndroidManifest.xml
```
or
* Add the following permissions manually to your existing manifest:
```
<uses-permission android:name="android.permission.QUERY_ALL_PACKAGES"
tools:ignore="QueryAllPackagesPermission" />
<uses-permission android:name="android.permission.PACKAGE_USAGE_STATS" />
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
**Important Notes:**
* **Placement:** Add the permissions inside the <manifest> tag but outside the <application> tag.
* **Manual Integration:** These permissions are not added automatically due to a conflict with main AndroidManifest Make sure to insert them manually.
* **Minimum SDK Requirements:**
    * `QUERY_ALL_PACKAGES` and `PACKAGE_USAGE_STATS` may require your app to target **API level 30+**.
    * Google Play may require justification for using `QUERY_ALL_PACKAGES`.

# Documentation

Full documentation is available at:
👉 https://sdk-doc.tyrads.com

# Support

If you encounter any issues or have feature requests, please contact us:
    •   📧 Email: tech@tyrads.com

# License

© 2025 Tyrads PTE. LTD. All rights reserved.