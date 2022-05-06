package com.jetbrains.rider.plugins.rdprotocol

import com.intellij.openapi.project.Project
import com.jetbrains.rdclient.util.idea.LifetimedProjectComponent
import com.jetbrains.rider.projectView.solution

class Component(project: Project) : LifetimedProjectComponent(project) {
    init {
        // Get the model
        val model = project.solution.rdProtocolModel

        // Properties
        model.property.set(
            CustomType(
                string = "value",
                boolean = true,
                array = arrayOf("first", "second")
            )
        )

        model.property.advise(componentLifetime) {
            // Handle changes
        }

        // Maps
        model.map["key"] = "value"
        val mapValue = model.map["key"]

        model.map.advise(componentLifetime) {
            // Handle changes
            var mapKey = it.key
        }

        // Call backend remote procedure
        model.call.sync("args")

        // Set frontend remote procedure
        model.callback.set { arg -> arrayOf(arg) }

        // Trigger frontend event
        model.source.fire("value")

        // Subscribe backend event
        model.sink.advise(componentLifetime) { }

        // Trigger/subscribe bidirectional event
        model.signal.fire("value")
        model.signal.advise(componentLifetime) { }
    }
}
