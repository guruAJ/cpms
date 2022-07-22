using Microsoft.EntityFrameworkCore.Migrations;

namespace CPMS_AUTO.Migrations
{
    public partial class a7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string procedure = @"CREATE procedure [dbo].[GETCOUNT]
                                @Project nvarchar(50),
                                @FromDate datetime,
                                @ToDate datetime
                                AS
                                select  mHrms.Unit,3 as TotalUnit, mSUs.Hrms , mSUs.Iqmp
	                                from mHrms INNER JOIN mSUs ON mHrms.Unit = mSUs.Unit  and mHrms.QueryDate >= @FromDate and mHrms.QueryDate < @ToDate and mHrms.Project = @Project
	                                Group By mHrms.Unit , mSUs.Hrms, mSUs.Iqmp";

            migrationBuilder.Sql(procedure);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string procedure = @"Drop procedure GETCOUNT";
            migrationBuilder.Sql(procedure);
        }
    }
}
