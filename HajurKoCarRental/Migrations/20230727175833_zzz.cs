using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HajurKoCarRental.Migrations
{
    public partial class zzz : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "TotalPrice",
                table: "Requests",
                type: "real",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "280b32ae-81fe-e6d6-6690-a6d573352b63",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d851a86a-0896-4150-9276-9c40a569d748", "AQAAAAEAACcQAAAAEOoa88LrvApqHKrBiUXg8YYjl5GHJm+hb//Z2AgmLkO4qAZdA5dmtMjRUVvt2zuOhg==", "c6fb7e35-2328-48d5-a3a3-d3aded6c837c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "34fdd202-6cb4-3bea-3537-37d5147dc812",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b00abc79-a4a2-4cd4-a689-742d409a47ac", "AQAAAAEAACcQAAAAEG1rQJYRWC2S6II61xjI1yUmds9DUrwc5CP72R/1BV84uCiiiQLQOXeiUcvBoNnZAA==", "7a1cfe45-a6e0-424a-a3ff-e9edd4194e48" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6693a7cb-9cfa-8619-2627-437d832fb9c6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "684b89f2-4c76-456e-8b87-6a18b42b1b30", "AQAAAAEAACcQAAAAEPB4MXymKsopv9i943I7ZgnWg3QQ15Os+BS1k2yWzfPWIQkGLapsfRjzu2fKTOxdBw==", "52745b24-6348-49a4-b446-4694618e43af" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b261b29c-7eba-48ba-9708-ec3d3cfdff90", "AQAAAAEAACcQAAAAECGeek2sophwEVYDWnX0EMKL5/pVhL1o7MRSKSNfaXIh59lmwU6Czw+hpxPNw99ovg==", "404df202-c6b2-479e-aadd-801484f01e20" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a4581e92-25c7-ad88-f114-2be0fb5caeb7",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "edc3267b-6fae-4f42-8707-39a1032e0819", "AQAAAAEAACcQAAAAEGbH89P3DNX7JsXyTUz2ku5gSh/gdbtEZMdkQcNwXveUkTfsejEEGqynjwbQzPqSEQ==", "294f1a66-62bd-4f89-84be-773952df2103" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cc2dde8a-a19b-4c94-bbc1-f0007f06cc22",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dcdd7167-8bf2-4baa-b1be-e267ce51fc28", "AQAAAAEAACcQAAAAECgmsxVNk4bfnJ8Bh13hwMdqiRAD0arhV9+R6Ic6JiuDclxM9icNZAd3FInR8+IqVQ==", "0af10c51-cc2e-4c96-80f2-530849b2cd00" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "TotalPrice",
                table: "Requests",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

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
    }
}
