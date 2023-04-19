package com.jetbrains.rider.plugins.samples.actions

import com.jetbrains.rider.actions.base.RiderAnAction
import icons.ReSharperIcons

class SampleFrontendTestedAction : RiderAnAction(
    backendActionId = "SampleFrontendTested",
    text = "ReSharper SDK: SampleFrontendTestedAction",
    description = null,
    icon = ReSharperIcons.FeaturesInternal.QuickStartToolWindow
)
