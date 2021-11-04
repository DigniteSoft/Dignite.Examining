using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dignite.Examining.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExaminingExams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Announcement = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Settings = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuestionSettings = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExaminingExams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExaminingLibraries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExaminingLibraries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExaminingAnswerPapers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ExamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    TakeUpSeconds = table.Column<double>(type: "float", nullable: false),
                    TotalScore = table.Column<float>(type: "real", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExaminingAnswerPapers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExaminingAnswerPapers_ExaminingExams_ExamId",
                        column: x => x.ExamId,
                        principalTable: "ExaminingExams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExaminingExamUsers",
                columns: table => new
                {
                    ExamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExamCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExaminingExamUsers", x => new { x.ExamId, x.UserId });
                    table.ForeignKey(
                        name: "FK_ExaminingExamUsers_ExaminingExams_ExamId",
                        column: x => x.ExamId,
                        principalTable: "ExaminingExams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExaminingQuestions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LibraryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    QuestionTypeProviderName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Analysis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Score = table.Column<float>(type: "real", nullable: true),
                    RightAnswer = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    Configuration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExaminingQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExaminingQuestions_ExaminingLibraries_LibraryId",
                        column: x => x.LibraryId,
                        principalTable: "ExaminingLibraries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExaminingUserAnswers",
                columns: table => new
                {
                    AnswerPaperId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    Score = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExaminingUserAnswers", x => new { x.AnswerPaperId, x.QuestionId });
                    table.ForeignKey(
                        name: "FK_ExaminingUserAnswers_ExaminingAnswerPapers_AnswerPaperId",
                        column: x => x.AnswerPaperId,
                        principalTable: "ExaminingAnswerPapers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExaminingUserAnswers_ExaminingQuestions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "ExaminingQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExaminingWrongAnswers",
                columns: table => new
                {
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExaminingWrongAnswers", x => new { x.CreatorId, x.QuestionId });
                    table.ForeignKey(
                        name: "FK_ExaminingWrongAnswers_ExaminingQuestions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "ExaminingQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExaminingAnswerPapers_ExamId_CreationTime",
                table: "ExaminingAnswerPapers",
                columns: new[] { "ExamId", "CreationTime" });

            migrationBuilder.CreateIndex(
                name: "IX_ExaminingAnswerPapers_ExamId_OrganizationUnitId",
                table: "ExaminingAnswerPapers",
                columns: new[] { "ExamId", "OrganizationUnitId" });

            migrationBuilder.CreateIndex(
                name: "IX_ExaminingAnswerPapers_ExamId_UserId",
                table: "ExaminingAnswerPapers",
                columns: new[] { "ExamId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_ExaminingExams_CreationTime",
                table: "ExaminingExams",
                column: "CreationTime");

            migrationBuilder.CreateIndex(
                name: "IX_ExaminingQuestions_LibraryId",
                table: "ExaminingQuestions",
                column: "LibraryId");

            migrationBuilder.CreateIndex(
                name: "IX_ExaminingUserAnswers_QuestionId",
                table: "ExaminingUserAnswers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_ExaminingWrongAnswers_CreationTime",
                table: "ExaminingWrongAnswers",
                column: "CreationTime");

            migrationBuilder.CreateIndex(
                name: "IX_ExaminingWrongAnswers_QuestionId",
                table: "ExaminingWrongAnswers",
                column: "QuestionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExaminingExamUsers");

            migrationBuilder.DropTable(
                name: "ExaminingUserAnswers");

            migrationBuilder.DropTable(
                name: "ExaminingWrongAnswers");

            migrationBuilder.DropTable(
                name: "ExaminingAnswerPapers");

            migrationBuilder.DropTable(
                name: "ExaminingQuestions");

            migrationBuilder.DropTable(
                name: "ExaminingExams");

            migrationBuilder.DropTable(
                name: "ExaminingLibraries");
        }
    }
}
