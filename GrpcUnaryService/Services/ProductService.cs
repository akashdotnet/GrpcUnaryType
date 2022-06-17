using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace GrpcUnaryService.Services
{
    public class ProductService:Product.ProductBase
    {
        public override Task<ProductResponseModel> SaveProduct(ProductRequestModel request, ServerCallContext context)
        {
            // return base.SaveProduct(request, context);
            //Insert data into database

            Console.WriteLine($"{request.ProductCode} | {request.ProductName} | {request.Price} | {request.StockDate}");
            var result = new ProductResponseModel
            {
                IsSuccess = true,
                StatucCode = 1
            };

            return Task.FromResult(result);
        }

        public override Task<ProductList> GetProducts(Empty request, ServerCallContext context)
        {
            var stockDate = DateTime.SpecifyKind(new DateTime(2022, 1, 1), DateTimeKind.Utc);
            var result = new ProductList();
            var product1 = new ProductRequestModel
            {
                Price = 100,
                ProductName = "gas 1",
                ProductCode = "201",
                StockDate = Timestamp.FromDateTime(stockDate)

            };
            var product2 = new ProductRequestModel
            {
                Price = 100,
                ProductName = "gas 2",
                StockDate = Timestamp.FromDateTime(stockDate),
                ProductCode = "202",
            };
            var product3 = new ProductRequestModel
            {
                Price = 100,
                ProductName = "gas 3",
                ProductCode = "203",
                StockDate = Timestamp.FromDateTime(stockDate)
            };
            result.Products.Add(product1);
            result.Products.Add(product2);
            result.Products.Add(product3);

            return Task.FromResult(result);
        }
    }
}
