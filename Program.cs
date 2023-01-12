using Confluent.Kafka;
using System;
using System.Threading;
using Microsoft.Extensions.Configuration;
using ConsoleApp1;
using Newtonsoft.Json;

class Consumer
{

    static void Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine("Please provide the configuration file path as a command line argument");
        }

        //IConfiguration configuration = new ConfigurationBuilder()
        //    .AddIniFile(args[0])
        //    .Build();

        var cConfig = new ConsumerConfig
        {
            BootstrapServers = "pkc-ew3qg.asia-southeast2.gcp.confluent.cloud:9092",
            SaslMechanism = SaslMechanism.Plain,
            SecurityProtocol = SecurityProtocol.SaslSsl,
            SaslUsername = "C7ZGXUJOTRB4TU74",
            SaslPassword = "qzg7jTQxj6EZ9KSeDpGSj/wBhWXFbl0qu3nhgcfQnhk5jX0rIXRkmwddTKlw7s3q",
            GroupId = Guid.NewGuid().ToString(),
            AutoOffsetReset = AutoOffsetReset.Earliest,
            SessionTimeoutMs= 45000
        };

        //configuration["group.id"] = "kafka-dotnet-getting-started";
        //configuration["auto.offset.reset"] = "earliest";

        const string topic = "topic_email";

        CancellationTokenSource cts = new CancellationTokenSource();
        Console.CancelKeyPress += (_, e) => {
            e.Cancel = true; // prevent the process from terminating.
            cts.Cancel();
        };

        using (var consumer = new ConsumerBuilder<string, string>(cConfig).Build())
        {
            consumer.Subscribe(topic);
            try
            {
                while (true)
                {
                    var cr = consumer.Consume(cts.Token);
                    //string data = JsonConvert.SerializeObject(cr.Message.Value);
                    KafkaEmailStatus kafkaEmailStatus = JsonConvert.DeserializeObject<KafkaEmailStatus>(cr.Message.Value);
                    Console.WriteLine(kafkaEmailStatus);
                    Console.WriteLine($"Consumed event from topic {topic} with key {cr.Message.Key,-10} and value {cr.Message.Value}");
                }
            }
            catch (OperationCanceledException)
            {
                // Ctrl-C was pressed.
            }
            finally
            {
                consumer.Close();
            }
        }
    }
}