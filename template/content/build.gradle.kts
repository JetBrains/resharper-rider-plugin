// Reference https://www.jetbrains.org/intellij/sdk/docs/tutorials/build_system/gradle_guide.html
import groovy.ant.FileNameFinder
import org.apache.tools.ant.taskdefs.condition.Os
import java.io.ByteArrayOutputStream

plugins {
    id("java")
    id("org.jetbrains.kotlin.jvm") version "2.0.0"
    id("org.jetbrains.intellij") version "1.17.3"     // See https://github.com/JetBrains/gradle-intellij-plugin/releases
    id("com.jetbrains.rdgen") version "2024.1.1"      // See https://github.com/JetBrains/rd/releases
    id("me.filippov.gradle.jvm.wrapper") version "0.14.0"
}

val isWindows = Os.isFamily(Os.FAMILY_WINDOWS)
val rdLibDirectory: () -> File = {
    File(tasks.setupDependencies.get().idea.get().classes, "lib/rd")
}

extra["isWindows"] = isWindows
extra["rdLibDirectory"] = rdLibDirectory

val DotnetSolution: String by project
val BuildConfiguration: String by project
val ProductVersion: String by project
val DotnetPluginId: String by project
val RiderPluginId: String by project
val PublishToken: String by project

repositories {
    maven { setUrl("https://cache-redirector.jetbrains.com/intellij-repository/snapshots") }
    maven { setUrl("https://cache-redirector.jetbrains.com/maven-central") }
}

tasks.wrapper {
    gradleVersion = "8.8"
    distributionType = Wrapper.DistributionType.ALL
    distributionUrl = "https://cache-redirector.jetbrains.com/services.gradle.org/distributions/gradle-${gradleVersion}-all.zip"
}

version = extra["PluginVersion"] as String

tasks.processResources {
    from("dependencies.json") { into("META-INF") }
}

sourceSets {
    main {
        java.srcDir("src/rider/main/java")
        kotlin.srcDir("src/rider/main/kotlin")
        resources.srcDir("src/rider/main/resources")
    }
}

tasks.compileKotlin {
    kotlinOptions { jvmTarget = "17" }
}

val setBuildTool by tasks.registering {
    doLast {
        extra["executable"] = "dotnet"
        var args = mutableListOf("msbuild")

        if (isWindows) {
            val stdout = ByteArrayOutputStream()
            exec {
                executable("${rootDir}\\tools\\vswhere.exe")
                args("-latest", "-property", "installationPath", "-products", "*")
                standardOutput = stdout
                workingDir(rootDir)
            }

            val directory = stdout.toString().trim()
            if (directory.isNotEmpty()) {
                val files = FileNameFinder().getFileNames("${directory}\\MSBuild", "**/MSBuild.exe")
                extra["executable"] = files.get(0)
                args = mutableListOf("/v:minimal")
            }
        }

        args.add("${DotnetSolution}")
        args.add("/p:Configuration=${BuildConfiguration}")
        args.add("/p:HostFullIdentifier=")
        extra["args"] = args
    }
}

val compileDotNet by tasks.registering {
    dependsOn(setBuildTool)
    doLast {
        val executable: String by setBuildTool.get().extra
        val arguments = (setBuildTool.get().extra["args"] as List<String>).toMutableList()
        arguments.add("/t:Restore;Rebuild")
        exec {
            executable(executable)
            args(arguments)
            workingDir(rootDir)
        }
    }
}

val testDotNet by tasks.registering {
    doLast {
        exec {
            executable("dotnet")
            args("test","${DotnetSolution}","--logger","GitHubActions")
            workingDir(rootDir)
        }
    }
}

