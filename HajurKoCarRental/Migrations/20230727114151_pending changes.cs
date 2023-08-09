using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HajurKoCarRental.Migrations
{
    public partial class pendingchanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Inventory",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Inventory",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

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
    }
}
