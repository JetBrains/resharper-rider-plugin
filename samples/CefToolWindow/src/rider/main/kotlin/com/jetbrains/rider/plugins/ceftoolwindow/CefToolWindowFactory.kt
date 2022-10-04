package com.jetbrains.rider.plugins.ceftoolwindow

import com.intellij.openapi.project.Project
import com.intellij.openapi.wm.ToolWindow
import com.intellij.openapi.wm.ToolWindowFactory
import com.intellij.ui.content.ContentFactory
import com.jetbrains.rd.platform.util.lifetime
import com.jetbrains.rd.ui.bindable.views.utils.BeControlHost

class CefToolWindowFactory : ToolWindowFactory {
    override fun createToolWindowContent(project: Project, toolWindow: ToolWindow) {
        val contentFactory = ContentFactory.SERVICE.getInstance()
        val component = BeControlHost(project).apply {
            bind(project.lifetime, CefToolWindowModelHost.getInstance(project).interactionModel.toolWindowContent) }
        val content = contentFactory.createContent(component, null, false)
        toolWindow.contentManager.addContent(content)
    }
}


