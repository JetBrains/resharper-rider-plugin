package com.jetbrains.rider.plugins.ceftoolwindow

import com.intellij.ui.jcef.JBCefJSQuery
import com.jetbrains.rd.ide.model.BeCefToolWindowPanel
import com.jetbrains.rd.ui.bindable.ViewBinder
import com.jetbrains.rd.util.lifetime.Lifetime
import com.jetbrains.rd.util.lifetime.onTermination
import org.cef.CefApp
import org.cef.browser.CefBrowser
import org.cef.browser.CefFrame
import org.cef.callback.CefCallback
import org.cef.handler.CefLoadHandlerAdapter
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

        // Open Dev Tools
        viewModel.openDevTools.advise(lifetime) {
            if (it)
                htmlPanel.browser.openDevtools()
            else
                htmlPanel.browser.cefBrowser.devTools.close(true)
        }

        // Open URL
        viewModel.openUrl.advise(lifetime) {
            htmlPanel.browser.loadURL(it)
        }


        CefApp.getInstance().registerSchemeHandlerFactory("nuke", "*") { _, _, _, request ->
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


        // Web -> Rider
        val jsRequestHandler = JBCefJSQuery.create(htmlPanel.browser)
        jsRequestHandler.addHandler { request: String ->
            viewModel.messageReceived.fire(request)
            null
        }
        lifetime.onTermination { jsRequestHandler.dispose() }

        val loadHandler = object : CefLoadHandlerAdapter() {

            override fun onLoadEnd(browser: CefBrowser?, frame: CefFrame?, httpStatusCode: Int) {
                val addEventListener = """
                    if (!messageSentEvent) {
                        var messageSentEvent = new Event('messageSentEvent');
                        document.addEventListener('messageSentEvent', (message) => {
                            ${jsRequestHandler.inject("JSON.stringify(message.detail)")}
                        });
                    }
                    """.trimIndent()
                frame?.executeJavaScript(addEventListener, frame.url, 0)
            }
        }
        htmlPanel.browser.jbCefClient.addLoadHandler(loadHandler, htmlPanel.browser.cefBrowser)
        lifetime.onTermination {
            htmlPanel.browser.jbCefClient.removeLoadHandler(loadHandler, htmlPanel.browser.cefBrowser)
        }

        // Rider -> Web
        viewModel.sendMessage.set { message: String ->
            val dispatchMessage = "document.dispatchEvent($message)"
            htmlPanel.browser.cefBrowser.executeJavaScript(dispatchMessage, htmlPanel.browser.cefBrowser.url, 0)
        }

        return htmlPanel.component
    }
}
