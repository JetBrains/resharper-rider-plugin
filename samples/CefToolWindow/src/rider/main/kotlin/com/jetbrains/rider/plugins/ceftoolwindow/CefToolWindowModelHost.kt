package com.jetbrains.rider.plugins.ceftoolwindow

import com.intellij.openapi.project.Project
import com.intellij.openapi.wm.ToolWindowManager
import com.jetbrains.rd.ide.model.CefToolWindowModel
import com.jetbrains.rd.ide.model.cefToolWindowModel
import com.jetbrains.rd.platform.util.idea.ProtocolSubscribedProjectComponent
import com.jetbrains.rider.protocol.protocol
import com.jetbrains.rider.util.idea.getComponent

class CefToolWindowModelHost(project: Project) : ProtocolSubscribedProjectComponent(project) {

    companion object {

        fun getInstance(project: Project): CefToolWindowModelHost {
            return project.getComponent()
        }
    }

    public val interactionModel: CefToolWindowModel

    init {

        interactionModel = project.protocol.cefToolWindowModel
        interactionModel.activateToolWindow.change.advise(projectComponentLifetime) {
            if (!it) return@advise

            val toolWindowManager = ToolWindowManager.getInstance(project)
            toolWindowManager.getToolWindow("CefToolWindow")!!.show()
        }
    }
}