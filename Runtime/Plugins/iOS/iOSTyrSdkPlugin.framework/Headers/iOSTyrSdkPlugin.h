//
//  iOSTyrSdkPlugin.h
//  iOSTyrSdkPlugin
//
#pragma once
#import <Foundation/Foundation.h>

//! Project version number for iOSTyrSdkPlugin.
FOUNDATION_EXPORT double iOSTyrSdkPluginVersionNumber;

//! Project version string for iOSTyrSdkPlugin.
FOUNDATION_EXPORT const unsigned char iOSTyrSdkPluginVersionString[];

// MARK: - Unity Native Interface Declarations

#ifdef __cplusplus
extern "C" {
#endif

// MARK: - Initialization
/// Initialize the iOS TyrSDK plugin
/// @return true if initialization was successful, false otherwise
bool tyrads_initialize(void);

// MARK: - User selection status of the tracking permission
/// Get selection status of the tracking permission as string
/// @return unknown or selected state
const char* tyrads_getTrackingPermissionStatusSelected(void);

// MARK: - Advertising ID Management
/// Start the asynchronous advertising ID fetch process
void tyrads_fetchAdvertisingIdAsync(void);

/// Check if the advertising ID fetch has completed
/// @return true if fetch is complete, false otherwise
bool tyrads_getAdvertisingIdFetchCompleted(void);

/// Get the advertising ID string (call only after fetch is complete)
/// @return Pointer to the advertising ID string, or NULL if not available
const char* tyrads_getAdvertisingId(void);

// MARK: - Device Data
/// Get device data as JSON string
/// @return Pointer to the device data JSON string
const char* tyrads_getDeviceData(void);

// MARK: - Web View Management
/// Open a native web view with the specified URL
/// @param url The URL to load in the web view
void tyrads_openNativeWebView(const char* url);

/// Close the currently open web view
void tyrads_closeWebView(void);

// MARK: - Language Change Callback
/// Register language change callback function pointer
/// @param callback Function pointer to the callback function
void tyrads_registerLanguageChangeCallback(void (*callback)(const char*));

/// Notify Unity about language change
/// @param languageCode The language code to send to Unity
void tyrads_notifyLanguageChange(const char* languageCode);

// MARK: - App Tracking Transparency
/// Check if app tracking permission is granted
/// @return true if permission is granted, false otherwise
bool tyrads_hasAppTrackingPermission(void);

#ifdef __cplusplus
}
#endif
