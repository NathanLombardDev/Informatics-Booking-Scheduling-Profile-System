using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PedalProAPI.Migrations
{
    public partial class @new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                });

            migrationBuilder.CreateTable(
                name: "BicycleCategory",
                columns: table => new
                {
                    BicycleCategory_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BicycleCategoryName = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BicycleCategory", x => x.BicycleCategory_ID);
                });

            migrationBuilder.CreateTable(
                name: "BicyclePart",
                columns: table => new
                {
                    BicyclePart_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BicyclePartName = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BicyclePart", x => x.BicyclePart_ID);
                });

            migrationBuilder.CreateTable(
                name: "BookingStatus",
                columns: table => new
                {
                    BookingStatus_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingStatusName = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingStatus", x => x.BookingStatus_ID);
                });

            migrationBuilder.CreateTable(
                name: "BookingType",
                columns: table => new
                {
                    BookingType_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingTypeName = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingType", x => x.BookingType_ID);
                });

            migrationBuilder.CreateTable(
                name: "CartStatus",
                columns: table => new
                {
                    CartStatus_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartStatusName = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartStatus", x => x.CartStatus_ID);
                });

            migrationBuilder.CreateTable(
                name: "CheckoutStatus",
                columns: table => new
                {
                    CheckoutStatus_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CheckoutStatusName = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckoutStatus", x => x.CheckoutStatus_ID);
                });

            migrationBuilder.CreateTable(
                name: "ClientType",
                columns: table => new
                {
                    ClientType_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientTypeName = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientType", x => x.ClientType_ID);
                });

            migrationBuilder.CreateTable(
                name: "Date",
                columns: table => new
                {
                    Date_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Date", x => x.Date_ID);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeStatus",
                columns: table => new
                {
                    EmpStatus_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpStatusName = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeStatus", x => x.EmpStatus_ID);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeType",
                columns: table => new
                {
                    EmpType_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpTypeName = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    EmpTypeDescription = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeType", x => x.EmpType_ID);
                });

            migrationBuilder.CreateTable(
                name: "FeedbackCategory",
                columns: table => new
                {
                    FeedbackCategory_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FeedbackCategoryName = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedbackCategory", x => x.FeedbackCategory_ID);
                });

            migrationBuilder.CreateTable(
                name: "HELPCategory",
                columns: table => new
                {
                    HelpCategory_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HelpCategoryName = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HELPCategory", x => x.HelpCategory_ID);
                });

            migrationBuilder.CreateTable(
                name: "ImageType",
                columns: table => new
                {
                    ImageType_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageTypeName = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageType", x => x.ImageType_ID);
                });

            migrationBuilder.CreateTable(
                name: "IndemnityForms",
                columns: table => new
                {
                    IndemnityForm_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileContent = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    DateUploaded = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndemnityForms", x => x.IndemnityForm_ID);
                });

            migrationBuilder.CreateTable(
                name: "Package",
                columns: table => new
                {
                    Package_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PackageName = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    PackageDescription = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Package", x => x.Package_ID);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    Payment_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentAmount = table.Column<double>(type: "float", nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.Payment_ID);
                });

            migrationBuilder.CreateTable(
                name: "PedalProRole",
                columns: table => new
                {
                    Role_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedalProRole", x => x.Role_ID);
                });

            migrationBuilder.CreateTable(
                name: "Price",
                columns: table => new
                {
                    Price_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<double>(type: "float", nullable: true),
                    PriceDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Price", x => x.Price_ID);
                });

            migrationBuilder.CreateTable(
                name: "RefundReason",
                columns: table => new
                {
                    RefundReason_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RefundReasonDesc = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefundReason", x => x.RefundReason_ID);
                });

            migrationBuilder.CreateTable(
                name: "Timeslot",
                columns: table => new
                {
                    Timeslot_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndTime = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timeslot", x => x.Timeslot_ID);
                });

            migrationBuilder.CreateTable(
                name: "TrainingModuleStatus",
                columns: table => new
                {
                    TrainingModuleStatus_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainingModuleStatusName = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingModuleStatus", x => x.TrainingModuleStatus_ID);
                });

            migrationBuilder.CreateTable(
                name: "VAT",
                columns: table => new
                {
                    VAT_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VATPecerntage = table.Column<double>(type: "float", nullable: true),
                    VATDate = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VAT", x => x.VAT_ID);
                });

            migrationBuilder.CreateTable(
                name: "VideoType",
                columns: table => new
                {
                    VideoType_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VideoTypeName = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoType", x => x.VideoType_ID);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutType",
                columns: table => new
                {
                    WorkoutType_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkoutTypeName = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutType", x => x.WorkoutType_ID);
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
                name: "Administrator",
                columns: table => new
                {
                    AdministratorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    AdminName = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    AdminSurname = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    AdminEmail = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    AdminPhoneNum = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrator", x => x.AdministratorID);
                    table.ForeignKey(
                        name: "FK_Administrator_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "Client",
                columns: table => new
                {
                    ClientID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClientType_ID = table.Column<int>(type: "int", nullable: true),
                    ClientTitle = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    ClientName = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    ClientSurname = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    ClientEmailAddress = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    ClientPhysicalAddress = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    ClientPhoneNum = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    ClientDateOfBirth = table.Column<DateTime>(type: "date", nullable: true),
                    ClientProfilePicture = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.ClientID);
                    table.ForeignKey(
                        name: "FK_Client_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Client_ClientType_ClientType_ID",
                        column: x => x.ClientType_ID,
                        principalTable: "ClientType",
                        principalColumn: "ClientType_ID");
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    EmployeeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmpType_ID = table.Column<int>(type: "int", nullable: true),
                    EmpStatus_ID = table.Column<int>(type: "int", nullable: true),
                    EmpTitle = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    EmpName = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    EmpSurname = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    EmpPhoneNum = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    EmpEmailAddress = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.EmployeeID);
                    table.ForeignKey(
                        name: "FK_Employee_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employee_EmployeeStatus_EmpStatus_ID",
                        column: x => x.EmpStatus_ID,
                        principalTable: "EmployeeStatus",
                        principalColumn: "EmpStatus_ID");
                    table.ForeignKey(
                        name: "FK_Employee_EmployeeType_EmpType_ID",
                        column: x => x.EmpType_ID,
                        principalTable: "EmployeeType",
                        principalColumn: "EmpType_ID");
                });

            migrationBuilder.CreateTable(
                name: "HELP",
                columns: table => new
                {
                    Help_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HelpCategory_ID = table.Column<int>(type: "int", nullable: true),
                    HelpName = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    HelpDescription = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HELP", x => x.Help_ID);
                    table.ForeignKey(
                        name: "FK_HELP_HELPCategory_HelpCategory_ID",
                        column: x => x.HelpCategory_ID,
                        principalTable: "HELPCategory",
                        principalColumn: "HelpCategory_ID");
                });

            migrationBuilder.CreateTable(
                name: "BrandImage",
                columns: table => new
                {
                    BrandImage_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageType_ID = table.Column<int>(type: "int", nullable: true),
                    BrandImgName = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    ImageURL = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrandImage", x => x.BrandImage_ID);
                    table.ForeignKey(
                        name: "FK_BrandImage_ImageType_ImageType_ID",
                        column: x => x.ImageType_ID,
                        principalTable: "ImageType",
                        principalColumn: "ImageType_ID");
                });

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    Cart_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartQuantity = table.Column<int>(type: "int", nullable: true),
                    CartAmount = table.Column<double>(type: "float", nullable: true),
                    CartStatus_ID = table.Column<int>(type: "int", nullable: true),
                    Payment_ID = table.Column<int>(type: "int", nullable: true),
                    Package_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.Cart_ID);
                    table.ForeignKey(
                        name: "FK_Cart_CartStatus_CartStatus_ID",
                        column: x => x.CartStatus_ID,
                        principalTable: "CartStatus",
                        principalColumn: "CartStatus_ID");
                    table.ForeignKey(
                        name: "FK_Cart_Package_Package_ID",
                        column: x => x.Package_ID,
                        principalTable: "Package",
                        principalColumn: "Package_ID");
                    table.ForeignKey(
                        name: "FK_Cart_Payment_Payment_ID",
                        column: x => x.Payment_ID,
                        principalTable: "Payment",
                        principalColumn: "Payment_ID");
                });

            migrationBuilder.CreateTable(
                name: "PackagePrice",
                columns: table => new
                {
                    PackagePrice_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Package_ID = table.Column<int>(type: "int", nullable: true),
                    Price_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackagePrice", x => x.PackagePrice_ID);
                    table.ForeignKey(
                        name: "FK_PackagePrice_Package_Package_ID",
                        column: x => x.Package_ID,
                        principalTable: "Package",
                        principalColumn: "Package_ID");
                    table.ForeignKey(
                        name: "FK_PackagePrice_Price_Price_ID",
                        column: x => x.Price_ID,
                        principalTable: "Price",
                        principalColumn: "Price_ID");
                });

            migrationBuilder.CreateTable(
                name: "DateSlot",
                columns: table => new
                {
                    DateSlot_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Timeslot_ID = table.Column<int>(type: "int", nullable: true),
                    Date_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DateSlot", x => x.DateSlot_ID);
                    table.ForeignKey(
                        name: "FK_DateSlot_Date_Date_ID",
                        column: x => x.Date_ID,
                        principalTable: "Date",
                        principalColumn: "Date_ID");
                    table.ForeignKey(
                        name: "FK_DateSlot_Timeslot_Timeslot_ID",
                        column: x => x.Timeslot_ID,
                        principalTable: "Timeslot",
                        principalColumn: "Timeslot_ID");
                });

            migrationBuilder.CreateTable(
                name: "TrainingModule",
                columns: table => new
                {
                    TrainingModule_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainingModuleName = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    TrainingModuleDescription = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Package_ID = table.Column<int>(type: "int", nullable: true),
                    TrainingModuleStatus_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingModule", x => x.TrainingModule_ID);
                    table.ForeignKey(
                        name: "FK_TrainingModule_Package_Package_ID",
                        column: x => x.Package_ID,
                        principalTable: "Package",
                        principalColumn: "Package_ID");
                    table.ForeignKey(
                        name: "FK_TrainingModule_TrainingModuleStatus_TrainingModuleStatus_ID",
                        column: x => x.TrainingModuleStatus_ID,
                        principalTable: "TrainingModuleStatus",
                        principalColumn: "TrainingModuleStatus_ID");
                });

            migrationBuilder.CreateTable(
                name: "VideoLink",
                columns: table => new
                {
                    VideoLink_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VideoURL = table.Column<string>(type: "varchar(550)", unicode: false, maxLength: 550, nullable: true),
                    VideoType_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoLink", x => x.VideoLink_ID);
                    table.ForeignKey(
                        name: "FK_VideoLink_VideoType_VideoType_ID",
                        column: x => x.VideoType_ID,
                        principalTable: "VideoType",
                        principalColumn: "VideoType_ID");
                });

            migrationBuilder.CreateTable(
                name: "ClientIndemnityForms",
                columns: table => new
                {
                    IndemnityForm_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileContent = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    DateUploaded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Client_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientIndemnityForms", x => x.IndemnityForm_ID);
                    table.ForeignKey(
                        name: "FK_ClientIndemnityForms_Client_Client_ID",
                        column: x => x.Client_ID,
                        principalTable: "Client",
                        principalColumn: "ClientID");
                });

            migrationBuilder.CreateTable(
                name: "ClientPackage",
                columns: table => new
                {
                    ClientPackage = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Package_ID = table.Column<int>(type: "int", nullable: true),
                    ClientID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientPackage", x => x.ClientPackage);
                    table.ForeignKey(
                        name: "FK_ClientPackage_Client_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Client",
                        principalColumn: "ClientID");
                    table.ForeignKey(
                        name: "FK_ClientPackage_Package_Package_ID",
                        column: x => x.Package_ID,
                        principalTable: "Package",
                        principalColumn: "Package_ID");
                });

            migrationBuilder.CreateTable(
                name: "TrainingSession",
                columns: table => new
                {
                    TrainingSession_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientID = table.Column<int>(type: "int", nullable: true),
                    TrainingSessionDescription = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingSession", x => x.TrainingSession_ID);
                    table.ForeignKey(
                        name: "FK_TrainingSession_Client_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Client",
                        principalColumn: "ClientID");
                });

            migrationBuilder.CreateTable(
                name: "Workout",
                columns: table => new
                {
                    Workout_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Distance = table.Column<double>(type: "float", nullable: true),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: true),
                    HeartRate = table.Column<int>(type: "int", nullable: true),
                    Client_ID = table.Column<int>(type: "int", nullable: true),
                    WorkoutType_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workout", x => x.Workout_ID);
                    table.ForeignKey(
                        name: "FK_Workout_Client_Client_ID",
                        column: x => x.Client_ID,
                        principalTable: "Client",
                        principalColumn: "ClientID");
                    table.ForeignKey(
                        name: "FK_Workout_WorkoutType_WorkoutType_ID",
                        column: x => x.WorkoutType_ID,
                        principalTable: "WorkoutType",
                        principalColumn: "WorkoutType_ID");
                });

            migrationBuilder.CreateTable(
                name: "BicycleBrand",
                columns: table => new
                {
                    BicycleBrand_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandImage_ID = table.Column<int>(type: "int", nullable: true),
                    BrandName = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BicycleBrand", x => x.BicycleBrand_ID);
                    table.ForeignKey(
                        name: "FK_BicycleBrand_BrandImage_BrandImage_ID",
                        column: x => x.BrandImage_ID,
                        principalTable: "BrandImage",
                        principalColumn: "BrandImage_ID");
                });

            migrationBuilder.CreateTable(
                name: "Checkout",
                columns: table => new
                {
                    Checkout_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CheckoutStatus_ID = table.Column<int>(type: "int", nullable: true),
                    Payment_ID = table.Column<int>(type: "int", nullable: true),
                    Cart_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Checkout", x => x.Checkout_ID);
                    table.ForeignKey(
                        name: "FK_Checkout_Cart_Cart_ID",
                        column: x => x.Cart_ID,
                        principalTable: "Cart",
                        principalColumn: "Cart_ID");
                    table.ForeignKey(
                        name: "FK_Checkout_CheckoutStatus_CheckoutStatus_ID",
                        column: x => x.CheckoutStatus_ID,
                        principalTable: "CheckoutStatus",
                        principalColumn: "CheckoutStatus_ID");
                    table.ForeignKey(
                        name: "FK_Checkout_Payment_Payment_ID",
                        column: x => x.Payment_ID,
                        principalTable: "Payment",
                        principalColumn: "Payment_ID");
                });

            migrationBuilder.CreateTable(
                name: "TrainingMaterial",
                columns: table => new
                {
                    TrainingMaterial_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainingMaterialName = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    Content = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    VideoLink_ID = table.Column<int>(type: "int", nullable: true),
                    TrainingModule_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingMaterial", x => x.TrainingMaterial_ID);
                    table.ForeignKey(
                        name: "FK_TrainingMaterial_TrainingModule_TrainingModule_ID",
                        column: x => x.TrainingModule_ID,
                        principalTable: "TrainingModule",
                        principalColumn: "TrainingModule_ID");
                    table.ForeignKey(
                        name: "FK_TrainingMaterial_VideoLink_VideoLink_ID",
                        column: x => x.VideoLink_ID,
                        principalTable: "VideoLink",
                        principalColumn: "VideoLink_ID");
                });

            migrationBuilder.CreateTable(
                name: "Feedback",
                columns: table => new
                {
                    Feedback_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FeedbackDescription = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    FeedbackRating = table.Column<int>(type: "int", nullable: true),
                    FeedbackCategory_ID = table.Column<int>(type: "int", nullable: true),
                    Client_ID = table.Column<int>(type: "int", nullable: true),
                    Trainingsession_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedback", x => x.Feedback_ID);
                    table.ForeignKey(
                        name: "FK_Feedback_Client_Client_ID",
                        column: x => x.Client_ID,
                        principalTable: "Client",
                        principalColumn: "ClientID");
                    table.ForeignKey(
                        name: "FK_Feedback_FeedbackCategory_FeedbackCategory_ID",
                        column: x => x.FeedbackCategory_ID,
                        principalTable: "FeedbackCategory",
                        principalColumn: "FeedbackCategory_ID");
                    table.ForeignKey(
                        name: "FK_Feedback_TrainingSession_Trainingsession_ID",
                        column: x => x.Trainingsession_ID,
                        principalTable: "TrainingSession",
                        principalColumn: "TrainingSession_ID");
                });

            migrationBuilder.CreateTable(
                name: "Bicycle",
                columns: table => new
                {
                    Bicycle_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientID = table.Column<int>(type: "int", nullable: true),
                    BicycleCategory_ID = table.Column<int>(type: "int", nullable: true),
                    BicycleBrand_ID = table.Column<int>(type: "int", nullable: true),
                    BicycleName = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bicycle", x => x.Bicycle_ID);
                    table.ForeignKey(
                        name: "FK_Bicycle_BicycleBrand_BicycleBrand_ID",
                        column: x => x.BicycleBrand_ID,
                        principalTable: "BicycleBrand",
                        principalColumn: "BicycleBrand_ID");
                    table.ForeignKey(
                        name: "FK_Bicycle_BicycleCategory_BicycleCategory_ID",
                        column: x => x.BicycleCategory_ID,
                        principalTable: "BicycleCategory",
                        principalColumn: "BicycleCategory_ID");
                    table.ForeignKey(
                        name: "FK_Bicycle_Client_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Client",
                        principalColumn: "ClientID");
                });

            migrationBuilder.CreateTable(
                name: "Service",
                columns: table => new
                {
                    Service_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BicyclePart_ID = table.Column<int>(type: "int", nullable: true),
                    Bicycle_ID = table.Column<int>(type: "int", nullable: true),
                    ServiceDescription = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service", x => x.Service_ID);
                    table.ForeignKey(
                        name: "FK_Service_Bicycle_Bicycle_ID",
                        column: x => x.Bicycle_ID,
                        principalTable: "Bicycle",
                        principalColumn: "Bicycle_ID");
                    table.ForeignKey(
                        name: "FK_Service_BicyclePart_BicyclePart_ID",
                        column: x => x.BicyclePart_ID,
                        principalTable: "BicyclePart",
                        principalColumn: "BicyclePart_ID");
                });

            migrationBuilder.CreateTable(
                name: "Setup",
                columns: table => new
                {
                    Setup_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bicycle_ID = table.Column<int>(type: "int", nullable: true),
                    SetupDescription = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setup", x => x.Setup_ID);
                    table.ForeignKey(
                        name: "FK_Setup_Bicycle_Bicycle_ID",
                        column: x => x.Bicycle_ID,
                        principalTable: "Bicycle",
                        principalColumn: "Bicycle_ID");
                });

            migrationBuilder.CreateTable(
                name: "Schedule",
                columns: table => new
                {
                    Schedule_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Dateslot_ID = table.Column<int>(type: "int", nullable: true),
                    Setup_ID = table.Column<int>(type: "int", nullable: true),
                    Employee_ID = table.Column<int>(type: "int", nullable: true),
                    TrainingSession_ID = table.Column<int>(type: "int", nullable: true),
                    Service_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedule", x => x.Schedule_ID);
                    table.ForeignKey(
                        name: "FK_Schedule_DateSlot_Dateslot_ID",
                        column: x => x.Dateslot_ID,
                        principalTable: "DateSlot",
                        principalColumn: "DateSlot_ID");
                    table.ForeignKey(
                        name: "FK_Schedule_Employee_Employee_ID",
                        column: x => x.Employee_ID,
                        principalTable: "Employee",
                        principalColumn: "EmployeeID");
                    table.ForeignKey(
                        name: "FK_Schedule_Service_Service_ID",
                        column: x => x.Service_ID,
                        principalTable: "Service",
                        principalColumn: "Service_ID");
                    table.ForeignKey(
                        name: "FK_Schedule_Setup_Setup_ID",
                        column: x => x.Setup_ID,
                        principalTable: "Setup",
                        principalColumn: "Setup_ID");
                    table.ForeignKey(
                        name: "FK_Schedule_TrainingSession_TrainingSession_ID",
                        column: x => x.TrainingSession_ID,
                        principalTable: "TrainingSession",
                        principalColumn: "TrainingSession_ID");
                });

            migrationBuilder.CreateTable(
                name: "ServicePrice",
                columns: table => new
                {
                    ServicePrice_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Setup_ID = table.Column<int>(type: "int", nullable: true),
                    Price_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicePrice", x => x.ServicePrice_ID);
                    table.ForeignKey(
                        name: "FK_ServicePrice_Price_Price_ID",
                        column: x => x.Price_ID,
                        principalTable: "Price",
                        principalColumn: "Price_ID");
                    table.ForeignKey(
                        name: "FK_ServicePrice_Setup_Setup_ID",
                        column: x => x.Setup_ID,
                        principalTable: "Setup",
                        principalColumn: "Setup_ID");
                });

            migrationBuilder.CreateTable(
                name: "SetupPrice",
                columns: table => new
                {
                    SetupPrice_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Setup_ID = table.Column<int>(type: "int", nullable: true),
                    Price_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SetupPrice", x => x.SetupPrice_ID);
                    table.ForeignKey(
                        name: "FK_SetupPrice_Price_Price_ID",
                        column: x => x.Price_ID,
                        principalTable: "Price",
                        principalColumn: "Price_ID");
                    table.ForeignKey(
                        name: "FK_SetupPrice_Setup_Setup_ID",
                        column: x => x.Setup_ID,
                        principalTable: "Setup",
                        principalColumn: "Setup_ID");
                });

            migrationBuilder.CreateTable(
                name: "Booking",
                columns: table => new
                {
                    Booking_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingStatus_ID = table.Column<int>(type: "int", nullable: true),
                    ClientID = table.Column<int>(type: "int", nullable: true),
                    BookingType_ID = table.Column<int>(type: "int", nullable: true),
                    Schedule_ID = table.Column<int>(type: "int", nullable: true),
                    ReferenceNum = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Booking", x => x.Booking_ID);
                    table.ForeignKey(
                        name: "FK_Booking_BookingStatus_BookingStatus_ID",
                        column: x => x.BookingStatus_ID,
                        principalTable: "BookingStatus",
                        principalColumn: "BookingStatus_ID");
                    table.ForeignKey(
                        name: "FK_Booking_BookingType_BookingType_ID",
                        column: x => x.BookingType_ID,
                        principalTable: "BookingType",
                        principalColumn: "BookingType_ID");
                    table.ForeignKey(
                        name: "FK_Booking_Client_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Client",
                        principalColumn: "ClientID");
                    table.ForeignKey(
                        name: "FK_Booking_Schedule_Schedule_ID",
                        column: x => x.Schedule_ID,
                        principalTable: "Schedule",
                        principalColumn: "Schedule_ID");
                });

            migrationBuilder.CreateTable(
                name: "Refund",
                columns: table => new
                {
                    Refund_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Booking_ID = table.Column<int>(type: "int", nullable: true),
                    RefundReason_ID = table.Column<int>(type: "int", nullable: true),
                    RefundDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    RefundAmount = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Refund", x => x.Refund_ID);
                    table.ForeignKey(
                        name: "FK_Refund_Booking_Booking_ID",
                        column: x => x.Booking_ID,
                        principalTable: "Booking",
                        principalColumn: "Booking_ID");
                    table.ForeignKey(
                        name: "FK_Refund_RefundReason_RefundReason_ID",
                        column: x => x.RefundReason_ID,
                        principalTable: "RefundReason",
                        principalColumn: "RefundReason_ID");
                });

            migrationBuilder.InsertData(
                table: "BookingStatus",
                columns: new[] { "BookingStatus_ID", "BookingStatusName" },
                values: new object[,]
                {
                    { 1, "Progress" },
                    { 2, "Booked" },
                    { 3, " payment" }
                });

            migrationBuilder.InsertData(
                table: "BookingType",
                columns: new[] { "BookingType_ID", "BookingTypeName" },
                values: new object[,]
                {
                    { 1, "Training" },
                    { 2, "Repair" },
                    { 3, " Setup" }
                });

            migrationBuilder.InsertData(
                table: "ClientType",
                columns: new[] { "ClientType_ID", "ClientTypeName" },
                values: new object[,]
                {
                    { 1, "Basic" },
                    { 2, "Remote" },
                    { 3, "Paid" }
                });

            migrationBuilder.InsertData(
                table: "EmployeeStatus",
                columns: new[] { "EmpStatus_ID", "EmpStatusName" },
                values: new object[] { 1, "Available" });

            migrationBuilder.InsertData(
                table: "HELPCategory",
                columns: new[] { "HelpCategory_ID", "HelpCategoryName" },
                values: new object[,]
                {
                    { 1, "Bookings" },
                    { 2, "Account" },
                    { 3, "TrainingM" },
                    { 4, "Workout" }
                });

            migrationBuilder.InsertData(
                table: "ImageType",
                columns: new[] { "ImageType_ID", "ImageTypeName" },
                values: new object[,]
                {
                    { 1, "JPG" },
                    { 2, "JPEG" },
                    { 3, "PNG" },
                    { 4, "WEBJ" }
                });

            migrationBuilder.InsertData(
                table: "Package",
                columns: new[] { "Package_ID", "PackageDescription", "PackageName" },
                values: new object[] { 1, "The base package", "Platinum" });

            migrationBuilder.InsertData(
                table: "TrainingModuleStatus",
                columns: new[] { "TrainingModuleStatus_ID", "TrainingModuleStatusName" },
                values: new object[] { 1, "Unstarted" });

            migrationBuilder.InsertData(
                table: "VideoType",
                columns: new[] { "VideoType_ID", "VideoTypeName" },
                values: new object[,]
                {
                    { 1, "MP4" },
                    { 2, "Mov" },
                    { 3, "AVI" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Administrator_UserID",
                table: "Administrator",
                column: "UserID");

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
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Bicycle_BicycleBrand_ID",
                table: "Bicycle",
                column: "BicycleBrand_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Bicycle_BicycleCategory_ID",
                table: "Bicycle",
                column: "BicycleCategory_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Bicycle_ClientID",
                table: "Bicycle",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_BicycleBrand_BrandImage_ID",
                table: "BicycleBrand",
                column: "BrandImage_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_BookingStatus_ID",
                table: "Booking",
                column: "BookingStatus_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_BookingType_ID",
                table: "Booking",
                column: "BookingType_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_ClientID",
                table: "Booking",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_Schedule_ID",
                table: "Booking",
                column: "Schedule_ID");

            migrationBuilder.CreateIndex(
                name: "IX_BrandImage_ImageType_ID",
                table: "BrandImage",
                column: "ImageType_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_CartStatus_ID",
                table: "Cart",
                column: "CartStatus_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_Package_ID",
                table: "Cart",
                column: "Package_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_Payment_ID",
                table: "Cart",
                column: "Payment_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Checkout_Cart_ID",
                table: "Checkout",
                column: "Cart_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Checkout_CheckoutStatus_ID",
                table: "Checkout",
                column: "CheckoutStatus_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Checkout_Payment_ID",
                table: "Checkout",
                column: "Payment_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Client_ClientType_ID",
                table: "Client",
                column: "ClientType_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Client_UserID",
                table: "Client",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_ClientIndemnityForms_Client_ID",
                table: "ClientIndemnityForms",
                column: "Client_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ClientPackage_ClientID",
                table: "ClientPackage",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_ClientPackage_Package_ID",
                table: "ClientPackage",
                column: "Package_ID");

            migrationBuilder.CreateIndex(
                name: "IX_DateSlot_Date_ID",
                table: "DateSlot",
                column: "Date_ID");

            migrationBuilder.CreateIndex(
                name: "IX_DateSlot_Timeslot_ID",
                table: "DateSlot",
                column: "Timeslot_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_EmpStatus_ID",
                table: "Employee",
                column: "EmpStatus_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_EmpType_ID",
                table: "Employee",
                column: "EmpType_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_UserID",
                table: "Employee",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_Client_ID",
                table: "Feedback",
                column: "Client_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_FeedbackCategory_ID",
                table: "Feedback",
                column: "FeedbackCategory_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_Trainingsession_ID",
                table: "Feedback",
                column: "Trainingsession_ID");

            migrationBuilder.CreateIndex(
                name: "IX_HELP_HelpCategory_ID",
                table: "HELP",
                column: "HelpCategory_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PackagePrice_Package_ID",
                table: "PackagePrice",
                column: "Package_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PackagePrice_Price_ID",
                table: "PackagePrice",
                column: "Price_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Refund_Booking_ID",
                table: "Refund",
                column: "Booking_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Refund_RefundReason_ID",
                table: "Refund",
                column: "RefundReason_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_Dateslot_ID",
                table: "Schedule",
                column: "Dateslot_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_Employee_ID",
                table: "Schedule",
                column: "Employee_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_Service_ID",
                table: "Schedule",
                column: "Service_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_Setup_ID",
                table: "Schedule",
                column: "Setup_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_TrainingSession_ID",
                table: "Schedule",
                column: "TrainingSession_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Service_Bicycle_ID",
                table: "Service",
                column: "Bicycle_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Service_BicyclePart_ID",
                table: "Service",
                column: "BicyclePart_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ServicePrice_Price_ID",
                table: "ServicePrice",
                column: "Price_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ServicePrice_Setup_ID",
                table: "ServicePrice",
                column: "Setup_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Setup_Bicycle_ID",
                table: "Setup",
                column: "Bicycle_ID");

            migrationBuilder.CreateIndex(
                name: "IX_SetupPrice_Price_ID",
                table: "SetupPrice",
                column: "Price_ID");

            migrationBuilder.CreateIndex(
                name: "IX_SetupPrice_Setup_ID",
                table: "SetupPrice",
                column: "Setup_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingMaterial_TrainingModule_ID",
                table: "TrainingMaterial",
                column: "TrainingModule_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingMaterial_VideoLink_ID",
                table: "TrainingMaterial",
                column: "VideoLink_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingModule_Package_ID",
                table: "TrainingModule",
                column: "Package_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingModule_TrainingModuleStatus_ID",
                table: "TrainingModule",
                column: "TrainingModuleStatus_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingSession_ClientID",
                table: "TrainingSession",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_VideoLink_VideoType_ID",
                table: "VideoLink",
                column: "VideoType_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Workout_Client_ID",
                table: "Workout",
                column: "Client_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Workout_WorkoutType_ID",
                table: "Workout",
                column: "WorkoutType_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Administrator");

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
                name: "Checkout");

            migrationBuilder.DropTable(
                name: "ClientIndemnityForms");

            migrationBuilder.DropTable(
                name: "ClientPackage");

            migrationBuilder.DropTable(
                name: "Feedback");

            migrationBuilder.DropTable(
                name: "HELP");

            migrationBuilder.DropTable(
                name: "IndemnityForms");

            migrationBuilder.DropTable(
                name: "PackagePrice");

            migrationBuilder.DropTable(
                name: "PedalProRole");

            migrationBuilder.DropTable(
                name: "Refund");

            migrationBuilder.DropTable(
                name: "ServicePrice");

            migrationBuilder.DropTable(
                name: "SetupPrice");

            migrationBuilder.DropTable(
                name: "TrainingMaterial");

            migrationBuilder.DropTable(
                name: "VAT");

            migrationBuilder.DropTable(
                name: "Workout");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "CheckoutStatus");

            migrationBuilder.DropTable(
                name: "FeedbackCategory");

            migrationBuilder.DropTable(
                name: "HELPCategory");

            migrationBuilder.DropTable(
                name: "Booking");

            migrationBuilder.DropTable(
                name: "RefundReason");

            migrationBuilder.DropTable(
                name: "Price");

            migrationBuilder.DropTable(
                name: "TrainingModule");

            migrationBuilder.DropTable(
                name: "VideoLink");

            migrationBuilder.DropTable(
                name: "WorkoutType");

            migrationBuilder.DropTable(
                name: "CartStatus");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "BookingStatus");

            migrationBuilder.DropTable(
                name: "BookingType");

            migrationBuilder.DropTable(
                name: "Schedule");

            migrationBuilder.DropTable(
                name: "Package");

            migrationBuilder.DropTable(
                name: "TrainingModuleStatus");

            migrationBuilder.DropTable(
                name: "VideoType");

            migrationBuilder.DropTable(
                name: "DateSlot");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Service");

            migrationBuilder.DropTable(
                name: "Setup");

            migrationBuilder.DropTable(
                name: "TrainingSession");

            migrationBuilder.DropTable(
                name: "Date");

            migrationBuilder.DropTable(
                name: "Timeslot");

            migrationBuilder.DropTable(
                name: "EmployeeStatus");

            migrationBuilder.DropTable(
                name: "EmployeeType");

            migrationBuilder.DropTable(
                name: "BicyclePart");

            migrationBuilder.DropTable(
                name: "Bicycle");

            migrationBuilder.DropTable(
                name: "BicycleBrand");

            migrationBuilder.DropTable(
                name: "BicycleCategory");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "BrandImage");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ClientType");

            migrationBuilder.DropTable(
                name: "ImageType");
        }
    }
}
