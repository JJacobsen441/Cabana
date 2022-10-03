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
    [ComposeBefore(typeof(MovieComposer))]
    [RuntimeLevel(MinLevel = RuntimeLevel.Run)]
    public class MyUserComposer : ComponentComposer<MyUserComponent>
    {
    }

    public class MyUserComponent : IComponent
    {
        private IScopeProvider _scopeProvider;
        private IMigrationBuilder _migrationBuilder;
        private IKeyValueService _keyValueService;
        private ILogger _logger;

        public MyUserComponent(IScopeProvider scopeProvider, IMigrationBuilder migrationBuilder, IKeyValueService keyValueService, ILogger logger)
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
            var migrationPlan = new MigrationPlan("Init");

            // This is the steps we need to take
            // Each step in the migration adds a unique value
            migrationPlan.From(string.Empty)
                .To<AddMyUserTable>("myuser-db");

            migrationPlan.From("myuser-db")
                .To<AddColumn>("myuser-addcolumn-db");

            migrationPlan.From("myuser-addcolumn-db")
                .To<AddColumn>("myuser-addcolumnagain-db");

            migrationPlan.From("myuser-addcolumnagain-db")
                .To<AddColumnAgain3>("myuser-addcolumnagain3-db");

            // Go and upgrade our site (Will check if it needs to do the work or not)
            // Based on the current/latest step
            var upgrader = new Upgrader(migrationPlan);
            upgrader.Execute(_scopeProvider, _migrationBuilder, _keyValueService, _logger);
        }

        public void Terminate()
        {
        }
    }

    public class AddMyUserTable : MigrationBase
    {
        public AddMyUserTable(IMigrationContext context) : base(context)
        {
        }

        public override void Migrate()
        {
            Logger.Debug<AddMyUserTable>("Running migration {MigrationStep}", "AddMyUserTable");

            // Lots of methods available in the MigrationBase class - discover with this.
            if (!TableExists("MyUser"))
            {
                Create.Table<MyUserSchema>().Do();
            }
            else
            {
                Logger.Debug<AddMyUserTable>("The database table {DbTable} already exists, skipping", "MyUser");
            }
        }

        [TableName("MyUser")]
        [PrimaryKey("Id", AutoIncrement = true)]
        [ExplicitColumns]
        public class MyUserSchema
        {
            [PrimaryKeyColumn(AutoIncrement = true, IdentitySeed = 1)]
            [Column("Id")]
            public long Id { get; set; }

            [Column("Name")]
            public string Name { get; set; }

        }
    }

    public class AddColumn : MigrationBase
    {
        public AddColumn(IMigrationContext context) : base(context)
        {
        }

        public override void Migrate()
        {
            Logger.Debug<AddMyUserTable>("Running migration {MigrationStep}", "AddColumn");

            // Lots of methods available in the MigrationBase class - discover with this.
            if (!ColumnExists("MyUser", "UmbID"))
            {
                //Create.Column("UmbId").OnTable("MyUser").AsInt32().Do();
            }
            else
            {
                Logger.Debug<AddMyUserTable>("The column in {DbTable} already exists, skipping", "MyUser");
            }
        }
    }

    public class AddColumnAgain : MigrationBase
    {
        public AddColumnAgain(IMigrationContext context) : base(context)
        {
        }

        public override void Migrate()
        {
            Logger.Debug<AddMyUserTable>("Running migration {MigrationStep}", "AddColumn");

            // Lots of methods available in the MigrationBase class - discover with this.
            if (!ColumnExists("MyUser", "UmbID"))
            {
                Create.Column("UmbId").OnTable("MyUser").AsInt32().Do();
            }
            else
            {
                Logger.Debug<AddMyUserTable>("The column in {DbTable} already exists, skipping", "MyUser");
            }
        }
    }

    public class AddColumnAgain3 : MigrationBase
    {
        public AddColumnAgain3(IMigrationContext context) : base(context)
        {
        }

        public override void Migrate()
        {
            Logger.Debug<AddMyUserTable>("Running migration {MigrationStep}", "AddColumn");

            // Lots of methods available in the MigrationBase class - discover with this.
            if (!ColumnExists("MyUser", "UmbID"))
            {
                Create.Column("UmbId").OnTable("MyUser").AsInt32().Nullable().Do();
            }
            else
            {
                Logger.Debug<AddMyUserTable>("The column in {DbTable} already exists, skipping", "MyUser");
            }
        }
    }
}