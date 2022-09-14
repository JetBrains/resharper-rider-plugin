package com.jetbrains.rider.plugins.samples.actions

import com.jetbrains.rider.actions.base.RiderAnAction
import icons.ReSharperIcons

class TestAction : RiderAnAction(
    backendActionId = "TestAction",
    text = "ReSharper SDK: Test Action",
    description = null,
    icon = ReSharperIcons.FeaturesInternal.QuickStartToolWindow
)
