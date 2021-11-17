package com.jetbrains.rider.plugins.sampleplugin.toolWindow

import com.jetbrains.rd.ui.bindable.ViewBinder
import com.jetbrains.rd.util.lifetime.Lifetime
import com.jetbrains.rider.sampleplugin.BeSampleToolWindowPanel
import javax.swing.JComponent

class SampleToolWindowPanel : ViewBinder<BeSampleToolWindowPanel>
{
    override fun bind(viewModel: BeSampleToolWindowPanel, lifetime: Lifetime): JComponent {
        return null!!
    }

}
