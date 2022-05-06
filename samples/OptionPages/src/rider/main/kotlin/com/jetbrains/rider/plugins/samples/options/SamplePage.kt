package com.jetbrains.rider.plugins.samples.options

import com.jetbrains.rider.settings.simple.SimpleOptionsPage

class SamplePage : SimpleOptionsPage(
    name = "ReSharper SDK",
    pageId = "SamplePage" // Must be in sync with SamplePage.PID
) {
    override fun getId(): String {
        return "SamplePage"
    }
}