using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace E_Diary.Infrastructure.Persistence.Migrations
{
    public partial class Initial_DatabaseModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TGroup",
                columns: table => new
                {
                    GroupId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_t_group", x => x.GroupId);
                });

            migrationBuilder.CreateTable(
                name: "TSubject",
                columns: table => new
                {
                    SubjectId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_t_subject", x => x.SubjectId);
                });

            migrationBuilder.CreateTable(
                name: "TSchooler",
                columns: table => new
                {
                    SchoolerId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    MiddleName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LastName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    group_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_t_schooler", x => x.SchoolerId);
                    table.ForeignKey(
                        name: "fk_t_schooler_t_group_group_id",
                        column: x => x.group_id,
                        principalTable: "TGroup",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TSchoolerMark",
                columns: table => new
                {
                    SchoolerMarkId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    schooler_id = table.Column<int>(type: "integer", nullable: false),
                    subject_id = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Score = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_t_schooler_mark", x => x.SchoolerMarkId);
                    table.ForeignKey(
                        name: "fk_t_schooler_mark_t_schooler_schooler_id",
                        column: x => x.schooler_id,
                        principalTable: "TSchooler",
                        principalColumn: "SchoolerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_t_schooler_mark_t_subject_subject_id",
                        column: x => x.subject_id,
                        principalTable: "TSubject",
                        principalColumn: "SubjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_t_schooler_group_id",
                table: "TSchooler",
                column: "group_id");

            migrationBuilder.CreateIndex(
                name: "ix_t_schooler_mark_schooler_id",
                table: "TSchoolerMark",
                column: "schooler_id");

            migrationBuilder.CreateIndex(
                name: "ix_t_schooler_mark_subject_id",
                table: "TSchoolerMark",
                column: "subject_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TSchoolerMark");

            migrationBuilder.DropTable(
                name: "TSchooler");

            migrationBuilder.DropTable(
                name: "TSubject");

            migrationBuilder.DropTable(
                name: "TGroup");
        }
    }
}
