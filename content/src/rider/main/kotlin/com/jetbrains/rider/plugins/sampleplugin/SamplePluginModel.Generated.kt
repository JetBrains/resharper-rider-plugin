@file:Suppress("EXPERIMENTAL_API_USAGE","EXPERIMENTAL_UNSIGNED_LITERALS","PackageDirectoryMismatch","UnusedImport","unused","LocalVariableName","CanBeVal","PropertyName","EnumEntryName","ClassName","ObjectPropertyName","UnnecessaryVariable","SpellCheckingInspection")
package com.jetbrains.rider.sampleplugin

import com.jetbrains.rd.framework.*
import com.jetbrains.rd.framework.base.*
import com.jetbrains.rd.framework.impl.*

import com.jetbrains.rd.util.lifetime.*
import com.jetbrains.rd.util.reactive.*
import com.jetbrains.rd.util.string.*
import com.jetbrains.rd.util.*
import kotlin.reflect.KClass



/**
 * #### Generated from [SamplePluginModel.kt:11]
 */
class SamplePluginModel private constructor(
    private val _myString: RdOptionalProperty<String>,
    private val _myBool: RdOptionalProperty<Boolean>,
    private val _myEnum: RdProperty<MyEnum?>,
    private val _data: RdMap<String, String>,
    private val _myStructure: RdSignal<MyStructure>,
    private val _toolWindowContent: RdOptionalProperty<com.jetbrains.ide.model.uiautomation.BeControl>,
    private val _activateToolWindow: RdOptionalProperty<Boolean>
) : RdExtBase() {
    //companion
    
    companion object : ISerializersOwner {
        
        override fun registerSerializersCore(serializers: ISerializers)  {
            serializers.register(MyEnum.marshaller)
            serializers.register(MyStructure)
            serializers.register(BeSampleToolWindowPanel)
        }
        
        
        
        private val __MyEnumNullableSerializer = MyEnum.marshaller.nullable()
        
        const val serializationHash = -6592640456691276960L
        
    }
    override val serializersOwner: ISerializersOwner get() = SamplePluginModel
    override val serializationHash: Long get() = SamplePluginModel.serializationHash
    
    //fields
    val myString: IOptProperty<String> get() = _myString
    val myBool: IOptProperty<Boolean> get() = _myBool
    val myEnum: IProperty<MyEnum?> get() = _myEnum
    val `data`: IMutableViewableMap<String, String> get() = _data
    val myStructure: ISignal<MyStructure> get() = _myStructure
    val toolWindowContent: IOptProperty<com.jetbrains.ide.model.uiautomation.BeControl> get() = _toolWindowContent
    val activateToolWindow: IOptProperty<Boolean> get() = _activateToolWindow
    //methods
    //initializer
    init {
        _myString.optimizeNested = true
        _myBool.optimizeNested = true
        _myEnum.optimizeNested = true
        _data.optimizeNested = true
        _activateToolWindow.optimizeNested = true
    }
    
    init {
        bindableChildren.add("myString" to _myString)
        bindableChildren.add("myBool" to _myBool)
        bindableChildren.add("myEnum" to _myEnum)
        bindableChildren.add("data" to _data)
        bindableChildren.add("myStructure" to _myStructure)
        bindableChildren.add("toolWindowContent" to _toolWindowContent)
        bindableChildren.add("activateToolWindow" to _activateToolWindow)
    }
    
    //secondary constructor
    internal constructor(
    ) : this(
        RdOptionalProperty<String>(FrameworkMarshallers.String),
        RdOptionalProperty<Boolean>(FrameworkMarshallers.Bool),
        RdProperty<MyEnum?>(null, __MyEnumNullableSerializer),
        RdMap<String, String>(FrameworkMarshallers.String, FrameworkMarshallers.String),
        RdSignal<MyStructure>(MyStructure),
        RdOptionalProperty<com.jetbrains.ide.model.uiautomation.BeControl>(AbstractPolymorphic(com.jetbrains.ide.model.uiautomation.BeControl)),
        RdOptionalProperty<Boolean>(FrameworkMarshallers.Bool)
    )
    
    //equals trait
    //hash code trait
    //pretty print
    override fun print(printer: PrettyPrinter)  {
        printer.println("SamplePluginModel (")
        printer.indent {
            print("myString = "); _myString.print(printer); println()
            print("myBool = "); _myBool.print(printer); println()
            print("myEnum = "); _myEnum.print(printer); println()
            print("data = "); _data.print(printer); println()
            print("myStructure = "); _myStructure.print(printer); println()
            print("toolWindowContent = "); _toolWindowContent.print(printer); println()
            print("activateToolWindow = "); _activateToolWindow.print(printer); println()
        }
        printer.print(")")
    }
    //deepClone
    override fun deepClone(): SamplePluginModel   {
        return SamplePluginModel(
            _myString.deepClonePolymorphic(),
            _myBool.deepClonePolymorphic(),
            _myEnum.deepClonePolymorphic(),
            _data.deepClonePolymorphic(),
            _myStructure.deepClonePolymorphic(),
            _toolWindowContent.deepClonePolymorphic(),
            _activateToolWindow.deepClonePolymorphic()
        )
    }
    //contexts
}
val com.jetbrains.rd.ide.model.Solution.samplePluginModel get() = getOrCreateExtension("samplePluginModel", ::SamplePluginModel)



/**
 * #### Generated from [SamplePluginModel.kt:38]
 */
