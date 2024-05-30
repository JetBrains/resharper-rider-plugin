package com.jetbrains.rider.plugins.samples

import com.intellij.DynamicBundle
import org.jetbrains.annotations.Nls
import org.jetbrains.annotations.NonNls
import org.jetbrains.annotations.PropertyKey

class OptionPagesBundle : DynamicBundle(BUNDLE) {
    companion object {
        @NonNls
        private const val BUNDLE = "messages.OptionPagesBundle"
        private val INSTANCE: OptionPagesBundle = OptionPagesBundle()

        @Nls
        fun message(
            @PropertyKey(resourceBundle = BUNDLE) key: String,
            vararg params: Any
        ): String {
            return INSTANCE.getMessage(key, *params)
        }
    }
}
