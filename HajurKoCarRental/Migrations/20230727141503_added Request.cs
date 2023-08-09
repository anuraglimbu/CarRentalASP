using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HajurKoCarRental.Migrations
{
    public partial class addedRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarId = table.Column<int>(type: "int", nullable: false),
                    PickingUp = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DroppingOff = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordCreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TotalPrice = table.Column<float>(type: "real", nullable: false),
                    Pending = table.Column<int>(type: "int", nullable: true),
                    Approved = table.Column<int>(type: "int", nullable: true),
                    Claimed = table.Column<int>(type: "int", nullable: true),
                    Returned = table.Column<int>(type: "int", nullable: true),
                    Paid = table.Column<int>(type: "int", nullable: true),
                    RequesterId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApproverId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "280b32ae-81fe-e6d6-6690-a6d573352b63",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4ecd55ed-7691-4b2e-8d07-e905cea874f7", "AQAAAAEAACcQAAAAEEw3/+l/wbG/K4QxVp3C0IvcbDtxas5NGInwM+N0WrEWG3c2WaEHJEbt3Y+uCkc2lw==", "f4ea5e46-aad7-495f-bb27-290c93da6a45" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "34fdd202-6cb4-3bea-3537-37d5147dc812",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a77722be-11ae-442d-a07d-26666770922c", "AQAAAAEAACcQAAAAECd1tmuKjj/QqZeyqtxQ7kTmKMB8Kk0YovOqY5RmYPfSyNI4XJLN9CddjvABFoVlvg==", "8d6de6bd-d3a3-4183-8217-503fdccbd322" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6693a7cb-9cfa-8619-2627-437d832fb9c6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b1cebd8b-d330-421c-9d79-43d9951e7a73", "AQAAAAEAACcQAAAAEIcgZD0xI77MSy22rgT3K0I+UIaSB0PPmTqND2HlRexkUHN4ZC5q+0ZobkEqNEyxmQ==", "9910c1e7-0e77-49e0-86f4-529f33a5f305" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2eabc2e7-d22b-4ebe-a276-9f898110d75c", "AQAAAAEAACcQAAAAEIfcL3D2YLBStE9T+uImHeSxOM+FLJeB/+uOBQgPEXkertx8XsG8/CEAflMcCqRRqw==", "6b67ca78-78ef-4027-9274-2c349bdfc2d5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a4581e92-25c7-ad88-f114-2be0fb5caeb7",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "20888a47-7758-467c-9b64-4b9839662688", "AQAAAAEAACcQAAAAED0M+9jq1TL7XoOWFJ/AXgA5jjHHlkx2qYlmYqV+l+Dg2yOdFu3L9eVjo0GPO44Low==", "ff0c30e0-420b-464f-8060-0968d0da7814" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cc2dde8a-a19b-4c94-bbc1-f0007f06cc22",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "65bc4502-dc3b-468f-920d-17bb1a770f19", "AQAAAAEAACcQAAAAELNsmS0iPDf8ZgG3BgLpaOBM7kD8P3LFsD0AjHyPFuqjqxnFI3YZ02v0fWKVQLhptA==", "8b6714a6-a7e4-479c-85eb-f645a35a35da" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "280b32ae-81fe-e6d6-6690-a6d573352b63",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9019c0e3-ff02-4ac7-8cdf-f693f1f40d8b", "AQAAAAEAACcQAAAAEJqNT4RmPGAXW6SZLh8g7o81DblkVfpMMHWj1MZDE05OyKsriaFSGD8GIelJJOS5zw==", "c6cd4f12-a417-4577-82f9-5b00ebf0fafa" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "34fdd202-6cb4-3bea-3537-37d5147dc812",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fce0c3f8-2cae-4a4e-a2d2-3118ab2275c6", "AQAAAAEAACcQAAAAEFjsVJbreF/u3QOcUt7e0eAFRNZLu0r2WxfsBFLbcp7fYNTwgzcfWnlH130979gzrg==", "e8d8c077-3680-4cc2-a67f-b16226713dd9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6693a7cb-9cfa-8619-2627-437d832fb9c6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8bbfed0e-d5fc-4576-a7f8-faddf83a5c3d", "AQAAAAEAACcQAAAAEJ03mw0HFViAAyX0IUXdcv6nyS2hKeyxhA5P679F/gdoe62IXm1fBXOl7hmTi6yWJw==", "d299643f-d251-43fa-8dbd-dcf7345bcf1a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "42aaed06-3f48-4362-b06c-97a49e58aafb", "AQAAAAEAACcQAAAAEGMFATygATj2XiVFyaRG0eVrFN/QJHhRmtBnGg24WXwGrVFnrnu7VNs0KcQwDmXDZA==", "7874d2a2-0004-433e-ac8a-e55781f1d5e4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a4581e92-25c7-ad88-f114-2be0fb5caeb7",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "05a3b6b2-155a-4e65-a0a9-13dc89dc54f0", "AQAAAAEAACcQAAAAENWFAUj8YXskBUPdI31rXa3PUBjzdM1T4GkrqU0C2zl/oytP6k5lv0npmBe8V8+gWA==", "f9ddcd20-7d07-4e31-b65c-21ebfacbfeda" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cc2dde8a-a19b-4c94-bbc1-f0007f06cc22",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "892d7e9a-047e-4d81-96fb-8a43e41f1e95", "AQAAAAEAACcQAAAAEHnvnDsO6VZOgAgSlukbj8M3doorQXt2W8vQxgyj8tKBAE9JLIDpwsb3BCBQWqYLEA==", "a82e88ba-ae11-4e08-a003-207a8151c215" });
        }
    }
}