class BeSampleToolWindowPanel private constructor(
    val arg: String,
    _enabled: RdProperty<Boolean>,
    _controlId: RdProperty<String>,
    _tooltip: RdProperty<String?>,
    _focus: RdSignal<Unit>,
    _visible: RdOptionalProperty<com.jetbrains.ide.model.uiautomation.ControlVisibility>
) : com.jetbrains.ide.model.uiautomation.BeControl (
    _enabled,
    _controlId,
    _tooltip,
    _focus,
    _visible
) {
    //companion
    
    companion object : IMarshaller<BeSampleToolWindowPanel> {
        override val _type: KClass<BeSampleToolWindowPanel> = BeSampleToolWindowPanel::class
        
        @Suppress("UNCHECKED_CAST")
        override fun read(ctx: SerializationCtx, buffer: AbstractBuffer): BeSampleToolWindowPanel  {
            val _id = RdId.read(buffer)
            val _enabled = RdProperty.read(ctx, buffer, FrameworkMarshallers.Bool)
            val _controlId = RdProperty.read(ctx, buffer, FrameworkMarshallers.String)
            val _tooltip = RdProperty.read(ctx, buffer, __StringNullableSerializer)
            val _focus = RdSignal.read(ctx, buffer, FrameworkMarshallers.Void)
            val _visible = RdOptionalProperty.read(ctx, buffer, com.jetbrains.ide.model.uiautomation.ControlVisibility.marshaller)
            val arg = buffer.readString()
            return BeSampleToolWindowPanel(arg, _enabled, _controlId, _tooltip, _focus, _visible).withId(_id)
        }
        
        override fun write(ctx: SerializationCtx, buffer: AbstractBuffer, value: BeSampleToolWindowPanel)  {
            value.rdid.write(buffer)
            RdProperty.write(ctx, buffer, value._enabled)
            RdProperty.write(ctx, buffer, value._controlId)
            RdProperty.write(ctx, buffer, value._tooltip)
            RdSignal.write(ctx, buffer, value._focus)
            RdOptionalProperty.write(ctx, buffer, value._visible)
            buffer.writeString(value.arg)
        }
        
        private val __StringNullableSerializer = FrameworkMarshallers.String.nullable()
        
    }
    //fields
    //methods
    //initializer
    //secondary constructor
    constructor(
        arg: String
    ) : this(
        arg,
        RdProperty<Boolean>(true, FrameworkMarshallers.Bool),
        RdProperty<String>("", FrameworkMarshallers.String),
        RdProperty<String?>(null, __StringNullableSerializer),
        RdSignal<Unit>(FrameworkMarshallers.Void),
        RdOptionalProperty<com.jetbrains.ide.model.uiautomation.ControlVisibility>(com.jetbrains.ide.model.uiautomation.ControlVisibility.marshaller)
    )
    
    //equals trait
    //hash code trait
    //pretty print
    override fun print(printer: PrettyPrinter)  {
        printer.println("BeSampleToolWindowPanel (")
        printer.indent {
            print("arg = "); arg.print(printer); println()
            print("enabled = "); _enabled.print(printer); println()
            print("controlId = "); _controlId.print(printer); println()
            print("tooltip = "); _tooltip.print(printer); println()
            print("focus = "); _focus.print(printer); println()
            print("visible = "); _visible.print(printer); println()
        }
        printer.print(")")
    }
    //deepClone
    override fun deepClone(): BeSampleToolWindowPanel   {
        return BeSampleToolWindowPanel(
            arg,
            _enabled.deepClonePolymorphic(),
            _controlId.deepClonePolymorphic(),
            _tooltip.deepClonePolymorphic(),
            _focus.deepClonePolymorphic(),
            _visible.deepClonePolymorphic()
        )
    }
    //contexts
}


/**
 * #### Generated from [SamplePluginModel.kt:13]
 */
enum class MyEnum {
    FirstValue, 
    SecondValue;
    
    companion object {
        val marshaller = FrameworkMarshallers.enum<MyEnum>()
        
    }
}


/**
 * #### Generated from [SamplePluginModel.kt:18]
 */
data class MyStructure (
    val projectFile: String,
    val target: String
) : IPrintable {
    //companion
    
    companion object : IMarshaller<MyStructure> {
        override val _type: KClass<MyStructure> = MyStructure::class
        
        @Suppress("UNCHECKED_CAST")
        override fun read(ctx: SerializationCtx, buffer: AbstractBuffer): MyStructure  {
            val projectFile = buffer.readString()
            val target = buffer.readString()
            return MyStructure(projectFile, target)
        }
        
        override fun write(ctx: SerializationCtx, buffer: AbstractBuffer, value: MyStructure)  {
            buffer.writeString(value.projectFile)
            buffer.writeString(value.target)
        }
        
        
    }
    //fields
    //methods
    //initializer
    //secondary constructor
    //equals trait
    override fun equals(other: Any?): Boolean  {
        if (this === other) return true
        if (other == null || other::class != this::class) return false
        
        other as MyStructure
        
        if (projectFile != other.projectFile) return false
        if (target != other.target) return false
        
        return true
    }
    //hash code trait
    override fun hashCode(): Int  {
        var __r = 0
        __r = __r*31 + projectFile.hashCode()
        __r = __r*31 + target.hashCode()
        return __r
    }
    //pretty print
    override fun print(printer: PrettyPrinter)  {
        printer.println("MyStructure (")
        printer.indent {
            print("projectFile = "); projectFile.print(printer); println()
            print("target = "); target.print(printer); println()
        }
        printer.print(")")
    }
    //deepClone
    //contexts
}
