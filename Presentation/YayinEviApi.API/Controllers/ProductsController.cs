using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using YayinEviApi.Application.Abstractions.Services;
using YayinEviApi.Application.Abstractions.Storage;
using YayinEviApi.Application.Features.Commands.Product.CreateProduct;
using YayinEviApi.Application.Features.Commands.Product.RemoveProduct;
using YayinEviApi.Application.Features.Commands.ProductImageFile.ChangeShowcaseImage;
using YayinEviApi.Application.Features.Commands.ProductImageFile.RemoveProductImage;
using YayinEviApi.Application.Features.Queries.Product.GetAllProduct;
using YayinEviApi.Application.Features.Queries.ProductImageFile.GetProductImages;
using YayinEviApi.Application.Repositories;
using YayinEviApi.Application.ViewModels.Products;
using YayinEviApi.Domain.Entities;

namespace YayinEviApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IStorageService _storageService;
        readonly IProductService _productService;
        //readonly IFileService _fileService;
        readonly IFileManagementReadRepository _fileManagementReadRepository;
        readonly IFileManagementWriteRepository _fileManagementWriteRepository;
        private readonly IPublishFileWriteRepository _publishFileWriteRepository;
        private readonly IPublishFileReadRepository _publishFileReadRepository;
        private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
        private readonly IProductImageFileReadRepository _productImageFileReadRepository;
        private readonly IInvoiceFileReadRepository _invoiceFileReadRepository;
        private readonly IInvoiceFileWriteRepository _invoiceFileWriteRepository;
        readonly IMediator _mediator;
        public ProductsController(
            IProductReadRepository productReadRepository,
            IProductWriteRepository productWriteRepository,
            IWebHostEnvironment webHostEnvironment,
            IStorageService storageService,
            //IFileService fileService,
            IPublishFileWriteRepository publishFileWriteRepository,
            IPublishFileReadRepository publishFileReadRepository,
            IProductImageFileWriteRepository productImageFileWriteRepository,
            IProductImageFileReadRepository productImageFileReadRepository,
            IInvoiceFileReadRepository invoiceFileReadRepository,
            IInvoiceFileWriteRepository invoiceFileWriteRepository,
            IFileManagementReadRepository fileManagementReadRepository,
            IFileManagementWriteRepository fileManagementWriteRepository,
            IMediator mediator
,
            IProductService productService)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _webHostEnvironment = webHostEnvironment;
            _storageService = storageService;
            //_fileService=fileService;
            _productImageFileReadRepository = productImageFileReadRepository;
            _productImageFileWriteRepository = productImageFileWriteRepository;
            _fileManagementReadRepository = fileManagementReadRepository;
            _fileManagementWriteRepository = fileManagementWriteRepository;
            _publishFileReadRepository = publishFileReadRepository;
            _publishFileWriteRepository = publishFileWriteRepository;
            _invoiceFileReadRepository = invoiceFileReadRepository;
            _invoiceFileWriteRepository = invoiceFileWriteRepository;
            _mediator = mediator;
            _productService = productService;
        }

        //[HttpGet]
        //public async Task<IActionResult> Get([FromQuery] Pagination pagination)
        //{
        //    var totalProductCount = _productReadRepository.GetAll(false).Count();

        //    var products = _productReadRepository.GetAll(false)
        //        .Select(x => x).Skip(pagination.Page * pagination.Size).Take(pagination.Size);
        //    return Ok(new { totalProductCount, products });

        //}

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductQueryRequest getAllProductQueryRequest)
        {
            GetAllProductQueryResponse response = await _mediator.Send(getAllProductQueryRequest);
            return Ok(response);
        }

        [HttpGet("qrcode/{productId}")]
        public async Task<IActionResult> GetQrCodeToProduct([FromRoute] string productId)
        {
            var data = await _productService.QrCodeToProductAsync(productId);
            return File(data, "image/png");
        }

        [HttpPut("qrcode")]
        public async Task<IActionResult> UpdateStockQrCodeToProduct(string productId,int stock)
        {
            _productService.StockUpdateToProductAsync(productId, stock);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            Product product = await _productReadRepository.GetByIdAsync(id,false);
            return Ok(product);
        }
        //[HttpPost]
        //public async Task<IActionResult> Post(VM_Create_Product model)
        //{
        //    if (ModelState.IsValid) {
            
            
        //    }
        //    await _productWriteRepository.AddAsync(new()
        //    {
        //        Name = model.Name,
        //        Price = model.Price,
        //        Stock = model.Stock,
        //    });
        //    await _productWriteRepository.SaveAsync();
        //    return StatusCode((int)HttpStatusCode.Created);
        //}
        [HttpPost]
        //[Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Post(CreateProductCommandRequest createProductCommandRequest)
        {
            CreateProductCommandResponse response = await _mediator.Send(createProductCommandRequest);
            return StatusCode((int)HttpStatusCode.Created);
        }
       
        [HttpPut]
        //[Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Put(VM_Update_Product model)
        {
            Product product = await _productReadRepository.GetByIdAsync(model.Id);
            product.Stock = model.Stock;
            product.Price = model.Price;
            product.Name = model.Name;
            await _productWriteRepository.SaveAsync();
            return Ok();
        }

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(string id)
        //{
        //    await _productWriteRepository.RemoveAsync(id);
        //    await _productWriteRepository.SaveAsync();
        //    return Ok();
        //}
        [HttpDelete("{Id}")]
        //[Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Delete([FromRoute] RemoveProductCommandRequest removeProductCommandRequest)
        {
            RemoveProductCommandResponse response = await _mediator.Send(removeProductCommandRequest);
            return Ok();
        }
         
        [HttpPost("[action]")]
        //[Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Upload(string id) {
            //todo FileService sınıfında FileRenameAsync() metodu ile dosya adının aynı olmaması için bir yapı kuruldu video 28 tekrar izleyip yapıyı oluştur
            //var datas = await _storageService.UploadAsync("files", Request.Form.Files); AZURE için
            //var datas = await _fileService.UploadAsync("resorce/product-images", Request.Form.Files);

            var datas = await _storageService.UploadAsync($@"resorce\product-images", Request.Form.Files);
            
            Product product = await _productReadRepository.GetByIdAsync(id);

            await _productImageFileWriteRepository.AddRangeAsync(datas.Select(d => new ProductImageFile
            {
                FileName = d.filename,
                Path = d.pathOrContainerName,
                Storage=_storageService.StorageName,
                Products=new List<Product>() {product}
            }).ToList());
            
            await _productImageFileWriteRepository.SaveAsync();
            return Ok();

            //_webHostEnvironment.WebRootPath biz wwwroot klasörünün yolunu verir
            //string uploadpath = Path.Combine(_webHostEnvironment.WebRootPath, "resorce/product-images");
            //if (!Directory.Exists(uploadpath))
            //{
            //    Directory.CreateDirectory(uploadpath);
            //}
            //Random r = new();
            //foreach (IFormFile file in Request.Form.Files)
            //{
            //    string fullpath = Path.Combine(uploadpath, $"{file.Name}{Path.GetExtension(file.FileName)}");

            //    using FileStream filestream = new(fullpath, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);

            //    await file.CopyToAsync(filestream);
            //    //stream in boşaltılmasını sağlar
            //    await filestream.FlushAsync();
            //}
            //return Ok();
        }
        //[HttpGet("[action]/{id}")]
        //public async Task<IActionResult> GetProductImages(string id) { 
        
        //    Product? product=await _productReadRepository.Table.Include(p=>p.ProductImageFiles)
        //        .FirstOrDefaultAsync(p=>p.Id==Guid.Parse(id));

        //    return Ok(product.ProductImageFiles.Select(p=> new
        //    {
        //        //todo ürün görsellerinin gösterilebilmesi için Angular select-product-image-dialog-component.html dosyasına uygun yol tanımının yapılması gerekiyor
        //        p.Path,//Path=Path.Combine(_webHostEnvironment.WebRootPath, p.Path),
        //        p.FileName,
        //        p.Id,
        //        p.Showcase
        //    }));
        //}
        [HttpGet("[action]/{id}")]
        //[Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> GetProductImages([FromRoute] GetProductImagesQueryRequest getProductImagesQueryRequest)
        {
            List<GetProductImagesQueryResponse> response = await _mediator.Send(getProductImagesQueryRequest);
            return Ok(response);
        }

        //[HttpDelete("[action]/{id}")]
        //public async Task<IActionResult> DeleteProductImage(string id, string imageId)
        //{

        //    Product? product = await _productReadRepository.Table.Include(p => p.ProductImageFiles)
        //        .FirstOrDefaultAsync(p => p.Id == Guid.Parse(id));
        //    ProductImageFile? productImageFile = product.ProductImageFiles.FirstOrDefault(p => p.Id == Guid.Parse(imageId));
        //    product.ProductImageFiles.Remove(productImageFile);
        //    await _productWriteRepository.SaveAsync();
        //    return Ok();
        //}
        [HttpDelete("[action]/{id}")]
        //[Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> DeleteProductImage([FromRoute] RemoveProductImageCommandRequest removeProductImageCommandRequest, [FromQuery] string imageId)
        {
            //Ders sonrası not !
            //Burada RemoveProductImageCommandRequest sınıfı içerisindeki ImageId property'sini de 'FromQuery' attribute'u ile işaretleyebilirdik!

            removeProductImageCommandRequest.ImageId = imageId;
            RemoveProductImageCommandResponse response = await _mediator.Send(removeProductImageCommandRequest);
            return Ok();
        }

        [HttpGet("[action]")]
        //[Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> ChangeShowcaseImage([FromQuery] ChangeShowcaseImageCommandRequest changeShowcaseImageCommandRequest)
        {
            ChangeShowcaseImageCommandResponse response = await _mediator.Send(changeShowcaseImageCommandRequest);
            return Ok(response);
        }
    }
}
