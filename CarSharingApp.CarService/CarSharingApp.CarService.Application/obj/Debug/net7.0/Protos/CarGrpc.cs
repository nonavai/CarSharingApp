// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Protos/Car.proto
// </auto-generated>
#pragma warning disable 0414, 1591, 8981, 0612
#region Designer generated code

using grpc = global::Grpc.Core;

namespace CarService {
  public static partial class Car
  {
    static readonly string __ServiceName = "carService.Car";

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static void __Helper_SerializeMessage(global::Google.Protobuf.IMessage message, grpc::SerializationContext context)
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (message is global::Google.Protobuf.IBufferMessage)
      {
        context.SetPayloadLength(message.CalculateSize());
        global::Google.Protobuf.MessageExtensions.WriteTo(message, context.GetBufferWriter());
        context.Complete();
        return;
      }
      #endif
      context.Complete(global::Google.Protobuf.MessageExtensions.ToByteArray(message));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static class __Helper_MessageCache<T>
    {
      public static readonly bool IsBufferMessage = global::System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(global::Google.Protobuf.IBufferMessage)).IsAssignableFrom(typeof(T));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static T __Helper_DeserializeMessage<T>(grpc::DeserializationContext context, global::Google.Protobuf.MessageParser<T> parser) where T : global::Google.Protobuf.IMessage<T>
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (__Helper_MessageCache<T>.IsBufferMessage)
      {
        return parser.ParseFrom(context.PayloadAsReadOnlySequence());
      }
      #endif
      return parser.ParseFrom(context.PayloadAsNewBuffer());
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::CarService.CarAvailableRequest> __Marshaller_carService_CarAvailableRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::CarService.CarAvailableRequest.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::CarService.CarAvailableResponse> __Marshaller_carService_CarAvailableResponse = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::CarService.CarAvailableResponse.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::CarService.ChangeStatus> __Marshaller_carService_ChangeStatus = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::CarService.ChangeStatus.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::CarService.ChangeCarStatusResponse> __Marshaller_carService_ChangeCarStatusResponse = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::CarService.ChangeCarStatusResponse.Parser));

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::CarService.CarAvailableRequest, global::CarService.CarAvailableResponse> __Method_IsCarAvailable = new grpc::Method<global::CarService.CarAvailableRequest, global::CarService.CarAvailableResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "IsCarAvailable",
        __Marshaller_carService_CarAvailableRequest,
        __Marshaller_carService_CarAvailableResponse);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::CarService.ChangeStatus, global::CarService.ChangeCarStatusResponse> __Method_ChangeCarStatus = new grpc::Method<global::CarService.ChangeStatus, global::CarService.ChangeCarStatusResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "ChangeCarStatus",
        __Marshaller_carService_ChangeStatus,
        __Marshaller_carService_ChangeCarStatusResponse);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::CarService.CarReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of Car</summary>
    [grpc::BindServiceMethod(typeof(Car), "BindService")]
    public abstract partial class CarBase
    {
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::CarService.CarAvailableResponse> IsCarAvailable(global::CarService.CarAvailableRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::CarService.ChangeCarStatusResponse> ChangeCarStatus(global::CarService.ChangeStatus request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static grpc::ServerServiceDefinition BindService(CarBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_IsCarAvailable, serviceImpl.IsCarAvailable)
          .AddMethod(__Method_ChangeCarStatus, serviceImpl.ChangeCarStatus).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static void BindService(grpc::ServiceBinderBase serviceBinder, CarBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_IsCarAvailable, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::CarService.CarAvailableRequest, global::CarService.CarAvailableResponse>(serviceImpl.IsCarAvailable));
      serviceBinder.AddMethod(__Method_ChangeCarStatus, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::CarService.ChangeStatus, global::CarService.ChangeCarStatusResponse>(serviceImpl.ChangeCarStatus));
    }

  }
}
#endregion
