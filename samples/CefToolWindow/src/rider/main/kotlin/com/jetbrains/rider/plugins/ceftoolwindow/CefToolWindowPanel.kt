package com.jetbrains.rider.plugins.ceftoolwindow

import com.jetbrains.rd.ide.model.BeCefToolWindowPanel
import com.jetbrains.rd.ui.bindable.ViewBinder
import com.jetbrains.rd.util.lifetime.Lifetime
import org.cef.CefApp
import org.cef.callback.CefCallback
import org.cef.handler.CefResourceHandler
import org.cef.misc.IntRef
import org.cef.misc.StringRef
import org.cef.network.CefRequest
import org.cef.network.CefResponse
import java.io.IOException
import javax.swing.JComponent

class CefToolWindowPanel : ViewBinder<BeCefToolWindowPanel>
{
    override fun bind(viewModel: BeCefToolWindowPanel, lifetime: Lifetime): JComponent {
        val htmlPanel = LoadableHtmlPanel(viewModel.url, viewModel.html)

        CefApp.getInstance().registerSchemeHandlerFactory("nuke", "*") { browser, frame, scheme_name, request ->
            val resource = viewModel.getResource.sync(request.url)
            val stream = resource.byteInputStream()

            object : CefResourceHandler {
                override fun processRequest(req: CefRequest, callback: CefCallback): Boolean {
                    callback.Continue()

                    return true
                }

                override fun getResponseHeaders(response: CefResponse, response_length: IntRef, redirectUrl: StringRef?) {
                    response.mimeType = "text/html"
                    response.status = 200
                }

                override fun readResponse(data_out: ByteArray, bytes_to_read: Int, bytes_read: IntRef, callback: CefCallback): Boolean {
                    try {
                        val availableSize = stream.available()
                        return if (availableSize > 0) {
                            bytes_read.set(stream.read(data_out, 0, bytes_to_read.coerceAtMost(availableSize)))
                            true
                        } else {
                            bytes_read.set(0)
                            try {
                                stream.close()
                            } catch (ex: IOException) {
                                // TODO
                            }

                            false
                        }
                    } catch (ex: IOException) {
                        // TODO
                        return false
                    }
                }

                override fun cancel() {
                }
            }
        }

        // Open Dev Tools
        viewModel.openDevTools.advise(lifetime) {
           htmlPanel.browser.openDevtools()
        }

        // Open URL
        viewModel.openUrl.advise(lifetime) {
            htmlPanel.browser.loadURL(it)
        }

        return htmlPanel.component
    }
}
