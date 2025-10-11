using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using YayinEviApi.Application.Abstractions.Services;
using YayinEviApi.Application.Abstractions.Storage;
using YayinEviApi.Application.DTOs.MaterialDtos;
using YayinEviApi.Application.DTOs.User;
using YayinEviApi.Application.Repositories;
using YayinEviApi.Application.Repositories.IMaterialR;
using YayinEviApi.Application.RequestParameters;
using YayinEviApi.Domain.Entities;
using YayinEviApi.Domain.Entities.MaterialE;
using YayinEviApi.Domain.Entities.UnitE;
using YayinEviApi.Domain.Entities.WarehouseE;
using YayinEviApi.Domain.Enum;
using YayinEviApi.Infrastructure.Operations;

namespace YayinEviApi.API.Controllers.MaterialControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]

    public class MaterialController : ControllerBase
    {
        private IUserService _userService;
        readonly IMediator _mediator;
        readonly CreateUser _user;
        readonly IFileManagementReadRepository _fileManagementReadRepository;
        readonly IFileManagementWriteRepository _fileManagementWriteRepository;
        readonly IMaterialRepository _materialRepository;
        readonly IMaterialFileRepository _materialFileRepository;
        private readonly IStorageService _storageService;
        Expression<Func<Material, bool>>? _materailFilterExpression;

        public MaterialController(IMaterialRepository materialRepository, IUserService userService, IMaterialFileRepository materialFileRepository, IStorageService storageService, IMediator mediator, IFileManagementWriteRepository fileManagementWriteRepository, IFileManagementReadRepository fileManagementReadRepository)
        {
            _materialRepository = materialRepository;
            _userService = userService;
            _materialFileRepository = materialFileRepository;
            _storageService = storageService;
            _fileManagementWriteRepository = fileManagementWriteRepository;
            _fileManagementReadRepository = fileManagementReadRepository;

            _user = _userService.GetUser().Result;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] Pagination? pagination)
        {
            var totalMaterialCount = _materialRepository.GetAll(false).Count();
            var materialList = _materialRepository.Table.Select(x => new
            {
                material = x,
                wh = x.Warehouse != null ? x.Warehouse : new Warehouse(),
                hwh = x.HallofWarehouse != null ? x.HallofWarehouse : new HallofWarehouse(),
                swh = x.ShelfofWarehouse != null ? x.ShelfofWarehouse : new ShelfofWarehouse(),
                cwh = x.CellofWarehouse != null ? x.CellofWarehouse : new CellofWarehouse(),
                materialType = x.MaterialType != null ? x.MaterialType.toName() : ""

            })
            .Select(x => new MaterailDto
            {
                Id = x.material.Id.ToString(),
                Code = x.material.Code,
                Name = x.material.Name,
                Description = x.material.Description,
                MaterialType = x.materialType,
                IsActive = x.material.IsActive,
                WarehouseId = x.material.WarehouseId != null ? x.material.WarehouseId.ToString() : null,
                WarehouseCode = x.wh.Code,
                WarehouseName = x.wh.Name,
                HallofWarehouseId = x.material.HallofWarehouseId != null ? x.material.HallofWarehouseId.ToString() : null,
                HallofWarehouseCode = x.hwh.Code,
                HallofWarehouseName = x.hwh.Name,
                ShelfofWarehouseId = x.material.ShelfofWarehouseId != null ? x.material.ShelfofWarehouseId.ToString() : null,
                ShelfofWarehouseCode = x.swh.Code,
                ShelfofWarehouseName = x.swh.Name,
                CellofWarehouseId = x.material.CellofWarehouseId != null ? x.material.CellofWarehouseId.ToString() : null,
                CellofWarehouseCode = x.cwh.Code,
                CellofWarehouseName = x.cwh.Name,
            }).ToList();
            var materials = materialList.Select(x => x).Skip(pagination.Page * pagination.Size).Take(pagination.Size);
            return Ok(new {totalMaterialCount,materials});

        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllWithoutPagination()
        {
            var totalMaterialCount = _materialRepository.GetAll(false).Count();
            var materials = _materialRepository.Table.Select(x => new
            {
                material = x,
                wh = x.Warehouse != null ? x.Warehouse : new Warehouse(),
                hwh = x.HallofWarehouse != null ? x.HallofWarehouse : new HallofWarehouse(),
                swh = x.ShelfofWarehouse != null ? x.ShelfofWarehouse : new ShelfofWarehouse(),
                cwh = x.CellofWarehouse != null ? x.CellofWarehouse : new CellofWarehouse(),
                materialType = x.MaterialType != null ? x.MaterialType.toName() : ""

            })
            .Select(x => new MaterailDto
            {
                Id = x.material.Id.ToString(),
                Code = x.material.Code,
                Name = x.material.Name,
                Description = x.material.Description,
                MaterialType = x.materialType,
                IsActive = x.material.IsActive,
                WarehouseId = x.material.WarehouseId != null ? x.material.WarehouseId.ToString() : null,
                WarehouseCode = x.wh.Code,
                WarehouseName = x.wh.Name,
                HallofWarehouseId = x.material.HallofWarehouseId != null ? x.material.HallofWarehouseId.ToString() : null,
                HallofWarehouseCode = x.hwh.Code,
                HallofWarehouseName = x.hwh.Name,
                ShelfofWarehouseId = x.material.ShelfofWarehouseId != null ? x.material.ShelfofWarehouseId.ToString() : null,
                ShelfofWarehouseCode = x.swh.Code,
                ShelfofWarehouseName = x.swh.Name,
                CellofWarehouseId = x.material.CellofWarehouseId != null ? x.material.CellofWarehouseId.ToString() : null,
                CellofWarehouseCode = x.cwh.Code,
                CellofWarehouseName = x.cwh.Name,
            }).ToList();
            return Ok( materials);

        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var files = _fileManagementReadRepository.Select(x => x.EntityId == id, x => x).ToList();
            var imagePath = files.Where(x => x.IsActive).FirstOrDefault()?.Path;
            var material = _materialRepository.Table.Where(x => x.Id.ToString() == id).Select(x => new
            {
                material = x,
                wh = x.Warehouse!=null?x.Warehouse:new Warehouse(),
                hwh = x.HallofWarehouse!=null?x.HallofWarehouse:new HallofWarehouse(),
                swh = x.ShelfofWarehouse!=null?x.ShelfofWarehouse:new ShelfofWarehouse(),
                cwh = x.CellofWarehouse != null ? x.CellofWarehouse : new CellofWarehouse(),
                unit = x.Unit != null ? x.Unit : new MaterialUnit(),
                materialType=x.MaterialType != null ? x.MaterialType.toName() : ""

            })
            .Select(x => new MaterailDto
            {
                Id = x.material.Id.ToString(),
                Code = x.material.Code,
                Name = x.material.Name,
                Description = x.material.Description,
                MaterialType = x.materialType,
                IsActive = x.material.IsActive,
                UnitId=x.material.UnitId!=null?x.material.UnitId.ToString():null,
                UnitName=x.unit.Name,
                WarehouseId = x.material.WarehouseId!=null?x.material.WarehouseId.ToString():null,
                WarehouseCode = x.wh.Code,
                WarehouseName = x.wh.Name,
                HallofWarehouseId = x.material.HallofWarehouseId!=null? x.material.HallofWarehouseId.ToString():null,
                HallofWarehouseCode = x.hwh.Code,
                HallofWarehouseName = x.hwh.Name,
                ShelfofWarehouseId = x.material.ShelfofWarehouseId!=null? x.material.ShelfofWarehouseId.ToString():null,
                ShelfofWarehouseCode = x.swh.Code,
                ShelfofWarehouseName = x.swh.Name,
                CellofWarehouseId = x.material.CellofWarehouseId!=null? x.material.CellofWarehouseId.ToString():null,
                CellofWarehouseCode = x.cwh.Code,
                CellofWarehouseName = x.cwh.Name,
                MaterialFiles = files,
                ImagePath = imagePath,
            }).FirstOrDefault();

            //var newMaterial = new MaterailDto
            //{
            //    Id = material.Id.ToString(),
            //    Code = material.Code,
            //    Name = material.Name,
            //    Description= material.Description,
            //    MaterialType=material.MaterialType?.toName(),
            //    IsActive = material.IsActive,
            //    CellofWarehouseId=material.CellofWarehouseId?.ToString(),
            //    CellofWarehouseCode = material.CellofWarehouse?.Code,
            //    CellofWarehouseName= material.CellofWarehouse?.Name,
            //    ShelfofWarehouseId = material.ShelfofWarehouse?.Id.ToString(),
            //    ShelfofWarehouseCode = material.ShelfofWarehouse?.Code,
            //    ShelfofWarehouseName = material.ShelfofWarehouse?.Name,
            //    HallofWarehouseId=material.HallofWarehouseId?.ToString(),
            //    HallofWarehouseCode=material.HallofWarehouse?.Code,
            //    HallofWarehouseName=material.HallofWarehouse?.Name,
            //    WarehouseId = material.WarehouseId?.ToString(),
            //    WarehouseCode = material.Warehouse?.Code,
            //    WarehouseName = material.Warehouse?.Name,
            //    MaterialFiles=files,
            //    ImagePath=imagePath?.Path,
            //};

            return Ok(material);
        }

        [HttpPost()]
        public async Task<IActionResult> Add(MaterailDto materialdto)
        {
           
            var material = new Material
            {
                Code = materialdto.Code,
                Name = materialdto.Name,
                IsActive = materialdto.IsActive,
                MaterialType=materialdto.MaterialType!=null&&materialdto.MaterialType!=""?materialdto.MaterialType.GetEnum<MaterialTypes>():null,
                CellofWarehouseId = materialdto.CellofWarehouseId!=null? Guid.Parse(materialdto.CellofWarehouseId):null,
                ShelfofWarehouseId = materialdto.ShelfofWarehouseId != null ? Guid.Parse(materialdto.ShelfofWarehouseId) : null,
                HallofWarehouseId = materialdto.HallofWarehouseId != null ? Guid.Parse(materialdto.HallofWarehouseId) : null,
                WarehouseId = materialdto.WarehouseId != null ? Guid.Parse(materialdto.WarehouseId) : null,
                UnitId= materialdto.UnitId!=null?Guid.Parse(materialdto.UnitId) : null,
            };
            await _materialRepository.AddAsync(material);

            await _materialRepository.SaveAsync();
            materialdto.Id=material.Id.ToString();
            return Ok(materialdto);
            //return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<IActionResult> Edit(MaterailDto materialdto)
        {
            var material = new Material
            {
                Id=Guid.Parse(materialdto.Id),
                Code = materialdto.Code,
                Name = materialdto.Name,
                IsActive = materialdto.IsActive,
                MaterialType = materialdto.MaterialType != null && materialdto.MaterialType != "" ? materialdto.MaterialType.GetEnum<MaterialTypes>() : null,
                CellofWarehouseId = materialdto.CellofWarehouseId != null || materialdto.CellofWarehouseId=="" ? Guid.Parse(materialdto.CellofWarehouseId) : null,
                ShelfofWarehouseId = materialdto.ShelfofWarehouseId != null|| materialdto.ShelfofWarehouseId=="" ? Guid.Parse(materialdto.ShelfofWarehouseId) : null,
                HallofWarehouseId = materialdto.HallofWarehouseId != null || materialdto.HallofWarehouseId=="" ? Guid.Parse(materialdto.HallofWarehouseId) : null,
                WarehouseId = materialdto.WarehouseId != null || materialdto.WarehouseId == "" ? Guid.Parse(materialdto.WarehouseId) : null,
                UnitId=materialdto.UnitId!=null || materialdto.UnitId == "" ? Guid.Parse(materialdto.UnitId) : null,
            };
             _materialRepository.Update(material);

            await _materialRepository.SaveAsync();
            
            return Ok(material);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _materialRepository.RemoveAsync(id);
            await _materialRepository.SaveAsync();
            return Ok();
        }

        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Upload(string entityId)
        {

            var datas = await _storageService.UploadAsync($@"resorce\product-images", Request.Form.Files);

            var isActive = _fileManagementReadRepository.Select(x => x.EntityId == entityId, x => x).Any(a => a.IsActive);

            await _fileManagementWriteRepository.AddRangeAsync(datas.Select(d => new FileManagement
            {
                FileName = d.filename,
                Path = d.pathOrContainerName,
                Storage = _storageService.StorageName,
                EntityId = entityId,
                WhichPage = "MaterialAdd/Edit",
                WhichClass = "MaterialClass",
                IsActive=!isActive,
                AddingUserId = _user.UserId,
            }).ToList());

            await _fileManagementWriteRepository.SaveAsync();

            //await _fileManagementWriteRepository.SaveAsync();
            //await _materialFileRepository.AddRangeAsync(datas.Select(d => new MaterialFile
            //{
            //    FileName = d.filename,
            //    Path = d.pathOrContainerName,
            //    IsActive = !isActive,
            //    EntityId = id,
            //    AddingUserId = userId,
            //    Storage = _storageService.StorageName,
            //    Materials = new List<Material>() { material }
            //}).ToList());

            //await _materialFileRepository.SaveAsync();
           
            return Ok();

        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> GetFiles(string entityId)
        {
            var file = _fileManagementReadRepository.Select(x => x.EntityId == entityId, x => x);

            return Ok(file);
            //List<GetMaterialFileQueryResponse> response = await _mediator.Send(getMaterialFileQueryRequest);
            //return Ok(response);
        }
        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> ChangeShowcaseImage(string imageId, string id)
        {
            var files = _fileManagementReadRepository.Select(x => x.EntityId == id, x => x);
            var file1 = files.Where(x => x.IsActive).FirstOrDefault();
            if (file1 != null)
            {
                file1.IsActive = false;
            }
            var file2 = files.Where(x => x.EntityId == id && x.Id.ToString() == imageId).FirstOrDefault();
            if (file2 != null)
            {
                file2.IsActive = true;
            }
            await _fileManagementWriteRepository.SaveAsync();

            return Ok();
            //ChangeShowcaseImageCommandResponse response = await _mediator.Send(changeShowcaseImageCommandRequest);
            //return Ok(response);
        }
    }
}
