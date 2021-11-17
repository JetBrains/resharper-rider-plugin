package com.jetbrains.rider.plugins.sampleplugin.toolWindow

import com.intellij.openapi.project.Project
import com.intellij.openapi.wm.ToolWindow
import com.intellij.openapi.wm.ToolWindowFactory
import com.intellij.ui.content.ContentFactory
import com.jetbrains.rd.platform.util.lifetime
import com.jetbrains.rd.ui.bindable.views.utils.BeControlHost
import com.jetbrains.rider.plugins.sampleplugin.SamplePluginModelHost

class SampleToolWindowFactory : ToolWindowFactory {
    override fun createToolWindowContent(p0: Project, p1: ToolWindow) {
        val contentFactory = ContentFactory.SERVICE.getInstance()
        var component = BeControlHost(p0).apply {
            bind(p0.lifetime, SamplePluginModelHost.getInstance(p0).interactionModel.toolWindowContent) }
        val content = contentFactory.createContent(component, "", false)
        p1.contentManager.addContent(content)
    }
}
