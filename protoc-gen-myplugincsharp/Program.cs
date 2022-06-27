using Google.Protobuf;
using Google.Protobuf.Compiler;
using System.Text;

namespace protoc_gen_myplugincsharp
{
    // assume current directory is the output directory, and it contains protoc.exe.
    // protoc.exe --plugin=protoc-gen-myplugincsharp.exe --myplugincsharp_out=./ --proto_path=%userprofile%\.nuget\packages\google.protobuf.tools\3.21.1\tools --proto_path=./ chat.proto

    internal class Program
    {
        static void Main(string[] args)
        {
            // you can attach debugger
            // System.Diagnostics.Debugger.Launch();

            // get request from standard input
            CodeGeneratorRequest request;
            using (var stdin = Console.OpenStandardInput())
            {
                request = Deserialize<CodeGeneratorRequest>(stdin);
            }

            var response = new CodeGeneratorResponse();

            foreach (var file in request.FileToGenerate)
            {
                var output = new StringBuilder();

                // make service method list
                foreach (var serviceType in request.ProtoFile.SelectMany((x) => x.Service))
                {
                    output.AppendLine($"service {serviceType.Name}");

                    foreach (var method in serviceType.Method)
                    {
                        output.AppendLine($"   {method.OutputType} {method.Name}({method.InputType})");
                    }
                }

                // make message field list
                foreach (var messageType in request.ProtoFile.SelectMany((x) => x.MessageType))
                {
                    output.AppendLine($"message {messageType.Name}");

                    foreach (var field in messageType.Field)
                    {
                        output.AppendLine($"   {field.TypeName} {field.Name}");
                    }
                }

                // set as response
                response.File.Add(
                    new CodeGeneratorResponse.Types.File()
                    {
                        Name = file + ".txt",
                        Content = output.ToString(),
                    }
                );
            }

            // set result to standard output
            using (var stdout = Console.OpenStandardOutput())
            {
                response.WriteTo(stdout);
            }
        }

        static T Deserialize<T>(Stream stream) where T : IMessage<T>, new()
            => new MessageParser<T>(() => new T()).ParseFrom(stream);
    }
}