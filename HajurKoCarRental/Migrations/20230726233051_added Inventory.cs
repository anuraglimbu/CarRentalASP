using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HajurKoCarRental.Migrations
{
    public partial class addedInventory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Inventory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    SubTypeId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Picture = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PictureName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PictureExtension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fuel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlateNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OutOfDisplay = table.Column<bool>(type: "bit", nullable: true),
                    ReviewerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordCreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventory", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "280b32ae-81fe-e6d6-6690-a6d573352b63",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c49528d1-fc69-4bb7-b194-90171cbb31b0", "AQAAAAEAACcQAAAAEK+EbuMkbO8wYrCEsN65hDFhx9IWEJi7XjJWwHNUCQMbH6x7hghcTQ9tLyAr6UzeGw==", "4ab1c792-a9dd-4ef4-9136-c85d42d35859" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "34fdd202-6cb4-3bea-3537-37d5147dc812",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "986ad27b-73c3-4bd7-937d-bb2de0601f93", "AQAAAAEAACcQAAAAED9QBnjrtXvHJKHeRc/VkA+9VCiG4VOnDt8gDhJ/ks1P+l4k4aO6LlKMOg19j7hzYw==", "5f155bac-1d64-4c7c-a766-46dba27978a6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6693a7cb-9cfa-8619-2627-437d832fb9c6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6910d793-3be6-4114-8663-6334c2589906", "AQAAAAEAACcQAAAAEFaDJpVpkGaQMJT84uX1kr1F0hWO+vBSmoNYvdSXt+Val7pApwLbFLLKufgt7S3PjA==", "6d8a9c85-3d48-43ab-94ae-f8365ed843aa" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9ac3e5e7-60fc-4c38-9d94-1cabe8732f29", "AQAAAAEAACcQAAAAEJ6Hxm3Io1bG2wlq/nPtPo95s0FLEgnQ/vmYyStjzzfdEtWP0jn+4Xre2U/p27jZCQ==", "386c469b-7d29-4cda-94e5-091f067918c3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a4581e92-25c7-ad88-f114-2be0fb5caeb7",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "36580c3f-529a-4715-8eac-328caf400599", "AQAAAAEAACcQAAAAEKICpdjP6cwq1p+0OZfBCcIxMMPN5tGBt+re2Q6Vq7N4TGIv8PH6MUyQfUyN1S5tiA==", "b7dbbb60-b339-401c-a914-57f79ef06f64" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cc2dde8a-a19b-4c94-bbc1-f0007f06cc22",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "824e6b4c-0069-40a1-9746-a5e2f25c19d1", "AQAAAAEAACcQAAAAECzwIokgVEeHkTPvm2e/5I8QEI4Tjd4aohLRKasyJHnwBNY2wQsQpCsXNeu72LrOQw==", "32c0fb90-f22c-4b4c-9d45-e7ca3861c109" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inventory");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "280b32ae-81fe-e6d6-6690-a6d573352b63",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "968a9057-0271-4d1b-8acb-21ce9173e547", "AQAAAAEAACcQAAAAELPHfPkGuJl9g0GF4VFzw3taU7O5hRysybpuMGeWdLVC7HsFkmGc4xRZUakEj5xzag==", "2e2271eb-a7b2-4bb7-8c04-93c2f97c722c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "34fdd202-6cb4-3bea-3537-37d5147dc812",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a2810ac0-ef0e-4d03-8fac-6afe8d552099", "AQAAAAEAACcQAAAAEDpRlFpPlRQHGW5guLP9sYx3VeUvFBXOA7x7UqA+VC5ngCsiBUDIx4Zyiwo7RofRdA==", "1b69f138-d0ce-4594-858c-97a4a0a7e990" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6693a7cb-9cfa-8619-2627-437d832fb9c6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "656ac651-39c2-4557-ae59-0cac8b78828e", "AQAAAAEAACcQAAAAEOqxvbTN6yYYHRTLIYG0T7p1HC9OPBDNAHrB33gVX++sjsBo4U4n94wvrlq1dTQR9A==", "a2eed436-1e6d-4e13-a152-c21747af6998" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7cc7ad51-b9c9-443a-a857-8b469ba69aa6", "AQAAAAEAACcQAAAAEDKX4cxOH0JZ9AawBg6INq0b6uPl8+petMkRbFEdmMO9H/ZMWy9A1hWzzGXdhE2PIg==", "e09372c7-64db-4f9f-9017-eaa5db17af21" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a4581e92-25c7-ad88-f114-2be0fb5caeb7",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "faee4717-d13e-4a9e-ab51-bbe12b56ea18", "AQAAAAEAACcQAAAAECrt1BIpA/nEY4rdtKf2hKkoFIRp2Squzojue5O2Y70P3uZUbWIH+pKG2nj1RXhxrg==", "e5f4807e-b504-498e-9396-20dff16e38d1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cc2dde8a-a19b-4c94-bbc1-f0007f06cc22",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d7faae77-fe79-41a7-b9ce-eb90da39f160", "AQAAAAEAACcQAAAAEMcsjaOSCf7WQRTUhfoz44eusRrRVQ8V9y+Dx3aAc75LjZjZVZDIDCd/phjDfDwyuA==", "f36b5044-7113-4f65-a846-53b409baa617" });
        }
    }
}
