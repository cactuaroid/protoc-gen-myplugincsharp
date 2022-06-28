protoc (Protocol Buffers Compiler) plugin sample in C#.
protoc gives parsed input .proto to the plugin as `CodeGeneratorRequest` object via standard input. The plugin shall put `CodeGeneratorResponse` object to standard output to generate text file.

You cannot execute the plugin itself but you can attach debugger by calling `System.Diagnostics.Debugger.Launch()`.

See https://github.com/cactuaroid/protoc-gen-opcua for further example.

##### Input chat.proto[^1]
[^1]: https://github.com/cactuaroid/GrpcWpfSample/blob/master/grpc-dotnet/GrpcChatSample2.Common/chat.proto

```proto
syntax = "proto3";

// well known types
import "google/protobuf/timestamp.proto";
import "google/protobuf/empty.proto";

option csharp_namespace = "GrpcChatSample.Common";

package GrpcChatSample.Common;

service Chat {
  rpc Write(ChatLog) returns (google.protobuf.Empty) {}
  rpc Subscribe(google.protobuf.Empty) returns (stream ChatLog) {}
}

message ChatLog {
	string name = 1;
	string content = 2;
	google.protobuf.Timestamp at = 3;
}
```

##### Command Line
`
protoc.exe --plugin=protoc-gen-myplugincsharp.exe --myplugincsharp_out=./ --proto_path=%userprofile%\.nuget\packages\google.protobuf.tools\3.21.1\tools --proto_path=./ chat.proto
`

##### Output chat.proto.txt
```
service Cha
   .google.protobuf.Empty Write(.GrpcChatSample.Common.ChatLog)
   .GrpcChatSample.Common.ChatLog Subscribe(.google.protobuf.Empty)
message Timestamp
    seconds
    nanos
message Empty
message ChatLog
    name
    content
   .google.protobuf.Timestamp at
```
