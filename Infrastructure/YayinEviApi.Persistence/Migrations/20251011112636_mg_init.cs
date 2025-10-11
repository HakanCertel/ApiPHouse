using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YayinEviApi.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mg_init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agencies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Town = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxDeparmant = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaxNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber_1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber_2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponsibleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WebSite = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<byte>(type: "tinyint", nullable: true),
                    LocalOrForeing = table.Column<byte>(type: "tinyint", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Appellation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentStatu = table.Column<byte>(type: "tinyint", nullable: false),
                    LocalOrForeing = table.Column<byte>(type: "tinyint", nullable: false),
                    CurrentState = table.Column<byte>(type: "tinyint", nullable: false),
                    TaxDepartment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponsiblePerson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartmentofPerson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobilePhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    County = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Town = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FileManagements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Storage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    EntityId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WhichPage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WhichClass = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddingUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    AgencyFile_Showcase = table.Column<bool>(type: "bit", nullable: true),
                    Showcase = table.Column<bool>(type: "bit", nullable: true),
                    Price = table.Column<int>(type: "int", nullable: true),
                    MaterialFile_Showcase = table.Column<bool>(type: "bit", nullable: true),
                    ProductImageFile_Showcase = table.Column<bool>(type: "bit", nullable: true),
                    PublishFile_Showcase = table.Column<bool>(type: "bit", nullable: true),
                    WorkId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartmentId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileManagements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HubMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MessageHead = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MessageBody = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SendedUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatingUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatingUserNameSurname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Readed = table.Column<bool>(type: "bit", nullable: true),
                    IsShowed = table.Column<bool>(type: "bit", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HubMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaterialUnits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialUnits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderCollections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatingUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatingUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCollectionCompleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderCollections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderDistributeds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatingUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDistributeds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProccessCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatingUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatingUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProccessCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StockCounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    ConfirmDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatingUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatingUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockCounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsGoodsAcceptWareHouse = table.Column<bool>(type: "bit", nullable: false),
                    IsShippingWareHouse = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TypeCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AgencyConnectionInformations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AgencyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameSurname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgencyConnectionInformations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AgencyConnectionInformations_Agencies_AgencyId",
                        column: x => x.AgencyId,
                        principalTable: "Agencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AgencyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<byte>(type: "tinyint", nullable: false),
                    CreatingUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatingUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Authors_Agencies_AgencyId",
                        column: x => x.AgencyId,
                        principalTable: "Agencies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubAddress",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ResponsiblePerson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartmentofPerson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobilePhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    County = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Town = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubAddress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubAddress_Currents_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Currents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubCurrent",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Appellation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentStatu = table.Column<byte>(type: "tinyint", nullable: false),
                    LocalOrForeing = table.Column<byte>(type: "tinyint", nullable: false),
                    CurrentState = table.Column<byte>(type: "tinyint", nullable: false),
                    TaxDepartment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponsiblePerson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartmentofPerson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobilePhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    County = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Town = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Default = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCurrent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCurrent_Currents_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Currents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NameSurname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AgencyAgencyFile",
                columns: table => new
                {
                    AgenciesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AgencyFilesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgencyAgencyFile", x => new { x.AgenciesId, x.AgencyFilesId });
                    table.ForeignKey(
                        name: "FK_AgencyAgencyFile_Agencies_AgenciesId",
                        column: x => x.AgenciesId,
                        principalTable: "Agencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AgencyAgencyFile_FileManagements_AgencyFilesId",
                        column: x => x.AgencyFilesId,
                        principalTable: "FileManagements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Proccesses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProccessCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proccesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Proccesses_ProccessCategories_ProccessCategoryId",
                        column: x => x.ProccessCategoryId,
                        principalTable: "ProccessCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductProductImageFile",
                columns: table => new
                {
                    ProductImageFilesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductProductImageFile", x => new { x.ProductImageFilesId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_ProductProductImageFile_FileManagements_ProductImageFilesId",
                        column: x => x.ProductImageFilesId,
                        principalTable: "FileManagements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductProductImageFile_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HallofWarehouses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HallofWarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HallofWarehouses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HallofWarehouses_HallofWarehouses_HallofWarehouseId",
                        column: x => x.HallofWarehouseId,
                        principalTable: "HallofWarehouses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HallofWarehouses_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkCategories_WorkTypes_WorkTypeId",
                        column: x => x.WorkTypeId,
                        principalTable: "WorkTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuthorAuthorFile",
                columns: table => new
                {
                    AuthorFilesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuthorsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorAuthorFile", x => new { x.AuthorFilesId, x.AuthorsId });
                    table.ForeignKey(
                        name: "FK_AuthorAuthorFile_Authors_AuthorsId",
                        column: x => x.AuthorsId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorAuthorFile_FileManagements_AuthorFilesId",
                        column: x => x.AuthorFilesId,
                        principalTable: "FileManagements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeliveryCurrentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CurrencyType = table.Column<byte>(type: "tinyint", nullable: false),
                    PaymentMethodId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PriceListId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatingUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatingUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MainDiscountRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FixDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsSendedShippingOrder = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sales_Currents_CurrentId",
                        column: x => x.CurrentId,
                        principalTable: "Currents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sales_SubCurrent_DeliveryCurrentId",
                        column: x => x.DeliveryCurrentId,
                        principalTable: "SubCurrent",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Basket",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Basket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Basket_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShelfofWarehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HallofWarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShelfofWarehouse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShelfofWarehouse_HallofWarehouses_HallofWarehouseId",
                        column: x => x.HallofWarehouseId,
                        principalTable: "HallofWarehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Works",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkOrginalName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isbn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Barcode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstPrintingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LasttPrintingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastPrintingQuantity = table.Column<int>(type: "int", nullable: false),
                    CertificateNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AgencyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NameTranslating = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameReducting = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameReading = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameDrawing = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameTypeSetting = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bandrol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrintingHouse = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StockQuantity = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Works", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Works_Agencies_AgencyId",
                        column: x => x.AgencyId,
                        principalTable: "Agencies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Works_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Works_WorkCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "WorkCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BasketItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BasketId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasketItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BasketItem_Basket_BasketId",
                        column: x => x.BasketId,
                        principalTable: "Basket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BasketItem_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Basket_Id",
                        column: x => x.Id,
                        principalTable: "Basket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CellesofWarehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDefaultCell = table.Column<bool>(type: "bit", nullable: false),
                    ShelfofWarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CellesofWarehouse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CellesofWarehouse_ShelfofWarehouse_ShelfofWarehouseId",
                        column: x => x.ShelfofWarehouseId,
                        principalTable: "ShelfofWarehouse",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProccessForWorks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProccessId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProccessForWorks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProccessForWorks_Proccesses_ProccessId",
                        column: x => x.ProccessId,
                        principalTable: "Proccesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProccessForWorks_Works_WorkId",
                        column: x => x.WorkId,
                        principalTable: "Works",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AgencyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WorkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    State = table.Column<byte>(type: "tinyint", nullable: false),
                    ContractStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ContractFinishDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ContractPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_Agencies_AgencyId",
                        column: x => x.AgencyId,
                        principalTable: "Agencies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Projects_Works_WorkId",
                        column: x => x.WorkId,
                        principalTable: "Works",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PublishFileWork",
                columns: table => new
                {
                    PublishFilesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorksId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublishFileWork", x => new { x.PublishFilesId, x.WorksId });
                    table.ForeignKey(
                        name: "FK_PublishFileWork_FileManagements_PublishFilesId",
                        column: x => x.PublishFilesId,
                        principalTable: "FileManagements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PublishFileWork_Works_WorksId",
                        column: x => x.WorksId,
                        principalTable: "Works",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderProduct",
                columns: table => new
                {
                    OrdersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProduct", x => new { x.OrdersId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_OrderProduct_Orders_OrdersId",
                        column: x => x.OrdersId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderProduct_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GoodsAcceps",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AcceptCellofWarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CurrentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    County = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Town = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatingUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatingUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsAcceps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GoodsAcceps_CellesofWarehouse_AcceptCellofWarehouseId",
                        column: x => x.AcceptCellofWarehouseId,
                        principalTable: "CellesofWarehouse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaterialType = table.Column<byte>(type: "tinyint", nullable: true),
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    HallofWarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ShelfofWarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CellofWarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Materials_CellesofWarehouse_CellofWarehouseId",
                        column: x => x.CellofWarehouseId,
                        principalTable: "CellesofWarehouse",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Materials_HallofWarehouses_HallofWarehouseId",
                        column: x => x.HallofWarehouseId,
                        principalTable: "HallofWarehouses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Materials_MaterialUnits_UnitId",
                        column: x => x.UnitId,
                        principalTable: "MaterialUnits",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Materials_ShelfofWarehouse_ShelfofWarehouseId",
                        column: x => x.ShelfofWarehouseId,
                        principalTable: "ShelfofWarehouse",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Materials_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Ships",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShipCellofWarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    County = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Town = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatingUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatingUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ships_CellesofWarehouse_ShipCellofWarehouseId",
                        column: x => x.ShipCellofWarehouseId,
                        principalTable: "CellesofWarehouse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ships_Currents_CurrentId",
                        column: x => x.CurrentId,
                        principalTable: "Currents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransferDemandsBetweenWarehouses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DemandedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConfirmedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TransferingCellofWarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TransferedCellofWarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatingUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatingUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferDemandsBetweenWarehouses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransferDemandsBetweenWarehouses_CellesofWarehouse_TransferedCellofWarehouseId",
                        column: x => x.TransferedCellofWarehouseId,
                        principalTable: "CellesofWarehouse",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TransferDemandsBetweenWarehouses_CellesofWarehouse_TransferingCellofWarehouseId",
                        column: x => x.TransferingCellofWarehouseId,
                        principalTable: "CellesofWarehouse",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectWorkOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProccessId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AssignedUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatingUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatingUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FinishedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WorkState = table.Column<byte>(type: "tinyint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectWorkOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectWorkOrders_Proccesses_ProccessId",
                        column: x => x.ProccessId,
                        principalTable: "Proccesses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectWorkOrders_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GoodsAcceptItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsAcceptItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GoodsAcceptItems_GoodsAcceps_ParentId",
                        column: x => x.ParentId,
                        principalTable: "GoodsAcceps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GoodsAcceptItems_MaterialUnits_UnitId",
                        column: x => x.UnitId,
                        principalTable: "MaterialUnits",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GoodsAcceptItems_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaterialMaterialFile",
                columns: table => new
                {
                    MaterialFilesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaterialsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialMaterialFile", x => new { x.MaterialFilesId, x.MaterialsId });
                    table.ForeignKey(
                        name: "FK_MaterialMaterialFile_FileManagements_MaterialFilesId",
                        column: x => x.MaterialFilesId,
                        principalTable: "FileManagements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaterialMaterialFile_Materials_MaterialsId",
                        column: x => x.MaterialsId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rezervations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChildId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ChildCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CellofWarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RezervationQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RezervationStatu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RezervationState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatingUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatingUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCancel = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rezervations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rezervations_CellesofWarehouse_CellofWarehouseId",
                        column: x => x.CellofWarehouseId,
                        principalTable: "CellesofWarehouse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rezervations_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SaleItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxType = table.Column<byte>(type: "tinyint", nullable: false),
                    ItemDiscountRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsSendedShippingOrder = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaleItems_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SaleItems_Sales_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Sales",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StockCountItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StockCountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CellofWarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockCountItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockCountItems_CellesofWarehouse_CellofWarehouseId",
                        column: x => x.CellofWarehouseId,
                        principalTable: "CellesofWarehouse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockCountItems_MaterialUnits_UnitId",
                        column: x => x.UnitId,
                        principalTable: "MaterialUnits",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StockCountItems_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockCountItems_StockCounts_StockCountId",
                        column: x => x.StockCountId,
                        principalTable: "StockCounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockMovements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EnteringCellId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OutgoingCellId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MovementQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatingUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MovementClass = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MovementClassId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MovementClassCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockMovements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockMovements_CellesofWarehouse_EnteringCellId",
                        column: x => x.EnteringCellId,
                        principalTable: "CellesofWarehouse",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StockMovements_CellesofWarehouse_OutgoingCellId",
                        column: x => x.OutgoingCellId,
                        principalTable: "CellesofWarehouse",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StockMovements_MaterialUnits_UnitId",
                        column: x => x.UnitId,
                        principalTable: "MaterialUnits",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StockMovements_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CellofWarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stocks_CellesofWarehouse_CellofWarehouseId",
                        column: x => x.CellofWarehouseId,
                        principalTable: "CellesofWarehouse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Stocks_MaterialUnits_UnitId",
                        column: x => x.UnitId,
                        principalTable: "MaterialUnits",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Stocks_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShipItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShipItems_MaterialUnits_UnitId",
                        column: x => x.UnitId,
                        principalTable: "MaterialUnits",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ShipItems_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShipItems_Ships_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Ships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransferDemandItemsBetweenWarehouses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    ConfirmedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DemandedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferDemandItemsBetweenWarehouses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransferDemandItemsBetweenWarehouses_MaterialUnits_UnitId",
                        column: x => x.UnitId,
                        principalTable: "MaterialUnits",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TransferDemandItemsBetweenWarehouses_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransferDemandItemsBetweenWarehouses_TransferDemandsBetweenWarehouses_ParentId",
                        column: x => x.ParentId,
                        principalTable: "TransferDemandsBetweenWarehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkAssignedUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkAssignedUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkAssignedUsers_ProjectWorkOrders_WorkOrderId",
                        column: x => x.WorkOrderId,
                        principalTable: "ProjectWorkOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkOrderMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrderMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkOrderMessages_ProjectWorkOrders_WorkOrderId",
                        column: x => x.WorkOrderId,
                        principalTable: "ProjectWorkOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderCollectionItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShippingOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SaleOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SaleItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CollectedQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CellId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderCollectionItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderCollectionItems_CellesofWarehouse_CellId",
                        column: x => x.CellId,
                        principalTable: "CellesofWarehouse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderCollectionItems_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderCollectionItems_OrderCollections_ParentId",
                        column: x => x.ParentId,
                        principalTable: "OrderCollections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderCollectionItems_SaleItems_SaleItemId",
                        column: x => x.SaleItemId,
                        principalTable: "SaleItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItemDistributeds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SaleOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SaleItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DistributedCellId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CollectedQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItemDistributeds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItemDistributeds_CellesofWarehouse_DistributedCellId",
                        column: x => x.DistributedCellId,
                        principalTable: "CellesofWarehouse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItemDistributeds_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItemDistributeds_OrderDistributeds_ParentId",
                        column: x => x.ParentId,
                        principalTable: "OrderDistributeds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItemDistributeds_SaleItems_SaleItemId",
                        column: x => x.SaleItemId,
                        principalTable: "SaleItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShippingOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SaleItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SaleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AssignedUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FinishedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatingUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatingUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsStartedCollection = table.Column<bool>(type: "bit", nullable: false),
                    IsComplededCollection = table.Column<bool>(type: "bit", nullable: false),
                    IsStartedPacking = table.Column<bool>(type: "bit", nullable: false),
                    IsCompletedPacking = table.Column<bool>(type: "bit", nullable: false),
                    IsShipped = table.Column<bool>(type: "bit", nullable: false),
                    OrderCollectionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OrderPackingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ShippingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShippedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ShippingOrderState = table.Column<byte>(type: "tinyint", nullable: false),
                    ProccessedQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShippingOrders_SaleItems_SaleItemId",
                        column: x => x.SaleItemId,
                        principalTable: "SaleItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShippingOrders_Sales_SaleId",
                        column: x => x.SaleId,
                        principalTable: "Sales",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AssignedUserToShippingWork",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShippingOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignedUserToShippingWork", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssignedUserToShippingWork_ShippingOrders_ShippingOrderId",
                        column: x => x.ShippingOrderId,
                        principalTable: "ShippingOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AgencyAgencyFile_AgencyFilesId",
                table: "AgencyAgencyFile",
                column: "AgencyFilesId");

            migrationBuilder.CreateIndex(
                name: "IX_AgencyConnectionInformations_AgencyId",
                table: "AgencyConnectionInformations",
                column: "AgencyId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DepartmentId",
                table: "AspNetUsers",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AssignedUserToShippingWork_ShippingOrderId",
                table: "AssignedUserToShippingWork",
                column: "ShippingOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorAuthorFile_AuthorsId",
                table: "AuthorAuthorFile",
                column: "AuthorsId");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_AgencyId",
                table: "Authors",
                column: "AgencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Basket_UserId",
                table: "Basket",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BasketItem_BasketId",
                table: "BasketItem",
                column: "BasketId");

            migrationBuilder.CreateIndex(
                name: "IX_BasketItem_ProductId",
                table: "BasketItem",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CellesofWarehouse_ShelfofWarehouseId",
                table: "CellesofWarehouse",
                column: "ShelfofWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsAcceps_AcceptCellofWarehouseId",
                table: "GoodsAcceps",
                column: "AcceptCellofWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsAcceptItems_MaterialId",
                table: "GoodsAcceptItems",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsAcceptItems_ParentId",
                table: "GoodsAcceptItems",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsAcceptItems_UnitId",
                table: "GoodsAcceptItems",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_HallofWarehouses_HallofWarehouseId",
                table: "HallofWarehouses",
                column: "HallofWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_HallofWarehouses_WarehouseId",
                table: "HallofWarehouses",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialMaterialFile_MaterialsId",
                table: "MaterialMaterialFile",
                column: "MaterialsId");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_CellofWarehouseId",
                table: "Materials",
                column: "CellofWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_HallofWarehouseId",
                table: "Materials",
                column: "HallofWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_ShelfofWarehouseId",
                table: "Materials",
                column: "ShelfofWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_UnitId",
                table: "Materials",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_WarehouseId",
                table: "Materials",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderCollectionItems_CellId",
                table: "OrderCollectionItems",
                column: "CellId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderCollectionItems_MaterialId",
                table: "OrderCollectionItems",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderCollectionItems_ParentId",
                table: "OrderCollectionItems",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderCollectionItems_SaleItemId",
                table: "OrderCollectionItems",
                column: "SaleItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemDistributeds_DistributedCellId",
                table: "OrderItemDistributeds",
                column: "DistributedCellId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemDistributeds_MaterialId",
                table: "OrderItemDistributeds",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemDistributeds_ParentId",
                table: "OrderItemDistributeds",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemDistributeds_SaleItemId",
                table: "OrderItemDistributeds",
                column: "SaleItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProduct_ProductsId",
                table: "OrderProduct",
                column: "ProductsId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderCode",
                table: "Orders",
                column: "OrderCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Proccesses_ProccessCategoryId",
                table: "Proccesses",
                column: "ProccessCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProccessForWorks_ProccessId",
                table: "ProccessForWorks",
                column: "ProccessId");

            migrationBuilder.CreateIndex(
                name: "IX_ProccessForWorks_WorkId",
                table: "ProccessForWorks",
                column: "WorkId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductProductImageFile_ProductsId",
                table: "ProductProductImageFile",
                column: "ProductsId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_AgencyId",
                table: "Projects",
                column: "AgencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_WorkId",
                table: "Projects",
                column: "WorkId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectWorkOrders_ProccessId",
                table: "ProjectWorkOrders",
                column: "ProccessId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectWorkOrders_ProjectId",
                table: "ProjectWorkOrders",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_PublishFileWork_WorksId",
                table: "PublishFileWork",
                column: "WorksId");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervations_CellofWarehouseId",
                table: "Rezervations",
                column: "CellofWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervations_MaterialId",
                table: "Rezervations",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleItems_MaterialId",
                table: "SaleItems",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleItems_ParentId",
                table: "SaleItems",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_CurrentId",
                table: "Sales",
                column: "CurrentId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_DeliveryCurrentId",
                table: "Sales",
                column: "DeliveryCurrentId");

            migrationBuilder.CreateIndex(
                name: "IX_ShelfofWarehouse_HallofWarehouseId",
                table: "ShelfofWarehouse",
                column: "HallofWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipItems_MaterialId",
                table: "ShipItems",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipItems_ParentId",
                table: "ShipItems",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipItems_UnitId",
                table: "ShipItems",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingOrders_SaleId",
                table: "ShippingOrders",
                column: "SaleId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingOrders_SaleItemId",
                table: "ShippingOrders",
                column: "SaleItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Ships_CurrentId",
                table: "Ships",
                column: "CurrentId");

            migrationBuilder.CreateIndex(
                name: "IX_Ships_ShipCellofWarehouseId",
                table: "Ships",
                column: "ShipCellofWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_StockCountItems_CellofWarehouseId",
                table: "StockCountItems",
                column: "CellofWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_StockCountItems_MaterialId",
                table: "StockCountItems",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_StockCountItems_StockCountId",
                table: "StockCountItems",
                column: "StockCountId");

            migrationBuilder.CreateIndex(
                name: "IX_StockCountItems_UnitId",
                table: "StockCountItems",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_StockMovements_EnteringCellId",
                table: "StockMovements",
                column: "EnteringCellId");

            migrationBuilder.CreateIndex(
                name: "IX_StockMovements_MaterialId",
                table: "StockMovements",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_StockMovements_OutgoingCellId",
                table: "StockMovements",
                column: "OutgoingCellId");

            migrationBuilder.CreateIndex(
                name: "IX_StockMovements_UnitId",
                table: "StockMovements",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_CellofWarehouseId",
                table: "Stocks",
                column: "CellofWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_MaterialId",
                table: "Stocks",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_UnitId",
                table: "Stocks",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_SubAddress_ParentId",
                table: "SubAddress",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCurrent_ParentId",
                table: "SubCurrent",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferDemandItemsBetweenWarehouses_MaterialId",
                table: "TransferDemandItemsBetweenWarehouses",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferDemandItemsBetweenWarehouses_ParentId",
                table: "TransferDemandItemsBetweenWarehouses",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferDemandItemsBetweenWarehouses_UnitId",
                table: "TransferDemandItemsBetweenWarehouses",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferDemandsBetweenWarehouses_TransferedCellofWarehouseId",
                table: "TransferDemandsBetweenWarehouses",
                column: "TransferedCellofWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferDemandsBetweenWarehouses_TransferingCellofWarehouseId",
                table: "TransferDemandsBetweenWarehouses",
                column: "TransferingCellofWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkAssignedUsers_WorkOrderId",
                table: "WorkAssignedUsers",
                column: "WorkOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkCategories_WorkTypeId",
                table: "WorkCategories",
                column: "WorkTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderMessages_WorkOrderId",
                table: "WorkOrderMessages",
                column: "WorkOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Works_AgencyId",
                table: "Works",
                column: "AgencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Works_AuthorId",
                table: "Works",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Works_CategoryId",
                table: "Works",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgencyAgencyFile");

            migrationBuilder.DropTable(
                name: "AgencyConnectionInformations");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AssignedUserToShippingWork");

            migrationBuilder.DropTable(
                name: "AuthorAuthorFile");

            migrationBuilder.DropTable(
                name: "BasketItem");

            migrationBuilder.DropTable(
                name: "GoodsAcceptItems");

            migrationBuilder.DropTable(
                name: "HubMessages");

            migrationBuilder.DropTable(
                name: "MaterialMaterialFile");

            migrationBuilder.DropTable(
                name: "OrderCollectionItems");

            migrationBuilder.DropTable(
                name: "OrderItemDistributeds");

            migrationBuilder.DropTable(
                name: "OrderProduct");

            migrationBuilder.DropTable(
                name: "ProccessForWorks");

            migrationBuilder.DropTable(
                name: "ProductProductImageFile");

            migrationBuilder.DropTable(
                name: "PublishFileWork");

            migrationBuilder.DropTable(
                name: "Rezervations");

            migrationBuilder.DropTable(
                name: "ShipItems");

            migrationBuilder.DropTable(
                name: "StockCountItems");

            migrationBuilder.DropTable(
                name: "StockMovements");

            migrationBuilder.DropTable(
                name: "Stocks");

            migrationBuilder.DropTable(
                name: "SubAddress");

            migrationBuilder.DropTable(
                name: "Subes");

            migrationBuilder.DropTable(
                name: "TransferDemandItemsBetweenWarehouses");

            migrationBuilder.DropTable(
                name: "WorkAssignedUsers");

            migrationBuilder.DropTable(
                name: "WorkOrderMessages");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "ShippingOrders");

            migrationBuilder.DropTable(
                name: "GoodsAcceps");

            migrationBuilder.DropTable(
                name: "OrderCollections");

            migrationBuilder.DropTable(
                name: "OrderDistributeds");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "FileManagements");

            migrationBuilder.DropTable(
                name: "Ships");

            migrationBuilder.DropTable(
                name: "StockCounts");

            migrationBuilder.DropTable(
                name: "TransferDemandsBetweenWarehouses");

            migrationBuilder.DropTable(
                name: "ProjectWorkOrders");

            migrationBuilder.DropTable(
                name: "SaleItems");

            migrationBuilder.DropTable(
                name: "Basket");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Proccesses");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ProccessCategories");

            migrationBuilder.DropTable(
                name: "Works");

            migrationBuilder.DropTable(
                name: "CellesofWarehouse");

            migrationBuilder.DropTable(
                name: "MaterialUnits");

            migrationBuilder.DropTable(
                name: "SubCurrent");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "WorkCategories");

            migrationBuilder.DropTable(
                name: "ShelfofWarehouse");

            migrationBuilder.DropTable(
                name: "Currents");

            migrationBuilder.DropTable(
                name: "Agencies");

            migrationBuilder.DropTable(
                name: "WorkTypes");

            migrationBuilder.DropTable(
                name: "HallofWarehouses");

            migrationBuilder.DropTable(
                name: "Warehouses");
        }
    }
}
