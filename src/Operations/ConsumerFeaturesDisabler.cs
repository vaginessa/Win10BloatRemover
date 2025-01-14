﻿using Microsoft.Win32;
using Win10BloatRemover.UI;
using Win10BloatRemover.Utils;

namespace Win10BloatRemover.Operations;

public class ConsumerFeaturesDisabler : IOperation
{
    private readonly IUserInterface ui;
    public ConsumerFeaturesDisabler(IUserInterface ui) => this.ui = ui;

    public void Run()
    {
        ui.PrintMessage("Writing values into the Registry...");

        // These two policies apply only to Education and Enterprise editions
        Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\CloudContent", "DisableWindowsConsumerFeatures", 1);
        RegistryUtils.SetForCurrentAndDefaultUser(
            @"Software\Policies\Microsoft\Windows\CloudContent", "DisableWindowsSpotlightFeatures", 1);
        // Applies only to Pro, Education and Enterprise editions
        RegistryUtils.SetForCurrentAndDefaultUser(
            @"Software\Policies\Microsoft\Windows\CloudContent", "DisableTailoredExperiencesWithDiagnosticData", 1);
        // This is needed to disable Spotlight on Windows 10 Home and Pro
        RegistryUtils.SetForCurrentAndDefaultUser(
            @"Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "RotatingLockScreenEnabled", 0);
        // Disable customized background images and text, suggestions, notifications, and tips in Microsoft Edge
        Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Edge", "SpotlightExperiencesAndRecommendationsEnabled", 0);

        // Disable automatic apps installation
        RegistryUtils.SetForCurrentAndDefaultUser(
            @"Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "ContentDeliveryAllowed", 0);
        RegistryUtils.SetForCurrentAndDefaultUser(
            @"Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "SilentInstalledAppsEnabled", 0);
        // Meet Now icon
        RegistryUtils.SetForCurrentAndDefaultUser(@"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", "HideSCAMeetNow", 1);
        // News and Interests
        Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Windows Feeds", "EnableFeeds", 0);
        // Search Highlights
        Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Windows Search", "EnableDynamicContentInWSB", 0);
        // Programmable taskbar
        Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\CloudContent", "DisableCloudOptimizedContent", 1);
    }

}
