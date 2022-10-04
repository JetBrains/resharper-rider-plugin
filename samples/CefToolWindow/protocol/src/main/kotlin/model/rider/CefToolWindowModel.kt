package model.rider

import com.jetbrains.rider.model.nova.ide.SolutionModel
import com.jetbrains.rd.generator.nova.*
import com.jetbrains.rd.generator.nova.PredefinedType.*
import com.jetbrains.rd.generator.nova.csharp.CSharp50Generator
import com.jetbrains.rd.generator.nova.kotlin.Kotlin11Generator
import com.jetbrains.rider.model.nova.ide.IdeRoot
import com.jetbrains.rider.model.nova.ide.UIAutomationInteractionModel.BeControl

@Suppress("unused")
object CefToolWindowModel : Ext(IdeRoot) {
    init {
        // setting(CSharp50Generator.Namespace, "ReSharper.Nuke")
        // setting(Kotlin11Generator.Namespace, "com.jetbrains.rider.plugins.nuke")

        property("toolWindowContent", BeControl)
        property("activateToolWindow", bool) // maybe signal?

        @Suppress("LocalVariableName")
        val CefToolWindowPanel = classdef("BeCefToolWindowPanel") extends BeControl {
            field("url", string.nullable)
            field("html", string.nullable)

            signal("openDevTools", bool)
            signal("openUrl", string)

            call("getResource", string, string).async
        }
    }
}
