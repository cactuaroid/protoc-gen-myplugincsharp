protoc (Protocol Buffers Compiler) plugin sample in C#.
protoc gives parsed input .proto to the plugin as `CodeGeneratorRequest` object via standard input. The plugin shall put `CodeGeneratorResponse` object to standard output to generate text file.

##### Input

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
from https://github.com/cactuaroid/GrpcWpfSample/blob/master/grpc-dotnet/GrpcChatSample2.Common/chat.proto

##### Output
```
service Chat
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
