plugins {
    id("java")
    id("org.jetbrains.kotlin.jvm")
}

dependencies {
    implementation("org.jetbrains.kotlin:kotlin-stdlib")
    implementation(group = "", name = "rd-gen")
    implementation(group = "", name = "rider-model")
}

val rdLibDirectory: () -> File by rootProject.extra

repositories {
    mavenCentral()
    flatDir {
        dirs(rdLibDirectory().absolutePath)
    }
}
