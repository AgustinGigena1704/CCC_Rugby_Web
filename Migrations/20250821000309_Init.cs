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
                name: "articulos",
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
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    tipo_articulo_id = table.Column<int>(type: "int", nullable: false),
                    precio = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    archivo_imagen_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_articulos", x => x.id);
                    table.ForeignKey(
                        name: "FK_articulos_archivo_archivo_imagen_id",
                        column: x => x.archivo_imagen_id,
                        principalTable: "archivo",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "menu",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    descripcion = table.Column<string>(type: "longtext", nullable: true)
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
                name: "pedido",
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
                    usuario_id = table.Column<int>(type: "int", nullable: false),
                    fecha = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EstadoId = table.Column<int>(type: "int", nullable: false),
                    tipo_pago_id = table.Column<int>(type: "int", nullable: true),
                    nombre_comprador = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    direccion_entrega = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pedido", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "pedido_articulos",
                columns: table => new
                {
                    ArticulosId = table.Column<int>(type: "int", nullable: false),
                    PedidosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pedido_articulos", x => new { x.ArticulosId, x.PedidosId });
                    table.ForeignKey(
                        name: "FK_pedido_articulos_articulos_ArticulosId",
                        column: x => x.ArticulosId,
                        principalTable: "articulos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_pedido_articulos_pedido_PedidosId",
                        column: x => x.PedidosId,
                        principalTable: "pedido",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "pedido_estado",
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
                    codigo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pedido_estado", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "permiso",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    descripcion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    codigo = table.Column<string>(type: "varchar(255)", nullable: false)
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
                    table.PrimaryKey("PK_permiso", x => x.id);
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
                name: "tipo_articulo",
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
                    table.PrimaryKey("PK_tipo_articulo", x => x.id);
                    table.ForeignKey(
                        name: "FK_tipo_articulo_usuario_created_by",
                        column: x => x.created_by,
                        principalTable: "usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tipo_articulo_usuario_deleted_by",
                        column: x => x.deleted_by,
                        principalTable: "usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_tipo_articulo_usuario_updated_by",
                        column: x => x.updated_by,
                        principalTable: "usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tipo_pago",
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
                    table.PrimaryKey("PK_tipo_pago", x => x.id);
                    table.ForeignKey(
                        name: "FK_tipo_pago_usuario_created_by",
                        column: x => x.created_by,
                        principalTable: "usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tipo_pago_usuario_deleted_by",
                        column: x => x.deleted_by,
                        principalTable: "usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_tipo_pago_usuario_updated_by",
                        column: x => x.updated_by,
                        principalTable: "usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "rol_permisos",
                columns: table => new
                {
                    PermisosId = table.Column<int>(type: "int", nullable: false),
                    RolesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rol_permisos", x => new { x.PermisosId, x.RolesId });
                    table.ForeignKey(
                        name: "FK_rol_permisos_permiso_PermisosId",
                        column: x => x.PermisosId,
                        principalTable: "permiso",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_rol_permisos_rol_RolesId",
                        column: x => x.RolesId,
                        principalTable: "rol",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "usuario_roles",
                columns: table => new
                {
                    RolesId = table.Column<int>(type: "int", nullable: false),
                    UsuariosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuario_roles", x => new { x.RolesId, x.UsuariosId });
                    table.ForeignKey(
                        name: "FK_usuario_roles_rol_RolesId",
                        column: x => x.RolesId,
                        principalTable: "rol",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_usuario_roles_usuario_UsuariosId",
                        column: x => x.UsuariosId,
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
                name: "IX_articulos_archivo_imagen_id",
                table: "articulos",
                column: "archivo_imagen_id");

            migrationBuilder.CreateIndex(
                name: "IX_articulos_created_by",
                table: "articulos",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_articulos_deleted_by",
                table: "articulos",
                column: "deleted_by");

            migrationBuilder.CreateIndex(
                name: "IX_articulos_tipo_articulo_id",
                table: "articulos",
                column: "tipo_articulo_id");

            migrationBuilder.CreateIndex(
                name: "IX_articulos_updated_by",
                table: "articulos",
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
                name: "IX_pedido_created_by",
                table: "pedido",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_pedido_deleted_by",
                table: "pedido",
                column: "deleted_by");

            migrationBuilder.CreateIndex(
                name: "IX_pedido_EstadoId",
                table: "pedido",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_pedido_tipo_pago_id",
                table: "pedido",
                column: "tipo_pago_id");

            migrationBuilder.CreateIndex(
                name: "IX_pedido_updated_by",
                table: "pedido",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "IX_pedido_usuario_id",
                table: "pedido",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "IX_pedido_articulos_PedidosId",
                table: "pedido_articulos",
                column: "PedidosId");

            migrationBuilder.CreateIndex(
                name: "IX_pedido_estado_created_by",
                table: "pedido_estado",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_pedido_estado_deleted_by",
                table: "pedido_estado",
                column: "deleted_by");

            migrationBuilder.CreateIndex(
                name: "IX_pedido_estado_updated_by",
                table: "pedido_estado",
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
                name: "IX_rol_permisos_RolesId",
                table: "rol_permisos",
                column: "RolesId");

            migrationBuilder.CreateIndex(
                name: "IX_tipo_articulo_created_by",
                table: "tipo_articulo",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_tipo_articulo_deleted_by",
                table: "tipo_articulo",
                column: "deleted_by");

            migrationBuilder.CreateIndex(
                name: "IX_tipo_articulo_updated_by",
                table: "tipo_articulo",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "IX_tipo_pago_created_by",
                table: "tipo_pago",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_tipo_pago_deleted_by",
                table: "tipo_pago",
                column: "deleted_by");

            migrationBuilder.CreateIndex(
                name: "IX_tipo_pago_updated_by",
                table: "tipo_pago",
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
                name: "IX_usuario_roles_UsuariosId",
                table: "usuario_roles",
                column: "UsuariosId");

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
                name: "FK_articulos_tipo_articulo_tipo_articulo_id",
                table: "articulos",
                column: "tipo_articulo_id",
                principalTable: "tipo_articulo",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_articulos_usuario_created_by",
                table: "articulos",
                column: "created_by",
                principalTable: "usuario",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_articulos_usuario_deleted_by",
                table: "articulos",
                column: "deleted_by",
                principalTable: "usuario",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_articulos_usuario_updated_by",
                table: "articulos",
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
                name: "FK_pedido_pedido_estado_EstadoId",
                table: "pedido",
                column: "EstadoId",
                principalTable: "pedido_estado",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_pedido_tipo_pago_tipo_pago_id",
                table: "pedido",
                column: "tipo_pago_id",
                principalTable: "tipo_pago",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_pedido_usuario_created_by",
                table: "pedido",
                column: "created_by",
                principalTable: "usuario",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_pedido_usuario_deleted_by",
                table: "pedido",
                column: "deleted_by",
                principalTable: "usuario",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_pedido_usuario_updated_by",
                table: "pedido",
                column: "updated_by",
                principalTable: "usuario",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_pedido_usuario_usuario_id",
                table: "pedido",
                column: "usuario_id",
                principalTable: "usuario",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_pedido_estado_usuario_created_by",
                table: "pedido_estado",
                column: "created_by",
                principalTable: "usuario",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_pedido_estado_usuario_deleted_by",
                table: "pedido_estado",
                column: "deleted_by",
                principalTable: "usuario",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_pedido_estado_usuario_updated_by",
                table: "pedido_estado",
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
                name: "pedido_articulos");

            migrationBuilder.DropTable(
                name: "rol_permisos");

            migrationBuilder.DropTable(
                name: "usuario_roles");

            migrationBuilder.DropTable(
                name: "menu_grupo");

            migrationBuilder.DropTable(
                name: "articulos");

            migrationBuilder.DropTable(
                name: "pedido");

            migrationBuilder.DropTable(
                name: "permiso");

            migrationBuilder.DropTable(
                name: "menu");

            migrationBuilder.DropTable(
                name: "rol");

            migrationBuilder.DropTable(
                name: "tipo_articulo");

            migrationBuilder.DropTable(
                name: "pedido_estado");

            migrationBuilder.DropTable(
                name: "tipo_pago");

            migrationBuilder.DropTable(
                name: "usuario");

            migrationBuilder.DropTable(
                name: "archivo");

            migrationBuilder.DropTable(
                name: "persona");
        }
    }
}
