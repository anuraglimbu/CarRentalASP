using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HajurKoCarRental.Migrations
{
    public partial class addedDamage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Damages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecordCreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RepairPrice = table.Column<float>(type: "real", nullable: true),
                    Pending = table.Column<int>(type: "int", nullable: true),
                    Reviewed = table.Column<int>(type: "int", nullable: true),
                    Paid = table.Column<int>(type: "int", nullable: true),
                    RequesterId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApproverId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Damages", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "280b32ae-81fe-e6d6-6690-a6d573352b63",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4c5f1106-7e14-4472-ab7a-3c1785353f41", "AQAAAAEAACcQAAAAEJfEOcD8kOpiIwsYDIMLDfWRWbjA+vIWdbxCNJCB9MmghRNAmII7CmNkWkivkQ+tEw==", "0f696f17-a2c4-468c-87f9-d912b3d5220d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "34fdd202-6cb4-3bea-3537-37d5147dc812",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "211decbb-2720-4999-998c-5135b7e615b5", "AQAAAAEAACcQAAAAEJwZbsz3SBk2UyjLszSug0GwChKxhYRqgSg87O1nFU+lRRiP2EsU8RbA8fFaWGi1tQ==", "34bf2a90-f482-4a50-a89c-84efa3dff4d9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6693a7cb-9cfa-8619-2627-437d832fb9c6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e133c489-ab39-40ff-8f6c-88c35e624af6", "AQAAAAEAACcQAAAAEBKdpWHta7anb/mKXFK0+8UJmxI/PiuN6302SVRVlPJjVMQaE88k4N8Brykpu9x7tA==", "da718f96-8e57-4daa-86f4-dbf3e621264c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ca47371d-6dbb-4408-865e-53866d1641f5", "AQAAAAEAACcQAAAAEELKntCVLo3Rv/1nHc/dkPDo12he/BPpGdLi9eXM0gIgd0OPxVHpJcHnoAPrYADgQA==", "0a31919b-c269-4eb7-944b-c79f62d9ef2b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a4581e92-25c7-ad88-f114-2be0fb5caeb7",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "336f4d50-6692-450c-aa20-bf25323dea15", "AQAAAAEAACcQAAAAEJ6JmMpSueVv6HL5gwUJVNbqJswjrRzGkqxeRq7me59SspvW23CX/U7entGT2cqf8Q==", "94539933-9d53-4c10-bd1b-cb213501ee89" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cc2dde8a-a19b-4c94-bbc1-f0007f06cc22",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "de72bf89-7c1c-4bfc-acbd-9986c0f3f08a", "AQAAAAEAACcQAAAAEMJuI0lQb1avKGGlEy6oZC9VdGYjhuqqnGbsejtz/T4cpL0d8S9dFQYPAAbzIiIKyg==", "874037ab-40c8-4790-8120-7304be71ae22" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Damages");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "280b32ae-81fe-e6d6-6690-a6d573352b63",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "31c93c31-bbbc-4b57-9c33-010d121f6aaf", "AQAAAAEAACcQAAAAEELo9p7VTKN/CYOLJ8GSidzS0aBoP2q6hmLiFr6zmR60Gr4FeTsM6wxGESS/6nT7sw==", "78feba8e-edd0-4a4c-afd3-af8196f9f0c9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "34fdd202-6cb4-3bea-3537-37d5147dc812",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d798d9b2-0ca7-493a-b719-550cf84b1810", "AQAAAAEAACcQAAAAEOSlrBoLFWSAWdD57yCF7hL4cmEo2OrjHE28+9eF7APLaXMC0D44faET/2afgbB3ig==", "1d87a8c4-c49c-4d6e-a157-c0f9afe5b571" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6693a7cb-9cfa-8619-2627-437d832fb9c6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b99077cf-2d5a-49ed-9ff1-02f3663bbae7", "AQAAAAEAACcQAAAAELPBNfRmsLBVMGo4U4dqjPaT7ZB0X6V8VK115a0p32a1JkD4Bp1hP2B4DPOuharpXw==", "7b06b469-e820-4d5d-b5af-0cfeff566eba" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e6e94b98-59ed-4184-9b0a-51a60e8a91f2", "AQAAAAEAACcQAAAAEPxDw/5ttJ4Sx7gnKGSD6V8PBNuXFdlmTj8vM2yBACljO+ZgxXd7L9yF3fmdLls3vw==", "f7874ae7-b182-4606-a774-6c66a5f60b1b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a4581e92-25c7-ad88-f114-2be0fb5caeb7",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e50f42fd-6bf8-451d-9686-ebb7c3f0c23d", "AQAAAAEAACcQAAAAECZQhVmpb0sNbAo0EQRXN2yAZT9vYillK0Aks4ZZ9tNtWEWkImPTF3kh4Zx/wcQqDA==", "f0f908f0-29ed-4e90-94e0-5e157f811cf3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cc2dde8a-a19b-4c94-bbc1-f0007f06cc22",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b767d245-a6ee-4d26-abae-a931450ea90f", "AQAAAAEAACcQAAAAEIZiZ0myBOe0bIC6oz4sbNxAxhfBShHPQa7h1IViZdK2ZiWM/NfqBXl0S+ovWV95Ug==", "6ea21986-c3ba-4896-ba8c-446c863020ff" });
        }
    }
}
