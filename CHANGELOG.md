# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

---

## [4.0.0] - 2026-03-13

### Added
- Support for sending age and gender in the initialization request via the `UserAge` and `UserGender` fields in `TyradsUserInfo.cs`.
- Background preloading and caching of the WebView’s last visited page after SDK initialization.
- Option to hide the Privacy Policy page in the initialization wizard.
- Option to hide the Usage Stats permission page in the initialization wizard.
- Option to set iframe orientation.

### Changed
- Android Dependency Management: Migrated from manual Gradle dependency injection to automatic dependency resolution using EDM4U.
- Support `placementId` in the ShowOffer API for redirecting between different offer walls.
- "Age & Gender" page moved to the webView presentation.
- `LoginUser` method migrated to async/await instead of a coroutine.
- `LoginUser` method accepts a single `LoginData` object instead of multiple individual parameters.
- `LoginUser` method returns `LoginResult` object instead of `bool`.

### Fixed
- Fixed incorrect store redirection behavior on iOS.
- Syncronized WebView orientation with app orientation on iOS.
- Added robust error handling for JSON parsing of backend responses.
- Fixed stencil-related rendering conflicts of UI masks.
- Fixed WebView rendering area.
- Fixed iframe overlap with camera punch holes and notches in landscape mode.
- Fixed unintended cleanup during scene reloads, object destruction, or multiple plugin instantiation scenarios.

### Removed
- Removed the requirement to declare the QUERY_ALL_PACKAGES permission in AndroidManifest.xml, improving compliance with Google Play policies.
- Removed the Premium Widget.

## [4.0.0-pre.5] - 2026-02-26

### Changed
- Android Dependency Management: Migrated from manual Gradle dependency injection to automatic dependency resolution using EDM4U.

## [4.0.0-pre.4] - 2026-01-29

### Added
- Background preloading and caching of the WebView’s last visited page after SDK initialization.

### Fixed
- Fixed incorrect store redirection behavior on iOS.

### Changed
- Reverted to a single session (ads unit instance).
- Replaced the local sessionId (string) with backend provided placementId (uint) for redirecting between different offer walls.
- Added extra device data collection on iOS for machine learning analysis:
  - `hardware`: Device hardware identifier (e.g., iPhone15,3)
  - `virtual`: Indicates if running on a virtual device (simulator/emulator)
  - `totalMemory`: Total device storage in GB
  - `connectionType`: Network connection type (WiFi/Cellular/Ethernet/None)

### Removed
- Removed the requirement to declare the QUERY_ALL_PACKAGES permission in AndroidManifest.xml, improving compliance with Google Play policies.
- Temporarily removed the Premium Widget.

## [4.0.0-pre.3] - 2025-12-29

### Fixed
- Fixed the compatibility with WebGL and UWP builds.

## [4.0.0-pre.2] - 2025-12-04

### Changed
- Removed service disposal from OnDestroy() in runtime plugin to prevent unintended cleanup during scene reloads, object destruction, or multiple plugin instantiation scenarios.
- Removed "Age & Gender" page from the initialization wizard.
- Make a WebView background black.

### Fixed
- Syncronized WebView orientation with app orientation on iOS.
- Added robust error handling for JSON parsing of backend responses.
- Fixed the session credential override validation logic.
- Fixed stencil-related rendering conflicts of UI masks.
- Fixed WebView rendering area.

## [4.0.0-pre.1] - 2025-11-17

### Added
- Support for multiple sessions (multi-instance configuration).
- Option to hide the Privacy Policy page in the initialization wizard.
- Option to hide the Usage Stats permission page in the initialization wizard.
- Option to set iframe orientation.

### Changed
- `LoginUser` method migrated to async/await instead of a coroutine.
- `LoginUser` method accepts a single `LoginData` object instead of multiple individual parameters.
- `LoginUser` method returns `LoginResult` object instead of `bool`.

### Fixed
- Fixed iframe overlap with camera punch holes and notches in landscape mode.

## [3.1.1] - 2025-10-31

### Added
- Support for sending `engagementId` via `TyradsEngagementInfo` in `LoginUser` method.

### Fixed
- Fixed iOS compatibility with iOS 12.0.
- Fixed iOS advertising ID not persisting correctly after app restart.
- Fixed nullable fields not appearing in JSON serialization when they have values.
- Fixed `gender` field being sent with value `0` causing API validation errors.

### Changed
- Made `mediaSourceName` attribute mandatory in the `TyradsMediaSourceInfo.cs`.

## [3.1.0] - 2025-10-27

### Added
- Initial release.
- Introducing unity version of TyrAds SDK based on webView solution.
- Sliding-cards visual style for Premium widget.
- Support of deeplinking routes in `TyrSDKPlugin.ShowOffers`.
- Possibility to send `UserInfo` and `MediaSourceData` in the `TyrSDKPlugin.LoginUser`.

[4.0.0]: https://github.com/tyrads-com/tyrads-unity-sdk-package/compare/v3.1.1...v4.0.0
[4.0.0-pre.5]: https://github.com/tyrads-com/tyrads-unity-sdk-package/compare/v4.0.0-pre.4...v4.0.0-pre.5
[4.0.0-pre.4]: https://github.com/tyrads-com/tyrads-unity-sdk-package/compare/v4.0.0-pre.3...v4.0.0-pre.4
[4.0.0-pre.3]: https://github.com/tyrads-com/tyrads-unity-sdk-package/compare/v4.0.0-pre.2...v4.0.0-pre.3
[4.0.0-pre.2]: https://github.com/tyrads-com/tyrads-unity-sdk-package/compare/v4.0.0-pre.1...v4.0.0-pre.2
[4.0.0-pre.1]: https://github.com/tyrads-com/tyrads-unity-sdk-package/compare/v3.1.1...v4.0.0-pre.1
[3.1.1]: https://github.com/tyrads-com/tyrads-unity-sdk-package/compare/v3.1.0...v3.1.1
[3.1.0]: https://github.com/tyrads-com/tyrads-unity-sdk-package/releases/tag/v3.1.0
