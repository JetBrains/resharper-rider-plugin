package model.rider

import com.jetbrains.rider.model.nova.ide.SolutionModel
import com.jetbrains.rd.generator.nova.*
import com.jetbrains.rd.generator.nova.PredefinedType.*
import com.jetbrains.rd.generator.nova.csharp.CSharp50Generator
import com.jetbrains.rd.generator.nova.kotlin.Kotlin11Generator
import com.jetbrains.rider.model.nova.ide.UIAutomationInteractionModel.BeControl

@Suppress("unused")
object SamplePluginModel : Ext(SolutionModel.Solution) {

    val MyEnum = enum {
        +"FirstValue"
        +"SecondValue"
    }

    val MyStructure = structdef {
        field("projectFile", string)
        field("target", string)
    }

    init {
        setting(CSharp50Generator.Namespace, "ReSharperPlugin.SamplePlugin")
        setting(Kotlin11Generator.Namespace, "com.jetbrains.rider.sampleplugin")

        property("myString", string)
        property("myBool", bool)
        property("myEnum", MyEnum.nullable)

        map("data", string, string)

        signal("myStructure", MyStructure)

        property("toolWindowContent", BeControl)
        property("activateToolWindow", bool) // maybe signal?

        val BeSampleToolWindowPanel = classdef("BeSampleToolWindowPanel") extends BeControl {
            field("arg", string)
        }
    }
}