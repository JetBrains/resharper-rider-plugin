package com.jetbrains.rider.plugins.samples.actions

import com.jetbrains.rider.test.asserts.shouldBeFalse
import com.jetbrains.rider.test.asserts.shouldBeTrue
import com.jetbrains.rider.test.base.BaseTestWithSolution
import com.jetbrains.rider.test.scriptingApi.callAction
import org.testng.annotations.Test

class SampleFrontendTestedActionTest : BaseTestWithSolution() {

    override fun getSolutionDirectoryName() = "SolutionForTest"

    @Test
    fun testSampleAction() {
        val testFile = activeSolutionDirectory.resolve("file.txt")
        testFile.isFile.shouldBeFalse("Test file should not exist before test")

        callAction(project, "SampleFrontendTested")

        testFile.isFile.shouldBeTrue("Test file should be created by the action")
    }
}
