using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookify.DAL.Database.Migrations
{
    /// <inheritdoc />
    public partial class ExtendUsersTableAndAddCreatedByIdColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "BookCopies");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "BookCopies");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "BookCopies");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "BookCopies");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Authors");

            migrationBuilder.RenameColumn(
                name: "UpdatedOn",
                table: "Categories",
                newName: "LastUpdatedOn");

            migrationBuilder.RenameColumn(
                name: "UpdatedOn",
                table: "Books",
                newName: "LastUpdatedOn");

            migrationBuilder.RenameColumn(
                name: "UpdatedOn",
                table: "BookCopies",
                newName: "LastUpdatedOn");

            migrationBuilder.RenameColumn(
                name: "UpdatedOn",
                table: "Authors",
                newName: "LastUpdatedOn");

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Categories",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastUpdatedById",
                table: "Categories",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Books",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastUpdatedById",
                table: "Books",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "BookCopies",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastUpdatedById",
                table: "BookCopies",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Authors",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastUpdatedById",
                table: "Authors",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastUpdatedById",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedOn",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CreatedById",
                table: "Categories",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_LastUpdatedById",
                table: "Categories",
                column: "LastUpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Books_CreatedById",
                table: "Books",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Books_LastUpdatedById",
                table: "Books",
                column: "LastUpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_BookCopies_CreatedById",
                table: "BookCopies",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_BookCopies_LastUpdatedById",
                table: "BookCopies",
                column: "LastUpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_CreatedById",
                table: "Authors",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_LastUpdatedById",
                table: "Authors",
                column: "LastUpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_AspNetUsers_CreatedById",
                table: "Authors",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_AspNetUsers_LastUpdatedById",
                table: "Authors",
                column: "LastUpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookCopies_AspNetUsers_CreatedById",
                table: "BookCopies",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookCopies_AspNetUsers_LastUpdatedById",
                table: "BookCopies",
                column: "LastUpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_AspNetUsers_CreatedById",
                table: "Books",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_AspNetUsers_LastUpdatedById",
                table: "Books",
                column: "LastUpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_AspNetUsers_CreatedById",
                table: "Categories",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_AspNetUsers_LastUpdatedById",
                table: "Categories",
                column: "LastUpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_AspNetUsers_CreatedById",
                table: "Authors");

            migrationBuilder.DropForeignKey(
                name: "FK_Authors_AspNetUsers_LastUpdatedById",
                table: "Authors");

            migrationBuilder.DropForeignKey(
                name: "FK_BookCopies_AspNetUsers_CreatedById",
                table: "BookCopies");

            migrationBuilder.DropForeignKey(
                name: "FK_BookCopies_AspNetUsers_LastUpdatedById",
                table: "BookCopies");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_AspNetUsers_CreatedById",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_AspNetUsers_LastUpdatedById",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_AspNetUsers_CreatedById",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_AspNetUsers_LastUpdatedById",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_CreatedById",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_LastUpdatedById",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Books_CreatedById",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_LastUpdatedById",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_BookCopies_CreatedById",
                table: "BookCopies");

            migrationBuilder.DropIndex(
                name: "IX_BookCopies_LastUpdatedById",
                table: "BookCopies");

            migrationBuilder.DropIndex(
                name: "IX_Authors_CreatedById",
                table: "Authors");

            migrationBuilder.DropIndex(
                name: "IX_Authors_LastUpdatedById",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "LastUpdatedById",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "LastUpdatedById",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "BookCopies");

            migrationBuilder.DropColumn(
                name: "LastUpdatedById",
                table: "BookCopies");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "LastUpdatedById",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastUpdatedById",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastUpdatedOn",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "LastUpdatedOn",
                table: "Categories",
                newName: "UpdatedOn");

            migrationBuilder.RenameColumn(
                name: "LastUpdatedOn",
                table: "Books",
                newName: "UpdatedOn");

            migrationBuilder.RenameColumn(
                name: "LastUpdatedOn",
                table: "BookCopies",
                newName: "UpdatedOn");

            migrationBuilder.RenameColumn(
                name: "LastUpdatedOn",
                table: "Authors",
                newName: "UpdatedOn");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Categories",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Books",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "BookCopies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "BookCopies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "BookCopies",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "BookCopies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Authors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Authors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Authors",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Authors",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
