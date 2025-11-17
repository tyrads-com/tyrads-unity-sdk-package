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
  - [Initialization](#initialization)
    - [Configure SDK Initialization Wizard](#configure-sdk-initialization-wizard)
  - [User Login](#user-login)
    - [Advanced Practices for personalized rewards](#advanced-practices-for-personalized-rewards)
  - [Show Offerwall](#show-offerwall)
- [Premium Offers](#premium-offers)
  - [Available Widget Style options](#available-widget-style-options)
  - [Setting Up the Premium Widget](#setting-up-the-premium-widget)
- [Change Language](#change-language)
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
https://github.com/tyrads-com/tyrads-unity-sdk-package.git#v3.1.0
https://github.com/tyrads-com/tyrads-unity-sdk-package.git#v4.0.0-pre.1
```

# Getting Started

## Scene setup

Add the `TyrSDKPlugin` prefab to your initial scene:
```
Packages/com.tyrads.unity-sdk/Runtime/Resources/TyrAds/Prefabs/TyrSDKPlugin.prefab
```
This enables API access via SDKPlugin.Instance in code.

## Initialization

To initializes the **TyrAds Unity SDK** you must provide the necessary credentials obtained from the TyrAds platform.
These credentials allow your application to establish secure communication with the TyrAds' backend services.

> SDK Initialization best practices:
> * **Initiate early**: It's advisable to initialize the SDK promptly after your app launches to ensure that all Tyr SDK functionalities are accessible when needed.
> * **Initiate authentication**: Login to the SDK with current user details immediately after your user signs up or signs in to the app to set the `userId`.

The SDK now supports multiple session configurations, each defined by a unique identifier.
This enables advanced setups such as presenting different offers for different currencies.
You can configure these sessions through the Unity Editor or programmatically.

1. **Adding Credentials via the Editor**

   After importing the SDK, follow these steps to configure it in your project:
   * **Open the Configuration Window**
     
     Navigate to _**TyrSDK > TyrSDK Settings**_ to access the _**TyrSDK Settings**_ panel.
   * **Manage Session Credentials**
     
     The settings panel allows you to create and configure multiple session entries.
     Each entry includes:
      * Unique Identifier: A string used to reference this session configuration.
      * API Key: A 32-character hexadecimal string (provided by TyrAds).
      * API Secret: A 92-character hexadecimal string (provided by TyrAds).
      * Encryption Key: A 32-character hexadecimal string (provided by TyrAds).
![Screenshot of the Configuration window](https://images.gitbook.com/__img/dpr=2,width=760,onerror=redirect,format=auto,signature=520916382/https%3A%2F%2Ffiles.gitbook.com%2Fv0%2Fb%2Fgitbook-x-prod.appspot.com%2Fo%2Fspaces%252FbqqMYRbr0w4Hv3JxQUF7%252Fuploads%252FSI4W1Cqd8NzlV9vjBm3N%252FScreenshot%25202025-11-17%2520at%252013.38.12.png%3Falt%3Dmedia%26token%3Dfc2ab438-8891-4b41-9c42-a91dde1369f3)

You can add or remove session entries using the **+/–** buttons.
All configured sessions will be available to the SDK at runtime.

2. **Adding Credentials via the Code**

   You can also initialize credentials programmatically. Two methods are available depending on your needs:

   **Initialize a Single Session**

   If your application uses only one session configuration, you can initialize it directly:
   ```
   TyrSDKPlugin.Instance.Init("API_KEY", "API_SECRET", "ENCRYPTION_KEY");
   ```
   **Initialize Multiple Sessions**

   For applications that require multiple session configurations, use:
   ```
   var sessions = new[]
   {
       new SessionConfig("session_1","API_KEY_1","API_SECRET_1","ENCRYPTION_KEY_1"),
       new SessionConfig("session_2","API_KEY_2","API_SECRET_2","ENCRYPTION_KEY_2")
   };

   TyrSDKPlugin.Instance.Init(sessions);
   ```
   Each SessionConfig entry corresponds to one set of credentials, matching the format used in the editor.
   
   **Notes**
   * When using multiple sessions, ensure that any operation requiring authentication references the correct unique identifier.
   * If both Editor-configured sessions and code-based initialization are used, programmatic initialization will override editor settings at runtime unless documented otherwise.
   * Make a _**Init**_ call before making a _**LoginUser**_ call to ensure that your credentials are properly set before the initialization process begins.

### Configure SDK Initialization Wizard
By default, the SDK displays an Initialization Wizard before showing any offers. 

This wizard allows users to:
* Review and accept the Privacy Policy
* Grant permission to collect usage statistics (Android only)
* Provide optional demographic information such as age and gender

You can disable the Privacy Policy page and/or the Usage Stats permission page. 

To modify the wizard behavior:
1. Navigate to _**TyrSDK > TyrSDK Settings**_ to open the _**TyrSDK Settings**_ panel.
2. Select the **Settings** tab.
3. Use the toggles to enable or disable:
   * Show Privacy Policy Page
   * Show Usage Stats Permit Page

![Screenshot of the Settings tab](https://images.gitbook.com/__img/dpr=2,width=760,onerror=redirect,format=auto,signature=-1214244692/https%3A%2F%2Ffiles.gitbook.com%2Fv0%2Fb%2Fgitbook-x-prod.appspot.com%2Fo%2Fspaces%252FbqqMYRbr0w4Hv3JxQUF7%252Fuploads%252FkKlyFhheDLyuKYVzRYuH%252FScreenshot%25202025-11-17%2520at%252013.38.21.png%3Falt%3Dmedia%26token%3D9a90eccc-5588-43d1-821e-f0ea4a10e1fb)

## User Login

Upon initializing the SDK, the mandatory step is to log in the user. 
However, passing a user ID is optional and is only necessary when the publisher operates its own user system. 
This login process ensures that user interactions with the offerwall are accurately tracked and attributed within the application.

> * If you do not provide a user ID, it will be generated automatically and stored in app storage. In that case, uninstalling the app will erase the ID and the user’s progress.
> * **Preferred: supply a backend-controlled, stable user ID (or equivalent) so progress persists across reinstalls and device changes.**

To determine when initialization has completed and to identify which session ID was used, you can await the login operation and read the relevant data from the returned LoginResult:
```
LoginData loginData = new LoginData(userId);
LoginResult result = await TyrSDKPlugin.Instance.LoginUserAsync(loginData);

if (result.IsSuccessful)
{
   //your code for successful init...
   //a list of id for successfully initialized session could be get from result.InitializedSessions 
}
```
You can retrieve the generated user ID after successful initialization by calling the following method:
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
    sub1: "campaign_source",
    sub2: "ad_group",
    sub3: "creative_type",
    sub4: "placement",
    sub5: "custom_param",
    mediaSourceName: "Facebook",
    mediaSourceId: "fb_123",
    mediaSubSourceId: "fb_sub_456",
    incentivized: true,
    mediaAdsetName: "Summer Sale Adset",
    mediaAdsetId: "adset_789",
    mediaCreativeName: "Summer Sale Creative",
    mediaCreativeId: "creative_101",
    mediaCampaignName: "Summer Sale Campaign"
);

var engagementInfo = new TyradsEngagementInfo(
    engagementId: 12345  // Optional: Unique identifier for tracking user engagement
);

LoginData loginData = new LoginData(userId, userInfo, mediaSourceInfo, engagementInfo);
LoginResult result = await TyrSDKPlugin.Instance.LoginUserAsync(loginData);
```
[Sending Media Source Data](https://sdk-doc.tyrads.com/getting-started/advanced-options/sending-media-source-data)

[Sending User Segments / User Info](https://sdk-doc.tyrads.com/getting-started/advanced-options/sending-user-segments-user-info)

## Show Offerwall

Once the SDK is initialized and the user is logged in (if applicable), you can display the offerwall to the user. 
This typically involves calling a function provided by the **TyrAds Unity SDK**, such as `ShowOffers`, passing in the context of your application. 
The offerwall is where users can engage with various offers, advertisements, or promotions provided by TyrAds, potentially earning rewards or incentives in the process.
```
// Note: Campaigns Page is the default route when no specific route is provided
TyrSDKPlugin.Instance.ShowOffers();
```
If you want to display offers for a specific session, you must provide its identifier in the `OffersRoutingData` parameter.
```
OffersRoutingData offersRoutingData = new OffersRoutingData("session_2");
TyrSDKPlugin.Instance.ShowOffers(offersRoutingData);
```
Also, you can specify a route to open a particular page. For campaign-specific routes, you'll need to provide the campaignID as well. Available routes and their usage:
* `TyradsDeepRoutes.Offers` - opens the Campaigns Page
* `TyradsDeepRoutes.ActiveOffers` - opens the Activated Campaigns Page
* `TyradsDeepRoutes.Offer` - opens the Campaign Details Page (requires campaignID)
* `TyradsDeepRoutes.Support` - opens the Campaign Tickets Page (requires campaignID)
```
//Use TyradsDeepRoutes class to avoid typos
OffersRoutingData offersRoutingData = new OffersRoutingData("session_2", TyradsDeepRoutes.Offers); 
TyrSDKPlugin.Instance.ShowOffers(offersRoutingData);

//Specify a route and campaignID
OffersRoutingData offersRoutingData = new OffersRoutingData("session_2", TyradsDeepRoutes.Offer, 111); 
TyrSDKPlugin.Instance.ShowOffers(offersRoutingData);

//If you use only one session, you can set empty string for sessionID
OffersRoutingData offersRoutingData = new OffersRoutingData(string.Empty, TyradsDeepRoutes.ActiveOffers); 
TyrSDKPlugin.Instance.ShowOffers(offersRoutingData);
```

# Premium Offers

The **Premium Widget** is a UI element that becomes available after SDK initialization. 
It is independent of the SDK itself and can be dragged into any **Canvas** within your application.

The **Premium Widget** provides seamless access to various offerwall features:
* More Offers – Opens the full offerwall.
* Active Offers – Displays active offers.
* Campaign record - Open the page with campaign details.  
* Play – Activate campaign and redirects users to the store.

This widget enhances user engagement by integrating smoothly within your game's UI.

The Premium Widget displays offers based on the active session.
To change the active session, call:
```
TyrSDKPlugin.Instance.SwitchToSession("session_2");
```

### Available Widget Style options

The widget style can be changed by setting the `visualizationType` property of the `PremiumWidget` component on prefab.
The default style is `PremiumWidgetVisualizationType.ListView`, which displays the offers in a list.
Other available style is `PremiumWidgetVisualizationType.SlideCards`, which displays the offers in a cards slider.

Alternatively, you can switch the widget visibility from the code:
```
TyrSDKPlugin.Instance.SetPremiumWidgetStyle(visualStyle); //where visualStyle is PremiumWidgetVisualizationType.ListView or PremiumWidgetVisualizationType.SlideCards
```

*List View*

![Premium Widget, List View](https://sdk-doc.tyrads.com/~gitbook/image?url=https%3A%2F%2F347922413-files.gitbook.io%2F%7E%2Ffiles%2Fv0%2Fb%2Fgitbook-x-prod.appspot.com%2Fo%2Fspaces%252FbqqMYRbr0w4Hv3JxQUF7%252Fuploads%252FcL2uTFxnyJ6JLVrB0gZm%252FFrame%25201171276648.png%3Falt%3Dmedia%26token%3D47522ee9-cb4e-488f-97bb-3c647b9ccdf6&width=300&dpr=2&quality=100&sign=72bb3045&sv=2)

*Card View*

![Premium Widget, Card View](https://www.gitbook.com/cdn-cgi/image/dpr=2,width=760,onerror=redirect,format=auto/https%3A%2F%2Ffiles.gitbook.com%2Fv0%2Fb%2Fgitbook-x-prod.appspot.com%2Fo%2Fspaces%252FbqqMYRbr0w4Hv3JxQUF7%252Fuploads%252Fgi6lJ2V1tOozjsIv6d6y%252FPremium%2520option%252015.png%3Falt%3Dmedia%26token%3D38919515-2a7b-442e-8fa9-ad3cc55dfd5d)

### Setting Up the Premium Widget

To integrate the **Premium Widget** into your application, navigate to `PremiumWidget` prefab:
```
Packages/com.tyrads.unity-sdk/Runtime/Prefabs/PremiumWidget/PremiumWidget.prefab
```
Drag the `PremiumWidget` prefab into Canvas on your scene.


*Game UI with integrated Premium Widget*
![Game UI with integrated Premium Widget](https://sdk-doc.tyrads.com/~gitbook/image?url=https%3A%2F%2F347922413-files.gitbook.io%2F%7E%2Ffiles%2Fv0%2Fb%2Fgitbook-x-prod.appspot.com%2Fo%2Fspaces%252FbqqMYRbr0w4Hv3JxQUF7%252Fuploads%252Fgx4NUoBPMPBFep7XoTm3%252FScreenshot%25202025-08-19%2520at%252021.35.12.png%3Falt%3Dmedia%26token%3Dc977076a-6255-4822-80f9-695b3d2a9c17&width=768&dpr=4&quality=100&sign=fe975c33&sv=2)

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

# Obtaining Advertising ID's
## Andrid 12+
Apps updating their target API level to 31 (Android 12) or higher will need to declare a Google Play services normal permission in the `AndroidManifest.xml` file.

Navigate to the `Assets/Plugins/Android/AndroidManifest.xml` inside your project, locate the `AndroidManifest.xml` file and add the following line just before the `<application>`.
```
<uses-permission android:name="com.google.android.gms.permission.AD_ID" />
```
You can read more about Google Advertising ID changes [here](https://support.google.com/googleplay/android-developer/answer/6048248).
## iOS 14+
`NSUserTrackingUsageDescription` should be added to `Info.plist` file like
```
<key>NSUserTrackingUsageDescription</key><string>
```
The SDK includes a Unity build post-process that adds this to the build’s `Info.plist`.
### Request IDFA Permission
Tyrads SDK can work with or without the IDFA permission on iOS 14+. 
If no permission is granted in the ATT popup, the SDK will serve non personalized offers to the user. 
In that scenario the conversion is expected to be lower. 
Offerwall integrations perform better compared to when no IDFA permission is given. 
Our recommendation is that you should ask for IDFA usage permission prior to TyrAds SDK initialization.

# Required Android Permissions
To ensure proper SDK functionality, configure your main AndroidManifest.xml at:
```
Assets/Plugins/Android/AndroidManifest.xml
```
You can either:
* Copy the reference manifest as your main file from 
```
Packages/com.tyrads.unity-sdk/Runtime/AndroidManifestReference/AndroidManifest.xml
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

