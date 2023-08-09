using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HajurKoCarRental.Migrations
{
    public partial class addedtransmissiontypetotheInventory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Transmission",
                table: "Inventory",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "280b32ae-81fe-e6d6-6690-a6d573352b63",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "20bad47e-96c2-4eb6-8d30-5f5a249de829", "AQAAAAEAACcQAAAAEAjbFgaeipmmdqrOj1QW6Ug9YJPDXOzhDbbYacyMWXEM4jijIrav35FXLTWRMkNzQA==", "81b85a26-e5cf-474e-9da2-84ee042308d1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "34fdd202-6cb4-3bea-3537-37d5147dc812",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d1fe2965-e61f-45ba-bdac-03891f388ef1", "AQAAAAEAACcQAAAAEGECu60uD9QgDoB+GmzMy/QJHMJlffZwIJ5czqSLYWk5b0Xd2vjKiV8P6tCC1Y5zTQ==", "4d3f9c14-461e-4849-8e7f-5041d59d3f0c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6693a7cb-9cfa-8619-2627-437d832fb9c6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2685d3a4-9a87-414a-9577-e24e9e333171", "AQAAAAEAACcQAAAAEDivmVWVylu79p+ZGcnNwL6MSfVSvvDYLkiYcXU+VETnPl/3nGYwXt4BWKI+fb4d9g==", "bb600feb-ec30-4885-a2d2-eb74c1a7e98b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8b7fa348-4dc5-41b4-9c35-6ce786df445b", "AQAAAAEAACcQAAAAEMCSQUkghAXWguIFERyqIVMdwylmrDP3BKkN+5jELRzOrBOYhRXDay5LUhg2iqlIjA==", "731db51d-e4bd-4829-badf-2647162e1d1a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a4581e92-25c7-ad88-f114-2be0fb5caeb7",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "49cd4dc6-8d65-4277-9a75-621b8eb9a45b", "AQAAAAEAACcQAAAAEH7fdpj083jCMVKPf5F/oOvurXL5qG4usPn7lItSI9TCSDSqsGCZ1XTEdxqWf30nig==", "cd0dc905-7e8f-49a7-a9ea-7943d8e07016" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cc2dde8a-a19b-4c94-bbc1-f0007f06cc22",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b41499f5-ad47-4d20-9df0-9d4b3f0fdca1", "AQAAAAEAACcQAAAAEKCf9XUkPiZVmaE+Dmi8QG/QOG0aZnqyHUhoKROc1rPqQYpsckmZMpHrdi1HFm2gyQ==", "e51863d1-d085-4ab1-b352-04c7650c1120" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Transmission",
                table: "Inventory");

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
    }
}
