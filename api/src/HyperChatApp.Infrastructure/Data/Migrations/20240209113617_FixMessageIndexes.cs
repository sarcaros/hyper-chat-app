using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HyperChatApp.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixMessageIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IDX_MESSAGE_ROOMID",
                table: "Messages");

            migrationBuilder.CreateIndex(
                name: "IDX_MESSAGE_ROOMID",
                table: "Messages",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IDX_MESSAGE_TIME_DESC",
                table: "Messages",
                column: "Time",
                descending: new bool[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IDX_MESSAGE_ROOMID",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IDX_MESSAGE_TIME_DESC",
                table: "Messages");

            migrationBuilder.CreateIndex(
                name: "IDX_MESSAGE_ROOMID",
                table: "Messages",
                column: "RoomId",
                unique: true);
        }
    }
}
