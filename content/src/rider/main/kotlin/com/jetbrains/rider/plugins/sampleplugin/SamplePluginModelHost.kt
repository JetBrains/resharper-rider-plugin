package com.jetbrains.rider.plugins.sampleplugin

import com.intellij.openapi.project.Project
import com.intellij.openapi.wm.ToolWindowManager
import com.jetbrains.rd.platform.util.getComponent
import com.jetbrains.rd.platform.util.idea.ProtocolSubscribedProjectComponent
import com.jetbrains.rider.protocol.protocol
import com.jetbrains.rider.sampleplugin.SamplePluginModel

class SamplePluginModelHost(project: Project) : ProtocolSubscribedProjectComponent(project) {

    companion object {

        fun getInstance(project: Project): SamplePluginModelHost {
            return project.getComponent()
        }
    }

    public val interactionModel: SamplePluginModel

    init {

        interactionModel = SamplePluginModel.create(componentLifetime, project.protocol)
        interactionModel.activateToolWindow.change.advise(componentLifetime) {
            if (!it) return@advise

            val toolWindowManager = ToolWindowManager.getInstance(project)
            toolWindowManager.getToolWindow("SampleToolWindow")!!.show()
        }
    }
}