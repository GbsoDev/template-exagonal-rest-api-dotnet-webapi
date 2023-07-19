using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GbsoDevExagonalTemplate.Data.EfCore.Migrations
{
	/// <inheritdoc />
	public partial class InitialMigration : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Users",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
					UserName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
					Password = table.Column<string>(type: "CHAR(64)", maxLength: 64, nullable: false),
					Enabled = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
					CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 7, 19, 12, 0, 17, 521, DateTimeKind.Local).AddTicks(5168)),
					UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Users", x => x.Id);
				});

			migrationBuilder.CreateIndex(
				name: "IX_Users_UserName",
				table: "Users",
				column: "UserName",
				unique: true);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "Users");
		}
	}
}
