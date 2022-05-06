@file:Suppress("EXPERIMENTAL_API_USAGE","EXPERIMENTAL_UNSIGNED_LITERALS","PackageDirectoryMismatch","UnusedImport","unused","LocalVariableName","CanBeVal","PropertyName","EnumEntryName","ClassName","ObjectPropertyName","UnnecessaryVariable","SpellCheckingInspection")
package com.jetbrains.rider.plugins.rdprotocol

import com.jetbrains.rd.framework.*
import com.jetbrains.rd.framework.base.*
import com.jetbrains.rd.framework.impl.*

import com.jetbrains.rd.util.lifetime.*
import com.jetbrains.rd.util.reactive.*
import com.jetbrains.rd.util.string.*
import com.jetbrains.rd.util.*
import kotlin.reflect.KClass



/**
 * #### Generated from [RdProtocolModel.kt:11]
 */
class RdProtocolModel private constructor(
    private val _property: RdOptionalProperty<CustomType>,
    private val _map: RdMap<String, String>,
    private val _call: RdCall<String, Array<String>>,
    private val _callback: RdCall<String, Array<String>>,
    private val _sink: RdSignal<String>,
    private val _source: RdSignal<String>,
    private val _signal: RdSignal<String>
) : RdExtBase() {
    //companion
    
    companion object : ISerializersOwner {
        
        override fun registerSerializersCore(serializers: ISerializers)  {
            serializers.register(CustomType)
        }
        
        
        
        private val __StringArraySerializer = FrameworkMarshallers.String.array()
        
        const val serializationHash = 4856247557425747971L
        
    }
    override val serializersOwner: ISerializersOwner get() = RdProtocolModel
    override val serializationHash: Long get() = RdProtocolModel.serializationHash
    
    //fields
    val `property`: IOptProperty<CustomType> get() = _property
    val map: IMutableViewableMap<String, String> get() = _map
    val call: IRdCall<String, Array<String>> get() = _call
    val callback: IRdEndpoint<String, Array<String>> get() = _callback
    val sink: IAsyncSource<String> get() = _sink
    val source: IAsyncSignal<String> get() = _source
    val signal: ISignal<String> get() = _signal
    //methods
    //initializer
    init {
        _property.optimizeNested = true
        _map.optimizeNested = true
    }
    
    init {
        _call.async = true
        _callback.async = true
        _sink.async = true
        _source.async = true
    }
    
    init {
        bindableChildren.add("property" to _property)
        bindableChildren.add("map" to _map)
        bindableChildren.add("call" to _call)
        bindableChildren.add("callback" to _callback)
        bindableChildren.add("sink" to _sink)
        bindableChildren.add("source" to _source)
        bindableChildren.add("signal" to _signal)
    }
    
    //secondary constructor
    internal constructor(
    ) : this(
        RdOptionalProperty<CustomType>(CustomType),
        RdMap<String, String>(FrameworkMarshallers.String, FrameworkMarshallers.String),
        RdCall<String, Array<String>>(FrameworkMarshallers.String, __StringArraySerializer),
        RdCall<String, Array<String>>(FrameworkMarshallers.String, __StringArraySerializer),
        RdSignal<String>(FrameworkMarshallers.String),
        RdSignal<String>(FrameworkMarshallers.String),
        RdSignal<String>(FrameworkMarshallers.String)
    )
    
    //equals trait
    //hash code trait
    //pretty print
    override fun print(printer: PrettyPrinter)  {
        printer.println("RdProtocolModel (")
        printer.indent {
            print("property = "); _property.print(printer); println()
            print("map = "); _map.print(printer); println()
            print("call = "); _call.print(printer); println()
            print("callback = "); _callback.print(printer); println()
            print("sink = "); _sink.print(printer); println()
            print("source = "); _source.print(printer); println()
            print("signal = "); _signal.print(printer); println()
        }
        printer.print(")")
    }
    //deepClone
    override fun deepClone(): RdProtocolModel   {
        return RdProtocolModel(
            _property.deepClonePolymorphic(),
            _map.deepClonePolymorphic(),
            _call.deepClonePolymorphic(),
            _callback.deepClonePolymorphic(),
            _sink.deepClonePolymorphic(),
            _source.deepClonePolymorphic(),
            _signal.deepClonePolymorphic()
        )
    }
    //contexts
}
val com.jetbrains.rd.ide.model.Solution.rdProtocolModel get() = getOrCreateExtension("rdProtocolModel", ::RdProtocolModel)



/**
 * #### Generated from [RdProtocolModel.kt:17]
 */
data class CustomType (
    val string: String,
    val boolean: Boolean,
    val array: Array<String>
) : IPrintable {
    //companion
    
    companion object : IMarshaller<CustomType> {
        override val _type: KClass<CustomType> = CustomType::class
        
        @Suppress("UNCHECKED_CAST")
        override fun read(ctx: SerializationCtx, buffer: AbstractBuffer): CustomType  {
            val string = buffer.readString()
            val boolean = buffer.readBool()
            val array = buffer.readArray {buffer.readString()}
            return CustomType(string, boolean, array)
        }
        
        override fun write(ctx: SerializationCtx, buffer: AbstractBuffer, value: CustomType)  {
            buffer.writeString(value.string)
            buffer.writeBool(value.boolean)
            buffer.writeArray(value.array) { buffer.writeString(it) }
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
        
        other as CustomType
        
        if (string != other.string) return false
        if (boolean != other.boolean) return false
        if (!(array contentDeepEquals other.array)) return false
        
        return true
    }
    //hash code trait
    override fun hashCode(): Int  {
        var __r = 0
        __r = __r*31 + string.hashCode()
        __r = __r*31 + boolean.hashCode()
        __r = __r*31 + array.contentDeepHashCode()
        return __r
    }
    //pretty print
    override fun print(printer: PrettyPrinter)  {
        printer.println("CustomType (")
        printer.indent {
            print("string = "); string.print(printer); println()
            print("boolean = "); boolean.print(printer); println()
            print("array = "); array.print(printer); println()
        }
        printer.print(")")
    }
    //deepClone
    //contexts
}
