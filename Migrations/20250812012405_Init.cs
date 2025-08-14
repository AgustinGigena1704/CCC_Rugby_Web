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
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_by = table.Column<int>(type: "int", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    borrado_logico = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_archivo", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "menu",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_by = table.Column<int>(type: "int", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    borrado_logico = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    descripcion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    codigo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_menu", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "menu_grupo",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    descripcion = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    menu_id = table.Column<int>(type: "int", nullable: false),
                    rol_id = table.Column<int>(type: "int", nullable: true),
                    icono = table.Column<string>(type: "longtext", nullable: false, defaultValue: "<path d=\"M0 0h24v24H0z\" fill=\"none\"/><path d=\"M3 13h2v-2H3v2zm0 4h2v-2H3v2zm0-8h2V7H3v2zm4 4h14v-2H7v2zm0 4h14v-2H7v2zM7 7v2h14V7H7z\"/>")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_by = table.Column<int>(type: "int", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    borrado_logico = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_menu_grupo", x => x.id);
                    table.ForeignKey(
                        name: "FK_menu_grupo_menu_menu_id",
                        column: x => x.menu_id,
                        principalTable: "menu",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "menu_item",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    descripcion = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    codigo = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    url = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    menu_grupo_id = table.Column<int>(type: "int", nullable: false),
                    icono = table.Column<string>(type: "longtext", nullable: false, defaultValue: "<path d=\"M0 0h24v24H0z\" fill=\"none\"/><path d=\"M3 13h2v-2H3v2zm0 4h2v-2H3v2zm0-8h2V7H3v2zm4 4h14v-2H7v2zm0 4h14v-2H7v2zM7 7v2h14V7H7z\"/>")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_by = table.Column<int>(type: "int", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    borrado_logico = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_menu_item", x => x.id);
                    table.ForeignKey(
                        name: "FK_menu_item_menu_grupo_menu_grupo_id",
                        column: x => x.menu_grupo_id,
                        principalTable: "menu_grupo",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "permiso",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_by = table.Column<int>(type: "int", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    borrado_logico = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    descripcion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    codigo = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permiso", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "persona",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_by = table.Column<int>(type: "int", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    borrado_logico = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
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
                        .Annotation("MySql:CharSet", "utf8mb4")
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
                    borrado_logico = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false)
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
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_usuario_usuario_updated_by",
                        column: x => x.updated_by,
                        principalTable: "usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
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
                    codigo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_by = table.Column<int>(type: "int", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    borrado_logico = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false)
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
                name: "rol_permiso",
                columns: table => new
                {
                    rol_id = table.Column<int>(type: "int", nullable: false),
                    permiso_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rol_permiso", x => x.rol_id);
                    table.ForeignKey(
                        name: "FK_rol_permiso_permiso_permiso_id",
                        column: x => x.permiso_id,
                        principalTable: "permiso",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_rol_permiso_rol_rol_id",
                        column: x => x.rol_id,
                        principalTable: "rol",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
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
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_menu_created_by",
                table: "menu",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_menu_deleted_by",
                table: "menu",
                column: "deleted_by");

            migrationBuilder.CreateIndex(
                name: "IX_menu_updated_by",
                table: "menu",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "IX_menu_grupo_created_by",
                table: "menu_grupo",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_menu_grupo_deleted_by",
                table: "menu_grupo",
                column: "deleted_by");

            migrationBuilder.CreateIndex(
                name: "IX_menu_grupo_menu_id",
                table: "menu_grupo",
                column: "menu_id");

            migrationBuilder.CreateIndex(
                name: "IX_menu_grupo_rol_id",
                table: "menu_grupo",
                column: "rol_id");

            migrationBuilder.CreateIndex(
                name: "IX_menu_grupo_updated_by",
                table: "menu_grupo",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "IX_menu_item_codigo",
                table: "menu_item",
                column: "codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_menu_item_created_by",
                table: "menu_item",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_menu_item_deleted_by",
                table: "menu_item",
                column: "deleted_by");

            migrationBuilder.CreateIndex(
                name: "IX_menu_item_menu_grupo_id",
                table: "menu_item",
                column: "menu_grupo_id");

            migrationBuilder.CreateIndex(
                name: "IX_menu_item_updated_by",
                table: "menu_item",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "IX_permiso_codigo",
                table: "permiso",
                column: "codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_permiso_created_by",
                table: "permiso",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_permiso_deleted_by",
                table: "permiso",
                column: "deleted_by");

            migrationBuilder.CreateIndex(
                name: "IX_permiso_updated_by",
                table: "permiso",
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
                name: "IX_rol_permiso_permiso_id",
                table: "rol_permiso",
                column: "permiso_id");

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
                name: "FK_menu_usuario_created_by",
                table: "menu",
                column: "created_by",
                principalTable: "usuario",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_menu_usuario_deleted_by",
                table: "menu",
                column: "deleted_by",
                principalTable: "usuario",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_menu_usuario_updated_by",
                table: "menu",
                column: "updated_by",
                principalTable: "usuario",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_menu_grupo_rol_rol_id",
                table: "menu_grupo",
                column: "rol_id",
                principalTable: "rol",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_menu_grupo_usuario_created_by",
                table: "menu_grupo",
                column: "created_by",
                principalTable: "usuario",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_menu_grupo_usuario_deleted_by",
                table: "menu_grupo",
                column: "deleted_by",
                principalTable: "usuario",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_menu_grupo_usuario_updated_by",
                table: "menu_grupo",
                column: "updated_by",
                principalTable: "usuario",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_menu_item_usuario_created_by",
                table: "menu_item",
                column: "created_by",
                principalTable: "usuario",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_menu_item_usuario_deleted_by",
                table: "menu_item",
                column: "deleted_by",
                principalTable: "usuario",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_menu_item_usuario_updated_by",
                table: "menu_item",
                column: "updated_by",
                principalTable: "usuario",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_permiso_usuario_created_by",
                table: "permiso",
                column: "created_by",
                principalTable: "usuario",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_permiso_usuario_deleted_by",
                table: "permiso",
                column: "deleted_by",
                principalTable: "usuario",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_permiso_usuario_updated_by",
                table: "permiso",
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
                name: "menu_item");

            migrationBuilder.DropTable(
                name: "rol_permiso");

            migrationBuilder.DropTable(
                name: "usuario_rol");

            migrationBuilder.DropTable(
                name: "menu_grupo");

            migrationBuilder.DropTable(
                name: "permiso");

            migrationBuilder.DropTable(
                name: "menu");

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