tasks.buildPlugin {
    doLast {
        copy {
            from("${buildDir}/distributions/${rootProject.name}-${version}.zip")
            into("${rootDir}/output")
        }

        // TODO: See also org.jetbrains.changelog: https://github.com/JetBrains/gradle-changelog-plugin
        val changelogText = file("${rootDir}/CHANGELOG.md").readText()
        val changelogMatches = Regex("(?s)(-.+?)(?=##|$)").findAll(changelogText)
        val changeNotes = changelogMatches.map {
            it.groups[1]!!.value.replace("(?s)- ".toRegex(), "\u2022 ").replace("`", "").replace(",", "%2C").replace(";", "%3B")
        }.take(1).joinToString()

        val executable: String by setBuildTool.get().extra
        val arguments = (setBuildTool.get().extra["args"] as List<String>).toMutableList()
        arguments.add("/t:Pack")
        arguments.add("/p:PackageOutputPath=${rootDir}/output")
        arguments.add("/p:PackageReleaseNotes=${changeNotes}")
        arguments.add("/p:PackageVersion=${version}")
        exec {
            executable(executable)
            args(arguments)
            workingDir(rootDir)
        }
    }
}

dependencies {
    intellijPlatform {
        rider(ProductVersion)
        jetbrainsRuntime()
        instrumentationTools()

        // TODO: add plugins
        // bundledPlugin("uml")
        // bundledPlugin("com.jetbrains.ChooseRuntime:1.0.9")
    }
}

tasks.runIde {
    // Match Rider's default heap size of 1.5Gb (default for runIde is 512Mb)
    maxHeapSize = "1500m"

    // Rider's backend doesn't support dynamic plugins. It might be possible to work with auto-reload of the frontend
    // part of a plugin, but there are dangers about keeping plugins in sync
    autoReloadPlugins.set(false)

    // gradle-intellij-plugin will download the default version of the JBR for the snapshot. Update if required
    // jbrVersion = "jbr_jcef-11_0_6b765.40" // https://confluence.jetbrains.com/display/JBR/Release+notes
}

rdgen {
    val modelDir = File(rootDir, "protocol/src/main/kotlin/model")
    val csOutput = File(rootDir, "src/dotnet/${DotnetPluginId}/Rider")
    val ktOutput = File(rootDir, "src/rider/main/kotlin/${RiderPluginId.replace('.','/').toLowerCase()}")

    verbose = true
    classpath({
        "${rdLibDirectory()}/rider-model.jar"
    })
    sources("${modelDir}/rider")
    hashFolder = "${buildDir}"
    packages = "model.rider"

    generator {
        language = "kotlin"
        transform = "asis"
        root = "com.jetbrains.rider.model.nova.ide.IdeRoot"
        namespace = "com.jetbrains.rider.model"
        directory = "$ktOutput"
    }

    generator {
        language = "csharp"
        transform = "reversed"
        root = "com.jetbrains.rider.model.nova.ide.IdeRoot"
        namespace = "JetBrains.Rider.Model"
        directory = "$csOutput"
    }
}

tasks.patchPluginXml {
    // TODO: See also org.jetbrains.changelog: https://github.com/JetBrains/gradle-changelog-plugin
    val changelogText = file("${rootDir}/CHANGELOG.md").readText()
    val changelogMatches = Regex("(?s)(-.+?)(?=##|\$)").findAll(changelogText)

    changeNotes.set(changelogMatches.map {
        it.groups[1]!!.value.replace("(?s)\r?\n".toRegex(), "<br />\n")
    }.take(1).joinToString())
}

tasks.prepareSandbox {
    dependsOn(compileDotNet)

    val outputFolder = "${rootDir}/src/dotnet/${DotnetPluginId}/bin/${DotnetPluginId}.Rider/${BuildConfiguration}"
    val dllFiles = listOf(
            "$outputFolder/${DotnetPluginId}.dll",
            "$outputFolder/${DotnetPluginId}.pdb",

            // TODO: add additional assemblies
    )

    dllFiles.forEach({ f ->
        val file = file(f)
        from(file, { into("${rootProject.name}/dotnet") })
    })

    doLast {
        dllFiles.forEach({ f ->
            val file = file(f)
            if (!file.exists()) throw RuntimeException("File ${file} does not exist")
        })
    }
}

tasks.publishPlugin {
    dependsOn(testDotNet)
    dependsOn(tasks.buildPlugin)
    token.set("${PublishToken}")

    doLast {
        exec {
            executable("dotnet")
            args("nuget","push","output/${DotnetPluginId}.${version}.nupkg","--api-key","${PublishToken}","--source","https://plugins.jetbrains.com")
            workingDir(rootDir)
        }
    }
}
