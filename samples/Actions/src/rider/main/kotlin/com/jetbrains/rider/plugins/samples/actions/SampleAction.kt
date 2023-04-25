package com.jetbrains.rider.plugins.samples.actions

import com.jetbrains.rd.ui.bedsl.dsl.description
import com.jetbrains.rider.actions.RiderActionsBundle
import com.jetbrains.rider.actions.base.RiderAnAction
import icons.ReSharperIcons

class SampleAction : RiderAnAction(
    backendActionId = "Sample", // Id == CSharpClassName.TrimEnd("Action")
    // Icon must also be changed in backend code
    icon = ReSharperIcons.FeaturesInternal.QuickStartToolWindow
)
