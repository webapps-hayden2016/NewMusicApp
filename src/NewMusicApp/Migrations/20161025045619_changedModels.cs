﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NewMusicApp.Migrations
{
    public partial class changedModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Genres",
                maxLength: 20,
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Artists",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Albums",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Genres",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Artists",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Albums",
                nullable: true);
        }
    }
}
