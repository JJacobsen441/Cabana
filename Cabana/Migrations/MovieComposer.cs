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
    [ComposeBefore(typeof(FKComposer))]
    [RuntimeLevel(MinLevel = RuntimeLevel.Run)]
    public class MovieComposer : ComponentComposer<MovieComponent>
    {
    }

    public class MovieComponent : IComponent
    {
        private IScopeProvider _scopeProvider;
        private IMigrationBuilder _migrationBuilder;
        private IKeyValueService _keyValueService;
        private ILogger _logger;

        public MovieComponent(IScopeProvider scopeProvider, IMigrationBuilder migrationBuilder, IKeyValueService keyValueService, ILogger logger)
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
            var migrationPlan = new MigrationPlan("AddMovie");

            // This is the steps we need to take
            // Each step in the migration adds a unique value
            migrationPlan.From(string.Empty)
                .To<AddMovieTable>("movie-db");
            
            // Go and upgrade our site (Will check if it needs to do the work or not)
            // Based on the current/latest step
            var upgrader = new Upgrader(migrationPlan);
            upgrader.Execute(_scopeProvider, _migrationBuilder, _keyValueService, _logger);
        }

        public void Terminate()
        {
        }
    }

    public class AddMovieTable : MigrationBase
    {
        public AddMovieTable(IMigrationContext context) : base(context)
        {
        }

        public override void Migrate()
        {
            Logger.Debug<AddMovieTable>("Running migration {MigrationStep}", "AddMovieTable");

            // Lots of methods available in the MigrationBase class - discover with this.
            if (!TableExists("Movie"))
            {
                Create.Table<MovieSchema>().Do();
            }
            else
            {
                Logger.Debug<AddMovieTable>("The database table {DbTable} already exists, skipping", "Movie");
            }
        }

        [TableName("Movie")]
        [PrimaryKey("Id", AutoIncrement = true)]
        [ExplicitColumns]
        public class MovieSchema
        {
            [PrimaryKeyColumn(AutoIncrement = true, IdentitySeed = 1)]
            [Column("Id")]
            public int Id { get; set; }

            [Column("MovieID")]
            public long MovieID { get; set; }

            [Column("Title")]
            public string Title { get; set; }

            [Column("MyUserID")]//, ForeignKey(typeof(MyUser), Column = "Id", Name = "FK_MovieMyUser")]
            public long MyUserID { get; set; }
        }
    }
}