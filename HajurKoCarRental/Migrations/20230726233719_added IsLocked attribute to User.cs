using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HajurKoCarRental.Migrations
{
    public partial class addedIsLockedattributetoUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLocked",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "280b32ae-81fe-e6d6-6690-a6d573352b63",
                columns: new[] { "ConcurrencyStamp", "IsLocked", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c82c5238-79f1-40cd-87ca-7a85a5bfafcb", false, "AQAAAAEAACcQAAAAENAe6yvg/yrMXmZaOli7TC19n78lb8oXcw7l0hDY27sF8kaH5tKppcvonIN1AsTRqg==", "467264f4-37df-42aa-a2f6-c654123bba90" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "34fdd202-6cb4-3bea-3537-37d5147dc812",
                columns: new[] { "ConcurrencyStamp", "IsLocked", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b951a6a1-4a11-4999-aa7d-c66164eae329", false, "AQAAAAEAACcQAAAAEBmQIs+Yed1wXPm4Hp7aS3JoZqI6qN+8ddZOrh14IdFNH1+vuFvwdptWHoUBgdZcEA==", "8b702fd8-e182-4d92-87e0-6d884d290889" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6693a7cb-9cfa-8619-2627-437d832fb9c6",
                columns: new[] { "ConcurrencyStamp", "IsLocked", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ad1d6d1f-770f-4746-8283-be24c393e490", false, "AQAAAAEAACcQAAAAEM9mMYArGpke0nIamxfFkjgvUn0i4bV9iLJUF6YZJeYyZ7nCyGcfyZFm9f5B+AqH/g==", "5c9c9613-94aa-43a7-bf37-dd614351eec1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "IsLocked", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d43aee95-2e03-4341-9538-f22f3896aba4", false, "AQAAAAEAACcQAAAAEDCPW3d2oGTiPGber0rwIoWGmAIU/SY8azB/s4rZtJ2pQ8W03UAI2DJaOQa2087yPg==", "9ef3aa3c-9e15-4b75-a0cb-225155899e6e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a4581e92-25c7-ad88-f114-2be0fb5caeb7",
                columns: new[] { "ConcurrencyStamp", "IsLocked", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a0e5824a-03eb-4bfe-9423-2e3f7ac9c588", false, "AQAAAAEAACcQAAAAEKs+jmXVqWL7SJERqfmkIRu/7UHVs0knhaOZynArKSh4xWWviyPWm+bHS5X60fOc4Q==", "44f075de-8a0b-4500-8d63-ee87d8178993" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cc2dde8a-a19b-4c94-bbc1-f0007f06cc22",
                columns: new[] { "ConcurrencyStamp", "IsLocked", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a3ca8d30-6120-427d-8d1e-aee73a3bf29c", false, "AQAAAAEAACcQAAAAEFeSOla/31rmJzo8EMnfieemMV1WAKQd6EwBOB7VQlyJy/SAmvXJ7yRlDLUr8wPd6Q==", "8de13abe-c6ea-42e3-8884-6a36ba5e70dd" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLocked",
                table: "AspNetUsers");

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
    }
}
