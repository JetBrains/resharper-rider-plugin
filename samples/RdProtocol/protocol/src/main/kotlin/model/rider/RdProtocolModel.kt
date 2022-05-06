package model.rider


import com.jetbrains.rider.model.nova.ide.SolutionModel
import com.jetbrains.rd.generator.nova.*
import com.jetbrains.rd.generator.nova.PredefinedType.*
import com.jetbrains.rd.generator.nova.csharp.CSharp50Generator
import com.jetbrains.rd.generator.nova.kotlin.Kotlin11Generator

@Suppress("unused")
object RdProtocolModel : Ext(SolutionModel.Solution) {
    init {
        setting(CSharp50Generator.Namespace, "ReSharperPlugin.RdProtocol")
        setting(Kotlin11Generator.Namespace, "com.jetbrains.rider.plugins.rdprotocol")

        // Properties
        property("property", structdef("customType") {
            field("string", string)
            field("boolean", bool)
            field("array", array(string))
        })

        // Maps
        map("map", string, string)

        // Remote procedure on backend
        call("call", string, array(string)).async

        // Remote procedure on frontend
        callback("callback", string, array(string)).async

        // Event on backend
        sink("sink", string).async

        // Event on frontend
        source("source", string).async

        // Bidirectional event
        signal("signal", string)
    }
}
