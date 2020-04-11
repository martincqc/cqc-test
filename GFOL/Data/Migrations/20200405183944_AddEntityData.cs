using System;
using System.Data.SqlTypes;
using System.IO;
using System.Text;
using Microsoft.EntityFrameworkCore.Migrations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GFOL.Data.Migrations
{
    public partial class AddEntityData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var jsonString1 = File.ReadAllText("../../GFOL/GFOL/Content/form-schema.json");
            var jsonString2 = File.ReadAllText("../../GFOL/GFOL/Content/submission-schema.json");
            var sb = new StringBuilder();
            sb.Append("Insert [Schema] Select '" + jsonString1 + "', getdate() ");
            sb.Append("Insert [Schema] Select '" + jsonString2 + "', getdate() ");

            migrationBuilder.Sql(sb.ToString());
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Truncate Table [Schema]");
        }
    }
}
