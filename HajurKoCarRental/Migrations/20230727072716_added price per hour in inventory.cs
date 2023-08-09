using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HajurKoCarRental.Migrations
{
    public partial class addedpriceperhourininventory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReviewerId",
                table: "Inventory",
                newName: "InserterId");

            migrationBuilder.AddColumn<float>(
                name: "PricePerDay",
                table: "Inventory",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "280b32ae-81fe-e6d6-6690-a6d573352b63",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d605e1f7-46a9-4c4a-b87f-3561ac198ec9", "AQAAAAEAACcQAAAAEOMHGdSsp8jutR4uybxe4Yzr7eqlF6wKK2uJWePb2Ge0wEyofyuwBabeJCDNre2hLQ==", "e06cd31f-1053-4555-8647-a7c130fc537f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "34fdd202-6cb4-3bea-3537-37d5147dc812",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c6398357-cb97-4fc2-aaba-9433d2868933", "AQAAAAEAACcQAAAAEFBtJoQ9WwGIzFhJSoNiWFJ+L2md2CmKvVo9XPLTyeHYTA0mui1LsFRPKvFKQ0W9fg==", "423cd6dd-3fb5-4527-a602-d040adc2df39" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6693a7cb-9cfa-8619-2627-437d832fb9c6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dc131892-9ae2-41e9-b59e-23e5674056a9", "AQAAAAEAACcQAAAAEC5/EQphs3p2RDpbAKc/wsrqAPGg0vN6obfeOsLADbYyuGH9Y6DpXN36NcnzY9LbHg==", "dc44f370-4d64-4a23-b953-b9521df2000b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "359ae7a5-4cca-4be6-b98f-885d115c1ef2", "AQAAAAEAACcQAAAAEDpdSGvITXjuGz1ixalwum0PNmXKWsBjkZZ4fBnKC+ZzGvJbgpyZUujAtOXzQf6+uA==", "99b47773-4e8f-4f33-be8a-3ede27ae1ee4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a4581e92-25c7-ad88-f114-2be0fb5caeb7",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8a9764d8-0555-4bd9-a934-fd741e11ba7d", "AQAAAAEAACcQAAAAEOjkkFBKBsoz0l9LaOyYkYoq5mFgbPmnIeChKzdv8jYPf9YKz83kK0ExhORT1xsN+A==", "e3921d8f-a540-473b-86a7-698a4dceb84a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cc2dde8a-a19b-4c94-bbc1-f0007f06cc22",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7f3ff5c8-1aa7-4ae4-9466-92f3c101ab1e", "AQAAAAEAACcQAAAAECzNWl8ra9OHe8sJzt9VMWx+jr2c8TupeP4K8YKJ6tKKZO3Gbb8C9AH4pQKHTpCMPw==", "14a16e58-e2db-4113-bd72-677e08426a8b" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PricePerDay",
                table: "Inventory");

            migrationBuilder.RenameColumn(
                name: "InserterId",
                table: "Inventory",
                newName: "ReviewerId");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "280b32ae-81fe-e6d6-6690-a6d573352b63",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c82c5238-79f1-40cd-87ca-7a85a5bfafcb", "AQAAAAEAACcQAAAAENAe6yvg/yrMXmZaOli7TC19n78lb8oXcw7l0hDY27sF8kaH5tKppcvonIN1AsTRqg==", "467264f4-37df-42aa-a2f6-c654123bba90" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "34fdd202-6cb4-3bea-3537-37d5147dc812",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b951a6a1-4a11-4999-aa7d-c66164eae329", "AQAAAAEAACcQAAAAEBmQIs+Yed1wXPm4Hp7aS3JoZqI6qN+8ddZOrh14IdFNH1+vuFvwdptWHoUBgdZcEA==", "8b702fd8-e182-4d92-87e0-6d884d290889" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6693a7cb-9cfa-8619-2627-437d832fb9c6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ad1d6d1f-770f-4746-8283-be24c393e490", "AQAAAAEAACcQAAAAEM9mMYArGpke0nIamxfFkjgvUn0i4bV9iLJUF6YZJeYyZ7nCyGcfyZFm9f5B+AqH/g==", "5c9c9613-94aa-43a7-bf37-dd614351eec1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d43aee95-2e03-4341-9538-f22f3896aba4", "AQAAAAEAACcQAAAAEDCPW3d2oGTiPGber0rwIoWGmAIU/SY8azB/s4rZtJ2pQ8W03UAI2DJaOQa2087yPg==", "9ef3aa3c-9e15-4b75-a0cb-225155899e6e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a4581e92-25c7-ad88-f114-2be0fb5caeb7",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a0e5824a-03eb-4bfe-9423-2e3f7ac9c588", "AQAAAAEAACcQAAAAEKs+jmXVqWL7SJERqfmkIRu/7UHVs0knhaOZynArKSh4xWWviyPWm+bHS5X60fOc4Q==", "44f075de-8a0b-4500-8d63-ee87d8178993" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cc2dde8a-a19b-4c94-bbc1-f0007f06cc22",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a3ca8d30-6120-427d-8d1e-aee73a3bf29c", "AQAAAAEAACcQAAAAEFeSOla/31rmJzo8EMnfieemMV1WAKQd6EwBOB7VQlyJy/SAmvXJ7yRlDLUr8wPd6Q==", "8de13abe-c6ea-42e3-8884-6a36ba5e70dd" });
        }
    }
}
