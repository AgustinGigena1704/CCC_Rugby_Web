using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CCC_Rugby_Web.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "archivo",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    type = table.Column<int>(type: "int", nullable: false),
                    extension = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    base64 = table.Column<byte[]>(type: "blob", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_by = table.Column<int>(type: "int", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    borrado_logico = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_archivo", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "persona",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    tipo_documento = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    documento = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nombres = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    apellidos = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fecha_nacimiento = table.Column<DateOnly>(type: "date", nullable: false),
                    genero = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_by = table.Column<int>(type: "int", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    borrado_logico = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_persona", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "usuario",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    username = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    persona_id = table.Column<int>(type: "int", nullable: true),
                    avatar_archivo_id = table.Column<int>(type: "int", nullable: true),
                    last_login = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    login_trys = table.Column<int>(type: "int", nullable: false),
                    bloqueado = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_by = table.Column<int>(type: "int", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    borrado_logico = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuario", x => x.id);
                    table.ForeignKey(
                        name: "FK_usuario_archivo_avatar_archivo_id",
                        column: x => x.avatar_archivo_id,
                        principalTable: "archivo",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_usuario_persona_persona_id",
                        column: x => x.persona_id,
                        principalTable: "persona",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_usuario_usuario_deleted_by",
                        column: x => x.deleted_by,
                        principalTable: "usuario",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_usuario_usuario_updated_by",
                        column: x => x.updated_by,
                        principalTable: "usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "rol",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    descripcion = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_by = table.Column<int>(type: "int", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    borrado_logico = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rol", x => x.id);
                    table.ForeignKey(
                        name: "FK_rol_usuario_created_by",
                        column: x => x.created_by,
                        principalTable: "usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_rol_usuario_deleted_by",
                        column: x => x.deleted_by,
                        principalTable: "usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_rol_usuario_updated_by",
                        column: x => x.updated_by,
                        principalTable: "usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "usuario_rol",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuario_rol", x => x.UsuarioId);
                    table.ForeignKey(
                        name: "FK_usuario_rol_rol_RoleId",
                        column: x => x.RoleId,
                        principalTable: "rol",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_usuario_rol_usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_archivo_created_by",
                table: "archivo",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_archivo_deleted_by",
                table: "archivo",
                column: "deleted_by");

            migrationBuilder.CreateIndex(
                name: "IX_archivo_updated_by",
                table: "archivo",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "IX_persona_created_by",
                table: "persona",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_persona_deleted_by",
                table: "persona",
                column: "deleted_by");

            migrationBuilder.CreateIndex(
                name: "IX_persona_tipo_documento_documento",
                table: "persona",
                columns: new[] { "tipo_documento", "documento" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_persona_updated_by",
                table: "persona",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "IX_rol_created_by",
                table: "rol",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_rol_deleted_by",
                table: "rol",
                column: "deleted_by");

            migrationBuilder.CreateIndex(
                name: "IX_rol_updated_by",
                table: "rol",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "IX_usuario_avatar_archivo_id",
                table: "usuario",
                column: "avatar_archivo_id");

            migrationBuilder.CreateIndex(
                name: "IX_usuario_deleted_by",
                table: "usuario",
                column: "deleted_by");

            migrationBuilder.CreateIndex(
                name: "IX_usuario_persona_id",
                table: "usuario",
                column: "persona_id");

            migrationBuilder.CreateIndex(
                name: "IX_usuario_updated_by",
                table: "usuario",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "IX_usuario_username",
                table: "usuario",
                column: "username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_usuario_rol_RoleId",
                table: "usuario_rol",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_archivo_usuario_created_by",
                table: "archivo",
                column: "created_by",
                principalTable: "usuario",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_archivo_usuario_deleted_by",
                table: "archivo",
                column: "deleted_by",
                principalTable: "usuario",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_archivo_usuario_updated_by",
                table: "archivo",
                column: "updated_by",
                principalTable: "usuario",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_persona_usuario_created_by",
                table: "persona",
                column: "created_by",
                principalTable: "usuario",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_persona_usuario_deleted_by",
                table: "persona",
                column: "deleted_by",
                principalTable: "usuario",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_persona_usuario_updated_by",
                table: "persona",
                column: "updated_by",
                principalTable: "usuario",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_archivo_usuario_created_by",
                table: "archivo");

            migrationBuilder.DropForeignKey(
                name: "FK_archivo_usuario_deleted_by",
                table: "archivo");

            migrationBuilder.DropForeignKey(
                name: "FK_archivo_usuario_updated_by",
                table: "archivo");

            migrationBuilder.DropForeignKey(
                name: "FK_persona_usuario_created_by",
                table: "persona");

            migrationBuilder.DropForeignKey(
                name: "FK_persona_usuario_deleted_by",
                table: "persona");

            migrationBuilder.DropForeignKey(
                name: "FK_persona_usuario_updated_by",
                table: "persona");

            migrationBuilder.DropTable(
                name: "usuario_rol");

            migrationBuilder.DropTable(
                name: "rol");

            migrationBuilder.DropTable(
                name: "usuario");

            migrationBuilder.DropTable(
                name: "archivo");

            migrationBuilder.DropTable(
                name: "persona");
        }
    }
}
