using Umbraco.Core;
using Umbraco.Core.Logging;
using Umbraco.Core.Composing;
using Umbraco.Core.Migrations;
using Umbraco.Core.Migrations.Upgrade;
using Umbraco.Core.Scoping;
using Umbraco.Core.Services;
using NPoco;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace Cabana.DB
{
    [RuntimeLevel(MinLevel = RuntimeLevel.Run)]
    public class FKComposer : ComponentComposer<FKComponent>
    {
    }

    public class FKComponent : IComponent
    {
        private IScopeProvider _scopeProvider;
        private IMigrationBuilder _migrationBuilder;
        private IKeyValueService _keyValueService;
        private ILogger _logger;

        public FKComponent(IScopeProvider scopeProvider, IMigrationBuilder migrationBuilder, IKeyValueService keyValueService, ILogger logger)
        {
            _scopeProvider = scopeProvider;
            _migrationBuilder = migrationBuilder;
            _keyValueService = keyValueService;
            _logger = logger;
        }

        public void Initialize()
        {
            // Create a migration plan for a specific project/feature
            // We can then track that latest migration state/step for this project/feature
            var migrationPlan = new MigrationPlan("UpdateFK");

            // This is the steps we need to take
            // Each step in the migration adds a unique value
            migrationPlan.From(string.Empty)
                .To<UpdateFK>("fk-db");

            // Go and upgrade our site (Will check if it needs to do the work or not)
            // Based on the current/latest step
            var upgrader = new Upgrader(migrationPlan);
            upgrader.Execute(_scopeProvider, _migrationBuilder, _keyValueService, _logger);
        }

        public void Terminate()
        {
        }
    }

    public class UpdateFK : MigrationBase
    {
        public UpdateFK(IMigrationContext context) : base(context)
        {
        }

        public override void Migrate()
        {
            Logger.Debug<UpdateFK>("Running migration {MigrationStep}", "UpdateFK");

            // Lots of methods available in the MigrationBase class - discover with this.
            if (TableExists("Movie"))
            {
                Execute.Sql(
                "ALTER TABLE Movie ADD " +
                    "CONSTRAINT FK_Movie_MyUser FOREIGN KEY (MyUserId) " +
                    "REFERENCES MyUser(Id)"
                ).Do();
            }
            else
            {
                Logger.Debug<UpdateFK>("The database table {DbTable} already exists, skipping", "UpdateFK");
            }
        }
    }
}