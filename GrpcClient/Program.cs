using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using GrpcUnaryService;

namespace GrpcClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("http://localhost:5000");
            //Sample Service
            var client = new Sample.SampleClient(channel);
            var response = await client.GetFullNameAsync(
                new SampleRequest
                {
                    FirstName = "Akash",
                    LastName = "Gupta"
                });
            Console.WriteLine(response.FullName);

            //ProductService
            var stockDate = DateTime.SpecifyKind(new DateTime(2022, 1, 1), DateTimeKind.Utc);
            var productClient = new Product.ProductClient(channel);
            var productResponse = await productClient.SaveProductAsync(
                new ProductRequestModel
                {
                    ProductCode = "101",
                    ProductName = "Bag",
                    Price = 121,
                    StockDate=Timestamp.FromDateTime(stockDate)
                });
            Console.WriteLine($"{productResponse.IsSuccess} {productResponse.StatucCode}");

            var lResponse = await productClient.GetProductsAsync(new Google.Protobuf.WellKnownTypes.Empty());
            foreach (var product in lResponse.Products)
            {
                Console.WriteLine($"{product.ProductCode} | {product.ProductName} | {product.Price} | {product.StockDate.ToDateTime().ToString("dd-MM-yyyy")}");
            }

            await channel.ShutdownAsync();
            Console.ReadKey();
        }
    }
}