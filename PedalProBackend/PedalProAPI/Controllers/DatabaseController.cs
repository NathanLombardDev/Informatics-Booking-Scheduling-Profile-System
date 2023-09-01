using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Timers;
using System;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Hosting.Server;
using static NBitcoin.Scripting.PubKeyProvider;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using PedalProAPI.Models;


namespace PedalProAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DatabaseController : ControllerBase
    {
        private readonly string connectionString = "Server=.\\SQLEXPRESS;Database=PedalPedalPedal;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=true";
        private System.Timers.Timer backupTimer;
        private readonly UserManager<PedalProUser> _userManager;

        public DatabaseController(UserManager<PedalProUser> userManager)
        {
            // Create a timer that triggers the backup every day at midnight
            backupTimer = new System.Timers.Timer
            {
                Interval = CalculateIntervalUntilMidnight(),
                AutoReset = true,
                Enabled = true
            };
            backupTimer.Elapsed += BackupTimerElapsed;

            _userManager = userManager;
        }

        private double CalculateIntervalUntilMidnight()
        {
            var now = DateTime.Now;
            var midnight = now.Date.AddDays(1);
            var interval = (midnight - now).TotalMilliseconds;
            return interval;
        }

        private void BackupTimerElapsed(object sender, ElapsedEventArgs e)
        {
            // Perform the backup logic
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var backupPath = @"C:\Backup\PedalPro\backup.bak";
                   
                    var backupQuery = $"BACKUP DATABASE PedalPedalPedal TO DISK = '{backupPath}'";
                    using (var command = new SqlCommand(backupQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }

                Console.WriteLine("Backup completed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating backup: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("api/database/backup")]
        [Authorize(Roles = "Admin")]
        public IActionResult ManualBackup()
        {
            var username = User.FindFirst(ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(username))
            {
                return BadRequest("Username not found.");
            }

            var user = _userManager.FindByNameAsync(username);

            if (user == null)
            {
                return BadRequest("User not found.");
            }

            var userId = user.Id;

            var userClaims = User.Claims;

            bool hasAdminRole = userClaims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Admin");
            //bool hasEmployeeRole = userClaims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Employee");
            //bool hasClientRole = userClaims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Client");

            if (!hasAdminRole)
            {
                return Forbid("You do not have the necessary role to perform this action.");
            }

            try
            {
                BackupTimerElapsed(null, null); // Call the backup logic immediately
                return Ok("Manual backup initiated.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error during manual backup: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("api/database/restore")]
        public IActionResult RestoreDatabase()
        {
            try
            {
                
                var backupPath = @"C:\Backup\PedalPro\backup.bak";

                // Close existing connections to the target database
                CloseConnectionsToTargetDatabase();

                // Restore database from backup
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var restoreQuery = $"RESTORE DATABASE PedalPedalPedal FROM DISK = '{backupPath}' WITH REPLACE;";
                    using (var command = new SqlCommand(restoreQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }

                return Ok("Database restored successfully.");
            }
            catch
            {
                return BadRequest("hello");
            }
        }

        private void CloseConnectionsToTargetDatabase()
        {
            // Close existing connections to the target database before restoring
            // This step is important to ensure that the database is not in use during restoration
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var killConnectionsQuery = @"
            DECLARE @kill varchar(8000) = '';
            SELECT @kill = @kill + 'KILL ' + CONVERT(varchar(5), session_id) + ';'
            FROM sys.dm_exec_requests
            WHERE database_id = DB_ID('PedalPedalPedal')
            EXEC(@kill);";
                using (var command = new SqlCommand(killConnectionsQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        [HttpPost]
        [Route("api/database/restoretwo")]
        public IActionResult RestoreDatabasetwo()
        {
            try
            {
                var backupPath = @"C:\Backup\PedalPro\backup.bak";
                var restoreDatabaseName = "PedalPedalPedal";
                var dataFileLogicalName = "PedalPedalPedal";
                var logFileLogicalName = "PedalPedalPedal_log";

                // Close existing connections to the original database
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var closeConnectionsQuery = $"ALTER DATABASE [{restoreDatabaseName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE";
                    using (var command = new SqlCommand(closeConnectionsQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }

                // Perform the restore with the WITH MOVE option
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var restoreQuery = $@"
                RESTORE DATABASE [{restoreDatabaseName}]
                FROM DISK = '{backupPath}'
                WITH REPLACE,
                MOVE '{dataFileLogicalName}' TO 'NewPathForDataFile.mdf',
                MOVE '{logFileLogicalName}' TO 'NewPathForLogFile.ldf';";

                    using (var command = new SqlCommand(restoreQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }

                // Set the original database back to multi-user mode
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var multiUserQuery = $"ALTER DATABASE [{restoreDatabaseName}] SET MULTI_USER";
                    using (var command = new SqlCommand(multiUserQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }

                return Ok("Database restored successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}

